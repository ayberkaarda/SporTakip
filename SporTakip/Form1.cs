using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SporTakip
{
    public partial class Form1 : BorderlessForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ListeleSporcular()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT Id, Ad, SporDali FROM Sporcular");
                dt.Columns.Add("Gorunum", typeof(string));
                foreach (DataRow row in dt.Rows)
                    row["Gorunum"] = string.Format("{0} - {1} - {2}", row["Id"], row["Ad"], row["SporDali"]);
                lstSporcular.DisplayMember = "Gorunum";
                lstSporcular.ValueMember = "Id";
                lstSporcular.DataSource = dt;
            }
            catch (SqlException ex) { ErrorHandler.Show(ex); }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();
            string sporDali = txtSporDali.Text.Trim();
            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(sporDali))
            {
                MessageBox.Show("Lütfen sporcu adını ve spor dalını girin.", "Eksik bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                DatabaseHelper.ExecuteNonQuery("INSERT INTO Sporcular (Ad, SporDali) VALUES (@ad, @sporDali)",
                    new SqlParameter("@ad", ad), new SqlParameter("@sporDali", sporDali));
                MessageBox.Show("Sporcu kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAd.Clear();
                txtSporDali.Clear();
                txtAd.Focus();
                ListeleSporcular();
            }
            catch (SqlException ex) { ErrorHandler.Show(ex); }
        }

        private void btnAntrenmanEkle_Click(object sender, EventArgs e)
        {
            using (var frm = new AntrenmanEkleForm()) { frm.ShowDialog(this); }
        }

        private void btnTakipEkle_Click(object sender, EventArgs e)
        {
            using (var frm = new TakipEkleForm()) { frm.ShowDialog(this); }
        }

        private void btnSporcuDetay_Click(object sender, EventArgs e)
        {
            using (var frm = new SporcuDetayForm()) { frm.ShowDialog(this); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListeleSporcular();

        }

        private void txtAd_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtAd_Leave(object sender, EventArgs e)
        {
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var selected = lstSporcular.SelectedItem as DataRowView;
            if (selected == null)
            {
                MessageBox.Show("Silmek için bir sporcu seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = Convert.ToInt32(selected["Id"]);
            string ad = Convert.ToString(selected["Ad"]);
            if (MessageBox.Show(string.Format("\"{0}\" ve ona ait tüm antrenman ve takip kayıtları silinecek. Devam edilsin mi?", ad),
                "Silme onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            try
            {
                DatabaseHelper.DeleteSporcu(id);
                MessageBox.Show("Sporcu ve tüm ilişkili kayıtlar silindi.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListeleSporcular();
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

