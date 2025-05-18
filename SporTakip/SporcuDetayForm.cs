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

namespace SporTakip
{
    public partial class SporcuDetayForm : Form
    {
        public SporcuDetayForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void SporcuDetayForm_Load(object sender, EventArgs e)
        {
            var dt = DatabaseHelper.ExecuteQuery("SELECT * FROM Sporcular");
            cmbSporcular.DataSource = dt;
            cmbSporcular.DisplayMember = "Ad";
            cmbSporcular.ValueMember = "Id";
            this.FormBorderStyle = FormBorderStyle.None;

            // Çıkış ve diğer düğmeleri gizle
            this.ControlBox = false;
            this.MaximizeBox = false; // Maksimize butonunu gizle
            this.MinimizeBox = false; // Minimize butonunu gizle
        }
        private void ListeleDetaylar()
        {
            if (cmbSporcular.SelectedValue == null) return;

            int sporcuId = Convert.ToInt32(cmbSporcular.SelectedValue); // seçili veriyi int 32 yapıyo
            lstAntrenmanlar.Items.Clear();
            lstTakipler.Items.Clear();

            // Antrenmanları getiriyo
            var antDt = DatabaseHelper.ExecuteQuery("SELECT * FROM Antrenmanlar WHERE SporcuId = @sid",
                new SqlParameter("@sid", sporcuId));
            foreach (DataRow row in antDt.Rows)
            {
                lstAntrenmanlar.Items.Add($"{row["AntrenmanAdi"]}: {row["Detaylar"]}");
            }

            // Takip kayıtlarını getiriyo
            var takipDt = DatabaseHelper.ExecuteQuery("SELECT * FROM Takipler WHERE SporcuId = @sid ORDER BY Tarih DESC",
                new SqlParameter("@sid", sporcuId));
            foreach (DataRow row in takipDt.Rows)
            {
                lstTakipler.Items.Add($"{row["Tarih"]}: {row["Aciklama"]}");
            }
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
