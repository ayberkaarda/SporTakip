using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SporTakip
{
    public partial class SporcuDetayForm : BorderlessForm
    {
        private bool _isLoading;

        public SporcuDetayForm()
        {
            InitializeComponent();
        }

        private void SporcuDetayForm_Load(object sender, EventArgs e)
        {
            cmbSporcular.SelectedIndexChanged -= cmbSporcular_SelectedIndexChanged;
            cmbSporcular.SelectedIndexChanged += cmbSporcular_SelectedIndexChanged;
            _isLoading = true;
            try
            {
                cmbSporcular.DisplayMember = "Ad";
                cmbSporcular.ValueMember = "Id";
                cmbSporcular.DataSource = DatabaseHelper.ExecuteQuery("SELECT Id, Ad FROM Sporcular");
            }
            catch (SqlException ex) { ErrorHandler.Show(ex); }
            finally { _isLoading = false; }
            ListeleDetaylar();

        }
        private void ListeleDetaylar()
        {
            if (_isLoading) return;
            lstAntrenmanlar.Items.Clear();
            lstTakipler.Items.Clear();
            if (!(cmbSporcular.SelectedValue is int)) return;
            int sporcuId = (int)cmbSporcular.SelectedValue;
            try
            {
                var antDt = DatabaseHelper.ExecuteQuery(
                    "SELECT AntrenmanAdi, Detaylar FROM Antrenmanlar WHERE SporcuId = @sid",
                    new SqlParameter("@sid", sporcuId));
                var takipDt = DatabaseHelper.ExecuteQuery(
                    "SELECT Tarih, Aciklama FROM Takipler WHERE SporcuId = @sid ORDER BY Tarih DESC",
                    new SqlParameter("@sid", sporcuId));
                foreach (DataRow row in antDt.Rows)
                    lstAntrenmanlar.Items.Add($"{row["AntrenmanAdi"]}: {row["Detaylar"]}");
                foreach (DataRow row in takipDt.Rows)
                    lstTakipler.Items.Add($"{row["Tarih"]}: {row["Aciklama"]}");
            }
            catch (SqlException ex) { ErrorHandler.Show(ex); }
        }

        private void cmbSporcular_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeleDetaylar();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            ListeleDetaylar();
        }

        private void btnkapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAsagial_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

