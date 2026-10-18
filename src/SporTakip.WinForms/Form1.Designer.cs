namespace SporTakip
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtAd = new System.Windows.Forms.TextBox();
            this.txtSporDali = new System.Windows.Forms.TextBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.lstSporcular = new System.Windows.Forms.ListBox();
            this.txtAra = new System.Windows.Forms.TextBox();
            this.btnDuzenle = new System.Windows.Forms.Button();
            this.btnAntrenmanEkle = new System.Windows.Forms.Button();
            this.btnTakipEkle = new System.Windows.Forms.Button();
            this.btnSporcuDetay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnAsagial = new System.Windows.Forms.Button();
            this.btnTema = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblSporcular = new System.Windows.Forms.Label();
            this.lblYeniSporcu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // txtAd
            //
            this.txtAd.Location = new System.Drawing.Point(446, 96);
            this.txtAd.MaxLength = 32;
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(314, 25);
            this.txtAd.TabIndex = 7;
            this.txtAd.Enter += new System.EventHandler(this.txtAd_Enter);
            this.txtAd.Leave += new System.EventHandler(this.txtAd_Leave);
            //
            // txtSporDali
            //
            this.txtSporDali.Location = new System.Drawing.Point(446, 132);
            this.txtSporDali.MaxLength = 64;
            this.txtSporDali.Name = "txtSporDali";
            this.txtSporDali.Size = new System.Drawing.Size(314, 25);
            this.txtSporDali.TabIndex = 8;
            //
            // btnKaydet
            //
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Location = new System.Drawing.Point(446, 172);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(314, 36);
            this.btnKaydet.TabIndex = 9;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // lstSporcular
            //
            this.lstSporcular.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSporcular.FormattingEnabled = true;
            this.lstSporcular.HorizontalScrollbar = true;
            this.lstSporcular.IntegralHeight = false;
            this.lstSporcular.ItemHeight = 17;
            this.lstSporcular.Location = new System.Drawing.Point(40, 120);
            this.lstSporcular.Name = "lstSporcular";
            this.lstSporcular.Size = new System.Drawing.Size(230, 228);
            this.lstSporcular.TabIndex = 1;
            this.lstSporcular.DoubleClick += new System.EventHandler(this.lstSporcular_DoubleClick);
            //
            // txtAra
            //
            this.txtAra.Location = new System.Drawing.Point(40, 86);
            this.txtAra.Name = "txtAra";
            this.txtAra.PlaceholderText = "Ada veya branşa göre ara...";
            this.txtAra.Size = new System.Drawing.Size(230, 25);
            this.txtAra.TabIndex = 0;
            this.txtAra.TextChanged += new System.EventHandler(this.txtAra_TextChanged);
            this.txtAra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAra_KeyDown);
            //
            // btnDuzenle
            //
            this.btnDuzenle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDuzenle.Location = new System.Drawing.Point(40, 358);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(110, 34);
            this.btnDuzenle.TabIndex = 2;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);
            //
            // btnAntrenmanEkle
            //
            this.btnAntrenmanEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAntrenmanEkle.Location = new System.Drawing.Point(326, 264);
            this.btnAntrenmanEkle.Name = "btnAntrenmanEkle";
            this.btnAntrenmanEkle.Size = new System.Drawing.Size(434, 36);
            this.btnAntrenmanEkle.TabIndex = 4;
            this.btnAntrenmanEkle.Text = "Antrenman Ekle";
            this.btnAntrenmanEkle.Click += new System.EventHandler(this.btnAntrenmanEkle_Click);
            //
            // btnTakipEkle
            //
            this.btnTakipEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTakipEkle.Location = new System.Drawing.Point(326, 310);
            this.btnTakipEkle.Name = "btnTakipEkle";
            this.btnTakipEkle.Size = new System.Drawing.Size(434, 36);
            this.btnTakipEkle.TabIndex = 5;
            this.btnTakipEkle.Text = "Takip Ekle";
            this.btnTakipEkle.Click += new System.EventHandler(this.btnTakipEkle_Click);
            //
            // btnSporcuDetay
            //
            this.btnSporcuDetay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSporcuDetay.Location = new System.Drawing.Point(326, 356);
            this.btnSporcuDetay.Name = "btnSporcuDetay";
            this.btnSporcuDetay.Size = new System.Drawing.Size(434, 36);
            this.btnSporcuDetay.TabIndex = 6;
            this.btnSporcuDetay.Text = "Sporcu Detayı";
            this.btnSporcuDetay.Click += new System.EventHandler(this.btnSporcuDetay_Click);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(326, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sporcu Adı";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(326, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Spor Dalı";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnSil
            //
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.Location = new System.Drawing.Point(160, 358);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(110, 34);
            this.btnSil.TabIndex = 3;
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            //
            // btnTema
            //
            this.btnTema.Location = new System.Drawing.Point(662, 0);
            this.btnTema.Name = "btnTema";
            this.btnTema.Size = new System.Drawing.Size(46, 32);
            this.btnTema.TabIndex = 17;
            this.btnTema.TabStop = false;
            this.btnTema.Click += new System.EventHandler(this.btnTema_Click);
            //
            // btnAsagial
            //
            this.btnAsagial.Location = new System.Drawing.Point(708, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(46, 32);
            this.btnAsagial.TabIndex = 10;
            this.btnAsagial.TabStop = false;
            this.btnAsagial.Text = "–";
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            //
            // btnkapat
            //
            this.btnkapat.Location = new System.Drawing.Point(754, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 11;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Location = new System.Drawing.Point(22, 12);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(139, 30);
            this.lblBaslik.TabIndex = 12;
            this.lblBaslik.Text = "Spor Istanbul";
            //
            // lblSporcular
            //
            this.lblSporcular.AutoSize = true;
            this.lblSporcular.Location = new System.Drawing.Point(38, 60);
            this.lblSporcular.Name = "lblSporcular";
            this.lblSporcular.Size = new System.Drawing.Size(108, 17);
            this.lblSporcular.TabIndex = 13;
            this.lblSporcular.Text = "Kayıtlı Sporcular";
            //
            // lblYeniSporcu
            //
            this.lblYeniSporcu.AutoSize = true;
            this.lblYeniSporcu.Location = new System.Drawing.Point(324, 60);
            this.lblYeniSporcu.Name = "lblYeniSporcu";
            this.lblYeniSporcu.Size = new System.Drawing.Size(109, 25);
            this.lblYeniSporcu.TabIndex = 16;
            this.lblYeniSporcu.Text = "Yeni Sporcu";
            //
            // Form1
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 432);
            this.Controls.Add(this.lblYeniSporcu);
            this.Controls.Add(this.lblSporcular);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnTema);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSporcuDetay);
            this.Controls.Add(this.btnTakipEkle);
            this.Controls.Add(this.btnAntrenmanEkle);
            this.Controls.Add(this.lstSporcular);
            this.Controls.Add(this.txtAra);
            this.Controls.Add(this.btnDuzenle);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.txtSporDali);
            this.Controls.Add(this.txtAd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spor Istanbul";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.TextBox txtSporDali;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.ListBox lstSporcular;
        private System.Windows.Forms.TextBox txtAra;
        private System.Windows.Forms.Button btnDuzenle;
        private System.Windows.Forms.Button btnAntrenmanEkle;
        private System.Windows.Forms.Button btnTakipEkle;
        private System.Windows.Forms.Button btnSporcuDetay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnAsagial;
        private System.Windows.Forms.Button btnTema;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblSporcular;
        private System.Windows.Forms.Label lblYeniSporcu;
    }
}
