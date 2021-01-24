using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Thundergen.Thunder;
using Thundergen.Visualization.Colormaps;
using Thundergen;
using System.Threading;

namespace Thundergen.Lightning
{
    public class DBMBolt
    {
        public class PathGenerationProgressEventArgs : EventArgs
        {
            public int SegmentIndex;
            public int TotalSegments;

            public PathGenerationProgressEventArgs(int segmentIndex, int totalSegments)
            {
                SegmentIndex = segmentIndex;
                TotalSegments = totalSegments;
            }
        }

        public class InterpolationConfiguration
        {
            public double Scale;
            public int InitialSmoothing;
            public int EnvelopeExtent;
            public float InterpolatedSegmentLength;

            public InterpolationConfiguration(double scale, int initialSmoothing, int envelopeExtent, float interpolatedSegmentLength)
            {
                Scale = scale;
                InitialSmoothing = initialSmoothing;
                EnvelopeExtent = envelopeExtent;
                InterpolatedSegmentLength = interpolatedSegmentLength;
            }
        }

        public class Configuration
        {
            public DBMBreakdown Breakdown;
            public InterpolationConfiguration Interpolation;

            public Configuration(DBMBreakdown breakdown, InterpolationConfiguration interpConfig)
            {
                Breakdown = breakdown;
                Interpolation = interpConfig;
            }
        }

        public Configuration Config;

        public DBMBolt(Configuration config)
        {
            Config = config;
        }

        private static (Vector3 b, Vector3 m) Regress(IEnumerable<Vector3> points)
        {
            Vector3 x = Vector3.Zero, y = Vector3.Zero, x2 = Vector3.Zero, xy = Vector3.Zero;
            int n = 0;
            foreach (Vector3 p in points)
            {
                x += n * Vector3.One;
                x2 += n * n * Vector3.One;
                y += p;
                xy += n * p;
                n++;
            }
            Vector3 denom = n * x2 - x * x;
            Vector3 bNum = y * x2 - x * xy;
            Vector3 mNum = n * xy - x * y;
            return (new Vector3(bNum.X / denom.X, bNum.Y / denom.Y, bNum.Z / denom.Z), new Vector3(mNum.X / denom.X, mNum.Y / denom.Y, mNum.Z / denom.Z));
        }

        private static (Vector3 u, Vector3 v) PerpendicularBases(Vector3 d)
        {
            Vector3 dn = d / d.Length();
            Vector3 u, v;
            if (Math.Abs(Vector3.Dot(dn, Vector3.UnitX)) < 0.9f)
            {
                u = Vector3.Cross(dn, Vector3.UnitX);
            }
            else if (Math.Abs(Vector3.Dot(dn, Vector3.UnitY)) < 0.9f)
            {
                u = Vector3.Cross(dn, Vector3.UnitY);
            }
            else
            {
                u = Vector3.Cross(dn, Vector3.UnitZ);
            }
            u /= u.Length();
            v = Vector3.Cross(dn, u);
            return (u, v);
        }

        private static Vector3 ChangeBases(Vector3 v, Vector3 d0, Vector3 d1, Vector3 d2)
        {
            float denom = d0.Z * d1.Y * d2.X - d0.Y * d1.Z * d2.X - d0.Z * d1.X * d2.Y + d0.X * d1.Z * d2.Y + d0.Y * d1.X * d2.Z - d0.X * d1.Y * d2.Z;
            float anum = d1.Z * d2.Y * v.X - d1.Y * d2.Z * v.X - d1.Z * d2.X * v.Y + d1.X * d2.Z * v.Y + d1.Y * d2.X * v.Z - d1.X * d2.Y * v.Z;
            float bnum = -(d0.Z * d2.Y * v.X - d0.Y * d2.Z * v.X - d0.Z * d2.X * v.Y + d0.X * d2.Z * v.Y + d0.Y * d2.X * v.Z - d0.X * d2.Y * v.Z);
            float cnum = d0.Z * d1.Y * v.X - d0.Y * d1.Z * v.X - d0.Z * d1.X * v.Y + d0.X * d1.Z * v.Y + d0.Y * d1.X * v.Z - d0.X * d1.Y * v.Z;
            return new Vector3(anum / denom, bnum / denom, cnum / denom);
        }

        static readonly Vector3[] BOLT_NEIGHBORS = new Vector3[]
{
            new Vector3( 1,  0,  0),
            new Vector3(-1,  0,  0),
            new Vector3( 0,  1,  0),
            new Vector3( 0, -1,  0),
            new Vector3( 0,  0,  1),
            new Vector3( 0,  0, -1)
};

        static IEnumerable<Vector3> BoltNeighborsOf(Vector3 p)
        {
            foreach (Vector3 dp in BOLT_NEIGHBORS)
            {
                yield return p + dp;
            }
        }

        private IEnumerable<Vector3> GriddedBolt(Vector3 start, Vector3 end)
        {
            var next = new Dictionary<Vector3, Vector3>();
            var toExplore = new Queue<Vector3>();
            toExplore.Enqueue(end);

            while (toExplore.Count > 0)
            {
                Vector3 current = toExplore.Dequeue();
                foreach (Vector3 p in BoltNeighborsOf(current))
                {
                    if (Config.Breakdown.NegativeCharges.Contains(p) && !next.ContainsKey(p))
                    {
                        next[p] = current;
                        if (p == start)
                        {
                            current = start;
                            while (true)
                            {
                                yield return current;
                                if (current == end)
                                {
                                    yield break;
                                }
                                current = next[current];
                            }
                        }
                        toExplore.Enqueue(p);
                    }
                }
            }

            throw new InvalidOperationException("Could not find start from end");
        }

        static double SQRT3 = Math.Sqrt(3);

        public Vector3[] GeneratePath(CancellationToken token, EventHandler<PathGenerationProgressEventArgs> progress)
        {
            // Find the top and bottom charges in the breakdown
            Vector3 c0 = new Vector3(0, 0, float.NegativeInfinity);
            Vector3 c1 = new Vector3(0, 0, float.PositiveInfinity);
            foreach (Vector3 charge in Config.Breakdown.NegativeCharges)
            {
                if (charge.Z > c0.Z) c0 = charge;
                if (charge.Z < c1.Z) c1 = charge;
            }

            // Initially smooth the grid cell path into a more direct path
            Vector3[] bolt = GriddedBolt(c0, c1).ToArray();
            var segmentCenters = new List<Vector3>();
            var subset = new Vector3[2 * Config.Interpolation.InitialSmoothing + 1];
            for (int ib = 0; ib < bolt.Length; ib++)
            {
                int i = Config.Interpolation.InitialSmoothing;
                int i0 = ib - Config.Interpolation.InitialSmoothing;
                int i1 = ib + Config.Interpolation.InitialSmoothing;
                if (i0 < 0)
                {
                    i += i0;
                    i0 = 0;
                }
                if (i1 >= bolt.Length)
                {
                    i += i1 - bolt.Length + 1;
                    i0 -= i1 - bolt.Length + 1;
                }
                Array.Copy(bolt, i0, subset, 0, subset.Length);
                (Vector3 b, Vector3 m) = Regress(subset);
                segmentCenters.Add(m * i + b);
            }

            // Refine the path according to nearby charge distribution in the breakdown
            TimeSpan updatePeriod = TimeSpan.FromSeconds(1);
            DateTime nextUpdate = DateTime.UtcNow + updatePeriod;
            int c = 0; // Index of first of two segmentCenters being interpolated between
            float d = 0; // Distance along path between two segmentCenters
            float di = (segmentCenters[1] - segmentCenters[0]).Length(); // Total length between current two segmentCenters
            var boltCenters = new List<Vector3>();
            while (c < segmentCenters.Count - 1 && !token.IsCancellationRequested)
            {
                Vector3 vZ = segmentCenters[c + 1] - segmentCenters[c]; // Direction from current segmentCenter to next segmentCenter
                vZ /= vZ.Length();
                Vector3 p0 = segmentCenters[c] + d * vZ;
                (Vector3 vU, Vector3 vV) = PerpendicularBases(vZ);

                Vector2 weightedSum = Vector2.Zero;
                double weightSum = 0;
                for (int dz = -Config.Interpolation.EnvelopeExtent; dz <= Config.Interpolation.EnvelopeExtent; dz++)
                {
                    for (int dy = -Config.Interpolation.EnvelopeExtent; dy <= Config.Interpolation.EnvelopeExtent; dy++)
                    {
                        for (int dx = -Config.Interpolation.EnvelopeExtent; dx <= Config.Interpolation.EnvelopeExtent; dx++)
                        {
                            var charge = new Vector3((int)p0.X + dx, (int)p0.Y + dy, (int)p0.Z + dz);
                            if (Config.Breakdown.NegativeCharges.Contains(charge))
                            {
                                Vector3 alongBolt = ChangeBases(charge - p0, vU, vV, vZ);
                                double r = Math.Sqrt(alongBolt.X * alongBolt.X + alongBolt.Y * alongBolt.Y);
                                // TODO: Expose weighting function coefficients
                                double weight = 1 / (1 + Math.Exp(-6 * (SQRT3 - Math.Abs(alongBolt.Z)))) * 1 / (1 + Math.Exp(-(Config.Interpolation.EnvelopeExtent * 0.5 - r)));
                                weightSum += weight;
                                weightedSum += new Vector2(alongBolt.X, alongBolt.Y) * (float)weight;
                            }
                        }
                    }
                }
                weightedSum /= (float)weightSum;
                boltCenters.Add(p0 + weightedSum.X * vU + weightedSum.Y * vV);

                d += Config.Interpolation.InterpolatedSegmentLength;
                while (d >= di)
                {
                    d -= di;
                    c++;
                    if (c >= segmentCenters.Count - 1)
                    {
                        break;
                    }
                    di = (segmentCenters[c + 1] - segmentCenters[c]).Length();
                }

                if (progress != null && DateTime.UtcNow > nextUpdate)
                {
                    progress(this, new PathGenerationProgressEventArgs(c, segmentCenters.Count - 1));
                    while (nextUpdate < DateTime.UtcNow)
                    {
                        nextUpdate += updatePeriod;
                    }
                }
            }
            return boltCenters.ToArray();
        }

        public static void Draw(IEnumerable<Vector3> pts, Graphics g, int width, int height)
        {
            g.Clear(Color.Gray);

            Vector3[] points = pts.ToArray();
            float xmin = points.Select(p => p.X).Min();
            float xmax = points.Select(p => p.X).Max();
            float ymin = points.Select(p => p.Y).Min();
            float ymax = points.Select(p => p.Y).Max();
            float zmin = points.Select(p => p.Z).Min();
            float zmax = points.Select(p => p.Z).Max();

            float xscale = Math.Min(0.5f * width / Math.Abs(xmax), 0.5f * width / Math.Abs(xmin));
            float yscale = Math.Min(0.5f * width / Math.Abs(ymax), 0.5f * width / Math.Abs(ymin));
            float zscale = height / (zmax - zmin);
            float scale = Math.Min(Math.Min(xscale, yscale), zscale);
            Func<Vector3, PointF> xformx = v => new PointF(v.X * scale + width / 2, (zmax - v.Z) * scale);
            Func<Vector3, PointF> xformy = v => new PointF(v.Y * scale + width / 2, (zmax - v.Z) * scale);

            var pixel = new SizeF(1, 1);

            float colorref = Math.Min(xmin, ymin);
            float colorscale = Math.Max(xmax - colorref, ymax - colorref);

            Vector3 pf = points[0];
            Vector3 pl = points[points.Length - 1];

            var color = new byte[4];
            foreach (Vector3 p in points)
            {
                Magma.CopyColor((p.Y - colorref) / colorscale, color, 0);
                using (var b = new SolidBrush(Color.FromArgb(color[2], color[1], color[0])))
                {
                    g.FillRectangle(b, new RectangleF(xformx(p), pixel));
                }
            }

            g.FillEllipse(Brushes.Green, new RectangleF(xformx(pf).X - 4, xformx(pf).Y - 4, 7, 7));
            g.FillEllipse(Brushes.Red, new RectangleF(xformx(pl).X - 4, xformx(pl).Y - 4, 7, 7));
        }
    }
}
