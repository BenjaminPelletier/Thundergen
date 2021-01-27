namespace Thundergen
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            Thundergen.Thunder.Generator.Config config6 = new Thundergen.Thunder.Generator.Config();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssbVisualize = new System.Windows.Forms.ToolStripSplitButton();
            this.alwaysVisualizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssbOpenThunder = new System.Windows.Forms.ToolStripSplitButton();
            this.openfolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWAVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMakeThunder = new System.Windows.Forms.Button();
            this.gbBolt = new System.Windows.Forms.GroupBox();
            this.boltControl1 = new Thundergen.UI.BoltControl();
            this.gbThunder = new System.Windows.Forms.GroupBox();
            this.thunderGeneratorConfig1 = new Thundergen.UI.ThunderGeneratorConfigControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cmdImport = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdThunder = new System.Windows.Forms.OpenFileDialog();
            this.sfdThunder = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.gbBolt.SuspendLayout();
            this.gbThunder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1073, 936);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssbVisualize,
            this.tsslStatus,
            this.tssbOpenThunder});
            this.statusStrip1.Location = new System.Drawing.Point(0, 973);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1528, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssbVisualize
            // 
            this.tssbVisualize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbVisualize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysVisualizeToolStripMenuItem});
            this.tssbVisualize.Image = ((System.Drawing.Image)(resources.GetObject("tssbVisualize.Image")));
            this.tssbVisualize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbVisualize.Name = "tssbVisualize";
            this.tssbVisualize.Size = new System.Drawing.Size(32, 20);
            this.tssbVisualize.Text = "toolStripSplitButton1";
            this.tssbVisualize.Visible = false;
            this.tssbVisualize.ButtonClick += new System.EventHandler(this.tssbVisualize_ButtonClick);
            this.tssbVisualize.Click += new System.EventHandler(this.tssbVisualize_Click);
            // 
            // alwaysVisualizeToolStripMenuItem
            // 
            this.alwaysVisualizeToolStripMenuItem.Name = "alwaysVisualizeToolStripMenuItem";
            this.alwaysVisualizeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.alwaysVisualizeToolStripMenuItem.Text = "&Always visualize";
            this.alwaysVisualizeToolStripMenuItem.Click += new System.EventHandler(this.alwaysVisualizeToolStripMenuItem_Click);
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(105, 17);
            this.tsslStatus.Text = "Ready to generate.";
            // 
            // tssbOpenThunder
            // 
            this.tssbOpenThunder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbOpenThunder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openfolderToolStripMenuItem,
            this.openWAVFileToolStripMenuItem});
            this.tssbOpenThunder.Image = ((System.Drawing.Image)(resources.GetObject("tssbOpenThunder.Image")));
            this.tssbOpenThunder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbOpenThunder.Name = "tssbOpenThunder";
            this.tssbOpenThunder.Size = new System.Drawing.Size(32, 20);
            this.tssbOpenThunder.Text = "Thunder player";
            this.tssbOpenThunder.Visible = false;
            this.tssbOpenThunder.ButtonClick += new System.EventHandler(this.tssbOpenThunder_ButtonClick);
            // 
            // openfolderToolStripMenuItem
            // 
            this.openfolderToolStripMenuItem.Name = "openfolderToolStripMenuItem";
            this.openfolderToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openfolderToolStripMenuItem.Text = "Open &folder";
            this.openfolderToolStripMenuItem.Click += new System.EventHandler(this.openfolderToolStripMenuItem_Click);
            // 
            // openWAVFileToolStripMenuItem
            // 
            this.openWAVFileToolStripMenuItem.Name = "openWAVFileToolStripMenuItem";
            this.openWAVFileToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openWAVFileToolStripMenuItem.Text = "Open &WAV file";
            this.openWAVFileToolStripMenuItem.Click += new System.EventHandler(this.openWAVFileToolStripMenuItem_Click);
            // 
            // cmdMakeThunder
            // 
            this.cmdMakeThunder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMakeThunder.Location = new System.Drawing.Point(330, 917);
            this.cmdMakeThunder.Name = "cmdMakeThunder";
            this.cmdMakeThunder.Size = new System.Drawing.Size(88, 23);
            this.cmdMakeThunder.TabIndex = 10;
            this.cmdMakeThunder.Text = "Generate";
            this.cmdMakeThunder.UseVisualStyleBackColor = true;
            this.cmdMakeThunder.Click += new System.EventHandler(this.cmdMakeThunder_Click);
            // 
            // gbBolt
            // 
            this.gbBolt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBolt.Controls.Add(this.boltControl1);
            this.gbBolt.Location = new System.Drawing.Point(3, 4);
            this.gbBolt.Name = "gbBolt";
            this.gbBolt.Size = new System.Drawing.Size(415, 504);
            this.gbBolt.TabIndex = 11;
            this.gbBolt.TabStop = false;
            this.gbBolt.Text = "Lightning";
            // 
            // boltControl1
            // 
            this.boltControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boltControl1.Location = new System.Drawing.Point(6, 19);
            this.boltControl1.Name = "boltControl1";
            this.boltControl1.Size = new System.Drawing.Size(402, 479);
            this.boltControl1.TabIndex = 0;
            this.boltControl1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.Input_ValidityChanged);
            this.boltControl1.CacheChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.boltControl1_CacheChanged);
            // 
            // gbThunder
            // 
            this.gbThunder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbThunder.Controls.Add(this.thunderGeneratorConfig1);
            this.gbThunder.Location = new System.Drawing.Point(6, 514);
            this.gbThunder.Name = "gbThunder";
            this.gbThunder.Size = new System.Drawing.Size(415, 283);
            this.gbThunder.TabIndex = 12;
            this.gbThunder.TabStop = false;
            this.gbThunder.Text = "Thunder";
            // 
            // thunderGeneratorConfig1
            // 
            this.thunderGeneratorConfig1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thunderGeneratorConfig1.Config = config6;
            this.thunderGeneratorConfig1.Location = new System.Drawing.Point(6, 19);
            this.thunderGeneratorConfig1.Name = "thunderGeneratorConfig1";
            this.thunderGeneratorConfig1.Size = new System.Drawing.Size(403, 258);
            this.thunderGeneratorConfig1.TabIndex = 11;
            this.thunderGeneratorConfig1.ValidityChanged += new System.EventHandler<Thundergen.UI.ValidityChangedEventArgs>(this.Input_ValidityChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdExport);
            this.splitContainer1.Panel2.Controls.Add(this.cmdImport);
            this.splitContainer1.Panel2.Controls.Add(this.gbBolt);
            this.splitContainer1.Panel2.Controls.Add(this.cmdMakeThunder);
            this.splitContainer1.Panel2.Controls.Add(this.gbThunder);
            this.splitContainer1.Size = new System.Drawing.Size(1504, 943);
            this.splitContainer1.SplitterDistance = 1079;
            this.splitContainer1.TabIndex = 13;
            // 
            // cmdExport
            // 
            this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExport.Enabled = false;
            this.cmdExport.Location = new System.Drawing.Point(69, 917);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(60, 23);
            this.cmdExport.TabIndex = 14;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cmdImport
            // 
            this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdImport.Location = new System.Drawing.Point(3, 917);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(60, 23);
            this.cmdImport.TabIndex = 13;
            this.cmdImport.Text = "Import";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1528, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ofdThunder
            // 
            this.ofdThunder.DefaultExt = "thunder";
            this.ofdThunder.FileName = "thunder1";
            this.ofdThunder.Filter = "Thunder configs|*.thunder";
            this.ofdThunder.Title = "Export thunder configuration";
            // 
            // sfdThunder
            // 
            this.sfdThunder.DefaultExt = "thunder";
            this.sfdThunder.Filter = "Thunder configs|*.thunder";
            this.sfdThunder.Title = "Import thunder configuration";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1528, 995);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Thundergen";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbBolt.ResumeLayout(false);
            this.gbThunder.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.Button cmdMakeThunder;
        private System.Windows.Forms.GroupBox gbBolt;
        private System.Windows.Forms.GroupBox gbThunder;
        private System.Windows.Forms.ToolTip toolTip1;
        private UI.ThunderGeneratorConfigControl thunderGeneratorConfig1;
        private UI.BoltControl boltControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton tssbVisualize;
        private System.Windows.Forms.ToolStripMenuItem alwaysVisualizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton tssbOpenThunder;
        private System.Windows.Forms.ToolStripMenuItem openfolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWAVFileToolStripMenuItem;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.OpenFileDialog ofdThunder;
        private System.Windows.Forms.SaveFileDialog sfdThunder;
    }
}

