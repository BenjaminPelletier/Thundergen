using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
using Thundergen.Lightning;

namespace Thundergen.UI
{
    public partial class BoltControl : UserControl
    {
        public event EventHandler<DBMBreakdown.GroundPropagationProgressEventArgs> BreakdownPropagationProgress;
        public event EventHandler<DBMBolt.PathGenerationProgressEventArgs> PathGenerationProgress;

        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;

        public bool Valid
        {
            get
            {
                if (tcConfig.SelectedTab == tpDBM)
                {
                    return dbmBoltControl1.Valid;
                }
                else
                {
                    throw new NotImplementedException("Selected bolt type is not yet supported");
                }
            }
        }

        public BoltControl()
        {
            InitializeComponent();
        }

        public async Task<Vector3[]> RequestPath(CancellationToken token)
        {
            if (tcConfig.SelectedTab == tpDBM)
            {
                Vector3[] path = await dbmBoltControl1.RequestPath(token);
                return path;
            }
            else
            {
                throw new NotImplementedException("Selected bolt type is not yet supported");
            }
        }

        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not yet implemented");
        }

        private void dbmBoltControl1_BreakdownPropagationProgress(object sender, DBMBreakdown.GroundPropagationProgressEventArgs e)
        {
            BreakdownPropagationProgress?.Invoke(this, e);
        }

        private void dbmBoltControl1_PathGenerationProgress(object sender, DBMBolt.PathGenerationProgressEventArgs e)
        {
            PathGenerationProgress?.Invoke(this, e);
        }

        private void dbmBoltControl1_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            cmdGenerate.Enabled = e.Valid;
            if (tcConfig.SelectedTab == tpDBM)
            {
                ValidityChanged?.Invoke(this, e);
            }
        }
    }
}
