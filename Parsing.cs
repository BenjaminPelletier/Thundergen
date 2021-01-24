using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen
{
    public static class Parsing
    {
        public static Vector3? Vector3(string s)
        {
            if (s.Contains('<')) s = s.Substring(s.IndexOf('<') + 1);
            if (s.Contains('>')) s = s.Substring(0, s.IndexOf('>'));
            var cols = s.Split(',');
            if (cols.Length != 3) return null;
            float x, y, z;
            if (!float.TryParse(cols[0], out x)) return null;
            if (!float.TryParse(cols[1], out y)) return null;
            if (!float.TryParse(cols[2], out z)) return null;
            return new Vector3(x, y, z);
        }

        public static double? Double(string s)
        {
            double result;
            if (double.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static Func<string, double?> Double(double minValue, bool exclusiveMin, double maxValue, bool exclusiveMax)
        {
            return s =>
            {
                double? value = Double(s);
                if (value.HasValue && (exclusiveMin ? value.Value > minValue : value.Value >= minValue) && (exclusiveMax ? value.Value < maxValue : value.Value <= maxValue))
                {
                    return value;
                }
                return null;
            };
        }

        public static float? Float(string s)
        {
            float result;
            if (float.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static int? Int(string s)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static Func<string, int?> Int(int minValue, bool exclusiveMin, int maxValue, bool exclusiveMax)
        {
            return s =>
            {
                int? value = Int(s);
                if (value.HasValue && (exclusiveMin ? value.Value > minValue : value.Value >= minValue) && (exclusiveMax ? value.Value < maxValue : value.Value <= maxValue))
                {
                    return value;
                }
                return null;
            };
        }

        public static string AsString(this Vector3 v)
        {
            return "<" + v.X + ", " + v.Y + ", " + v.Z + ">";
        }
    }
}
