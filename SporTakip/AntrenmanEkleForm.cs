using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SporTakip
{
    public partial class AntrenmanEkleForm : BorderlessForm
    {
        private bool _isLoading;
        private readonly ErrorProvider _errors;

        public AntrenmanEkleForm()
        {
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        }

        private void AntrenmanEkleForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            try
            {
                comboSporcular.DisplayMember = "Ad";
                comboSporcular.ValueMember = "Id";
                comboSporcular.DataSource = DatabaseHelper.ExecuteQuery("SELECT Id, Ad FROM Sporcular");
            }
            catch (SqlException ex) { ErrorHandler.Show(ex); }
            finally { _isLoading = false; }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;
            _errors.Clear();
            bool valid = true;
            if (!(comboSporcular.SelectedValue is int))
            {
                _errors.SetError(comboSporcular, "Lütfen bir sporcu seçin.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtAntrenmanName.Text))
            {
                _errors.SetError(txtAntrenmanName, "Lütfen antrenman adını girin.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtDetaylar.Text))
            {
                _errors.SetError(txtDetaylar, "Lütfen antrenman detaylarını girin.");
                valid = false;
            }
            if (!valid) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO Antrenmanlar (SporcuId, AntrenmanAdi, Detaylar) VALUES (@sid, @adi, @detay)",
                    new SqlParameter("@sid", (int)comboSporcular.SelectedValue),
                    new SqlParameter("@adi", txtAntrenmanName.Text.Trim()),
                    new SqlParameter("@detay", txtDetaylar.Text.Trim()));
                MessageBox.Show("Antrenman kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAntrenmanName.Clear();
                txtDetaylar.Clear();
                txtAntrenmanName.Focus();
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

