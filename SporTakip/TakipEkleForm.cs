using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace SporTakip
{
    public partial class TakipEkleForm : BorderlessForm
    {
        private bool _isLoading;
        private readonly ErrorProvider _errors;

        public TakipEkleForm()
        {
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        }

        private void TakipEkleForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            try
            {
                cmbSporcular.DisplayMember = "Ad";
                cmbSporcular.ValueMember = "Id";
                cmbSporcular.DataSource = DatabaseHelper.ExecuteQuery("SELECT Id, Ad FROM Sporcular");
            }
            catch (SqlException ex) { ErrorHandler.Show(ex); }
            finally { _isLoading = false; }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;
            _errors.Clear();
            bool valid = true;
            if (!(cmbSporcular.SelectedValue is int))
            {
                _errors.SetError(cmbSporcular, "Lütfen bir sporcu seçin.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                _errors.SetError(txtAciklama, "Lütfen takip açıklamasını girin.");
                valid = false;
            }
            if (!valid) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO Takipler (SporcuId, Tarih, Aciklama) VALUES (@sid, @tarih, @aciklama)",
                    new SqlParameter("@sid", (int)cmbSporcular.SelectedValue),
                    new SqlParameter("@tarih", SqlDbType.Date) { Value = dtTarih.Value.Date },
                    new SqlParameter("@aciklama", txtAciklama.Text.Trim()));
                MessageBox.Show("Takip kaydı eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAciklama.Clear();
                dtTarih.Value = DateTime.Today;
                txtAciklama.Focus();
            }
            catch (SqlException ex) { ErrorHandler.Show(ex); }
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

