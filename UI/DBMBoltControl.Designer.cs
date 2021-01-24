namespace Thundergen.UI
{
    partial class DBMBoltControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbBreakdown = new System.Windows.Forms.GroupBox();
            this.breakdownControl1 = new Thundergen.UI.BreakdownControl();
            this.gbInterpolation = new System.Windows.Forms.GroupBox();
            this.dbmBoltInterpolationConfigControl1 = new Thundergen.UI.DBMBoltInterpolationConfigControl();
            this.gbBreakdown.SuspendLayout();
            this.gbInterpolation.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBreakdown
            // 
            this.gbBreakdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBreakdown.Controls.Add(this.breakdownControl1);
            this.gbBreakdown.Location = new System.Drawing.Point(3, 3);
            this.gbBreakdown.Name = "gbBreakdown";
            this.gbBreakdown.Size = new System.Drawing.Size(401, 211);
            this.gbBreakdown.TabIndex = 68;
            this.gbBreakdown.TabStop = false;
            this.gbBreakdown.Text = "Breakdown";
            // 
            // breakdownControl1
            // 
            this.breakdownControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.breakdownControl1.Location = new System.Drawing.Point(6, 14);
            this.breakdownControl1.Name = "breakdownControl1";
            this.breakdownControl1.Size = new System.Drawing.Size(389, 193);
            this.breakdownControl1.TabIndex = 1;
            this.breakdownControl1.BreakdownPropagationProgress += new System.EventHandler<Thundergen.Lightning.DBMBreakdown.GroundPropagationProgressEventArgs>(this.breakdownControl1_BreakdownPropagationProgress);
            this.breakdownControl1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.Input_ValidityChanged);
            this.breakdownControl1.ValueChanged += new System.EventHandler(this.Input_ValueChanged);
            // 
            // gbInterpolation
            // 
            this.gbInterpolation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInterpolation.Controls.Add(this.dbmBoltInterpolationConfigControl1);
            this.gbInterpolation.Location = new System.Drawing.Point(3, 216);
            this.gbInterpolation.Name = "gbInterpolation";
            this.gbInterpolation.Size = new System.Drawing.Size(401, 128);
            this.gbInterpolation.TabIndex = 70;
            this.gbInterpolation.TabStop = false;
            this.gbInterpolation.Text = "Path interpolation";
            // 
            // dbmBoltInterpolationConfigControl1
            // 
            this.dbmBoltInterpolationConfigControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbmBoltInterpolationConfigControl1.Location = new System.Drawing.Point(6, 19);
            this.dbmBoltInterpolationConfigControl1.Name = "dbmBoltInterpolationConfigControl1";
            this.dbmBoltInterpolationConfigControl1.Size = new System.Drawing.Size(389, 103);
            this.dbmBoltInterpolationConfigControl1.TabIndex = 69;
            this.dbmBoltInterpolationConfigControl1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.Input_ValidityChanged);
            this.dbmBoltInterpolationConfigControl1.ValueChanged += new System.EventHandler(this.Input_ValueChanged);
            // 
            // DBMBoltControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbInterpolation);
            this.Controls.Add(this.gbBreakdown);
            this.Name = "DBMBoltControl";
            this.Size = new System.Drawing.Size(407, 348);
            this.gbBreakdown.ResumeLayout(false);
            this.gbInterpolation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbBreakdown;
        private BreakdownControl breakdownControl1;
        private DBMBoltInterpolationConfigControl dbmBoltInterpolationConfigControl1;
        private System.Windows.Forms.GroupBox gbInterpolation;
    }
}
