namespace SporTakip
{
    partial class TakipDuzenleForm
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
            this.dtTarih = new System.Windows.Forms.DateTimePicker();
            this.lblTarih = new System.Windows.Forms.Label();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.lblAciklama = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblOlcumler = new System.Windows.Forms.Label();
            this.dgvOlcumler = new System.Windows.Forms.DataGridView();
            this.lblOlcumGiris = new System.Windows.Forms.Label();
            this.cmbOlcumTur = new System.Windows.Forms.ComboBox();
            this.txtOlcumDeger = new System.Windows.Forms.TextBox();
            this.cmbOlcumBirim = new System.Windows.Forms.ComboBox();
            this.btnOlcumEkle = new System.Windows.Forms.Button();
            this.btnOlcumGuncelle = new System.Windows.Forms.Button();
            this.btnOlcumSil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOlcumler)).BeginInit();
            this.SuspendLayout();
            //
            // dtTarih
            //
            this.dtTarih.Location = new System.Drawing.Point(140, 68);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(294, 25);
            this.dtTarih.TabIndex = 0;
            //
            // txtAciklama
            //
            this.txtAciklama.Multiline = true;
            this.txtAciklama.AcceptsReturn = true;
            this.txtAciklama.Location = new System.Drawing.Point(140, 104);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAciklama.Size = new System.Drawing.Size(294, 70);
            this.txtAciklama.TabIndex = 1;
            //
            // btnKaydet
            //
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Location = new System.Drawing.Point(140, 392);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(142, 36);
            this.btnKaydet.TabIndex = 9;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // btnIptal
            //
            this.btnIptal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.Location = new System.Drawing.Point(292, 392);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(142, 36);
            this.btnIptal.TabIndex = 10;
            this.btnIptal.Text = "İptal";
            //
            // btnkapat
            //
            this.btnkapat.Location = new System.Drawing.Point(424, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 11;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // lblTarih
            //
            this.lblTarih.Location = new System.Drawing.Point(36, 68);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(100, 25);
            this.lblTarih.TabIndex = 12;
            this.lblTarih.Text = "Tarih";
            this.lblTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAciklama
            //
            this.lblAciklama.Location = new System.Drawing.Point(36, 104);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(100, 25);
            this.lblAciklama.TabIndex = 13;
            this.lblAciklama.Text = "Açıklama";
            this.lblAciklama.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(150, 25);
            this.lblBaslik.TabIndex = 16;
            this.lblBaslik.Text = "Takibi Düzenle";
            //
            // lblOlcumler
            //
            this.lblOlcumler.Location = new System.Drawing.Point(36, 188);
            this.lblOlcumler.Name = "lblOlcumler";
            this.lblOlcumler.Size = new System.Drawing.Size(100, 25);
            this.lblOlcumler.TabIndex = 14;
            this.lblOlcumler.Text = "Ölçümler";
            this.lblOlcumler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvOlcumler
            //
            this.dgvOlcumler.Location = new System.Drawing.Point(140, 188);
            this.dgvOlcumler.Name = "dgvOlcumler";
            this.dgvOlcumler.Size = new System.Drawing.Size(294, 102);
            this.dgvOlcumler.TabIndex = 2;
            this.dgvOlcumler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOlcumler_KeyDown);
            //
            // lblOlcumGiris
            //
            this.lblOlcumGiris.Location = new System.Drawing.Point(36, 300);
            this.lblOlcumGiris.Name = "lblOlcumGiris";
            this.lblOlcumGiris.Size = new System.Drawing.Size(100, 25);
            this.lblOlcumGiris.TabIndex = 15;
            this.lblOlcumGiris.Text = "Ölçüm bilgisi";
            this.lblOlcumGiris.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbOlcumTur
            //
            this.cmbOlcumTur.FormattingEnabled = true;
            this.cmbOlcumTur.Location = new System.Drawing.Point(140, 300);
            this.cmbOlcumTur.MaxLength = 50;
            this.cmbOlcumTur.Name = "cmbOlcumTur";
            this.cmbOlcumTur.Size = new System.Drawing.Size(118, 25);
            this.cmbOlcumTur.TabIndex = 3;
            //
            // txtOlcumDeger
            //
            this.txtOlcumDeger.Location = new System.Drawing.Point(263, 300);
            this.txtOlcumDeger.MaxLength = 20;
            this.txtOlcumDeger.Name = "txtOlcumDeger";
            this.txtOlcumDeger.Size = new System.Drawing.Size(80, 25);
            this.txtOlcumDeger.TabIndex = 4;
            this.txtOlcumDeger.PlaceholderText = "Değer";
            //
            // cmbOlcumBirim
            //
            this.cmbOlcumBirim.FormattingEnabled = true;
            this.cmbOlcumBirim.Location = new System.Drawing.Point(348, 300);
            this.cmbOlcumBirim.MaxLength = 20;
            this.cmbOlcumBirim.Name = "cmbOlcumBirim";
            this.cmbOlcumBirim.Size = new System.Drawing.Size(86, 25);
            this.cmbOlcumBirim.TabIndex = 5;
            //
            // btnOlcumEkle
            //
            this.btnOlcumEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumEkle.Location = new System.Drawing.Point(140, 334);
            this.btnOlcumEkle.Name = "btnOlcumEkle";
            this.btnOlcumEkle.Size = new System.Drawing.Size(94, 30);
            this.btnOlcumEkle.TabIndex = 6;
            this.btnOlcumEkle.Text = "Ekle";
            this.btnOlcumEkle.Click += new System.EventHandler(this.btnOlcumEkle_Click);
            //
            // btnOlcumGuncelle
            //
            this.btnOlcumGuncelle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumGuncelle.Location = new System.Drawing.Point(240, 334);
            this.btnOlcumGuncelle.Name = "btnOlcumGuncelle";
            this.btnOlcumGuncelle.Size = new System.Drawing.Size(94, 30);
            this.btnOlcumGuncelle.TabIndex = 7;
            this.btnOlcumGuncelle.Text = "Güncelle";
            this.btnOlcumGuncelle.Click += new System.EventHandler(this.btnOlcumGuncelle_Click);
            //
            // btnOlcumSil
            //
            this.btnOlcumSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumSil.Location = new System.Drawing.Point(340, 334);
            this.btnOlcumSil.Name = "btnOlcumSil";
            this.btnOlcumSil.Size = new System.Drawing.Size(94, 30);
            this.btnOlcumSil.TabIndex = 8;
            this.btnOlcumSil.Text = "Sil";
            this.btnOlcumSil.Click += new System.EventHandler(this.btnOlcumSil_Click);
            //
            // TakipDuzenleForm
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(470, 464);
            this.Controls.Add(this.btnOlcumSil);
            this.Controls.Add(this.btnOlcumGuncelle);
            this.Controls.Add(this.btnOlcumEkle);
            this.Controls.Add(this.cmbOlcumBirim);
            this.Controls.Add(this.txtOlcumDeger);
            this.Controls.Add(this.cmbOlcumTur);
            this.Controls.Add(this.lblOlcumGiris);
            this.Controls.Add(this.dgvOlcumler);
            this.Controls.Add(this.lblOlcumler);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.dtTarih);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TakipDuzenleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spor Istanbul | Takibi Düzenle";
            this.Load += new System.EventHandler(this.TakipDuzenleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOlcumler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtTarih;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Label lblAciklama;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblOlcumler;
        private System.Windows.Forms.DataGridView dgvOlcumler;
        private System.Windows.Forms.Label lblOlcumGiris;
        private System.Windows.Forms.ComboBox cmbOlcumTur;
        private System.Windows.Forms.TextBox txtOlcumDeger;
        private System.Windows.Forms.ComboBox cmbOlcumBirim;
        private System.Windows.Forms.Button btnOlcumEkle;
        private System.Windows.Forms.Button btnOlcumGuncelle;
        private System.Windows.Forms.Button btnOlcumSil;
    }
}
