using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime.Cuda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Lightning
{
    class FieldGenerator2D
    {
        static object _StaticBaton = new object();
        static Context _Context = null;
        static Accelerator _Accelerator = null;
        static Action<Index2, int, int, ArrayView2D<float>, ArrayView2D<byte>, ArrayView2D<float>, float, ArrayView2D<float>, VariableView<byte>> _ComputeFieldCenter;
        static Action<Index2, int, int, ArrayView2D<float>, ArrayView2D<byte>, ArrayView2D<float>> _ComputeFieldEdges;

        private static void StaticInit()
        {
            lock (_StaticBaton)
            {
                if (_Context == null)
                {
                    _Context = new Context();
                    //_Accelerator = new CPUAccelerator(_Context);
                    _Accelerator = new CudaAccelerator(_Context);
                    _ComputeFieldCenter = _Accelerator.LoadAutoGroupedStreamKernel<Index2, int, int, ArrayView2D<float>, ArrayView2D<byte>, ArrayView2D<float>, float, ArrayView2D<float>, VariableView<byte>>(ComputeFieldCenter);
                    _ComputeFieldEdges = _Accelerator.LoadAutoGroupedStreamKernel<Index2, int, int, ArrayView2D<float>, ArrayView2D<byte>, ArrayView2D<float>>(ComputeFieldEdges);
                }
            }
        }

        public class LastComputeInformation
        {
            public readonly int Iterations;
            public readonly TimeSpan ElapsedTime;

            public LastComputeInformation(int iterations, TimeSpan elapsedTime)
            {
                Iterations = iterations;
                ElapsedTime = elapsedTime;
            }
        }

        public const float CHARGE_BOLT = 0f;
        public const float CHARGE_GROUND = 1f;
        public const byte MASK_BOUNDARY_CONDITION = 1;
        public const byte MASK_NONE = 0;
        private const byte WITHIN_TOLERANCE = 0;
        private const byte OVER_TOLERANCE = 1;

        public readonly int Width;
        public readonly int Height;
        public readonly float[,] Boundary;
        public readonly byte[,] Mask;
        public readonly float Tolerance;

        public LastComputeInformation LastCompute = null;

        public FieldGenerator2D(int width, int height, float tolerance)
        {
            StaticInit();
            Width = width;
            Height = height;
            Boundary = new float[Width, Height];
            Mask = new byte[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Boundary[x, y] = (CHARGE_GROUND + CHARGE_BOLT) * 0.5f;
                    Mask[x, y] = MASK_NONE;
                }
            }
            Tolerance = tolerance;
        }

        public void SetBolt(int x, int y)
        {
            Boundary[x, y] = CHARGE_BOLT;
            Mask[x, y] = MASK_BOUNDARY_CONDITION;
        }

        public void SetGround(int x, int y)
        {
            Boundary[x, y] = CHARGE_GROUND;
            Mask[x, y] = MASK_BOUNDARY_CONDITION;
        }

        public bool IsGround(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height && Mask[x, y] > 0 && Boundary[x, y] == CHARGE_GROUND;
        }

        public float[,] Compute(float[,] fieldGuess = null)
        {
            DateTime t0 = DateTime.UtcNow;
            int iterations = 0;
            var notOverTolerance = new byte[] { WITHIN_TOLERANCE };
            using (var boundary = _Accelerator.Allocate(Boundary))
            using (var mask = _Accelerator.Allocate(Mask))
            using (var prevField = _Accelerator.Allocate(fieldGuess == null ? Boundary : fieldGuess))
            using (var field = _Accelerator.Allocate<float>(Width, Height))
            using (var overTolerance = _Accelerator.Allocate(new byte[] { OVER_TOLERANCE }))
            {
                while (overTolerance.GetAsArray()[0] == OVER_TOLERANCE)
                {
                    overTolerance.CopyFrom(notOverTolerance, 0, 0, 1);
                    _ComputeFieldCenter(new Index2(Width - 1, Height - 1), Width, Height, boundary.View, mask.View, prevField.View, Tolerance, field.View, overTolerance.View.GetVariableView());
                    _Accelerator.Synchronize();
                    _ComputeFieldEdges(new Index2(Math.Max(Width, Height), 2), Width, Height, boundary.View, mask.View, field.View);
                    _Accelerator.Synchronize();
                    field.CopyTo(prevField, new LongIndex2(0, 0));
                    iterations++;
                }

                float[,] result = field.GetAs2DArray();
                LastCompute = new LastComputeInformation(iterations, DateTime.UtcNow - t0);
                return result;
            }
        }

        static void ComputeFieldCenter(Index2 index, int width, int height, ArrayView2D<float> boundary, ArrayView2D<byte> mask, ArrayView2D<float> prevField, float tolerance, ArrayView2D<float> field, VariableView<byte> overTolerance)
        {
            if (index.X == 0 || index.Y == 0)
            {
                return;
            }

            if (mask[index] == MASK_BOUNDARY_CONDITION)
            {
                field[index] = boundary[index];
            }
            else
            {
                field[index] = 0.25f * (prevField[index.X - 1, index.Y] + prevField[index.X + 1, index.Y] + prevField[index.X, index.Y - 1] + prevField[index.X, index.Y + 1]);
                float dField = IntrinsicMath.Abs(field[index] - prevField[index]) / IntrinsicMath.Max(0.001f, IntrinsicMath.Max(IntrinsicMath.Abs(field[index]), IntrinsicMath.Abs(prevField[index])));
                if (dField > tolerance) overTolerance.Value = OVER_TOLERANCE;
            }
        }

        static void ComputeFieldEdges(Index2 index, int width, int height, ArrayView2D<float> boundary, ArrayView2D<byte> mask, ArrayView2D<float> field)
        {
            if (index.Y == 0)
            {
                // Top and bottom edges
                if (index.X > 0 && index.X < width - 1)
                {
                    field[index.X, 0] = mask[index.X, 0] == MASK_BOUNDARY_CONDITION ? boundary[index.X, 0] : field[index.X, 1];
                    field[index.X, height - 1] = mask[index.X, height - 1] == MASK_BOUNDARY_CONDITION ? boundary[index.X, height - 1] : field[index.X, height - 2];
                }
                else
                {
                    field[0, 0] = 0;
                    field[width - 1, 0] = 0;
                    field[0, height - 1] = 0;
                    field[width - 1, height - 1] = 0;
                }
            }
            else
            {
                // Left and right edges
                if (index.X > 0 && index.X < height - 1)
                {
                    field[0, index.X] = mask[0, index.X] == MASK_BOUNDARY_CONDITION ? boundary[0, index.X] : field[1, index.X];
                    field[width - 1, index.X] = mask[width - 1, index.X] == MASK_BOUNDARY_CONDITION ? boundary[width - 1, index.X] : field[width - 2, index.X];
                }
            }
        }
    }
}
