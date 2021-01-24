namespace Thundergen.UI
{
    partial class DBMBoltInterpolationConfigControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEnvelopeExtent = new Thundergen.UI.ValidatingTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInitialSmoothing = new Thundergen.UI.ValidatingTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSegmentLength = new Thundergen.UI.ValidatingTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtScale = new Thundergen.UI.ValidatingTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "grid cells in each direction";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 78;
            this.label4.Text = "Envelope averaging";
            // 
            // txtEnvelopeExtent
            // 
            this.txtEnvelopeExtent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnvelopeExtent.BackColor = System.Drawing.Color.White;
            this.txtEnvelopeExtent.Location = new System.Drawing.Point(110, 81);
            this.txtEnvelopeExtent.Name = "txtEnvelopeExtent";
            this.txtEnvelopeExtent.Size = new System.Drawing.Size(73, 20);
            this.txtEnvelopeExtent.TabIndex = 77;
            this.txtEnvelopeExtent.Text = "10";
            this.txtEnvelopeExtent.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "grid cells behind and ahead";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Initial smoothing";
            // 
            // txtInitialSmoothing
            // 
            this.txtInitialSmoothing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInitialSmoothing.BackColor = System.Drawing.Color.White;
            this.txtInitialSmoothing.Location = new System.Drawing.Point(110, 55);
            this.txtInitialSmoothing.Name = "txtInitialSmoothing";
            this.txtInitialSmoothing.Size = new System.Drawing.Size(65, 20);
            this.txtInitialSmoothing.TabIndex = 74;
            this.txtInitialSmoothing.Text = "2";
            this.txtInitialSmoothing.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(164, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(157, 13);
            this.label13.TabIndex = 73;
            this.label13.Text = "meters per interpolated segment";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 72;
            this.label14.Text = "Segment length";
            // 
            // txtSegmentLength
            // 
            this.txtSegmentLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSegmentLength.BackColor = System.Drawing.Color.White;
            this.txtSegmentLength.Location = new System.Drawing.Point(110, 29);
            this.txtSegmentLength.Name = "txtSegmentLength";
            this.txtSegmentLength.Size = new System.Drawing.Size(48, 20);
            this.txtSegmentLength.TabIndex = 71;
            this.txtSegmentLength.Text = "1.0";
            this.txtSegmentLength.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(225, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 13);
            this.label12.TabIndex = 70;
            this.label12.Text = "meters per grid unit";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 69;
            this.label11.Text = "Scale";
            // 
            // txtScale
            // 
            this.txtScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScale.BackColor = System.Drawing.Color.White;
            this.txtScale.Location = new System.Drawing.Point(110, 3);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(109, 20);
            this.txtScale.TabIndex = 68;
            this.txtScale.Text = "1";
            this.txtScale.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            // 
            // DBMBoltInterpolationConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEnvelopeExtent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInitialSmoothing);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtSegmentLength);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtScale);
            this.Name = "DBMBoltInterpolationConfigControl";
            this.Size = new System.Drawing.Size(322, 103);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Thundergen.UI.ValidatingTextBox txtEnvelopeExtent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Thundergen.UI.ValidatingTextBox txtInitialSmoothing;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private Thundergen.UI.ValidatingTextBox txtSegmentLength;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private Thundergen.UI.ValidatingTextBox txtScale;
    }
}
