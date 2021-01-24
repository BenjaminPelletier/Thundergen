using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thundergen.Visualization.Colormaps;

namespace Thundergen.Lightning
{
    public class DBMBreakdown
    {
        #pragma warning disable 0414
        private int SerializationVersion = 1;
        #pragma warning restore 0414

        public class Configuration
        {
            public Bias[] Biases;
            public Vector3[] Seeds;
            public double MaxHeight;
            public double MinHeight;
            public int RandomSeed;
            public double Eta;
            public int GrowthPerIteration;
            public int CullThreshold;
            public int CullLevel;
            public double FractionToCullByCharge;
            public TimeSpan ProgressPeriod;

            public Configuration(
                IEnumerable<Vector3> seeds,
                IEnumerable<Bias> biases,
                double eta = 1.0,
                int? randomSeed = null,
                int growthPerIteration = 1,
                int cullThreshold = 1001,
                int cullLevel = 1000,
                double fractionToCullByCharge = 1.0)
            {
                Seeds = seeds.ToArray();
                Biases = biases != null ? biases.ToArray() : new Bias[0];

                MinHeight = 0;
                if (Biases.Length > 0)
                {
                    MaxHeight = Biases.Select(b => b.Height).Max() - 1;
                }
                else
                {
                    MaxHeight = double.PositiveInfinity;
                }

                Eta = eta;
                RandomSeed = randomSeed.HasValue ? randomSeed.Value : new Random().Next();
                GrowthPerIteration = growthPerIteration;
                CullThreshold = cullThreshold;
                CullLevel = cullLevel;
                FractionToCullByCharge = fractionToCullByCharge;
                ProgressPeriod = TimeSpan.FromSeconds(1);
            }
        }

        public struct Bias
        {
            public double FractionOfBolt;
            public double Height;

            public Bias(double height, double fractionOfBolt)
            {
                Height = height;
                FractionOfBolt = fractionOfBolt;
            }
        }

        public class GroundPropagationProgressEventArgs : EventArgs
        {
            public readonly DBMBreakdown Breakdown;
            public readonly Vector3 LowestCharge;
            public readonly bool FinalIteration;

            public GroundPropagationProgressEventArgs(DBMBreakdown breakdown, Vector3 lowestCharge, bool finalIteration)
            {
                Breakdown = breakdown;
                LowestCharge = lowestCharge;
                FinalIteration = finalIteration;
            }
        }

        public Configuration Config { get; private set; }

        public HashSet<Vector3> NegativeCharges;

        public Vector3 LowestCharge
        {
            get
            {
                Vector3 lowest = new Vector3(0, 0, float.PositiveInfinity);
                foreach (Vector3 charge in NegativeCharges)
                {
                    if (charge.Z < lowest.Z) lowest = charge;
                }
                return lowest;
            }
        }

        private Dictionary<Vector3, double> Candidates;
        private HashSet<Vector3> CandidateSet;

        static readonly Vector3[] GROWTH_NEIGHBORS = new Vector3[]
        {
            new Vector3( 1,  0,  0),
            new Vector3(-1,  0,  0),
            new Vector3( 0,  1,  0),
            new Vector3( 0, -1,  0),
            new Vector3( 0,  0,  1),
            new Vector3( 0,  0, -1)
        };

        private class CandidateChargeComparer : IComparer<Vector3>
        {
            private Dictionary<Vector3, double> Charges;

            public CandidateChargeComparer(Dictionary<Vector3, double> charges)
            {
                Charges = charges;
            }

            public int Compare(Vector3 x, Vector3 y)
            {
                return Charges[y].CompareTo(Charges[x]);
            }
        }

        public DBMBreakdown(Configuration config)
        {
            Config = config;

            NegativeCharges = new HashSet<Vector3>();

            Candidates = new Dictionary<Vector3, double>();
            CandidateSet = new HashSet<Vector3>();

            foreach (Vector3 charge in Config.Seeds)
            {
                AddCharge(charge);
            }
            foreach (Vector3 charge in NegativeCharges)
            {
                AddCandidates(charge);
            }
        }

        public Vector3[] TopCharges
        {
            get
            {
                float zMax = float.NegativeInfinity;
                var top = new List<Vector3>();
                foreach (var charge in NegativeCharges)
                {
                    if (charge.Z > zMax)
                    {
                        zMax = charge.Z;
                        top = new List<Vector3>();
                        top.Add(charge);
                    }
                    else if (charge.Z == zMax)
                    {
                        top.Add(charge);
                    }
                }
                return top.ToArray();
            }
        }

        public Vector3[] BottomCharges
        {
            get
            {
                float zMin = float.PositiveInfinity;
                var bottom = new List<Vector3>();
                foreach (var charge in NegativeCharges)
                {
                    if (charge.Z < zMin)
                    {
                        zMin = charge.Z;
                        bottom = new List<Vector3>();
                        bottom.Add(charge);
                    }
                    else if (charge.Z == zMin)
                    {
                        bottom.Add(charge);
                    }
                }
                return bottom.ToArray();
            }
        }

        private void AddCandidate(Vector3 p)
        {
            double totalCharge = 0;
            foreach (var bias in Config.Biases)
            {
                totalCharge += NegativeCharges.Count * bias.FractionOfBolt * (1 - 0.5 / Math.Abs(bias.Height - p.Z));
            }
            foreach (var c in NegativeCharges)
            {
                totalCharge += 1 - 0.5 / (p - c).Length();
            }
            Candidates[p] = totalCharge;
            CandidateSet.Add(p);
        }

        public void AddCharge(Vector3 p)
        {
            foreach (Vector3 c in CandidateSet)
            {
                double dFieldDueToCharge = 1 - 0.5 / (p - c).Length();
                double dFieldDueToBiases = 0;
                foreach (Bias bias in Config.Biases)
                {
                    dFieldDueToBiases += bias.FractionOfBolt * (1 - 0.5 / Math.Abs(bias.Height - c.Z));
                }
                Candidates[c] += dFieldDueToCharge + dFieldDueToBiases;
            }
            NegativeCharges.Add(p);
        }

        private void PromoteCandidate(Vector3 p)
        {
            Candidates.Remove(p);
            CandidateSet.Remove(p);
            AddCharge(p);
        }

        private Vector3[] ChooseCandidates(double[] p, double eta)
        {
            if (Candidates.Count == 0) throw new InvalidOperationException("Cannot choose candidate when no candidates are available");

            double fieldMin = double.PositiveInfinity;
            double fieldMax = double.NegativeInfinity;
            foreach (double field in Candidates.Values)
            {
                if (field < fieldMin) fieldMin = field;
                if (field > fieldMax) fieldMax = field;
            }

            Dictionary<Vector3, double> candidateWeights = Candidates.ToDictionary(kvp => kvp.Key, kvp => Math.Pow((kvp.Value - fieldMin) / (fieldMax - fieldMin), eta));
            var candidates = new Vector3[p.Length];
            double threshold, weightSum;
            bool selected;
            for (int i = 0; i < p.Length; i++)
            {
                threshold = p[i] * candidateWeights.Values.Sum();
                weightSum = 0;
                selected = false;
                foreach (var kvp in candidateWeights)
                {
                    weightSum += kvp.Value;
                    if (weightSum >= threshold)
                    {
                        candidates[i] = kvp.Key;
                        selected = true;
                        break;
                    }
                }

                if (!selected)
                {
                    throw new InvalidOperationException("Failed to choose candidate");
                }
            }
            return candidates;
        }

        public void AddCandidates(Vector3 c)
        {
            foreach (Vector3 dc in GROWTH_NEIGHBORS)
            {
                Vector3 n = c + dc;
                if (!Candidates.ContainsKey(n) && !NegativeCharges.Contains(n) && n.Z <= Config.MaxHeight && n.Z >= Config.MinHeight)
                {
                    AddCandidate(n);
                }
            }
        }

        public Vector3[] GrowNegativeCharge(double[] p, double eta)
        {
            Vector3[] chosenCandidates = ChooseCandidates(p, eta);
            foreach (Vector3 c in chosenCandidates)
            {
                if (Candidates.ContainsKey(c))
                {
                    PromoteCandidate(c);
                    AddCandidates(c);
                }
            }
            return chosenCandidates;
        }

        public void CullCandidates(int nCullThreshold, int nMinCandidates, double fractionToCullByCharge)
        {
            if (Candidates.Count > nCullThreshold)
            {
                var bestCandidates = Candidates.Select(kvp => (kvp.Key, kvp.Value)).ToList();
                if (fractionToCullByCharge > 0)
                {
                    bestCandidates.Sort((x, y) => y.Value.CompareTo(x.Value));
                    bestCandidates = bestCandidates.Take((int)(nMinCandidates + (bestCandidates.Count - nMinCandidates) * (1 - fractionToCullByCharge))).ToList();
                }
                if (fractionToCullByCharge < 1)
                {
                    bestCandidates.Sort((x, y) => x.Key.Z.CompareTo(y.Key.Z));
                }
                Candidates = bestCandidates.Take(nMinCandidates).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                CandidateSet = Candidates.Keys.ToHashSet();
            }
        }

        public void PropagateToGround(CancellationToken token, EventHandler<GroundPropagationProgressEventArgs> progress = null)
        {
            Vector3 lowestCharge = new Vector3(0, 0, float.PositiveInfinity);
            foreach (var charge in NegativeCharges)
            {
                if (charge.Z < lowestCharge.Z)
                {
                    lowestCharge = charge;
                }
            }
            if (float.IsPositiveInfinity(lowestCharge.Z))
            {
                throw new ArgumentException("DBMBreakdown cannot propagate without any seed charges");
            }
            progress?.Invoke(this, new GroundPropagationProgressEventArgs(this, lowestCharge, false));
            DateTime nextProgress = DateTime.UtcNow + Config.ProgressPeriod;
            var r = new Random(Config.RandomSeed);
            var p = new double[Config.GrowthPerIteration];
            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < Config.GrowthPerIteration; i++)
                {
                    p[i] = r.NextDouble();
                }
                Vector3[] newPoints = GrowNegativeCharge(p, Config.Eta);
                foreach (Vector3 newPoint in newPoints)
                {
                    if (newPoint.Z < lowestCharge.Z)
                    {
                        lowestCharge = newPoint;
                    }
                }

                if (progress != null)
                {
                    if (DateTime.UtcNow >= nextProgress)
                    {
                        progress?.Invoke(this, new GroundPropagationProgressEventArgs(this, lowestCharge, false));
                        while (nextProgress < DateTime.UtcNow)
                        {
                            nextProgress = DateTime.UtcNow + Config.ProgressPeriod;
                        }
                    }
                }
                CullCandidates(Config.CullThreshold, Config.CullLevel, Config.FractionToCullByCharge);
                if (newPoints.Any(pt => pt.Z <= 0))
                {
                    progress?.Invoke(this, new GroundPropagationProgressEventArgs(this, lowestCharge, true));
                    break;
                }
            }
        }

        public void Draw(Graphics g, int width, int height)
        {
            g.Clear(Color.Gray);

            Vector3[] points = NegativeCharges.ToArray();
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

            var pixel = new SizeF(Math.Max(1, xscale), Math.Max(1, yscale));

            float colorref = Math.Min(xmin, ymin);
            float colorscale = Math.Max(xmax - colorref, ymax - colorref);

            Vector3 pf = points[0];
            Vector3 pl = points[points.Length - 1];

            PointF pLowest = xformx(new Vector3(0, 0, -1));
            if (float.IsInfinity(pLowest.Y)) pLowest.Y = height;
            g.DrawLine(Pens.Black, 0, pLowest.Y, width, pLowest.Y);

            var color = new byte[4];
            foreach (Vector3 p in points.OrderBy(p => p.Y))
            {
                //Magma.CopyColor((p.X - colorref) / colorscale, color, 0);
                //using (var b = new SolidBrush(Color.FromArgb(color[2], color[1], color[0])))
                //{
                //    g.FillRectangle(b, new RectangleF(xformy(p), pixel));
                //}

                Magma.CopyColor((p.Y - colorref) / colorscale, color, 0);
                using (var b = new SolidBrush(Color.FromArgb(color[2], color[1], color[0])))
                {
                    g.FillRectangle(b, new RectangleF(xformx(p), pixel));
                }
            }

            double fieldmin = Candidates.Values.Min();
            double fieldmax = Candidates.Values.Max();
            foreach (Vector3 p in Candidates.Keys.OrderBy(p => p.Y))
            {
                using (var b = new SolidBrush(Color.FromArgb(0, 64 + (int)((255 - 64) * (Candidates[p] - fieldmin) / (fieldmax - fieldmin)), 0)))
                {
                    g.FillRectangle(b, new RectangleF(xformx(p), pixel));
                    //g.FillRectangle(b, new RectangleF(xformy(p), pixel));
                }
            }
        }
    }
}
