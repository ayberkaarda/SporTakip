namespace SporTakip
{
    partial class TakipDuzenleForm
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
            this.dtTarih = new System.Windows.Forms.DateTimePicker();
            this.lblTarih = new System.Windows.Forms.Label();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.lblAciklama = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // dtTarih
            //
            this.dtTarih.Location = new System.Drawing.Point(136, 56);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(300, 25);
            this.dtTarih.TabIndex = 0;
            //
            // txtAciklama
            //
            this.txtAciklama.Multiline = true;
            this.txtAciklama.AcceptsReturn = true;
            this.txtAciklama.Location = new System.Drawing.Point(136, 92);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAciklama.Size = new System.Drawing.Size(300, 70);
            this.txtAciklama.TabIndex = 1;
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
            this.btnKaydet.Location = new System.Drawing.Point(136, 176);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(145, 36);
            this.btnKaydet.TabIndex = 2;
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
            this.btnIptal.Location = new System.Drawing.Point(291, 176);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(145, 36);
            this.btnIptal.TabIndex = 3;
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
            this.btnkapat.TabIndex = 4;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.UseVisualStyleBackColor = false;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // lblTarih
            //
            this.lblTarih.BackColor = System.Drawing.Color.Transparent;
            this.lblTarih.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTarih.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblTarih.Location = new System.Drawing.Point(20, 56);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(110, 25);
            this.lblTarih.TabIndex = 5;
            this.lblTarih.Text = "Tarih";
            this.lblTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAciklama
            //
            this.lblAciklama.BackColor = System.Drawing.Color.Transparent;
            this.lblAciklama.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAciklama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(132)))));
            this.lblAciklama.Location = new System.Drawing.Point(20, 92);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(110, 25);
            this.lblAciklama.TabIndex = 6;
            this.lblAciklama.Text = "Açıklama";
            this.lblAciklama.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.lblBaslik.Text = "Takibi Düzenle";
            //
            // TakipDuzenleForm
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SporTakip.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(470, 232);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.dtTarih);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TakipDuzenleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spor Istanbul | Takibi Düzenle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtTarih;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Label lblAciklama;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Label lblBaslik;
    }
}
