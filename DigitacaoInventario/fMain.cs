using System;
using System.Data;
using System.Windows.Forms;

namespace DigitacaoInventario
{
    public partial class fPrincipal : Form
    {
        private int ultSubParcela = 0;
        private int GPS = 0;

      
        public fPrincipal()
        {
            InitializeComponent();
        }

        private void fPrincipal_Load(object sender, EventArgs e)
        {
            if(System.IO.File.Exists("Inventario.xml"))
                dsInventario.ReadXml("Inventario.xml");
            tp.SelectedPageIndex = 0;
            gvMetadados.Focus();
        }

        private void fPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            dsInventario.AcceptChanges();
            dsInventario.WriteXml("Inventario.xml", XmlWriteMode.WriteSchema);
        }
        void CalculaUTMArvore(ds.dtMetadadosRow md, ds.dtMedicoesRow me, ref double UTMX, ref double UTMY, ref double azimuteParcelaTrigonometrico)
        {
            if ((md.ItemArray[gvMetadados.Columns["ParcelaDeclinacao"].AbsoluteIndex] != DBNull.Value) &&
                (me.ItemArray[gvDados.Columns["Azimute"].AbsoluteIndex] != DBNull.Value) &&
                (me.ItemArray[gvDados.Columns["X"].AbsoluteIndex] != DBNull.Value) &&
                (me.ItemArray[gvDados.Columns["Y"].AbsoluteIndex] != DBNull.Value))
            {
                // Transforma o azimute em coordenada trigonométrica, corrige a declinação e transforma em radianos
                azimuteParcelaTrigonometrico = (((md.ParcelaAzimute > 90) ? 360 + (md.ParcelaAzimute * -1) + 90 : 360 + (md.ParcelaAzimute * -1) - 270) + (md.ParcelaDeclinacao * -1)) * Math.PI / 180;
                double azimuteArvoreTriginometrico = (((me.Azimute > 90) ? 360 + (me.Azimute * -1) + 90 : 360 + (me.Azimute * -1) - 270) + (md.ParcelaDeclinacao * -1)) * Math.PI / 180;
                // Calcula coordenada do ponto interseção entre o eixo da parcela e o alinhamento do TruPulse (azimute da árvore)
                double x1 = md.ParcelaUTMX + me.Y * Math.Cos(azimuteParcelaTrigonometrico);
                double y1 = md.ParcelaUTMY + me.Y * Math.Sin(azimuteParcelaTrigonometrico);
                UTMX = x1 + me.X * Math.Cos(azimuteArvoreTriginometrico);
                UTMY = y1 + me.X * Math.Sin(azimuteArvoreTriginometrico);
            }
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            ds.dtMetadadosRow md = (ds.dtMetadadosRow)((DataRowView)gvMetadados.GetRow(gvMetadados.FocusedRowHandle)).Row;
            ds.dtMedicoesRow me = (ds.dtMedicoesRow)((DataRowView)e.Row).Row;
            if ((me.DAP < me.dtEspecieRow.DAPMin))
                throw new Exception("DAP menor que o mínimo estabelecido para a espécie.");
            if (me.DAP > me.dtEspecieRow.DAPMax)
                throw new Exception("DAP maior que o máximo estabelecido para a espécie.");
            if ((me.Lado == "D") && ((me.Arvore % 2) != 0))
                throw new Exception("Árvores medidas do lado direito devem ser pares.");
            if ((me.Lado == "E") && ((me.Arvore % 2) == 0))
                throw new Exception("Árvores medidas do lado esquerdo devem ser ímpares.");
            if(me.Azimute > 360)
                throw new Exception("Ângulo maior que 360.");
            double UTMX = 0, UTMY = 0, azimuteParcelaTriginometrico = 0; // 
            CalculaUTMArvore(md, me, ref UTMX, ref UTMY, ref azimuteParcelaTriginometrico);
            double distanciaArvore = Math.Abs((Math.Tan(azimuteParcelaTriginometrico) * (UTMX - md.ParcelaUTMX)) - (UTMY - md.ParcelaUTMY))
                / Math.Sqrt(Math.Pow(Math.Tan(azimuteParcelaTriginometrico), 2) + 1);
            if (distanciaArvore > (md.ParcelaLargura / 2))
                throw new Exception("Árvore fora da parcela.");
            if (me.ItemArray[gvDados.Columns["GPS"].AbsoluteIndex] != DBNull.Value)
            {
                if ((GPS > 0) && (me.GPS > (GPS + 1)))
                    System.Windows.Forms.MessageBox.Show("Pulou pelo menos um número de ponto GPS.");
                GPS = me.GPS;
            }
            if (me.ItemArray[gvDados.Columns["SubParcela"].AbsoluteIndex] != DBNull.Value)
                ultSubParcela = me.SubParcela;
        }

        private void gvDados_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            ds.dtMetadadosRow md = (ds.dtMetadadosRow)((DataRowView)gvMetadados.GetRow(gvMetadados.FocusedRowHandle)).Row;
            ds.dtMedicoesRow me = (ds.dtMedicoesRow)((DataRowView)e.Row).Row;
            me.dtMetadadosRow = md;
            double UTMX = 0, UTMY = 0, dummyValue = 0; // Parâmetro não necessário aqui
            CalculaUTMArvore(md, me, ref UTMX, ref UTMY, ref dummyValue);
            /*
            if ((md.ItemArray[gvMetadados.Columns["ParcelaDeclinacao"].AbsoluteIndex] != DBNull.Value) && 
                (me.ItemArray[gvDados.Columns["Azimute"].AbsoluteIndex] != DBNull.Value) &&
                (me.ItemArray[gvDados.Columns["X"].AbsoluteIndex] != DBNull.Value) &&
                (me.ItemArray[gvDados.Columns["Y"].AbsoluteIndex] != DBNull.Value) &&
                (me.ItemArray[gvDados.Columns["Lado"].AbsoluteIndex] != DBNull.Value))
            {
                // Transforma o azimute em coordenada trigonométrica, corrige a declinação e transforma em radianos
                double azimuteParcelaTriginometrico = (((md.ParcelaAzimute > 90) ? 360 + (md.ParcelaAzimute * -1) + 90 : 360 + (md.ParcelaAzimute * -1) - 270) + (md.ParcelaDeclinacao * -1)) * Math.PI / 180;
                double azimuteArvoreTriginometrico = (((me.Azimute > 90) ? 360 + (me.Azimute * -1) + 90 : 360 + (me.Azimute * -1) - 270) + (md.ParcelaDeclinacao * -1)) * Math.PI / 180;
                // Calcula coordenada do ponto interseção entre o eixo da parcela e o alinhamento do TruPulse (azimute da árvore)
                double x1 = md.ParcelaUTMX + me.Y * Math.Cos(azimuteParcelaTriginometrico);
                double y1 = md.ParcelaUTMY + me.Y * Math.Sin(azimuteParcelaTriginometrico);
                */
            if ((e.Column.FieldName == "colUTMX") && (UTMX > 0))
            {
                e.Value = UTMX;
            }
            else
            if ((e.Column.FieldName == "colUTMY") && (UTMY > 0))
            {
                e.Value = UTMY;
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            ds.dtMetadadosRow md = (ds.dtMetadadosRow)((DataRowView)gvMetadados.GetRow(gvMetadados.FocusedRowHandle)).Row;
            sfd.FileName = string.Format("{0}_{1}_{2}_Medicao.xls", md.DataMedicao.ToString("yyyyMMdd"),
                Convert.ToUInt64(md.ParcelaUTMX), Convert.ToUInt64(md.ParcelaUTMY));
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Medicoes.ExportToXls(sfd.FileName);
                Metadados.ExportToXls(sfd.FileName.Replace("Medicao","Parcela"));
            }
            
        }

        private void bNovaMedicao_Click(object sender, EventArgs e)
        {
            if (gvMetadados.RowCount > 0)
                if (MessageBox.Show("Deseja criar uma nova medição? Os dados existentes serão apagados.", "Atenção", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dsInventario.dtMedicoes.Clear();
                    dsInventario.dtMetadados.Clear();
                }                
        }

        private void Metadados_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if ((e.KeyCode == Keys.Down) && (gvMetadados.FocusedRowHandle == (gvMetadados.RowCount - 1)))
            {
                ds.dtMetadadosRow md = (ds.dtMetadadosRow)((DataRowView)gvMetadados.GetRow(gvMetadados.FocusedRowHandle)).Row;
                gvMetadados.AddNewRow();
                gvMetadados.FocusedRowHandle = gvMetadados.RowCount - 1;
            }
            */
        }

        private void dtEspecieBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
