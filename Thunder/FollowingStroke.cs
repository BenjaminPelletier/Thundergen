using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Thunder
{
    public struct FollowingStroke
    {
        public double Interval;
        public double RelativeAmplitude;

        public FollowingStroke(double interval, double relativeAmplitude)
        {
            Interval = interval;
            RelativeAmplitude = relativeAmplitude;
        }

        private static Accord.Statistics.Distributions.Univariate.NormalDistribution STROKE_INTERVAL = new Accord.Statistics.Distributions.Univariate.NormalDistribution(5.847885872, 1.123725188);

        public static FollowingStroke[] GenerateSet(Random r)
        {
            const double M = 1.05922;
            const double B = -4.15374;
            int n = RandomCount(r);
            var amplitudes = Enumerable.Range(0, n).Select(i => Math.Exp((Math.Log(r.NextDouble()) - B) / M)).ToArray();
            return amplitudes.Skip(1).Select(a => new FollowingStroke(Math.Pow(2, STROKE_INTERVAL.Generate(r)) / 1000, a / amplitudes[0])).ToArray();
        }

        private static Accord.Statistics.Distributions.Univariate.ExponentialDistribution STROKE_COUNT = new Accord.Statistics.Distributions.Univariate.ExponentialDistribution(0.35380385);

        public static int RandomCount(Random r)
        {
            return (int)Math.Max(1, Math.Round(STROKE_COUNT.Generate(r)));
        }

        public static List<FollowingStroke> GroupFromString(string s)
        {
            var strokes = new List<FollowingStroke>();
            if (s.Trim().Length == 0) return strokes;
            foreach (string stroke in s.Split(','))
            {
                var cols = stroke.Split('@');
                if (cols.Length != 2) return null;
                if (cols[0].Contains("ms"))
                {
                    cols[0] = cols[0].Substring(0, cols[0].IndexOf("ms"));
                }
                double interval, relativeAmplitude;
                if (!double.TryParse(cols[0], out interval)) return null;
                if (!double.TryParse(cols[1], out relativeAmplitude)) return null;
                strokes.Add(new FollowingStroke(interval / 1000, relativeAmplitude));
            }
            return strokes;
        }
    }

    public static class FollowingStrokeExtentions
    {
        public static string AsString(this FollowingStroke[] strokes)
        {
            return strokes == null || strokes.Length == 0 ? "" : strokes.Select(s => s.Interval * 1000 + "ms@" + s.RelativeAmplitude).Aggregate((a, b) => a + ", " + b);
        }
    }
}
