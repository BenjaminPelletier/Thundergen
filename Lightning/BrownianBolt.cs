using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Thundergen.Thunder;

namespace Thundergen.Lightning
{
    class BrownianBolt
    {
        public readonly Vector3[] Points;

        public BrownianBolt(IEnumerable<Vector3> points)
        {
            Points = points.ToArray();
        }

        static double RandomAngle(Random r, double avgAngle)
        {
            double stDev = Math.Sqrt(avgAngle * Math.Sqrt(Math.PI / 2.0));
            double u1 = 1.0 - r.NextDouble();
            double u2 = 1.0 - r.NextDouble();
            return stDev * Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        }

        public static BrownianBolt Generate(float initialAltitudeM, double stepMeanMeters = 3f, double stepK = 3f, float downwardBias = 0.05f)
        {
            var r = new Random(1234);

            double stepLambda = stepMeanMeters / MathNet.Numerics.SpecialFunctions.Gamma(1 + 1 / stepK);

            var points = new List<Vector3>();
            points.Add(new Vector3(0, 0, initialAltitudeM));
            while (points[points.Count - 1].Z > 0)
            {
                double length = stepLambda * Math.Pow(-Math.Log(r.NextDouble()), 1.0 / stepK);

                var nominalDirection = Vector3.Zero;
                if (points.Count > 1)
                {
                    int n = 0;
                    for (int p = points.Count - 1; p > 0 && n < 4; p--)
                    {
                        Vector3 newDirection = points[p] - points[p - 1];
                        newDirection /= newDirection.Length();
                        nominalDirection += newDirection;
                        n++;
                    }
                    nominalDirection /= nominalDirection.Length();
                    nominalDirection = -downwardBias * Vector3.UnitZ + (1 - downwardBias) * nominalDirection;
                    nominalDirection /= nominalDirection.Length();
                }
                else
                {
                    nominalDirection = new Vector3(0, 0, -1);
                }
                double perturbationAngle = RandomAngle(r, 16.0 * Math.PI / 180);
                double azimuth = r.NextDouble() * 2 * Math.PI;
                var perturbation = new Vector3((float)(length * Math.Cos(azimuth) * Math.Sin(perturbationAngle)),
                                               (float)(length * Math.Sin(azimuth) * Math.Sin(perturbationAngle)),
                                               (float)(-length * Math.Cos(perturbationAngle)));
                var q = Quaternion.CreateFromAxisAngle(Vector3.Cross(-Vector3.UnitZ, nominalDirection), (float)Math.Acos(Vector3.Dot(-Vector3.UnitZ, nominalDirection)));
                Vector3 pNew = points[points.Count - 1] + Vector3.Transform(perturbation, q);
                if (pNew.Z < 0)
                {
                    break;
                }
                points.Add(pNew);
            }
            return new BrownianBolt(points);
        }
    }
}
