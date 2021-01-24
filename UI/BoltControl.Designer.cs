namespace Thundergen.UI
{
    partial class BoltControl
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
            this.tcConfig = new System.Windows.Forms.TabControl();
            this.tpDBM = new System.Windows.Forms.TabPage();
            this.dbmBoltControl1 = new Thundergen.UI.DBMBoltControl();
            this.tpBrownian = new System.Windows.Forms.TabPage();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.cmdImport = new System.Windows.Forms.Button();
            this.cmdExport = new System.Windows.Forms.Button();
            this.sfdBolt = new System.Windows.Forms.SaveFileDialog();
            this.ofdBolt = new System.Windows.Forms.OpenFileDialog();
            this.tcConfig.SuspendLayout();
            this.tpDBM.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcConfig
            // 
            this.tcConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcConfig.Controls.Add(this.tpDBM);
            this.tcConfig.Controls.Add(this.tpBrownian);
            this.tcConfig.Location = new System.Drawing.Point(3, 3);
            this.tcConfig.Name = "tcConfig";
            this.tcConfig.SelectedIndex = 0;
            this.tcConfig.Size = new System.Drawing.Size(362, 381);
            this.tcConfig.TabIndex = 0;
            this.tcConfig.TabIndexChanged += new System.EventHandler(this.tcConfig_TabIndexChanged);
            // 
            // tpDBM
            // 
            this.tpDBM.Controls.Add(this.dbmBoltControl1);
            this.tpDBM.Location = new System.Drawing.Point(4, 22);
            this.tpDBM.Name = "tpDBM";
            this.tpDBM.Padding = new System.Windows.Forms.Padding(3);
            this.tpDBM.Size = new System.Drawing.Size(354, 355);
            this.tpDBM.TabIndex = 0;
            this.tpDBM.Tag = "";
            this.tpDBM.Text = "Dielectric breakdown";
            this.tpDBM.UseVisualStyleBackColor = true;
            // 
            // dbmBoltControl1
            // 
            this.dbmBoltControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbmBoltControl1.Location = new System.Drawing.Point(3, 6);
            this.dbmBoltControl1.Name = "dbmBoltControl1";
            this.dbmBoltControl1.Size = new System.Drawing.Size(345, 343);
            this.dbmBoltControl1.TabIndex = 0;
            this.dbmBoltControl1.BreakdownPropagationProgress += new System.EventHandler<Thundergen.Lightning.DBMBreakdown.GroundPropagationProgressEventArgs>(this.dbmBoltControl1_BreakdownPropagationProgress);
            this.dbmBoltControl1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.dbmBoltControl1_ValidityChanged);
            this.dbmBoltControl1.ValueChanged += new System.EventHandler(this.dbmBoltControl1_ValueChanged);
            // 
            // tpBrownian
            // 
            this.tpBrownian.Location = new System.Drawing.Point(4, 22);
            this.tpBrownian.Name = "tpBrownian";
            this.tpBrownian.Padding = new System.Windows.Forms.Padding(3);
            this.tpBrownian.Size = new System.Drawing.Size(354, 355);
            this.tpBrownian.TabIndex = 1;
            this.tpBrownian.Text = "Brownian";
            this.tpBrownian.UseVisualStyleBackColor = true;
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGenerate.Location = new System.Drawing.Point(286, 390);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(75, 23);
            this.cmdGenerate.TabIndex = 1;
            this.cmdGenerate.Text = "Generate";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // cmdImport
            // 
            this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdImport.Location = new System.Drawing.Point(3, 390);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(54, 23);
            this.cmdImport.TabIndex = 2;
            this.cmdImport.Text = "Import";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExport.Enabled = false;
            this.cmdExport.Location = new System.Drawing.Point(63, 390);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(54, 23);
            this.cmdExport.TabIndex = 3;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // sfdBolt
            // 
            this.sfdBolt.DefaultExt = "bolt";
            this.sfdBolt.Filter = "Bolts|*.bolt";
            this.sfdBolt.Title = "Import lightning bolt geometry";
            // 
            // ofdBolt
            // 
            this.ofdBolt.DefaultExt = "bolt";
            this.ofdBolt.FileName = "breakdown1";
            this.ofdBolt.Filter = "Bolts|*.bolt";
            this.ofdBolt.Title = "Export lightning bolt geometry";
            // 
            // BoltControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.cmdGenerate);
            this.Controls.Add(this.tcConfig);
            this.Name = "BoltControl";
            this.Size = new System.Drawing.Size(368, 416);
            this.tcConfig.ResumeLayout(false);
            this.tpDBM.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcConfig;
        private System.Windows.Forms.TabPage tpDBM;
        private System.Windows.Forms.TabPage tpBrownian;
        private System.Windows.Forms.Button cmdGenerate;
        private DBMBoltControl dbmBoltControl1;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.SaveFileDialog sfdBolt;
        private System.Windows.Forms.OpenFileDialog ofdBolt;
    }
}
