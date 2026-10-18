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
            this.cmbSporcular.Location = new System.Drawing.Point(92, 56);
            this.cmbSporcular.Name = "cmbSporcular";
            this.cmbSporcular.Size = new System.Drawing.Size(175, 25);
            this.cmbSporcular.TabIndex = 0;
            this.cmbSporcular.Text = "Sporcu seç";
            //
            // dtTarih
            //
            this.dtTarih.Location = new System.Drawing.Point(92, 92);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(175, 25);
            this.dtTarih.TabIndex = 1;
            //
            // txtAciklama
            //
            this.txtAciklama.Location = new System.Drawing.Point(92, 128);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(175, 25);
            this.txtAciklama.TabIndex = 2;
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
            this.btnKaydet.Location = new System.Drawing.Point(92, 458);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(175, 36);
            this.btnKaydet.TabIndex = 9;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // btnkapat
            //
            this.btnkapat.BackColor = System.Drawing.Color.Transparent;
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnkapat.ForeColor = System.Drawing.Color.White;
            this.btnkapat.Location = new System.Drawing.Point(573, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 11;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.UseVisualStyleBackColor = false;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // btnAsagial
            //
            this.btnAsagial.BackColor = System.Drawing.Color.Transparent;
            this.btnAsagial.FlatAppearance.BorderSize = 0;
            this.btnAsagial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(160)))));
            this.btnAsagial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsagial.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAsagial.ForeColor = System.Drawing.Color.White;
            this.btnAsagial.Location = new System.Drawing.Point(527, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(46, 32);
            this.btnAsagial.TabIndex = 10;
            this.btnAsagial.TabStop = false;
            this.btnAsagial.Text = "–";
            this.btnAsagial.UseVisualStyleBackColor = false;
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            //
            // label1
            //
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.label1.Location = new System.Drawing.Point(20, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Açıklama";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSporcu
            //
            this.lblSporcu.BackColor = System.Drawing.Color.Transparent;
            this.lblSporcu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSporcu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblSporcu.Location = new System.Drawing.Point(20, 56);
            this.lblSporcu.Name = "lblSporcu";
            this.lblSporcu.Size = new System.Drawing.Size(68, 25);
            this.lblSporcu.TabIndex = 7;
            this.lblSporcu.Text = "Sporcu";
            this.lblSporcu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTarih
            //
            this.lblTarih.BackColor = System.Drawing.Color.Transparent;
            this.lblTarih.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTarih.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblTarih.Location = new System.Drawing.Point(20, 92);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(68, 25);
            this.lblTarih.TabIndex = 8;
            this.lblTarih.Text = "Tarih";
            this.lblTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(94, 25);
            this.lblBaslik.TabIndex = 6;
            this.lblBaslik.Text = "Takip Ekle";
            //
            // lblOlcumBaslik
            //
            this.lblOlcumBaslik.AutoSize = true;
            this.lblOlcumBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblOlcumBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOlcumBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblOlcumBaslik.Location = new System.Drawing.Point(18, 168);
            this.lblOlcumBaslik.Name = "lblOlcumBaslik";
            this.lblOlcumBaslik.Size = new System.Drawing.Size(170, 20);
            this.lblOlcumBaslik.TabIndex = 12;
            this.lblOlcumBaslik.Text = "Ölçümler (isteğe bağlı)";
            //
            // lblOlcumTur
            //
            this.lblOlcumTur.BackColor = System.Drawing.Color.Transparent;
            this.lblOlcumTur.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOlcumTur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblOlcumTur.Location = new System.Drawing.Point(20, 198);
            this.lblOlcumTur.Name = "lblOlcumTur";
            this.lblOlcumTur.Size = new System.Drawing.Size(68, 25);
            this.lblOlcumTur.TabIndex = 13;
            this.lblOlcumTur.Text = "Tür";
            this.lblOlcumTur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbOlcumTur
            //
            this.cmbOlcumTur.FormattingEnabled = true;
            this.cmbOlcumTur.Location = new System.Drawing.Point(92, 198);
            this.cmbOlcumTur.MaxLength = 50;
            this.cmbOlcumTur.Name = "cmbOlcumTur";
            this.cmbOlcumTur.Size = new System.Drawing.Size(175, 25);
            this.cmbOlcumTur.TabIndex = 3;
            //
            // lblOlcumDeger
            //
            this.lblOlcumDeger.BackColor = System.Drawing.Color.Transparent;
            this.lblOlcumDeger.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOlcumDeger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblOlcumDeger.Location = new System.Drawing.Point(20, 232);
            this.lblOlcumDeger.Name = "lblOlcumDeger";
            this.lblOlcumDeger.Size = new System.Drawing.Size(68, 25);
            this.lblOlcumDeger.TabIndex = 14;
            this.lblOlcumDeger.Text = "Değer";
            this.lblOlcumDeger.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtOlcumDeger
            //
            this.txtOlcumDeger.Location = new System.Drawing.Point(92, 232);
            this.txtOlcumDeger.MaxLength = 20;
            this.txtOlcumDeger.Name = "txtOlcumDeger";
            this.txtOlcumDeger.Size = new System.Drawing.Size(175, 25);
            this.txtOlcumDeger.TabIndex = 4;
            //
            // lblOlcumBirim
            //
            this.lblOlcumBirim.BackColor = System.Drawing.Color.Transparent;
            this.lblOlcumBirim.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOlcumBirim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblOlcumBirim.Location = new System.Drawing.Point(20, 266);
            this.lblOlcumBirim.Name = "lblOlcumBirim";
            this.lblOlcumBirim.Size = new System.Drawing.Size(68, 25);
            this.lblOlcumBirim.TabIndex = 15;
            this.lblOlcumBirim.Text = "Birim";
            this.lblOlcumBirim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbOlcumBirim
            //
            this.cmbOlcumBirim.FormattingEnabled = true;
            this.cmbOlcumBirim.Location = new System.Drawing.Point(92, 266);
            this.cmbOlcumBirim.MaxLength = 20;
            this.cmbOlcumBirim.Name = "cmbOlcumBirim";
            this.cmbOlcumBirim.Size = new System.Drawing.Size(175, 25);
            this.cmbOlcumBirim.TabIndex = 5;
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
            this.btnOlcumEkle.Location = new System.Drawing.Point(92, 300);
            this.btnOlcumEkle.Name = "btnOlcumEkle";
            this.btnOlcumEkle.Size = new System.Drawing.Size(100, 28);
            this.btnOlcumEkle.TabIndex = 6;
            this.btnOlcumEkle.Text = "Ölçüm Ekle";
            this.btnOlcumEkle.UseVisualStyleBackColor = false;
            this.btnOlcumEkle.Click += new System.EventHandler(this.btnOlcumEkle_Click);
            //
            // btnOlcumCikar
            //
            this.btnOlcumCikar.BackColor = System.Drawing.Color.White;
            this.btnOlcumCikar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.btnOlcumCikar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.btnOlcumCikar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.btnOlcumCikar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOlcumCikar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOlcumCikar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOlcumCikar.Location = new System.Drawing.Point(197, 300);
            this.btnOlcumCikar.Name = "btnOlcumCikar";
            this.btnOlcumCikar.Size = new System.Drawing.Size(70, 28);
            this.btnOlcumCikar.TabIndex = 7;
            this.btnOlcumCikar.Text = "Çıkar";
            this.btnOlcumCikar.UseVisualStyleBackColor = false;
            this.btnOlcumCikar.Click += new System.EventHandler(this.btnOlcumCikar_Click);
            //
            // dgvOlcumler
            //
            this.dgvOlcumler.Location = new System.Drawing.Point(20, 338);
            this.dgvOlcumler.Name = "dgvOlcumler";
            this.dgvOlcumler.Size = new System.Drawing.Size(247, 108);
            this.dgvOlcumler.TabIndex = 8;
            this.dgvOlcumler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOlcumler_KeyDown);
            //
            // TakipEkleForm
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnkapat;
            this.ClientSize = new System.Drawing.Size(619, 514);
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
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
