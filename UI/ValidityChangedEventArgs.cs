using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.UI
{
    public class ValidityChangedEventArgs : EventArgs
    {
        public readonly bool Valid;

        public ValidityChangedEventArgs(bool valid)
        {
            Valid = valid;
        }
    }
}
