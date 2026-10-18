namespace SporTakip
{
    partial class AntrenmanDuzenleForm
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
            this.txtAntrenmanAdi = new System.Windows.Forms.TextBox();
            this.lblAntrenmanAdi = new System.Windows.Forms.Label();
            this.txtDetaylar = new System.Windows.Forms.TextBox();
            this.lblDetaylar = new System.Windows.Forms.Label();
            this.cmbGun = new System.Windows.Forms.ComboBox();
            this.lblGun = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // txtAntrenmanAdi
            //
            this.txtAntrenmanAdi.Location = new System.Drawing.Point(136, 56);
            this.txtAntrenmanAdi.Name = "txtAntrenmanAdi";
            this.txtAntrenmanAdi.Size = new System.Drawing.Size(300, 25);
            this.txtAntrenmanAdi.TabIndex = 0;
            //
            // txtDetaylar
            //
            this.txtDetaylar.Multiline = true;
            this.txtDetaylar.AcceptsReturn = true;
            this.txtDetaylar.Location = new System.Drawing.Point(136, 92);
            this.txtDetaylar.Name = "txtDetaylar";
            this.txtDetaylar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetaylar.Size = new System.Drawing.Size(300, 70);
            this.txtDetaylar.TabIndex = 1;
            //
            // cmbGun
            //
            this.cmbGun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGun.FormattingEnabled = true;
            this.cmbGun.Location = new System.Drawing.Point(136, 172);
            this.cmbGun.Name = "cmbGun";
            this.cmbGun.Size = new System.Drawing.Size(300, 25);
            this.cmbGun.TabIndex = 2;
            //
            // lblGun
            //
            this.lblGun.BackColor = System.Drawing.Color.Transparent;
            this.lblGun.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblGun.Location = new System.Drawing.Point(20, 172);
            this.lblGun.Name = "lblGun";
            this.lblGun.Size = new System.Drawing.Size(110, 25);
            this.lblGun.TabIndex = 8;
            this.lblGun.Text = "Gün";
            this.lblGun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnKaydet.Location = new System.Drawing.Point(136, 212);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(145, 36);
            this.btnKaydet.TabIndex = 3;
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
            this.btnIptal.Location = new System.Drawing.Point(291, 212);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(145, 36);
            this.btnIptal.TabIndex = 4;
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
            this.btnkapat.TabIndex = 5;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.UseVisualStyleBackColor = false;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // lblAntrenmanAdi
            //
            this.lblAntrenmanAdi.BackColor = System.Drawing.Color.Transparent;
            this.lblAntrenmanAdi.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAntrenmanAdi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblAntrenmanAdi.Location = new System.Drawing.Point(20, 56);
            this.lblAntrenmanAdi.Name = "lblAntrenmanAdi";
            this.lblAntrenmanAdi.Size = new System.Drawing.Size(110, 25);
            this.lblAntrenmanAdi.TabIndex = 5;
            this.lblAntrenmanAdi.Text = "Antrenman Adı";
            this.lblAntrenmanAdi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblDetaylar
            //
            this.lblDetaylar.BackColor = System.Drawing.Color.Transparent;
            this.lblDetaylar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDetaylar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblDetaylar.Location = new System.Drawing.Point(20, 92);
            this.lblDetaylar.Name = "lblDetaylar";
            this.lblDetaylar.Size = new System.Drawing.Size(110, 25);
            this.lblDetaylar.TabIndex = 6;
            this.lblDetaylar.Text = "Detaylar";
            this.lblDetaylar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.lblBaslik.Text = "Antrenmanı Düzenle";
            //
            // AntrenmanDuzenleForm
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(470, 268);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.lblGun);
            this.Controls.Add(this.cmbGun);
            this.Controls.Add(this.lblDetaylar);
            this.Controls.Add(this.txtDetaylar);
            this.Controls.Add(this.lblAntrenmanAdi);
            this.Controls.Add(this.txtAntrenmanAdi);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AntrenmanDuzenleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spor Istanbul | Antrenmanı Düzenle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAntrenmanAdi;
        private System.Windows.Forms.Label lblAntrenmanAdi;
        private System.Windows.Forms.TextBox txtDetaylar;
        private System.Windows.Forms.Label lblDetaylar;
        private System.Windows.Forms.ComboBox cmbGun;
        private System.Windows.Forms.Label lblGun;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Label lblBaslik;
    }
}
