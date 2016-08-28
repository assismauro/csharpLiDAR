namespace LiDARFileInfo
{
    partial class fMain
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
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.SeparatorChar = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.CSVFileName = new DevExpress.XtraEditors.ButtonEdit();
            this.Points2File = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Points2Print = new System.Windows.Forms.NumericUpDown();
            this.bOpen = new DevExpress.XtraEditors.SimpleButton();
            this.LiDARfName = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.mePropertiesLiDARFile = new DevExpress.XtraEditors.MemoEdit();
            this.meFullWavePoints = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CSVFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Points2Print)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LiDARfName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mePropertiesLiDARFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meFullWavePoints.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ofd
            // 
            this.ofd.Filter = "LiDAR files|*.las";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel1.MinSize = 90;
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(645, 365);
            this.splitContainerControl1.SplitterPosition = 209;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SeparatorChar);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.CSVFileName);
            this.panel1.Controls.Add(this.Points2File);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Points2Print);
            this.panel1.Controls.Add(this.bOpen);
            this.panel1.Controls.Add(this.LiDARfName);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 209);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Char Sep.";
            // 
            // SeparatorChar
            // 
            this.SeparatorChar.FormattingEnabled = true;
            this.SeparatorChar.Items.AddRange(new object[] {
            ";",
            ","});
            this.SeparatorChar.Location = new System.Drawing.Point(208, 78);
            this.SeparatorChar.Name = "SeparatorChar";
            this.SeparatorChar.Size = new System.Drawing.Size(55, 21);
            this.SeparatorChar.TabIndex = 15;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 140);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(125, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "FWF Points CSV File Name";
            // 
            // CSVFileName
            // 
            this.CSVFileName.Location = new System.Drawing.Point(16, 157);
            this.CSVFileName.Name = "CSVFileName";
            this.CSVFileName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.CSVFileName.Size = new System.Drawing.Size(491, 20);
            this.CSVFileName.TabIndex = 13;
            this.CSVFileName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.CSVFileName_ButtonClick);
            // 
            // Points2File
            // 
            this.Points2File.AutoSize = true;
            this.Points2File.Location = new System.Drawing.Point(12, 105);
            this.Points2File.Name = "Points2File";
            this.Points2File.Size = new System.Drawing.Size(89, 17);
            this.Points2File.TabIndex = 12;
            this.Points2File.Text = "Points To File";
            this.Points2File.UseVisualStyleBackColor = true;
            this.Points2File.CheckedChanged += new System.EventHandler(this.Points2File_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Number of points to show (0 = all)";
            // 
            // Points2Print
            // 
            this.Points2Print.Location = new System.Drawing.Point(12, 78);
            this.Points2Print.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Points2Print.Name = "Points2Print";
            this.Points2Print.Size = new System.Drawing.Size(120, 21);
            this.Points2Print.TabIndex = 10;
            this.Points2Print.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(513, 30);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(75, 23);
            this.bOpen.TabIndex = 9;
            this.bOpen.Text = "Go";
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // LiDARfName
            // 
            this.LiDARfName.Location = new System.Drawing.Point(12, 32);
            this.LiDARfName.Name = "LiDARfName";
            this.LiDARfName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.LiDARfName.Size = new System.Drawing.Size(495, 20);
            this.LiDARfName.TabIndex = 7;
            this.LiDARfName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.fName_ButtonClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "LAS File Name";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.mePropertiesLiDARFile);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.meFullWavePoints);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(645, 151);
            this.splitContainerControl2.SplitterPosition = 77;
            this.splitContainerControl2.TabIndex = 2;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // mePropertiesLiDARFile
            // 
            this.mePropertiesLiDARFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mePropertiesLiDARFile.Location = new System.Drawing.Point(0, 0);
            this.mePropertiesLiDARFile.Name = "mePropertiesLiDARFile";
            this.mePropertiesLiDARFile.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mePropertiesLiDARFile.Properties.Appearance.Options.UseFont = true;
            this.mePropertiesLiDARFile.Properties.WordWrap = false;
            this.mePropertiesLiDARFile.Size = new System.Drawing.Size(645, 77);
            this.mePropertiesLiDARFile.TabIndex = 0;
            // 
            // meFullWavePoints
            // 
            this.meFullWavePoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meFullWavePoints.Location = new System.Drawing.Point(0, 0);
            this.meFullWavePoints.Name = "meFullWavePoints";
            this.meFullWavePoints.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meFullWavePoints.Properties.Appearance.Options.UseFont = true;
            this.meFullWavePoints.Size = new System.Drawing.Size(645, 69);
            this.meFullWavePoints.TabIndex = 1;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 365);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "fMain";
            this.Text = "LiDAR File Info";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Shown += new System.EventHandler(this.fMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CSVFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Points2Print)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LiDARfName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mePropertiesLiDARFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meFullWavePoints.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofd;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton bOpen;
        private DevExpress.XtraEditors.ButtonEdit LiDARfName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit mePropertiesLiDARFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Points2Print;
        private System.Windows.Forms.CheckBox Points2File;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ButtonEdit CSVFileName;
        private System.Windows.Forms.ComboBox SeparatorChar;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.MemoEdit meFullWavePoints;
    }
}

