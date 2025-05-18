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
            this.SuspendLayout();
            // 
            // cmbSporcular
            // 
            this.cmbSporcular.FormattingEnabled = true;
            this.cmbSporcular.Location = new System.Drawing.Point(344, 71);
            this.cmbSporcular.Name = "cmbSporcular";
            this.cmbSporcular.Size = new System.Drawing.Size(121, 21);
            this.cmbSporcular.TabIndex = 0;
            this.cmbSporcular.Text = "Sporcu seç";
            // 
            // dtTarih
            // 
            this.dtTarih.Location = new System.Drawing.Point(75, 71);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(200, 20);
            this.dtTarih.TabIndex = 1;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAciklama.Location = new System.Drawing.Point(344, 114);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(121, 29);
            this.txtAciklama.TabIndex = 2;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.Location = new System.Drawing.Point(199, 158);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(268, 32);
            this.btnKaydet.TabIndex = 3;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnkapat
            // 
            this.btnkapat.BackColor = System.Drawing.Color.White;
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnkapat.Location = new System.Drawing.Point(562, -1);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(58, 27);
            this.btnkapat.TabIndex = 13;
            this.btnkapat.Text = "X";
            this.btnkapat.UseVisualStyleBackColor = false;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            // 
            // btnAsagial
            // 
            this.btnAsagial.BackColor = System.Drawing.Color.White;
            this.btnAsagial.FlatAppearance.BorderSize = 0;
            this.btnAsagial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsagial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAsagial.Location = new System.Drawing.Point(505, -1);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(58, 27);
            this.btnAsagial.TabIndex = 12;
            this.btnAsagial.Text = "-";
            this.btnAsagial.UseVisualStyleBackColor = false;
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(274, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Açıklama";
            // 
            // TakipEkleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(619, 230);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.dtTarih);
            this.Controls.Add(this.cmbSporcular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TakipEkleForm";
            this.Text = "Spor Istanbul | Takip Ekle";
            this.Load += new System.EventHandler(this.TakipEkleForm_Load);
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
    }
}