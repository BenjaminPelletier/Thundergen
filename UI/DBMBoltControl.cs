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

        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;
        public event EventHandler ValueChanged;

        public bool Valid { get; private set; } = true;

        public DBMBreakdown.Configuration BreakdownConfiguration
        {
            get
            {
                return breakdownControl1.BreakdownConfiguration;
            }
        }

        public DBMBolt.InterpolationConfiguration InterpolationConfiguration
        {
            get
            {
                return dbmBoltInterpolationConfigControl1.Config;
            }
        }

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

        public async Task<DBMBolt> RequestBolt(CancellationToken token)
        {
            DBMBreakdown breakdown = await breakdownControl1.RequestBreakdown(token);
            DBMBolt.InterpolationConfiguration interpConfig = dbmBoltInterpolationConfigControl1.Config;
            return new DBMBolt(new DBMBolt.Configuration(breakdown, interpConfig));
        }

        public void SetBolt(DBMBolt bolt)
        {
            breakdownControl1.SetBreakdown(bolt.Config.Breakdown);
            dbmBoltInterpolationConfigControl1.Config = bolt.Config.Interpolation;
        }

        private void breakdownControl1_BreakdownPropagationProgress(object sender, DBMBreakdown.GroundPropagationProgressEventArgs e)
        {
            BreakdownPropagationProgress?.Invoke(this, e);
        }

        private void Input_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            ComputeValidity();
        }

        private void Input_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
