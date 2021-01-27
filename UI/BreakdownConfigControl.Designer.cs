namespace Thundergen.UI
{
    partial class BreakdownConfigControl
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
            this.txtEta = new Thundergen.UI.ValidatingTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGrowthPerIteration = new Thundergen.UI.ValidatingTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRandomSeed = new Thundergen.UI.ValidatingTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBiasStrength = new Thundergen.UI.ValidatingTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBiasHeight = new Thundergen.UI.ValidatingTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInitialBreakdown = new Thundergen.UI.ValidatingTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCullThreshold = new Thundergen.UI.ValidatingTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCullLevel = new Thundergen.UI.ValidatingTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFractionToCullByCharge = new Thundergen.UI.ValidatingTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtEta
            // 
            this.txtEta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEta.BackColor = System.Drawing.Color.White;
            this.txtEta.Location = new System.Drawing.Point(95, 133);
            this.txtEta.Name = "txtEta";
            this.txtEta.Size = new System.Drawing.Size(252, 20);
            this.txtEta.TabIndex = 71;
            this.txtEta.Text = "1";
            this.txtEta.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtEta.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 13);
            this.label10.TabIndex = 70;
            this.label10.Text = "Eta";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(168, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 13);
            this.label9.TabIndex = 69;
            this.label9.Text = "charges added per field computation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 68;
            this.label8.Text = "Growth speed";
            // 
            // txtGrowthPerIteration
            // 
            this.txtGrowthPerIteration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGrowthPerIteration.BackColor = System.Drawing.Color.White;
            this.txtGrowthPerIteration.Location = new System.Drawing.Point(95, 107);
            this.txtGrowthPerIteration.Name = "txtGrowthPerIteration";
            this.txtGrowthPerIteration.Size = new System.Drawing.Size(67, 20);
            this.txtGrowthPerIteration.TabIndex = 67;
            this.txtGrowthPerIteration.Text = "1";
            this.txtGrowthPerIteration.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtGrowthPerIteration.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 66;
            this.label7.Text = "Random seed";
            // 
            // txtRandomSeed
            // 
            this.txtRandomSeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRandomSeed.BackColor = System.Drawing.Color.White;
            this.txtRandomSeed.Location = new System.Drawing.Point(95, 81);
            this.txtRandomSeed.Name = "txtRandomSeed";
            this.txtRandomSeed.Size = new System.Drawing.Size(252, 20);
            this.txtRandomSeed.TabIndex = 65;
            this.txtRandomSeed.Text = "1234";
            this.txtRandomSeed.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtRandomSeed.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Bias strength";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "fraction of charge in bolt";
            // 
            // txtBiasStrength
            // 
            this.txtBiasStrength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBiasStrength.BackColor = System.Drawing.Color.White;
            this.txtBiasStrength.Location = new System.Drawing.Point(95, 55);
            this.txtBiasStrength.Name = "txtBiasStrength";
            this.txtBiasStrength.Size = new System.Drawing.Size(125, 20);
            this.txtBiasStrength.TabIndex = 62;
            this.txtBiasStrength.Text = "0.1";
            this.txtBiasStrength.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtBiasStrength.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "grid units";
            // 
            // txtBiasHeight
            // 
            this.txtBiasHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBiasHeight.BackColor = System.Drawing.Color.White;
            this.txtBiasHeight.Location = new System.Drawing.Point(95, 29);
            this.txtBiasHeight.Name = "txtBiasHeight";
            this.txtBiasHeight.Size = new System.Drawing.Size(197, 20);
            this.txtBiasHeight.TabIndex = 60;
            this.txtBiasHeight.Text = "101";
            this.txtBiasHeight.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtBiasHeight.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Bias height";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "grid units";
            // 
            // txtInitialBreakdown
            // 
            this.txtInitialBreakdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInitialBreakdown.BackColor = System.Drawing.Color.White;
            this.txtInitialBreakdown.Location = new System.Drawing.Point(95, 3);
            this.txtInitialBreakdown.Name = "txtInitialBreakdown";
            this.txtInitialBreakdown.Size = new System.Drawing.Size(197, 20);
            this.txtInitialBreakdown.TabIndex = 57;
            this.txtInitialBreakdown.Text = "<0, 0, 100>";
            this.txtInitialBreakdown.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtInitialBreakdown.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Initial breakdown";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 72;
            this.label11.Text = "Candidate sites";
            // 
            // txtCullThreshold
            // 
            this.txtCullThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCullThreshold.BackColor = System.Drawing.Color.White;
            this.txtCullThreshold.Location = new System.Drawing.Point(136, 159);
            this.txtCullThreshold.Name = "txtCullThreshold";
            this.txtCullThreshold.Size = new System.Drawing.Size(42, 20);
            this.txtCullThreshold.TabIndex = 73;
            this.txtCullThreshold.Text = "1001";
            this.txtCullThreshold.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtCullThreshold.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(92, 162);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 74;
            this.label12.Text = "Above";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(184, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 13);
            this.label13.TabIndex = 75;
            this.label13.Text = "candidates, cull to";
            // 
            // txtCullLevel
            // 
            this.txtCullLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCullLevel.BackColor = System.Drawing.Color.White;
            this.txtCullLevel.Location = new System.Drawing.Point(276, 159);
            this.txtCullLevel.Name = "txtCullLevel";
            this.txtCullLevel.Size = new System.Drawing.Size(71, 20);
            this.txtCullLevel.TabIndex = 76;
            this.txtCullLevel.Text = "1000";
            this.txtCullLevel.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtCullLevel.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(92, 188);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 13);
            this.label14.TabIndex = 77;
            this.label14.Text = "with";
            // 
            // txtFractionToCullByCharge
            // 
            this.txtFractionToCullByCharge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFractionToCullByCharge.BackColor = System.Drawing.Color.White;
            this.txtFractionToCullByCharge.Location = new System.Drawing.Point(124, 185);
            this.txtFractionToCullByCharge.Name = "txtFractionToCullByCharge";
            this.txtFractionToCullByCharge.Size = new System.Drawing.Size(42, 20);
            this.txtFractionToCullByCharge.TabIndex = 78;
            this.txtFractionToCullByCharge.Text = "100";
            this.txtFractionToCullByCharge.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            this.txtFractionToCullByCharge.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(169, 188);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(177, 13);
            this.label15.TabIndex = 79;
            this.label15.Text = "percent by charge (others by height)";
            // 
            // BreakdownConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtFractionToCullByCharge);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtCullLevel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCullThreshold);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtEta);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtGrowthPerIteration);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRandomSeed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBiasStrength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBiasHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInitialBreakdown);
            this.Controls.Add(this.label1);
            this.Name = "BreakdownConfigControl";
            this.Size = new System.Drawing.Size(351, 210);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Thundergen.UI.ValidatingTextBox txtEta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private Thundergen.UI.ValidatingTextBox txtGrowthPerIteration;
        private System.Windows.Forms.Label label7;
        private Thundergen.UI.ValidatingTextBox txtRandomSeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Thundergen.UI.ValidatingTextBox txtBiasStrength;
        private System.Windows.Forms.Label label4;
        private Thundergen.UI.ValidatingTextBox txtBiasHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Thundergen.UI.ValidatingTextBox txtInitialBreakdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private Thundergen.UI.ValidatingTextBox txtCullThreshold;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private Thundergen.UI.ValidatingTextBox txtCullLevel;
        private System.Windows.Forms.Label label14;
        private Thundergen.UI.ValidatingTextBox txtFractionToCullByCharge;
        private System.Windows.Forms.Label label15;
    }
}
