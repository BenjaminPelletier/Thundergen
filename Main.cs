using ILGPU;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thundergen.Lightning;
using Thundergen.Thunder;
using Thundergen.UI;
using Thundergen.Visualization;

namespace Thundergen
{
    public partial class Main : Form
    {
        private Bitmap mVisualization = null;
        private bool mVisualize = false;

        public bool Valid { get; private set; } = true;

        private void ComputeValidity()
        {
            bool old = Valid;
            Valid = boltControl1.Valid && thunderGeneratorConfig1.Valid;
            cmdMakeThunder.Enabled = Valid;
        }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            boltControl1.BreakdownPropagationProgress += OnUIThread<DBMBreakdown.GroundPropagationProgressEventArgs>(boltControl1_BreakdownPropagationProgress);
            boltControl1.PathGenerationProgress += OnUIThread<DBMBolt.PathGenerationProgressEventArgs>(boltControl1_PathGenerationProgress);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (mVisualization != null)
            {
                e.Graphics.DrawImage(mVisualization, 0, 0);
            }
        }

        private EventHandler<T> OnUIThread<T>(EventHandler<T> handler)
        {
            return (sender, e) =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => handler(sender, e)));
                }
                else
                {
                    handler(sender, e);
                }
            };
        }

        private async void cmdMakeThunder_Click(object sender, EventArgs e)
        {
            if (cmdMakeThunder.Tag == null)
            {
                var cts = new CancellationTokenSource();
                cmdMakeThunder.Tag = cts;
                cmdMakeThunder.Text = "Cancel";
                Vector3[] path = await boltControl1.RequestPath(cts.Token);
                Waveform thunder = await Asynchronizer.Wrap(() => Generator.Generate(path, thunderGeneratorConfig1.Config, OnUIThread<Generator.ProgressEventArgs>(ThunderGenerator_Progress)));
                WaveFileWriter.CreateWaveFile(@"thunder.wav", thunder.ToStream());
                var fi = new FileInfo(@"thunder.wav");
                tsslStatus.Text = "Wrote thunder to " + fi.FullName;
                using (var wo = new WaveOutEvent())
                {
                    wo.Init(thunder.ToStream());
                    wo.Play();
                    while (wo.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(500);
                    }
                }
            }
            else
            {
                CancellationTokenSource cts = cmdMakeThunder.Tag as CancellationTokenSource;
                cts.Cancel();
            }
            tssbVisualize.Visible = false;
            cmdMakeThunder.Tag = null;
            cmdMakeThunder.Text = "Generate";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitImg()
        {
            if (mVisualization == null || mVisualization.Width != pictureBox1.ClientSize.Width || mVisualization.Height != pictureBox1.ClientSize.Height)
            {
                mVisualization = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
        }

        private void boltControl1_BreakdownPropagationProgress(object sender, DBMBreakdown.GroundPropagationProgressEventArgs e)
        {
            tsslStatus.Text = "Propagating breakdown to ground: Lowest breakdown at " + e.LowestCharge.Z + " grid units above the ground";
            tssbVisualize.Visible = true;

            if (mVisualize)
            {
                InitImg();
                using (Graphics g = Graphics.FromImage(mVisualization))
                {
                    e.Breakdown.Draw(g, mVisualization.Width, mVisualization.Height);
                }
                pictureBox1.Refresh();
                if (!alwaysVisualizeToolStripMenuItem.Checked) mVisualize = false;
            }
        }

        private void boltControl1_PathGenerationProgress(object sender, DBMBolt.PathGenerationProgressEventArgs e)
        {
            tsslStatus.Text = "Generating lightning path from breakdown: " + e.SegmentIndex + " of " + e.TotalSegments + " complete (" + Math.Round(100 * (double)e.SegmentIndex / e.TotalSegments, 1) + "%)";
            tssbVisualize.Visible = true;
        }

        private void ThunderGenerator_Progress(object sender, Generator.ProgressEventArgs e)
        {
            tsslStatus.Text = "Generating thunder: " + e.SegmentsComplete + " of " + e.TotalSegments + " segments complete (" + Math.Round(100 * (double)e.SegmentsComplete / e.TotalSegments, 1) + "%)";
            tssbVisualize.Visible = true;
        }

        private void tssbVisualize_ButtonClick(object sender, EventArgs e)
        {
            mVisualize = true;
        }

        private void tssbVisualize_Click(object sender, EventArgs e)
        {

        }

        private void alwaysVisualizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alwaysVisualizeToolStripMenuItem.Checked = !alwaysVisualizeToolStripMenuItem.Checked;
            mVisualize = alwaysVisualizeToolStripMenuItem.Checked;
        }

        private void Input_ValidityChanged(object sender, UI.ValidityChangedEventArgs e)
        {
            ComputeValidity();
        }
    }
}
