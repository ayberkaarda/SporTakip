namespace SporTakip
{
    partial class TakipEkleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TakipEkleForm));
            this.cmbSporcular = new System.Windows.Forms.ComboBox();
            this.dtTarih = new System.Windows.Forms.DateTimePicker();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.btnAsagial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSporcu = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblOlcumBaslik = new System.Windows.Forms.Label();
            this.lblOlcumTur = new System.Windows.Forms.Label();
            this.cmbOlcumTur = new System.Windows.Forms.ComboBox();
            this.lblOlcumDeger = new System.Windows.Forms.Label();
            this.txtOlcumDeger = new System.Windows.Forms.TextBox();
            this.lblOlcumBirim = new System.Windows.Forms.Label();
            this.cmbOlcumBirim = new System.Windows.Forms.ComboBox();
            this.btnOlcumEkle = new System.Windows.Forms.Button();
            this.btnOlcumCikar = new System.Windows.Forms.Button();
            this.dgvOlcumler = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOlcumler)).BeginInit();
            this.SuspendLayout();
            //
            // cmbSporcular
            //
            this.cmbSporcular.FormattingEnabled = true;
            this.cmbSporcular.Location = new System.Drawing.Point(108, 76);
            this.cmbSporcular.Name = "cmbSporcular";
            this.cmbSporcular.Size = new System.Drawing.Size(176, 25);
            this.cmbSporcular.TabIndex = 0;
            this.cmbSporcular.Text = "Sporcu seç";
            //
            // dtTarih
            //
            this.dtTarih.Location = new System.Drawing.Point(108, 112);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(176, 25);
            this.dtTarih.TabIndex = 1;
            //
            // txtAciklama
            //
            this.txtAciklama.Location = new System.Drawing.Point(108, 148);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(176, 25);
            this.txtAciklama.TabIndex = 2;
            //
            // btnKaydet
            //
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Location = new System.Drawing.Point(108, 192);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(176, 36);
            this.btnKaydet.TabIndex = 9;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // btnkapat
            //
            this.btnkapat.Location = new System.Drawing.Point(573, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 11;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // btnAsagial
            //
            this.btnAsagial.Location = new System.Drawing.Point(527, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(46, 32);
            this.btnAsagial.TabIndex = 10;
            this.btnAsagial.TabStop = false;
            this.btnAsagial.Text = "–";
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(36, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Açıklama";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSporcu
            //
            this.lblSporcu.Location = new System.Drawing.Point(36, 76);
            this.lblSporcu.Name = "lblSporcu";
            this.lblSporcu.Size = new System.Drawing.Size(68, 25);
            this.lblSporcu.TabIndex = 17;
            this.lblSporcu.Text = "Sporcu";
            this.lblSporcu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTarih
            //
            this.lblTarih.Location = new System.Drawing.Point(36, 112);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(68, 25);
            this.lblTarih.TabIndex = 18;
            this.lblTarih.Text = "Tarih";
            this.lblTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(94, 25);
            this.lblBaslik.TabIndex = 16;
            this.lblBaslik.Text = "Takip Ekle";
            //
            // lblOlcumBaslik
            //
            this.lblOlcumBaslik.AutoSize = true;
            this.lblOlcumBaslik.Location = new System.Drawing.Point(334, 76);
            this.lblOlcumBaslik.Name = "lblOlcumBaslik";
            this.lblOlcumBaslik.Size = new System.Drawing.Size(170, 20);
            this.lblOlcumBaslik.TabIndex = 12;
            this.lblOlcumBaslik.Text = "Ölçümler (isteğe bağlı)";
            //
            // lblOlcumTur
            //
            this.lblOlcumTur.Location = new System.Drawing.Point(336, 108);
            this.lblOlcumTur.Name = "lblOlcumTur";
            this.lblOlcumTur.Size = new System.Drawing.Size(60, 25);
            this.lblOlcumTur.TabIndex = 13;
            this.lblOlcumTur.Text = "Tür";
            this.lblOlcumTur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbOlcumTur
            //
            this.cmbOlcumTur.FormattingEnabled = true;
            this.cmbOlcumTur.Location = new System.Drawing.Point(400, 108);
            this.cmbOlcumTur.MaxLength = 50;
            this.cmbOlcumTur.Name = "cmbOlcumTur";
            this.cmbOlcumTur.Size = new System.Drawing.Size(183, 25);
            this.cmbOlcumTur.TabIndex = 3;
            //
            // lblOlcumDeger
            //
            this.lblOlcumDeger.Location = new System.Drawing.Point(336, 142);
            this.lblOlcumDeger.Name = "lblOlcumDeger";
            this.lblOlcumDeger.Size = new System.Drawing.Size(60, 25);
            this.lblOlcumDeger.TabIndex = 14;
            this.lblOlcumDeger.Text = "Değer";
            this.lblOlcumDeger.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtOlcumDeger
            //
            this.txtOlcumDeger.Location = new System.Drawing.Point(400, 142);
            this.txtOlcumDeger.MaxLength = 20;
            this.txtOlcumDeger.Name = "txtOlcumDeger";
            this.txtOlcumDeger.Size = new System.Drawing.Size(183, 25);
            this.txtOlcumDeger.TabIndex = 4;
            //
            // lblOlcumBirim
            //
            this.lblOlcumBirim.Location = new System.Drawing.Point(336, 176);
            this.lblOlcumBirim.Name = "lblOlcumBirim";
            this.lblOlcumBirim.Size = new System.Drawing.Size(60, 25);
            this.lblOlcumBirim.TabIndex = 15;
            this.lblOlcumBirim.Text = "Birim";
            this.lblOlcumBirim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbOlcumBirim
            //
            this.cmbOlcumBirim.FormattingEnabled = true;
            this.cmbOlcumBirim.Location = new System.Drawing.Point(400, 176);
            this.cmbOlcumBirim.MaxLength = 20;
            this.cmbOlcumBirim.Name = "cmbOlcumBirim";
            this.cmbOlcumBirim.Size = new System.Drawing.Size(183, 25);
            this.cmbOlcumBirim.TabIndex = 5;
            //
            // btnOlcumEkle
            //
            this.btnOlcumEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumEkle.Location = new System.Drawing.Point(400, 210);
            this.btnOlcumEkle.Name = "btnOlcumEkle";
            this.btnOlcumEkle.Size = new System.Drawing.Size(104, 30);
            this.btnOlcumEkle.TabIndex = 6;
            this.btnOlcumEkle.Text = "Ölçüm Ekle";
            this.btnOlcumEkle.Click += new System.EventHandler(this.btnOlcumEkle_Click);
            //
            // btnOlcumCikar
            //
            this.btnOlcumCikar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumCikar.Location = new System.Drawing.Point(510, 210);
            this.btnOlcumCikar.Name = "btnOlcumCikar";
            this.btnOlcumCikar.Size = new System.Drawing.Size(73, 30);
            this.btnOlcumCikar.TabIndex = 7;
            this.btnOlcumCikar.Text = "Çıkar";
            this.btnOlcumCikar.Click += new System.EventHandler(this.btnOlcumCikar_Click);
            //
            // dgvOlcumler
            //
            this.dgvOlcumler.Location = new System.Drawing.Point(336, 250);
            this.dgvOlcumler.Name = "dgvOlcumler";
            this.dgvOlcumler.Size = new System.Drawing.Size(247, 142);
            this.dgvOlcumler.TabIndex = 8;
            this.dgvOlcumler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOlcumler_KeyDown);
            //
            // TakipEkleForm
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnkapat;
            this.ClientSize = new System.Drawing.Size(619, 428);
            this.Controls.Add(this.dgvOlcumler);
            this.Controls.Add(this.btnOlcumCikar);
            this.Controls.Add(this.btnOlcumEkle);
            this.Controls.Add(this.cmbOlcumBirim);
            this.Controls.Add(this.lblOlcumBirim);
            this.Controls.Add(this.txtOlcumDeger);
            this.Controls.Add(this.lblOlcumDeger);
            this.Controls.Add(this.cmbOlcumTur);
            this.Controls.Add(this.lblOlcumTur);
            this.Controls.Add(this.lblOlcumBaslik);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.lblSporcu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.dtTarih);
            this.Controls.Add(this.cmbSporcular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TakipEkleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spor Istanbul | Takip Ekle";
            this.Load += new System.EventHandler(this.TakipEkleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOlcumler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSporcular;
        private System.Windows.Forms.DateTimePicker dtTarih;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Button btnAsagial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSporcu;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblOlcumBaslik;
        private System.Windows.Forms.Label lblOlcumTur;
        private System.Windows.Forms.ComboBox cmbOlcumTur;
        private System.Windows.Forms.Label lblOlcumDeger;
        private System.Windows.Forms.TextBox txtOlcumDeger;
        private System.Windows.Forms.Label lblOlcumBirim;
        private System.Windows.Forms.ComboBox cmbOlcumBirim;
        private System.Windows.Forms.Button btnOlcumEkle;
        private System.Windows.Forms.Button btnOlcumCikar;
        private System.Windows.Forms.DataGridView dgvOlcumler;
    }
}
