using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thundergen.Lightning;
using System.Threading;
using System.Numerics;

namespace Thundergen.UI
{
    public partial class DBMBoltControl : UserControl
    {
        public event EventHandler<DBMBreakdown.GroundPropagationProgressEventArgs> BreakdownPropagationProgress;
        public event EventHandler<DBMBolt.PathGenerationProgressEventArgs> PathGenerationProgress;

        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;

        public bool Valid { get; private set; } = true;

        private void ComputeValidity()
        {
            bool old = Valid;
            Valid = breakdownControl1.Valid && dbmBoltInterpolationConfigControl1.Valid;

            if (Valid != old)
            {
                ValidityChanged?.Invoke(this, new ValidityChangedEventArgs(Valid));
            }
        }

        public DBMBoltControl()
        {
            InitializeComponent();
        }

        public async Task<Vector3[]> RequestPath(CancellationToken token)
        {
            DBMBreakdown breakdown = await breakdownControl1.RequestBreakdown(token);
            DBMBolt.InterpolationConfiguration interpConfig = dbmBoltInterpolationConfigControl1.Config;
            DBMBolt bolt = new DBMBolt(new DBMBolt.Configuration(breakdown, interpConfig));
            Vector3[] path = await Asynchronizer.Wrap(() => bolt.GeneratePath(token, PathGenerationProgress));
            return path;
        }

        private void breakdownControl1_BreakdownPropagationProgress(object sender, DBMBreakdown.GroundPropagationProgressEventArgs e)
        {
            BreakdownPropagationProgress?.Invoke(this, e);
        }

        private void Input_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            ComputeValidity();
        }
    }
}
