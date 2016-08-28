namespace DigitacaoInventario
{
    partial class fAbout
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
            this.About = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // About
            // 
            this.About.Dock = System.Windows.Forms.DockStyle.Fill;
            this.About.Location = new System.Drawing.Point(0, 0);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(387, 352);
            this.About.TabIndex = 0;
            this.About.Text = "Digitação de Inventário Florestal\n\nPrograma desenvolvido para entrada de dados de" +
    " inventário\n";
            // 
            // fAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 352);
            this.Controls.Add(this.About);
            this.Name = "fAbout";
            this.Text = "Sobre...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox About;
    }
}