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
            this.lblSporcu = new System.Windows.Forms.Label();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // cmbSporcular
            //
            this.cmbSporcular.FormattingEnabled = true;
            this.cmbSporcular.Location = new System.Drawing.Point(24, 74);
            this.cmbSporcular.Name = "cmbSporcular";
            this.cmbSporcular.Size = new System.Drawing.Size(170, 25);
            this.cmbSporcular.TabIndex = 0;
            this.cmbSporcular.Text = "Sporcu seç";
            //
            // btnGuncelle
            //
            this.btnGuncelle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.btnGuncelle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuncelle.FlatAppearance.BorderSize = 0;
            this.btnGuncelle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(72)))), ((int)(((byte)(170)))));
            this.btnGuncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuncelle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnGuncelle.Location = new System.Drawing.Point(24, 108);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(170, 34);
            this.btnGuncelle.TabIndex = 1;
            this.btnGuncelle.Text = "Listeyi Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            //
            // lstAntrenmanlar
            //
            this.lstAntrenmanlar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstAntrenmanlar.FormattingEnabled = true;
            this.lstAntrenmanlar.HorizontalScrollbar = true;
            this.lstAntrenmanlar.IntegralHeight = false;
            this.lstAntrenmanlar.ItemHeight = 17;
            this.lstAntrenmanlar.Location = new System.Drawing.Point(24, 182);
            this.lstAntrenmanlar.Name = "lstAntrenmanlar";
            this.lstAntrenmanlar.Size = new System.Drawing.Size(230, 106);
            this.lstAntrenmanlar.TabIndex = 2;
            //
            // lstTakipler
            //
            this.lstTakipler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTakipler.FormattingEnabled = true;
            this.lstTakipler.HorizontalScrollbar = true;
            this.lstTakipler.IntegralHeight = false;
            this.lstTakipler.ItemHeight = 17;
            this.lstTakipler.Location = new System.Drawing.Point(24, 326);
            this.lstTakipler.Name = "lstTakipler";
            this.lstTakipler.Size = new System.Drawing.Size(230, 100);
            this.lstTakipler.TabIndex = 3;
            //
            // lblAntrenman
            //
            this.lblAntrenman.AutoSize = true;
            this.lblAntrenman.BackColor = System.Drawing.Color.Transparent;
            this.lblAntrenman.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAntrenman.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblAntrenman.Location = new System.Drawing.Point(21, 160);
            this.lblAntrenman.Name = "lblAntrenman";
            this.lblAntrenman.Size = new System.Drawing.Size(88, 17);
            this.lblAntrenman.TabIndex = 8;
            this.lblAntrenman.Text = "Antrenmanlar";
            //
            // lblTakip
            //
            this.lblTakip.AutoSize = true;
            this.lblTakip.BackColor = System.Drawing.Color.Transparent;
            this.lblTakip.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTakip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblTakip.Location = new System.Drawing.Point(21, 304);
            this.lblTakip.Name = "lblTakip";
            this.lblTakip.Size = new System.Drawing.Size(54, 17);
            this.lblTakip.TabIndex = 9;
            this.lblTakip.Text = "Takipler";
            //
            // btnkapat
            //
            this.btnkapat.BackColor = System.Drawing.Color.Transparent;
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnkapat.ForeColor = System.Drawing.Color.White;
            this.btnkapat.Location = new System.Drawing.Point(457, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 5;
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
            this.btnAsagial.Location = new System.Drawing.Point(411, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(46, 32);
            this.btnAsagial.TabIndex = 4;
            this.btnAsagial.TabStop = false;
            this.btnAsagial.Text = "–";
            this.btnAsagial.UseVisualStyleBackColor = false;
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            //
            // lblSporcu
            //
            this.lblSporcu.AutoSize = true;
            this.lblSporcu.BackColor = System.Drawing.Color.Transparent;
            this.lblSporcu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSporcu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblSporcu.Location = new System.Drawing.Point(21, 52);
            this.lblSporcu.Name = "lblSporcu";
            this.lblSporcu.Size = new System.Drawing.Size(48, 17);
            this.lblSporcu.TabIndex = 7;
            this.lblSporcu.Text = "Sporcu";
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(122, 25);
            this.lblBaslik.TabIndex = 6;
            this.lblBaslik.Text = "Sporcu Detayı";
            //
            // SporcuDetayForm
            //
            this.AcceptButton = this.btnGuncelle;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnkapat;
            this.ClientSize = new System.Drawing.Size(503, 450);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblSporcu);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.lblTakip);
            this.Controls.Add(this.lblAntrenman);
            this.Controls.Add(this.lstTakipler);
            this.Controls.Add(this.lstAntrenmanlar);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.cmbSporcular);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SporcuDetayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.Label lblSporcu;
        private System.Windows.Forms.Label lblBaslik;
    }
}
