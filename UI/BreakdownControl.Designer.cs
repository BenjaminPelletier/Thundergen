namespace Thundergen.UI
{
    partial class BreakdownControl
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
            this.cmdImport = new System.Windows.Forms.Button();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.breakdownConfigControl1 = new Thundergen.UI.BreakdownConfigControl();
            this.cmdReset = new System.Windows.Forms.Button();
            this.sfdBreakdown = new System.Windows.Forms.SaveFileDialog();
            this.ofdBreakdown = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cmdImport
            // 
            this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdImport.Location = new System.Drawing.Point(3, 217);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(58, 23);
            this.cmdImport.TabIndex = 2;
            this.cmdImport.Text = "Import";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExport.Enabled = false;
            this.cmdExport.Location = new System.Drawing.Point(67, 217);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(58, 23);
            this.cmdExport.TabIndex = 3;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGenerate.Location = new System.Drawing.Point(261, 217);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(68, 23);
            this.cmdGenerate.TabIndex = 4;
            this.cmdGenerate.Text = "Generate";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // breakdownConfigControl1
            // 
            this.breakdownConfigControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.breakdownConfigControl1.Location = new System.Drawing.Point(3, 3);
            this.breakdownConfigControl1.Name = "breakdownConfigControl1";
            this.breakdownConfigControl1.Size = new System.Drawing.Size(326, 208);
            this.breakdownConfigControl1.TabIndex = 1;
            this.breakdownConfigControl1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.breakdownConfigControl1_ValidityChanged);
            this.breakdownConfigControl1.ValueChanged += new System.EventHandler(this.breakdownConfigControl1_ValueChanged);
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdReset.Enabled = false;
            this.cmdReset.Location = new System.Drawing.Point(131, 217);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(58, 23);
            this.cmdReset.TabIndex = 5;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // sfdBreakdown
            // 
            this.sfdBreakdown.DefaultExt = "breakdown";
            this.sfdBreakdown.Filter = "Breakdown patterns|*.breakdown";
            this.sfdBreakdown.Title = "Import dielectric breakdown pattern";
            // 
            // ofdBreakdown
            // 
            this.ofdBreakdown.DefaultExt = "breakdown";
            this.ofdBreakdown.FileName = "breakdown1";
            this.ofdBreakdown.Filter = "Breakdown patterns|*.breakdown";
            this.ofdBreakdown.Title = "Export dielectric breakdown pattern";
            // 
            // BreakdownControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdGenerate);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.breakdownConfigControl1);
            this.Name = "BreakdownControl";
            this.Size = new System.Drawing.Size(332, 244);
            this.ResumeLayout(false);

        }

        #endregion

        private BreakdownConfigControl breakdownConfigControl1;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.Button cmdGenerate;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.SaveFileDialog sfdBreakdown;
        private System.Windows.Forms.OpenFileDialog ofdBreakdown;
    }
}
