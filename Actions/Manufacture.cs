using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thundergen.Lightning;
using Thundergen.Thunder;
using Thundergen.UI;

namespace Thundergen.Actions
{
    static class Manufacture
    {
        public static void Breakdowns(DBMBreakdown.Configuration baseConfig, Action<string> update)
        {
            Parallel.For(10100, 10200, seed =>
            {
                try
                {
                    var fi = new FileInfo("6000m_" + seed + ".breakdown");
                    if (!fi.Exists)
                    {
                        var config = new DBMBreakdown.Configuration(baseConfig.Seeds, baseConfig.Biases, baseConfig.Eta, seed, baseConfig.GrowthPerIteration, baseConfig.CullThreshold, baseConfig.CullLevel, baseConfig.FractionToCullByCharge);
                        var breakdown = new DBMBreakdown(config);
                        breakdown.PropagateToGround(CancellationToken.None, (sender, e) =>
                        {
                            update("Seed " + seed + ": " + e.LowestCharge.Z);
                        });
                        Serialization.Write(breakdown, fi.FullName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }

        private struct BoltInfo
        {
            public double Scale;
            public List<double> Delays;
            public List<double> Intensities;

            public BoltInfo(double scale, double t0, double intensity, FollowingStroke[] followingStrokes)
            {
                Scale = scale;
                Delays = new List<double>();
                Intensities = new List<double>();
                double i0 = 1;
                if (followingStrokes.Length > 0) i0 = Math.Max(i0, followingStrokes.Max(fs => fs.RelativeAmplitude));
                Delays.Add(t0);
                Intensities.Add(intensity / i0);
                double t = t0;
                foreach (FollowingStroke fs in followingStrokes)
                {
                    t += fs.Interval;
                    Delays.Add(t);
                    Intensities.Add(intensity * fs.RelativeAmplitude / i0);
                }
            }
        }
        public static void Sounds(DBMBolt.InterpolationConfiguration baseInterpolationConfig, Generator.Config baseThunderConfig, Action<string> update)
        {
            const int seedMin = 10000;
            const int seedMax = 10200;
            const int nVariantsPerSeed = 20;
            const int nBolts = (seedMax - seedMin) * nVariantsPerSeed;
            Func<int, int, int> indexOf = (int seed, int variant) => variant * (seedMax - seedMin) + seed - seedMin;

            const double rmin = 50;
            const double rmax = 500;
            const double scaleMin = 0.6;
            const double scaleMax = 1.3;

            var bolts = new BoltInfo[nBolts];
            for (int seed = seedMin; seed < seedMax; seed++)
            {
                var r = new Random(seed);
                var fi = new FileInfo("6000m_" + seed + ".breakdown");
                if (Enumerable.Range(0, nVariantsPerSeed).Select(i => new FileInfo(string.Format("thunder/{0:D4}.wav", indexOf(seed, i))).Exists).All(v => v))
                    continue;
                var breakdown = Serialization.Read<DBMBreakdown>(fi.FullName);
                for (int i = 0; i < nVariantsPerSeed; i++)
                {
                    var interpolationConfig = new DBMBolt.InterpolationConfiguration(scaleMin + r.NextDouble() * (scaleMax - scaleMin), baseInterpolationConfig.InitialSmoothing, baseInterpolationConfig.EnvelopeExtent, baseInterpolationConfig.InterpolatedSegmentLength);
                    var bolt = new DBMBolt(new DBMBolt.Configuration(breakdown, interpolationConfig));
                    System.Numerics.Vector3[] path = bolt.GeneratePath(CancellationToken.None, (sender, e) => { });
                    System.Numerics.Vector3 s0 = path[path.Length - 1];

                    int index = indexOf(seed, i);
                    var wavfi = new FileInfo(string.Format("thunder/{0:D4}.wav", index));

                    double radius = rmin + (1 - Math.Pow(r.NextDouble(), 2)) * (rmax - rmin);
                    double theta = r.NextDouble() * 2 * Math.PI;
                    float dx = (float)(radius * Math.Cos(theta));
                    float dy = (float)(radius * Math.Sin(theta));
                    var thunderConfig = new Generator.Config()
                    {
                        ObserverLocation = new System.Numerics.Vector3(s0.X + dx, s0.Y + dy, 0),
                        InitialVolume = baseThunderConfig.InitialVolume,
                        R0 = baseThunderConfig.R0,
                        FollowingStrokes = FollowingStroke.GenerateSet(r),
                        Conditions = baseThunderConfig.Conditions,
                        NoAtmosphericAttenuation = baseThunderConfig.NoAtmosphericAttenuation,
                        MaximumSteepeningIndexShift = baseThunderConfig.MaximumSteepeningIndexShift,
                    };
                    var wav = Generator.Generate(path, thunderConfig);
                    wav.Normalize();
                    double intensity = 0.5 + Math.Pow((rmax - radius) / (rmax - rmin), 2) * 0.5;
                    wav.Scale(intensity);
                    wav.WriteToWAV(wavfi.FullName);
                    update(wavfi.FullName);
                    bolts[index] = new BoltInfo(interpolationConfig.Scale, wav.T0, intensity, thunderConfig.FollowingStrokes);
                }
            }

            var sbOffsets = new List<double>();
            var sbDelays = new List<double>();
            var sbIntensities = new List<double>();
            int offset = 0;
            for (int b = 0; b < bolts.Length; b++)
            {
                sbOffsets.Add(offset);
                sbDelays.AddRange(bolts[b].Delays);
                sbIntensities.AddRange(bolts[b].Intensities);
                offset += bolts[b].Delays.Count;
            }
            sbOffsets.Add(offset);
            using (var w = new StreamWriter("thunder/index.txt"))
            {
                w.WriteLine("offsets");
                w.WriteLine(sbOffsets.Select(v => v.ToString()).Aggregate((a, b) => a + ", " + b));

                w.WriteLine("delays");
                w.WriteLine(sbDelays.Select(v => Math.Round(v * 1000).ToString()).Aggregate((a, b) => a + ", " + b));

                w.WriteLine("intensities");
                w.WriteLine(sbIntensities.Select(v => Math.Floor(v * 255.999).ToString()).Aggregate((a, b) => a + ", " + b));

                w.WriteLine("scales");
                w.WriteLine(bolts.Select(b => Math.Round(b.Scale, 3).ToString()).Aggregate((a, b) => a + ", " + b));
            }
        }
    }
}
