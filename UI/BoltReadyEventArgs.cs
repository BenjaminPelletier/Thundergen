using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.UI
{
    public class BoltReadyEventArgs : EventArgs
    {
        public readonly Vector3[] Bolt;

        public BoltReadyEventArgs(Vector3[] bolt)
        {
            Bolt = bolt;
        }
    }
}
