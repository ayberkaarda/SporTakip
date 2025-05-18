namespace SporTakip
{
    partial class AntrenmanEkleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AntrenmanEkleForm));
            this.comboSporcular = new System.Windows.Forms.ComboBox();
            this.txtAntrenmanName = new System.Windows.Forms.TextBox();
            this.txtDetaylar = new System.Windows.Forms.TextBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.btnAsagial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboSporcular
            // 
            this.comboSporcular.FormattingEnabled = true;
            this.comboSporcular.Location = new System.Drawing.Point(68, 91);
            this.comboSporcular.Name = "comboSporcular";
            this.comboSporcular.Size = new System.Drawing.Size(121, 21);
            this.comboSporcular.TabIndex = 0;
            this.comboSporcular.Text = "Sporcu seç";
            // 
            // txtAntrenmanName
            // 
            this.txtAntrenmanName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAntrenmanName.Location = new System.Drawing.Point(314, 91);
            this.txtAntrenmanName.Name = "txtAntrenmanName";
            this.txtAntrenmanName.Size = new System.Drawing.Size(100, 29);
            this.txtAntrenmanName.TabIndex = 1;
            // 
            // txtDetaylar
            // 
            this.txtDetaylar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtDetaylar.Location = new System.Drawing.Point(314, 150);
            this.txtDetaylar.Name = "txtDetaylar";
            this.txtDetaylar.Size = new System.Drawing.Size(100, 29);
            this.txtDetaylar.TabIndex = 2;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.Location = new System.Drawing.Point(199, 228);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(147, 32);
            this.btnKaydet.TabIndex = 3;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnkapat
            // 
            this.btnkapat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnkapat.ForeColor = System.Drawing.Color.White;
            this.btnkapat.Location = new System.Drawing.Point(429, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(58, 27);
            this.btnkapat.TabIndex = 17;
            this.btnkapat.Text = "X";
            this.btnkapat.UseVisualStyleBackColor = false;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            // 
            // btnAsagial
            // 
            this.btnAsagial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnAsagial.FlatAppearance.BorderSize = 0;
            this.btnAsagial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsagial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAsagial.ForeColor = System.Drawing.Color.White;
            this.btnAsagial.Location = new System.Drawing.Point(372, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(58, 27);
            this.btnAsagial.TabIndex = 16;
            this.btnAsagial.Text = "-";
            this.btnAsagial.UseVisualStyleBackColor = false;
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(206, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Antrenman Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(187, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Antrenman Detayı";
            // 
            // AntrenmanEkleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::SporTakip.Properties.Resources.footerbg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(487, 309);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.txtDetaylar);
            this.Controls.Add(this.txtAntrenmanName);
            this.Controls.Add(this.comboSporcular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AntrenmanEkleForm";
            this.Text = "Spor Istanbul | Antrenman Ekle";
            this.Load += new System.EventHandler(this.AntrenmanEkleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboSporcular;
        private System.Windows.Forms.TextBox txtAntrenmanName;
        private System.Windows.Forms.TextBox txtDetaylar;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Button btnAsagial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}