namespace SporTakip
{
    partial class SporcuDetayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SporcuDetayForm));
            this.cmbSporcular = new System.Windows.Forms.ComboBox();
            this.btnGuncelle = new System.Windows.Forms.Button();
            System.Windows.Forms.DataGridViewCellStyle antrenmanBaslikStili = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle antrenmanHucreStili = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle takipBaslikStili = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle takipHucreStili = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle takipTarihStili = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAntrenmanlar = new System.Windows.Forms.DataGridView();
            this.colAntrenmanAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetaylar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTakipler = new System.Windows.Forms.DataGridView();
            this.colTarih = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAciklama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOlcumler = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGrafik = new System.Windows.Forms.Button();
            this.btnCsvAktar = new System.Windows.Forms.Button();
            this.btnHaftalikProgram = new System.Windows.Forms.Button();
            this.btnAntrenmanDuzenle = new System.Windows.Forms.Button();
            this.btnAntrenmanSil = new System.Windows.Forms.Button();
            this.btnTakipDuzenle = new System.Windows.Forms.Button();
            this.btnTakipSil = new System.Windows.Forms.Button();
            this.lblAntrenman = new System.Windows.Forms.Label();
            this.lblTakip = new System.Windows.Forms.Label();
            this.btnkapat = new System.Windows.Forms.Button();
            this.btnAsagial = new System.Windows.Forms.Button();
            this.lblSporcu = new System.Windows.Forms.Label();
            this.lblBaslik = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAntrenmanlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTakipler)).BeginInit();
            this.SuspendLayout();
            //
            // cmbSporcular
            //
            this.cmbSporcular.FormattingEnabled = true;
            this.cmbSporcular.Location = new System.Drawing.Point(36, 86);
            this.cmbSporcular.Name = "cmbSporcular";
            this.cmbSporcular.Size = new System.Drawing.Size(220, 25);
            this.cmbSporcular.TabIndex = 0;
            this.cmbSporcular.Text = "Sporcu seç";
            //
            // btnGuncelle
            //
            this.btnGuncelle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuncelle.Location = new System.Drawing.Point(36, 120);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(220, 32);
            this.btnGuncelle.TabIndex = 1;
            this.btnGuncelle.Text = "Listeyi Güncelle";
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            //
            // dgvAntrenmanlar
            //
            this.dgvAntrenmanlar.AllowUserToAddRows = false;
            this.dgvAntrenmanlar.AllowUserToDeleteRows = false;
            this.dgvAntrenmanlar.AllowUserToResizeRows = false;
            this.dgvAntrenmanlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAntrenmanlar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvAntrenmanlar.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAntrenmanlar.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            antrenmanBaslikStili.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            antrenmanBaslikStili.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAntrenmanlar.ColumnHeadersDefaultCellStyle = antrenmanBaslikStili;
            this.dgvAntrenmanlar.ColumnHeadersHeight = 28;
            this.dgvAntrenmanlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAntrenmanlar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAntrenmanAdi,
            this.colDetaylar});
            antrenmanHucreStili.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAntrenmanlar.DefaultCellStyle = antrenmanHucreStili;
            this.dgvAntrenmanlar.EnableHeadersVisualStyles = false;
            this.dgvAntrenmanlar.Location = new System.Drawing.Point(36, 240);
            this.dgvAntrenmanlar.MultiSelect = false;
            this.dgvAntrenmanlar.Name = "dgvAntrenmanlar";
            this.dgvAntrenmanlar.ReadOnly = true;
            this.dgvAntrenmanlar.RowHeadersVisible = false;
            this.dgvAntrenmanlar.RowTemplate.Height = 24;
            this.dgvAntrenmanlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAntrenmanlar.Size = new System.Drawing.Size(471, 104);
            this.dgvAntrenmanlar.StandardTab = true;
            this.dgvAntrenmanlar.TabIndex = 7;
            this.dgvAntrenmanlar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAntrenmanlar_CellDoubleClick);
            this.dgvAntrenmanlar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAntrenmanlar_KeyDown);
            //
            // colAntrenmanAdi
            //
            this.colAntrenmanAdi.DataPropertyName = "AntrenmanAdi";
            this.colAntrenmanAdi.FillWeight = 40F;
            this.colAntrenmanAdi.HeaderText = "Antrenman";
            this.colAntrenmanAdi.MinimumWidth = 80;
            this.colAntrenmanAdi.Name = "colAntrenmanAdi";
            this.colAntrenmanAdi.ReadOnly = true;
            //
            // colDetaylar
            //
            this.colDetaylar.DataPropertyName = "Detaylar";
            this.colDetaylar.FillWeight = 60F;
            this.colDetaylar.HeaderText = "Detaylar";
            this.colDetaylar.MinimumWidth = 80;
            this.colDetaylar.Name = "colDetaylar";
            this.colDetaylar.ReadOnly = true;
            //
            // dgvTakipler
            //
            this.dgvTakipler.AllowUserToAddRows = false;
            this.dgvTakipler.AllowUserToDeleteRows = false;
            this.dgvTakipler.AllowUserToResizeRows = false;
            this.dgvTakipler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTakipler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvTakipler.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTakipler.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            takipBaslikStili.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            takipBaslikStili.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTakipler.ColumnHeadersDefaultCellStyle = takipBaslikStili;
            this.dgvTakipler.ColumnHeadersHeight = 28;
            this.dgvTakipler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTakipler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTarih,
            this.colAciklama,
            this.colOlcumler});
            takipHucreStili.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTakipler.DefaultCellStyle = takipHucreStili;
            this.dgvTakipler.EnableHeadersVisualStyles = false;
            this.dgvTakipler.Location = new System.Drawing.Point(36, 428);
            this.dgvTakipler.MultiSelect = false;
            this.dgvTakipler.Name = "dgvTakipler";
            this.dgvTakipler.ReadOnly = true;
            this.dgvTakipler.RowHeadersVisible = false;
            this.dgvTakipler.RowTemplate.Height = 24;
            this.dgvTakipler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTakipler.Size = new System.Drawing.Size(471, 104);
            this.dgvTakipler.StandardTab = true;
            this.dgvTakipler.TabIndex = 10;
            this.dgvTakipler.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTakipler_CellDoubleClick);
            this.dgvTakipler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTakipler_KeyDown);
            //
            // colTarih
            //
            this.colTarih.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTarih.DataPropertyName = "Tarih";
            takipTarihStili.Format = "d";
            this.colTarih.DefaultCellStyle = takipTarihStili;
            this.colTarih.HeaderText = "Tarih";
            this.colTarih.MinimumWidth = 80;
            this.colTarih.Name = "colTarih";
            this.colTarih.ReadOnly = true;
            this.colTarih.Width = 100;
            //
            // colAciklama
            //
            this.colAciklama.DataPropertyName = "Aciklama";
            this.colAciklama.FillWeight = 55F;
            this.colAciklama.HeaderText = "Açıklama";
            this.colAciklama.MinimumWidth = 80;
            this.colAciklama.Name = "colAciklama";
            this.colAciklama.ReadOnly = true;
            //
            // colOlcumler
            //
            this.colOlcumler.FillWeight = 45F;
            this.colOlcumler.HeaderText = "Ölçümler";
            this.colOlcumler.MinimumWidth = 80;
            this.colOlcumler.Name = "colOlcumler";
            this.colOlcumler.ReadOnly = true;
            //
            // btnGrafik
            //
            this.btnGrafik.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGrafik.Location = new System.Drawing.Point(300, 120);
            this.btnGrafik.Name = "btnGrafik";
            this.btnGrafik.Size = new System.Drawing.Size(101, 32);
            this.btnGrafik.TabIndex = 3;
            this.btnGrafik.Text = "Grafik";
            this.btnGrafik.Click += new System.EventHandler(this.btnGrafik_Click);
            //
            // btnCsvAktar
            //
            this.btnCsvAktar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCsvAktar.Location = new System.Drawing.Point(406, 120);
            this.btnCsvAktar.Name = "btnCsvAktar";
            this.btnCsvAktar.Size = new System.Drawing.Size(101, 32);
            this.btnCsvAktar.TabIndex = 4;
            this.btnCsvAktar.Text = "CSV Aktar";
            this.btnCsvAktar.Click += new System.EventHandler(this.btnCsvAktar_Click);
            //
            // btnHaftalikProgram
            //
            this.btnHaftalikProgram.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHaftalikProgram.Location = new System.Drawing.Point(300, 84);
            this.btnHaftalikProgram.Name = "btnHaftalikProgram";
            this.btnHaftalikProgram.Size = new System.Drawing.Size(207, 30);
            this.btnHaftalikProgram.TabIndex = 2;
            this.btnHaftalikProgram.Text = "Haftalık Program";
            this.btnHaftalikProgram.Click += new System.EventHandler(this.btnHaftalikProgram_Click);
            //
            // btnAntrenmanDuzenle
            //
            this.btnAntrenmanDuzenle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAntrenmanDuzenle.Location = new System.Drawing.Point(325, 204);
            this.btnAntrenmanDuzenle.Name = "btnAntrenmanDuzenle";
            this.btnAntrenmanDuzenle.Size = new System.Drawing.Size(88, 28);
            this.btnAntrenmanDuzenle.TabIndex = 5;
            this.btnAntrenmanDuzenle.Text = "Düzenle";
            this.btnAntrenmanDuzenle.Click += new System.EventHandler(this.btnAntrenmanDuzenle_Click);
            //
            // btnAntrenmanSil
            //
            this.btnAntrenmanSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAntrenmanSil.Location = new System.Drawing.Point(419, 204);
            this.btnAntrenmanSil.Name = "btnAntrenmanSil";
            this.btnAntrenmanSil.Size = new System.Drawing.Size(88, 28);
            this.btnAntrenmanSil.TabIndex = 6;
            this.btnAntrenmanSil.Text = "Sil";
            this.btnAntrenmanSil.Click += new System.EventHandler(this.btnAntrenmanSil_Click);
            //
            // btnTakipDuzenle
            //
            this.btnTakipDuzenle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTakipDuzenle.Location = new System.Drawing.Point(325, 392);
            this.btnTakipDuzenle.Name = "btnTakipDuzenle";
            this.btnTakipDuzenle.Size = new System.Drawing.Size(88, 28);
            this.btnTakipDuzenle.TabIndex = 8;
            this.btnTakipDuzenle.Text = "Düzenle";
            this.btnTakipDuzenle.Click += new System.EventHandler(this.btnTakipDuzenle_Click);
            //
            // btnTakipSil
            //
            this.btnTakipSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTakipSil.Location = new System.Drawing.Point(419, 392);
            this.btnTakipSil.Name = "btnTakipSil";
            this.btnTakipSil.Size = new System.Drawing.Size(88, 28);
            this.btnTakipSil.TabIndex = 9;
            this.btnTakipSil.Text = "Sil";
            this.btnTakipSil.Click += new System.EventHandler(this.btnTakipSil_Click);
            //
            // lblAntrenman
            //
            this.lblAntrenman.AutoSize = true;
            this.lblAntrenman.Location = new System.Drawing.Point(34, 208);
            this.lblAntrenman.Name = "lblAntrenman";
            this.lblAntrenman.Size = new System.Drawing.Size(88, 17);
            this.lblAntrenman.TabIndex = 15;
            this.lblAntrenman.Text = "Antrenmanlar";
            //
            // lblTakip
            //
            this.lblTakip.AutoSize = true;
            this.lblTakip.Location = new System.Drawing.Point(34, 396);
            this.lblTakip.Name = "lblTakip";
            this.lblTakip.Size = new System.Drawing.Size(54, 17);
            this.lblTakip.TabIndex = 16;
            this.lblTakip.Text = "Takipler";
            //
            // btnkapat
            //
            this.btnkapat.Location = new System.Drawing.Point(497, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 12;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // btnAsagial
            //
            this.btnAsagial.Location = new System.Drawing.Point(451, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(46, 32);
            this.btnAsagial.TabIndex = 11;
            this.btnAsagial.TabStop = false;
            this.btnAsagial.Text = "–";
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            //
            // lblSporcu
            //
            this.lblSporcu.AutoSize = true;
            this.lblSporcu.Location = new System.Drawing.Point(34, 62);
            this.lblSporcu.Name = "lblSporcu";
            this.lblSporcu.Size = new System.Drawing.Size(48, 17);
            this.lblSporcu.TabIndex = 14;
            this.lblSporcu.Text = "Sporcu";
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(122, 25);
            this.lblBaslik.TabIndex = 13;
            this.lblBaslik.Text = "Sporcu Detayı";
            //
            // SporcuDetayForm
            //
            this.AcceptButton = this.btnGuncelle;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnkapat;
            this.ClientSize = new System.Drawing.Size(543, 568);
            this.Controls.Add(this.btnHaftalikProgram);
            this.Controls.Add(this.btnCsvAktar);
            this.Controls.Add(this.btnGrafik);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblSporcu);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.lblTakip);
            this.Controls.Add(this.lblAntrenman);
            this.Controls.Add(this.btnTakipSil);
            this.Controls.Add(this.btnTakipDuzenle);
            this.Controls.Add(this.btnAntrenmanSil);
            this.Controls.Add(this.btnAntrenmanDuzenle);
            this.Controls.Add(this.dgvTakipler);
            this.Controls.Add(this.dgvAntrenmanlar);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.cmbSporcular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SporcuDetayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spor Istanbul | Sporcu Detayı";
            this.Load += new System.EventHandler(this.SporcuDetayForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAntrenmanlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTakipler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSporcular;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.DataGridView dgvAntrenmanlar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAntrenmanAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetaylar;
        private System.Windows.Forms.DataGridView dgvTakipler;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarih;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAciklama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOlcumler;
        private System.Windows.Forms.Button btnGrafik;
        private System.Windows.Forms.Button btnCsvAktar;
        private System.Windows.Forms.Button btnHaftalikProgram;
        private System.Windows.Forms.Button btnAntrenmanDuzenle;
        private System.Windows.Forms.Button btnAntrenmanSil;
        private System.Windows.Forms.Button btnTakipDuzenle;
        private System.Windows.Forms.Button btnTakipSil;
        private System.Windows.Forms.Label lblAntrenman;
        private System.Windows.Forms.Label lblTakip;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Button btnAsagial;
        private System.Windows.Forms.Label lblSporcu;
        private System.Windows.Forms.Label lblBaslik;
    }
}
