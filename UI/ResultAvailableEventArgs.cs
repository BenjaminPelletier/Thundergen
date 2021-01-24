using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.UI
{
    public class ResultAvailableEventArgs<T> : EventArgs
    {
        public readonly T Result;

        public ResultAvailableEventArgs(T result)
        {
            Result = result;
        }
    }
}
