namespace Thundergen.UI
{
    partial class AtmosphericConditionsControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTemperature = new Thundergen.UI.ValidatingTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPressure = new Thundergen.UI.ValidatingTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRelativeHumidity = new Thundergen.UI.ValidatingTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Temperature";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemperature.BackColor = System.Drawing.Color.White;
            this.txtTemperature.Location = new System.Drawing.Point(96, 3);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(70, 20);
            this.txtTemperature.TabIndex = 1;
            this.txtTemperature.Text = "20";
            this.txtTemperature.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "deg C";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "mBar";
            // 
            // txtPressure
            // 
            this.txtPressure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPressure.BackColor = System.Drawing.Color.White;
            this.txtPressure.Location = new System.Drawing.Point(96, 29);
            this.txtPressure.Name = "txtPressure";
            this.txtPressure.Size = new System.Drawing.Size(70, 20);
            this.txtPressure.TabIndex = 4;
            this.txtPressure.Text = "1013.2";
            this.txtPressure.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Pressure";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "percent";
            // 
            // txtRelativeHumidity
            // 
            this.txtRelativeHumidity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRelativeHumidity.BackColor = System.Drawing.Color.White;
            this.txtRelativeHumidity.Location = new System.Drawing.Point(96, 55);
            this.txtRelativeHumidity.Name = "txtRelativeHumidity";
            this.txtRelativeHumidity.Size = new System.Drawing.Size(70, 20);
            this.txtRelativeHumidity.TabIndex = 7;
            this.txtRelativeHumidity.Text = "90";
            this.txtRelativeHumidity.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.txtInput_ValidityChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Relative humidity";
            // 
            // AtmosphericConditionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRelativeHumidity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPressure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTemperature);
            this.Controls.Add(this.label1);
            this.Name = "AtmosphericConditionsControl";
            this.Size = new System.Drawing.Size(218, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Thundergen.UI.ValidatingTextBox txtTemperature;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Thundergen.UI.ValidatingTextBox txtPressure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Thundergen.UI.ValidatingTextBox txtRelativeHumidity;
        private System.Windows.Forms.Label label6;
    }
}
