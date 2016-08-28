using System;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;


namespace LiDARFileInfo
{
    public partial class fMain : Form
    {

        LiDARFileStuff.LiDARFile lidarFile = null;
        public fMain()
        {
            InitializeComponent();
            SeparatorChar.Text = ";";
        }

        private void fName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ofd.Filter = "LiDAR files|*.las";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                LiDARfName.Text = ofd.FileName;
            }
        }

        private void fMain_Shown(object sender, EventArgs e)
        {
            LiDARfName.Text = Properties.Settings.Default.LidarFileName;
            Points2Print.Value = Properties.Settings.Default.Points2Print;
            if (File.Exists(LiDARfName.Text))
            {
                try
                {
                    bOpen_Click(null, null);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(string.Format("Error opening file {0}: {1}", LiDARfName.Text, ex.Message));
                }
            }
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LidarFileName = LiDARfName.Text;
            Properties.Settings.Default.Points2Print = (int)Points2Print.Value;
            Properties.Settings.Default.Save();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (lidarFile != null)
                    lidarFile.Close();
                lidarFile = new LiDARFileStuff.LiDARFile(LiDARfName.Text);
                mePropertiesLiDARFile.Text = lidarFile.GetLiDARPrintableData(Points2Print.Value);
                if (lidarFile.HasFWFData)
                    meFullWavePoints.Text = lidarFile.GetFWFData((int)Points2Print.Value, SeparatorChar.Text[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error opening file {0}: {1}", LiDARfName.Text, ex.Message));
            }
        }

        private void Points2File_CheckedChanged(object sender, EventArgs e)
        {
            if (Points2File.Checked)
                CSVFileName.Text = Path.ChangeExtension(LiDARfName.Text, "csv");
            else
                CSVFileName.Text = string.Empty;
        }

        private void CSVFileName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ofd.Filter = "CSV files|*.csf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LiDARfName.Text = ofd.FileName;
            }

        }
    }
}
