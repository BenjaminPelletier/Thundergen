using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen
{
    public static class ExtensionMethods
    {
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue result;
            return dict.TryGetValue(key, out result) ? result : defaultValue;
        }
    }
}
