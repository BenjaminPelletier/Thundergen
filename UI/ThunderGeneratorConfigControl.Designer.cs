namespace Thundergen.UI
{
    partial class ThunderGeneratorConfigControl
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
            this.label13 = new System.Windows.Forms.Label();
            this.atmosphericConditions1 = new Thundergen.UI.AtmosphericConditionsControl();
            this.chkAttenuation = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkSteepen = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSamplesPerStep = new Thundergen.UI.ValidatingTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtInitialDistance = new Thundergen.UI.ValidatingTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInitialVolume = new Thundergen.UI.ValidatingTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdRandomFollowingStrokes = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFollowingStrokes = new Thundergen.UI.ValidatingTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtObserver = new Thundergen.UI.ValidatingTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdClearFollowingStrokes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(2, 187);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 59;
            this.label13.Text = "Atmosphere";
            // 
            // atmosphericConditions1
            // 
            this.atmosphericConditions1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.atmosphericConditions1.Location = new System.Drawing.Point(98, 181);
            this.atmosphericConditions1.Name = "atmosphericConditions1";
            this.atmosphericConditions1.Size = new System.Drawing.Size(223, 81);
            this.atmosphericConditions1.TabIndex = 58;
            this.atmosphericConditions1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.Input_ValidityChanged);
            // 
            // chkAttenuation
            // 
            this.chkAttenuation.AutoSize = true;
            this.chkAttenuation.Checked = true;
            this.chkAttenuation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAttenuation.Location = new System.Drawing.Point(99, 161);
            this.chkAttenuation.Name = "chkAttenuation";
            this.chkAttenuation.Size = new System.Drawing.Size(15, 14);
            this.chkAttenuation.TabIndex = 57;
            this.chkAttenuation.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 161);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 56;
            this.label12.Text = "Attenuation";
            // 
            // chkSteepen
            // 
            this.chkSteepen.AutoSize = true;
            this.chkSteepen.Checked = true;
            this.chkSteepen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSteepen.Location = new System.Drawing.Point(99, 135);
            this.chkSteepen.Name = "chkSteepen";
            this.chkSteepen.Size = new System.Drawing.Size(15, 14);
            this.chkSteepen.TabIndex = 55;
            this.chkSteepen.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(213, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "max samples per step";
            // 
            // txtSamplesPerStep
            // 
            this.txtSamplesPerStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSamplesPerStep.BackColor = System.Drawing.Color.White;
            this.txtSamplesPerStep.Location = new System.Drawing.Point(120, 132);
            this.txtSamplesPerStep.Name = "txtSamplesPerStep";
            this.txtSamplesPerStep.Size = new System.Drawing.Size(87, 20);
            this.txtSamplesPerStep.TabIndex = 53;
            this.txtSamplesPerStep.Text = "0.5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Steepening effect";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(283, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "meters";
            // 
            // txtInitialDistance
            // 
            this.txtInitialDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInitialDistance.BackColor = System.Drawing.Color.White;
            this.txtInitialDistance.Location = new System.Drawing.Point(209, 106);
            this.txtInitialDistance.Name = "txtInitialDistance";
            this.txtInitialDistance.Size = new System.Drawing.Size(68, 20);
            this.txtInitialDistance.TabIndex = 50;
            this.txtInitialDistance.Text = "10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "dBSPL at ";
            // 
            // txtInitialVolume
            // 
            this.txtInitialVolume.BackColor = System.Drawing.Color.White;
            this.txtInitialVolume.Location = new System.Drawing.Point(98, 106);
            this.txtInitialVolume.Name = "txtInitialVolume";
            this.txtInitialVolume.Size = new System.Drawing.Size(44, 20);
            this.txtInitialVolume.TabIndex = 48;
            this.txtInitialVolume.Text = "180";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Initial volume";
            // 
            // cmdRandomFollowingStrokes
            // 
            this.cmdRandomFollowingStrokes.Location = new System.Drawing.Point(21, 48);
            this.cmdRandomFollowingStrokes.Name = "cmdRandomFollowingStrokes";
            this.cmdRandomFollowingStrokes.Size = new System.Drawing.Size(69, 23);
            this.cmdRandomFollowingStrokes.TabIndex = 46;
            this.cmdRandomFollowingStrokes.Text = "Randomize";
            this.cmdRandomFollowingStrokes.UseVisualStyleBackColor = true;
            this.cmdRandomFollowingStrokes.Click += new System.EventHandler(this.cmdRandomFollowingStrokes_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Following strokes";
            // 
            // txtFollowingStrokes
            // 
            this.txtFollowingStrokes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFollowingStrokes.Location = new System.Drawing.Point(98, 29);
            this.txtFollowingStrokes.Multiline = true;
            this.txtFollowingStrokes.Name = "txtFollowingStrokes";
            this.txtFollowingStrokes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFollowingStrokes.Size = new System.Drawing.Size(223, 71);
            this.txtFollowingStrokes.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(283, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "meters";
            // 
            // txtObserver
            // 
            this.txtObserver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObserver.BackColor = System.Drawing.Color.White;
            this.txtObserver.Location = new System.Drawing.Point(98, 3);
            this.txtObserver.Name = "txtObserver";
            this.txtObserver.Size = new System.Drawing.Size(182, 20);
            this.txtObserver.TabIndex = 39;
            this.txtObserver.Text = "<300, 0, 0>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Observer location";
            // 
            // cmdClearFollowingStrokes
            // 
            this.cmdClearFollowingStrokes.Location = new System.Drawing.Point(21, 77);
            this.cmdClearFollowingStrokes.Name = "cmdClearFollowingStrokes";
            this.cmdClearFollowingStrokes.Size = new System.Drawing.Size(69, 23);
            this.cmdClearFollowingStrokes.TabIndex = 61;
            this.cmdClearFollowingStrokes.Text = "Clear";
            this.cmdClearFollowingStrokes.UseVisualStyleBackColor = true;
            this.cmdClearFollowingStrokes.Click += new System.EventHandler(this.cmdClearFollowingStrokes_Click);
            // 
            // ThunderGeneratorConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdClearFollowingStrokes);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.atmosphericConditions1);
            this.Controls.Add(this.chkAttenuation);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.chkSteepen);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSamplesPerStep);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtInitialDistance);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtInitialVolume);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmdRandomFollowingStrokes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFollowingStrokes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtObserver);
            this.Controls.Add(this.label2);
            this.Name = "ThunderGeneratorConfigControl";
            this.Size = new System.Drawing.Size(323, 259);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label13;
        private AtmosphericConditionsControl atmosphericConditions1;
        private System.Windows.Forms.CheckBox chkAttenuation;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkSteepen;
        private System.Windows.Forms.Label label11;
        private Thundergen.UI.ValidatingTextBox txtSamplesPerStep;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private Thundergen.UI.ValidatingTextBox txtInitialDistance;
        private System.Windows.Forms.Label label7;
        private Thundergen.UI.ValidatingTextBox txtInitialVolume;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdRandomFollowingStrokes;
        private System.Windows.Forms.Label label6;
        private Thundergen.UI.ValidatingTextBox txtFollowingStrokes;
        private System.Windows.Forms.Label label5;
        private Thundergen.UI.ValidatingTextBox txtObserver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdClearFollowingStrokes;
    }
}
