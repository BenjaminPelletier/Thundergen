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
using System.IO;
using Json.Serialization;

namespace Thundergen.UI
{
    public partial class BreakdownControl : UserControl
    {
        private DBMBreakdown mBreakdown = null;

        public event EventHandler<DBMBreakdown.GroundPropagationProgressEventArgs> BreakdownPropagationProgress;

        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;
        public event EventHandler ValueChanged;

        private CancellationTokenSource mCancelViaUI = null;
        private TaskCompletionSource<DBMBreakdown> mCurrentRequest = null;

        public bool Valid { get { return breakdownConfigControl1.Valid; } }

        public DBMBreakdown.Configuration BreakdownConfiguration
        {
            get
            {
                return breakdownConfigControl1.Config;
            }
        }

        public BreakdownControl()
        {
            InitializeComponent();

            ofdBreakdown.InitialDirectory = Directory.GetCurrentDirectory();
            sfdBreakdown.InitialDirectory = ofdBreakdown.InitialDirectory;
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
                if (mBreakdown.LowestCharge.Z > 1)
                {
                    await Asynchronizer.Wrap(() => mBreakdown.PropagateToGround(token, Breakdown_Progress));
                }
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

        public void SetBreakdown(DBMBreakdown breakdown)
        {
            breakdownConfigControl1.Config = breakdown.Config;
            mBreakdown = breakdown;
            cmdExport.Enabled = true;
            cmdGenerate.Enabled = false;
            ValueChanged?.Invoke(this, EventArgs.Empty);
            BreakdownPropagationProgress?.Invoke(this, new DBMBreakdown.GroundPropagationProgressEventArgs(mBreakdown, breakdown.LowestCharge, true));
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            if (ofdBreakdown.ShowDialog(this) == DialogResult.OK)
            {
                var fi = new FileInfo(ofdBreakdown.FileName);
                DBMBreakdown breakdown;
                if (fi.Name.ToLower().EndsWith(".breakdown"))
                {
                    try
                    {
                        breakdown = Serialization.Read<DBMBreakdown>(fi.FullName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Error importing breakdown pattern: " + ex, "Breakdown import error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (fi.Name.ToLower().EndsWith(".blt"))
                {
                    JsonObject blt = JsonObject.Parse(File.ReadAllText(fi.FullName));
                    DBMBreakdown.Configuration config = breakdownConfigControl1.Config;
                    config.Biases = Serialization.Import<DBMBreakdown.Bias[]>(blt.Dictionary["Breakdown"]["Biases"]);
                    config.MaxHeight = blt.Dictionary["Breakdown"]["MaxHeight"].Number;
                    breakdown = new DBMBreakdown(config);
                    breakdown.NegativeCharges = Serialization.Import<HashSet<Vector3>>(Serialization.ConvertLegacyEnumerableOfVector3(blt.Dictionary["Breakdown"]["NegativeCharges"]));
                    //breakdown.RecomputeAllCandidates();
                    SetBreakdown(breakdown);
                }
                else
                {
                    MessageBox.Show(this, "Unrecognized extension");
                    return;
                }
                SetBreakdown(breakdown);
                sfdBreakdown.InitialDirectory = fi.DirectoryName;
                ofdBreakdown.InitialDirectory = fi.DirectoryName;
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            if (sfdBreakdown.ShowDialog(this) == DialogResult.OK)
            {
                var fi = new FileInfo(sfdBreakdown.FileName);
                Serialization.Write(mBreakdown, fi.FullName);
                sfdBreakdown.InitialDirectory = fi.DirectoryName;
                ofdBreakdown.InitialDirectory = fi.DirectoryName;
            }
        }

        private void ResetBreakdown()
        {
            bool triggerValueChanged = mBreakdown != null;
            mBreakdown = null;
            cmdReset.Enabled = false;
            cmdExport.Enabled = false;
            if (triggerValueChanged) ValueChanged?.Invoke(this, EventArgs.Empty);
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
                    cmdGenerate.Enabled = false;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
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
