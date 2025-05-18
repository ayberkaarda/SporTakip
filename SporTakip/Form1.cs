using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SporTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void ListeleSporcular()
        {
            lstSporcular.Items.Clear();

            string query = "SELECT Id, Ad, SporDali FROM Sporcular";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string ad = row["Ad"].ToString();
                string sporDali = row["SporDali"].ToString();

                lstSporcular.Items.Add($"{id} - {ad} - {sporDali}");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ListeleSporcular();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            string sporDali = txtSporDali.Text;

            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(sporDali))
            {
                MessageBox.Show("alanlar boş olmaz.");
                return;
            }

            string query = "INSERT INTO Sporcular (Ad, SporDali) VALUES (@ad, @sporDali)";
            DatabaseHelper.ExecuteNonQuery(query,
                new SqlParameter("@ad", ad),
                new SqlParameter("@sporDali", sporDali));

            MessageBox.Show("Sporcu sisteme kaydedildi");
            txtAd.Text = "";
            txtSporDali.Text = "";
            ListeleSporcular();
        }

        private void btnAntrenmanEkle_Click(object sender, EventArgs e)
        {
            new AntrenmanEkleForm().ShowDialog();
        }

        private void btnTakipEkle_Click(object sender, EventArgs e)
        {
            new TakipEkleForm().ShowDialog();
        }

        private void btnSporcuDetay_Click(object sender, EventArgs e)
        {
            new SporcuDetayForm().ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListeleSporcular();
            this.FormBorderStyle = FormBorderStyle.None;

            // Çıkış ve diğer düğmeleri gizle
            this.ControlBox = false;
            this.MaximizeBox = false; // Maksimize butonunu gizle
            this.MinimizeBox = false; // Minimize butonunu gizle
        }

        private void txtAd_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtAd_Leave(object sender, EventArgs e)
        {
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var selectedItem = lstSporcular.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Silmek için bir sporcu seçin.");
                return;
            }

            string selectedText = selectedItem.ToString();
            string[] parts = selectedText.Split(new[] { " - " }, StringSplitOptions.None);

            if (parts.Length < 3)
            {
                MessageBox.Show("Geçersiz veri formatı.");
                return;
            }

            int id = Convert.ToInt32(parts[0]);

            // Önce Takipler tablosundaki kayıtları sil
            string deleteTakipler = "DELETE FROM Takipler WHERE SporcuId = @id";
            DatabaseHelper.ExecuteNonQuery(deleteTakipler, new SqlParameter("@id", id));

            // Sonra Antrenmanlar tablosundaki kayıtları sil
            string deleteAntrenmanlar = "DELETE FROM Antrenmanlar WHERE SporcuId = @id";
            DatabaseHelper.ExecuteNonQuery(deleteAntrenmanlar, new SqlParameter("@id", id));

            // En son sporcuyu sil
            string deleteSporcu = "DELETE FROM Sporcular WHERE Id = @id";
            DatabaseHelper.ExecuteNonQuery(deleteSporcu, new SqlParameter("@id", id));

            MessageBox.Show("Sporcu ve tüm ilişkili kayıtlar silindi.");
            ListeleSporcular();
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
