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
using System.Numerics;
using System.Threading;

namespace Thundergen.UI
{
    public partial class BreakdownControl : UserControl
    {
        private DBMBreakdown mBreakdown = null;

        public event EventHandler<DBMBreakdown.GroundPropagationProgressEventArgs> BreakdownPropagationProgress;

        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;

        private CancellationTokenSource mCancelViaUI = null;
        private TaskCompletionSource<DBMBreakdown> mCurrentRequest = null;

        public bool Valid { get { return breakdownConfigControl1.Valid; } }

        public BreakdownControl()
        {
            InitializeComponent();
        }

        public async Task<DBMBreakdown> RequestBreakdown(CancellationToken token)
        {
            if (mCurrentRequest != null) return await mCurrentRequest.Task;
            mCurrentRequest = new TaskCompletionSource<DBMBreakdown>();
            mCancelViaUI = new CancellationTokenSource();

            this.Invoke(new Action(() => cmdGenerate.Text = "Pause"));
            token.Register(mCancelViaUI.Cancel);

            if (mBreakdown == null)
            {
                mBreakdown = new DBMBreakdown(breakdownConfigControl1.Config);
            }
            try
            {
                await Asynchronizer.Wrap(() => mBreakdown.PropagateToGround(token, Breakdown_Progress));
                this.Invoke(new Action(() => cmdGenerate.Enabled = false));
            }
            catch (TaskCanceledException)
            {
                // Do nothing (do not disable cmdGenerate)
            }
            this.Invoke(new Action(() =>
            {
                cmdGenerate.Text = "Generate";
                cmdReset.Enabled = true;
                cmdExport.Enabled = true;
            }));

            var currentRequest = mCurrentRequest;
            mCurrentRequest = null;
            currentRequest.TrySetResult(mBreakdown);
            mCancelViaUI = null;

            return mBreakdown;
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature not yet implemented");
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature not yet implemented");
        }

        private void ResetBreakdown()
        {
            mBreakdown = null;
            cmdReset.Enabled = false;
            cmdExport.Enabled = false;
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            ResetBreakdown();
            cmdGenerate.Enabled = breakdownConfigControl1.Valid;
        }

        private async void cmdGenerate_Click(object sender, EventArgs e)
        {
            if (mCancelViaUI == null)
            {
                try
                {
                    mBreakdown = await RequestBreakdown(CancellationToken.None);
                }
                catch (TaskCanceledException)
                {
                    // Do nothing
                }
            }
            else
            {
                mCancelViaUI.Cancel();
            }
        }

        private void Breakdown_Progress(object sender, DBMBreakdown.GroundPropagationProgressEventArgs e)
        {
            Invoke(new Action(() => BreakdownPropagationProgress?.Invoke(this, e)));
        }

        private void breakdownConfigControl1_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            cmdGenerate.Enabled = e.Valid;
            ValidityChanged?.Invoke(this, e);
        }

        private void breakdownConfigControl1_ValueChanged(object sender, EventArgs e)
        {
            cmdGenerate.Enabled = breakdownConfigControl1.Valid;
            ResetBreakdown();
        }
    }
}
