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
            this.lstAntrenmanlar = new System.Windows.Forms.ListBox();
            this.lstTakipler = new System.Windows.Forms.ListBox();
            this.lblAntrenman = new System.Windows.Forms.Label();
            this.lblTakip = new System.Windows.Forms.Label();
            this.btnkapat = new System.Windows.Forms.Button();
            this.btnAsagial = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbSporcular
            // 
            this.cmbSporcular.FormattingEnabled = true;
            this.cmbSporcular.Location = new System.Drawing.Point(326, 121);
            this.cmbSporcular.Name = "cmbSporcular";
            this.cmbSporcular.Size = new System.Drawing.Size(121, 21);
            this.cmbSporcular.TabIndex = 0;
            this.cmbSporcular.Text = "Sporcu seç";
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnGuncelle.Location = new System.Drawing.Point(313, 182);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(147, 32);
            this.btnGuncelle.TabIndex = 1;
            this.btnGuncelle.Text = "Listeyi Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // lstAntrenmanlar
            // 
            this.lstAntrenmanlar.FormattingEnabled = true;
            this.lstAntrenmanlar.Location = new System.Drawing.Point(38, 97);
            this.lstAntrenmanlar.Name = "lstAntrenmanlar";
            this.lstAntrenmanlar.Size = new System.Drawing.Size(217, 95);
            this.lstAntrenmanlar.TabIndex = 2;
            // 
            // lstTakipler
            // 
            this.lstTakipler.FormattingEnabled = true;
            this.lstTakipler.Location = new System.Drawing.Point(38, 232);
            this.lstTakipler.Name = "lstTakipler";
            this.lstTakipler.Size = new System.Drawing.Size(217, 95);
            this.lstTakipler.TabIndex = 3;
            // 
            // lblAntrenman
            // 
            this.lblAntrenman.AutoSize = true;
            this.lblAntrenman.BackColor = System.Drawing.Color.Transparent;
            this.lblAntrenman.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAntrenman.ForeColor = System.Drawing.Color.Black;
            this.lblAntrenman.Location = new System.Drawing.Point(35, 70);
            this.lblAntrenman.Name = "lblAntrenman";
            this.lblAntrenman.Size = new System.Drawing.Size(93, 17);
            this.lblAntrenman.TabIndex = 4;
            this.lblAntrenman.Text = "Antrenmanlar";
            // 
            // lblTakip
            // 
            this.lblTakip.AutoSize = true;
            this.lblTakip.BackColor = System.Drawing.Color.Transparent;
            this.lblTakip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTakip.ForeColor = System.Drawing.Color.Black;
            this.lblTakip.Location = new System.Drawing.Point(35, 206);
            this.lblTakip.Name = "lblTakip";
            this.lblTakip.Size = new System.Drawing.Size(57, 17);
            this.lblTakip.TabIndex = 5;
            this.lblTakip.Text = "Takipler";
            // 
            // btnkapat
            // 
            this.btnkapat.BackColor = System.Drawing.Color.White;
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnkapat.Location = new System.Drawing.Point(446, -1);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(58, 27);
            this.btnkapat.TabIndex = 15;
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
            this.btnAsagial.Location = new System.Drawing.Point(389, -1);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(58, 27);
            this.btnAsagial.TabIndex = 14;
            this.btnAsagial.Text = "-";
            this.btnAsagial.UseVisualStyleBackColor = false;
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            // 
            // SporcuDetayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(503, 450);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.lblTakip);
            this.Controls.Add(this.lblAntrenman);
            this.Controls.Add(this.lstTakipler);
            this.Controls.Add(this.lstAntrenmanlar);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.cmbSporcular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SporcuDetayForm";
            this.Text = "Spor Istanbul | Sporcu Detayı";
            this.Load += new System.EventHandler(this.SporcuDetayForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSporcular;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.ListBox lstAntrenmanlar;
        private System.Windows.Forms.ListBox lstTakipler;
        private System.Windows.Forms.Label lblAntrenman;
        private System.Windows.Forms.Label lblTakip;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Button btnAsagial;
    }
}