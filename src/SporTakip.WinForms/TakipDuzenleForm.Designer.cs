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
            this.dtTarih.Location = new System.Drawing.Point(136, 56);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(300, 25);
            this.dtTarih.TabIndex = 0;
            //
            // txtAciklama
            //
            this.txtAciklama.Multiline = true;
            this.txtAciklama.AcceptsReturn = true;
            this.txtAciklama.Location = new System.Drawing.Point(136, 92);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAciklama.Size = new System.Drawing.Size(300, 70);
            this.txtAciklama.TabIndex = 1;
            //
            // btnKaydet
            //
            this.btnKaydet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(72)))), ((int)(((byte)(170)))));
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(136, 384);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(145, 36);
            this.btnKaydet.TabIndex = 10;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // btnIptal
            //
            this.btnIptal.BackColor = System.Drawing.Color.White;
            this.btnIptal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnIptal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.btnIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIptal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnIptal.Location = new System.Drawing.Point(291, 384);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(145, 36);
            this.btnIptal.TabIndex = 11;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = false;
            //
            // btnkapat
            //
            this.btnkapat.BackColor = System.Drawing.Color.Transparent;
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnkapat.ForeColor = System.Drawing.Color.White;
            this.btnkapat.Location = new System.Drawing.Point(424, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 12;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.UseVisualStyleBackColor = false;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // lblTarih
            //
            this.lblTarih.BackColor = System.Drawing.Color.Transparent;
            this.lblTarih.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTarih.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblTarih.Location = new System.Drawing.Point(20, 56);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(110, 25);
            this.lblTarih.TabIndex = 5;
            this.lblTarih.Text = "Tarih";
            this.lblTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAciklama
            //
            this.lblAciklama.BackColor = System.Drawing.Color.Transparent;
            this.lblAciklama.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAciklama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblAciklama.Location = new System.Drawing.Point(20, 92);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(110, 25);
            this.lblAciklama.TabIndex = 6;
            this.lblAciklama.Text = "Açıklama";
            this.lblAciklama.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(150, 25);
            this.lblBaslik.TabIndex = 7;
            this.lblBaslik.Text = "Takibi Düzenle";
            //
            // lblOlcumler
            //
            this.lblOlcumler.BackColor = System.Drawing.Color.Transparent;
            this.lblOlcumler.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOlcumler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblOlcumler.Location = new System.Drawing.Point(20, 176);
            this.lblOlcumler.Name = "lblOlcumler";
            this.lblOlcumler.Size = new System.Drawing.Size(110, 25);
            this.lblOlcumler.TabIndex = 13;
            this.lblOlcumler.Text = "Ölçümler";
            this.lblOlcumler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvOlcumler
            //
            this.dgvOlcumler.Location = new System.Drawing.Point(136, 176);
            this.dgvOlcumler.Name = "dgvOlcumler";
            this.dgvOlcumler.Size = new System.Drawing.Size(300, 102);
            this.dgvOlcumler.TabIndex = 3;
            this.dgvOlcumler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOlcumler_KeyDown);
            //
            // lblOlcumGiris
            //
            this.lblOlcumGiris.BackColor = System.Drawing.Color.Transparent;
            this.lblOlcumGiris.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOlcumGiris.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblOlcumGiris.Location = new System.Drawing.Point(20, 288);
            this.lblOlcumGiris.Name = "lblOlcumGiris";
            this.lblOlcumGiris.Size = new System.Drawing.Size(110, 25);
            this.lblOlcumGiris.TabIndex = 14;
            this.lblOlcumGiris.Text = "Ölçüm bilgisi";
            this.lblOlcumGiris.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbOlcumTur
            //
            this.cmbOlcumTur.FormattingEnabled = true;
            this.cmbOlcumTur.Location = new System.Drawing.Point(136, 288);
            this.cmbOlcumTur.MaxLength = 50;
            this.cmbOlcumTur.Name = "cmbOlcumTur";
            this.cmbOlcumTur.Size = new System.Drawing.Size(120, 25);
            this.cmbOlcumTur.TabIndex = 4;
            //
            // txtOlcumDeger
            //
            this.txtOlcumDeger.Location = new System.Drawing.Point(261, 288);
            this.txtOlcumDeger.MaxLength = 20;
            this.txtOlcumDeger.Name = "txtOlcumDeger";
            this.txtOlcumDeger.Size = new System.Drawing.Size(80, 25);
            this.txtOlcumDeger.TabIndex = 5;
            this.txtOlcumDeger.PlaceholderText = "Değer";
            //
            // cmbOlcumBirim
            //
            this.cmbOlcumBirim.FormattingEnabled = true;
            this.cmbOlcumBirim.Location = new System.Drawing.Point(346, 288);
            this.cmbOlcumBirim.MaxLength = 20;
            this.cmbOlcumBirim.Name = "cmbOlcumBirim";
            this.cmbOlcumBirim.Size = new System.Drawing.Size(90, 25);
            this.cmbOlcumBirim.TabIndex = 6;
            //
            // btnOlcumEkle
            //
            this.btnOlcumEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnOlcumEkle.FlatAppearance.BorderSize = 0;
            this.btnOlcumEkle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(72)))), ((int)(((byte)(170)))));
            this.btnOlcumEkle.ForeColor = System.Drawing.Color.White;
            this.btnOlcumEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOlcumEkle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOlcumEkle.Location = new System.Drawing.Point(136, 322);
            this.btnOlcumEkle.Name = "btnOlcumEkle";
            this.btnOlcumEkle.Size = new System.Drawing.Size(96, 28);
            this.btnOlcumEkle.TabIndex = 7;
            this.btnOlcumEkle.Text = "Ekle";
            this.btnOlcumEkle.UseVisualStyleBackColor = false;
            this.btnOlcumEkle.Click += new System.EventHandler(this.btnOlcumEkle_Click);
            //
            // btnOlcumGuncelle
            //
            this.btnOlcumGuncelle.BackColor = System.Drawing.Color.White;
            this.btnOlcumGuncelle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnOlcumGuncelle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.btnOlcumGuncelle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnOlcumGuncelle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumGuncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOlcumGuncelle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOlcumGuncelle.Location = new System.Drawing.Point(238, 322);
            this.btnOlcumGuncelle.Name = "btnOlcumGuncelle";
            this.btnOlcumGuncelle.Size = new System.Drawing.Size(96, 28);
            this.btnOlcumGuncelle.TabIndex = 8;
            this.btnOlcumGuncelle.Text = "Güncelle";
            this.btnOlcumGuncelle.UseVisualStyleBackColor = false;
            this.btnOlcumGuncelle.Click += new System.EventHandler(this.btnOlcumGuncelle_Click);
            //
            // btnOlcumSil
            //
            this.btnOlcumSil.BackColor = System.Drawing.Color.White;
            this.btnOlcumSil.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.btnOlcumSil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.btnOlcumSil.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.btnOlcumSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOlcumSil.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOlcumSil.Location = new System.Drawing.Point(340, 322);
            this.btnOlcumSil.Name = "btnOlcumSil";
            this.btnOlcumSil.Size = new System.Drawing.Size(96, 28);
            this.btnOlcumSil.TabIndex = 9;
            this.btnOlcumSil.Text = "Sil";
            this.btnOlcumSil.UseVisualStyleBackColor = false;
            this.btnOlcumSil.Click += new System.EventHandler(this.btnOlcumSil_Click);
            //
            // TakipDuzenleForm
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(470, 442);
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
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
