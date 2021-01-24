using ILGPU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Lightning
{
    class Bolt2D
    {
        public class ProgressEventArgs : EventArgs
        {
            public FieldGenerator2D FieldGenerator;
            public Index2 NewPixel;
            public List<Index2> BoltPixels;
            public float[,] Field;

            public ProgressEventArgs(FieldGenerator2D fieldGenerator, Index2 newPixel, List<Index2> boltPixels, float[,] field)
            {
                FieldGenerator = fieldGenerator;
                NewPixel = newPixel;
                BoltPixels = boltPixels;
                Field = field;
            }
        }

        public event EventHandler<ProgressEventArgs> Progress;

        public void Generate(FieldGenerator2D fieldGenerator, float eta = 1.0f, List<Index2> boltPixels = null)
        {
            var r = new Random();
            if (boltPixels != null)
            {
                foreach (Index2 p in boltPixels)
                {
                    fieldGenerator.SetBolt(p.X, p.Y);
                }
            }
            else
            {
                boltPixels = new List<Index2>();
            }
            while (true)
            {
                float[,] field = fieldGenerator.Compute();
                List<Index2> candidates = Candidates(fieldGenerator.Boundary, fieldGenerator.Mask).ToList();
                Index2 p = ChooseNextPixel(field, candidates, eta, r);
                boltPixels.Add(p);
                Progress?.Invoke(this, new ProgressEventArgs(fieldGenerator, p, boltPixels, field));
                fieldGenerator.SetBolt(p.X, p.Y);
                if (fieldGenerator.IsGround(p.X - 1, p.Y) ||
                    fieldGenerator.IsGround(p.X + 1, p.Y) ||
                    fieldGenerator.IsGround(p.X, p.Y - 1) ||
                    fieldGenerator.IsGround(p.X, p.Y + 1))
                {
                    break;
                }
            }
        }

        Index2 ChooseNextPixel(float[,] field, List<Index2> candidates, float eta, Random r)
        {
            var probabilty = new List<float>();
            float pSum = 0f;
            foreach (Index2 c in candidates)
            {
                float fieldFlux = field[c.X, c.Y] * (field[c.X - 1, c.Y] +
                                                     field[c.X + 1, c.Y] +
                                                     field[c.X, c.Y - 1] +
                                                     field[c.X, c.Y + 1]);
                float p = (float)Math.Pow(fieldFlux, eta);
                probabilty.Add(p);
                pSum += p;
            }
            float threshold = pSum * (float)r.NextDouble();
            float cumSum = 0f;
            int selection = -1;
            for (int i = 0; i < candidates.Count; i++)
            {
                cumSum += probabilty[i];
                if (cumSum > threshold)
                {
                    selection = i;
                    break;
                }
            }
            return candidates[selection];
        }

        static HashSet<Index2> Candidates(float[,] boundary, byte[,] mask)
        {
            int width = mask.GetLength(0);
            int height = mask.GetLength(1);
            var candidates = new HashSet<Index2>();
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (mask[x, y] == FieldGenerator2D.MASK_BOUNDARY_CONDITION && boundary[x, y] == FieldGenerator2D.CHARGE_BOLT)
                    {
                        foreach (Index2 neighbor in new Index2[] { new Index2(x, y - 1), new Index2(x - 1, y), new Index2(x, y + 1), new Index2(x + 1, y) })
                        {
                            if (neighbor.X > 0 && neighbor.Y > 0 && neighbor.X < width - 1 && neighbor.Y < height - 1 &&
                                mask[neighbor.X, neighbor.Y] == FieldGenerator2D.MASK_NONE && !candidates.Contains(neighbor))
                            {
                                candidates.Add(neighbor);
                            }
                        }
                    }
                }
            }
            return candidates;
        }
    }
}
