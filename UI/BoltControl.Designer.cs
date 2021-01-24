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
            this.dbmBoltControl1.PathGenerationProgress += new System.EventHandler<Thundergen.Lightning.DBMBolt.PathGenerationProgressEventArgs>(this.dbmBoltControl1_PathGenerationProgress);
            this.dbmBoltControl1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.dbmBoltControl1_ValidityChanged);
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
            // BoltControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }
}
