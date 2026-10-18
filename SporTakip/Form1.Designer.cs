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
            this.btnAntrenmanEkle = new System.Windows.Forms.Button();
            this.btnTakipEkle = new System.Windows.Forms.Button();
            this.btnSporcuDetay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnAsagial = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblSporcular = new System.Windows.Forms.Label();
            this.lblYeniSporcu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // txtAd
            //
            this.txtAd.Location = new System.Drawing.Point(600, 104);
            this.txtAd.MaxLength = 32;
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(170, 25);
            this.txtAd.TabIndex = 0;
            this.txtAd.Enter += new System.EventHandler(this.txtAd_Enter);
            this.txtAd.Leave += new System.EventHandler(this.txtAd_Leave);
            //
            // txtSporDali
            //
            this.txtSporDali.Location = new System.Drawing.Point(600, 142);
            this.txtSporDali.MaxLength = 64;
            this.txtSporDali.Name = "txtSporDali";
            this.txtSporDali.Size = new System.Drawing.Size(170, 25);
            this.txtSporDali.TabIndex = 1;
            //
            // btnKaydet
            //
            this.btnKaydet.BackColor = System.Drawing.Color.White;
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnKaydet.Location = new System.Drawing.Point(600, 184);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(170, 36);
            this.btnKaydet.TabIndex = 2;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // lstSporcular
            //
            this.lstSporcular.BackColor = System.Drawing.SystemColors.Window;
            this.lstSporcular.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSporcular.FormattingEnabled = true;
            this.lstSporcular.HorizontalScrollbar = true;
            this.lstSporcular.IntegralHeight = false;
            this.lstSporcular.ItemHeight = 17;
            this.lstSporcular.Location = new System.Drawing.Point(40, 90);
            this.lstSporcular.Name = "lstSporcular";
            this.lstSporcular.Size = new System.Drawing.Size(230, 272);
            this.lstSporcular.TabIndex = 3;
            //
            // btnAntrenmanEkle
            //
            this.btnAntrenmanEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnAntrenmanEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAntrenmanEkle.FlatAppearance.BorderSize = 0;
            this.btnAntrenmanEkle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(72)))), ((int)(((byte)(170)))));
            this.btnAntrenmanEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAntrenmanEkle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAntrenmanEkle.ForeColor = System.Drawing.Color.White;
            this.btnAntrenmanEkle.Location = new System.Drawing.Point(300, 284);
            this.btnAntrenmanEkle.Name = "btnAntrenmanEkle";
            this.btnAntrenmanEkle.Size = new System.Drawing.Size(150, 34);
            this.btnAntrenmanEkle.TabIndex = 5;
            this.btnAntrenmanEkle.Text = "Antrenman Ekle";
            this.btnAntrenmanEkle.UseVisualStyleBackColor = false;
            this.btnAntrenmanEkle.Click += new System.EventHandler(this.btnAntrenmanEkle_Click);
            //
            // btnTakipEkle
            //
            this.btnTakipEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnTakipEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTakipEkle.FlatAppearance.BorderSize = 0;
            this.btnTakipEkle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(72)))), ((int)(((byte)(170)))));
            this.btnTakipEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakipEkle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTakipEkle.ForeColor = System.Drawing.Color.White;
            this.btnTakipEkle.Location = new System.Drawing.Point(300, 328);
            this.btnTakipEkle.Name = "btnTakipEkle";
            this.btnTakipEkle.Size = new System.Drawing.Size(150, 34);
            this.btnTakipEkle.TabIndex = 6;
            this.btnTakipEkle.Text = "Takip Ekle";
            this.btnTakipEkle.UseVisualStyleBackColor = false;
            this.btnTakipEkle.Click += new System.EventHandler(this.btnTakipEkle_Click);
            //
            // btnSporcuDetay
            //
            this.btnSporcuDetay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnSporcuDetay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSporcuDetay.FlatAppearance.BorderSize = 0;
            this.btnSporcuDetay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(72)))), ((int)(((byte)(170)))));
            this.btnSporcuDetay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSporcuDetay.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSporcuDetay.ForeColor = System.Drawing.Color.White;
            this.btnSporcuDetay.Location = new System.Drawing.Point(300, 372);
            this.btnSporcuDetay.Name = "btnSporcuDetay";
            this.btnSporcuDetay.Size = new System.Drawing.Size(150, 34);
            this.btnSporcuDetay.TabIndex = 7;
            this.btnSporcuDetay.Text = "Sporcu Detayı";
            this.btnSporcuDetay.UseVisualStyleBackColor = false;
            this.btnSporcuDetay.Click += new System.EventHandler(this.btnSporcuDetay_Click);
            //
            // label1
            //
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(470, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Sporcu Adı";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // label2
            //
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(470, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Spor Dalı";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // btnSil
            //
            this.btnSil.BackColor = System.Drawing.Color.White;
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.btnSil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.btnSil.Location = new System.Drawing.Point(40, 372);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(230, 34);
            this.btnSil.TabIndex = 4;
            this.btnSil.Text = "Seçili Sporcuyu Sil";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            //
            // btnAsagial
            //
            this.btnAsagial.BackColor = System.Drawing.Color.Transparent;
            this.btnAsagial.FlatAppearance.BorderSize = 0;
            this.btnAsagial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(160)))));
            this.btnAsagial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsagial.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAsagial.ForeColor = System.Drawing.Color.White;
            this.btnAsagial.Location = new System.Drawing.Point(708, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(46, 32);
            this.btnAsagial.TabIndex = 8;
            this.btnAsagial.TabStop = false;
            this.btnAsagial.Text = "–";
            this.btnAsagial.UseVisualStyleBackColor = false;
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            //
            // btnkapat
            //
            this.btnkapat.BackColor = System.Drawing.Color.Transparent;
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnkapat.ForeColor = System.Drawing.Color.White;
            this.btnkapat.Location = new System.Drawing.Point(754, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 9;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.UseVisualStyleBackColor = false;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblBaslik.Location = new System.Drawing.Point(35, 20);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(139, 30);
            this.lblBaslik.TabIndex = 10;
            this.lblBaslik.Text = "Spor Istanbul";
            //
            // lblSporcular
            //
            this.lblSporcular.AutoSize = true;
            this.lblSporcular.BackColor = System.Drawing.Color.Transparent;
            this.lblSporcular.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSporcular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblSporcular.Location = new System.Drawing.Point(37, 66);
            this.lblSporcular.Name = "lblSporcular";
            this.lblSporcular.Size = new System.Drawing.Size(108, 17);
            this.lblSporcular.TabIndex = 11;
            this.lblSporcular.Text = "Kayıtlı Sporcular";
            //
            // lblYeniSporcu
            //
            this.lblYeniSporcu.AutoSize = true;
            this.lblYeniSporcu.BackColor = System.Drawing.Color.Transparent;
            this.lblYeniSporcu.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblYeniSporcu.ForeColor = System.Drawing.Color.White;
            this.lblYeniSporcu.Location = new System.Drawing.Point(596, 62);
            this.lblYeniSporcu.Name = "lblYeniSporcu";
            this.lblYeniSporcu.Size = new System.Drawing.Size(109, 25);
            this.lblYeniSporcu.TabIndex = 14;
            this.lblYeniSporcu.Text = "Yeni Sporcu";
            //
            // Form1
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblYeniSporcu);
            this.Controls.Add(this.lblSporcular);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSporcuDetay);
            this.Controls.Add(this.btnTakipEkle);
            this.Controls.Add(this.btnAntrenmanEkle);
            this.Controls.Add(this.lstSporcular);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.txtSporDali);
            this.Controls.Add(this.txtAd);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
        private System.Windows.Forms.Button btnAntrenmanEkle;
        private System.Windows.Forms.Button btnTakipEkle;
        private System.Windows.Forms.Button btnSporcuDetay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnAsagial;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblSporcular;
        private System.Windows.Forms.Label lblYeniSporcu;
    }
}
