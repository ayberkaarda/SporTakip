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
            this.cmbGun = new System.Windows.Forms.ComboBox();
            this.lblGun = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnkapat = new System.Windows.Forms.Button();
            this.btnAsagial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSporcu = new System.Windows.Forms.Label();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // comboSporcular
            //
            this.comboSporcular.FormattingEnabled = true;
            this.comboSporcular.Location = new System.Drawing.Point(160, 76);
            this.comboSporcular.Name = "comboSporcular";
            this.comboSporcular.Size = new System.Drawing.Size(291, 25);
            this.comboSporcular.TabIndex = 0;
            this.comboSporcular.Text = "Sporcu seç";
            //
            // txtAntrenmanName
            //
            this.txtAntrenmanName.Location = new System.Drawing.Point(160, 114);
            this.txtAntrenmanName.Name = "txtAntrenmanName";
            this.txtAntrenmanName.Size = new System.Drawing.Size(291, 25);
            this.txtAntrenmanName.TabIndex = 1;
            //
            // txtDetaylar
            //
            this.txtDetaylar.Location = new System.Drawing.Point(160, 152);
            this.txtDetaylar.Name = "txtDetaylar";
            this.txtDetaylar.Size = new System.Drawing.Size(291, 25);
            this.txtDetaylar.TabIndex = 2;
            //
            // cmbGun
            //
            this.cmbGun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGun.FormattingEnabled = true;
            this.cmbGun.Location = new System.Drawing.Point(160, 190);
            this.cmbGun.Name = "cmbGun";
            this.cmbGun.Size = new System.Drawing.Size(291, 25);
            this.cmbGun.TabIndex = 3;
            //
            // lblGun
            //
            this.lblGun.Location = new System.Drawing.Point(36, 190);
            this.lblGun.Name = "lblGun";
            this.lblGun.Size = new System.Drawing.Size(120, 25);
            this.lblGun.TabIndex = 10;
            this.lblGun.Text = "Gün";
            this.lblGun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnKaydet
            //
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Location = new System.Drawing.Point(160, 236);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(291, 36);
            this.btnKaydet.TabIndex = 4;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // btnkapat
            //
            this.btnkapat.Location = new System.Drawing.Point(441, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 6;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            //
            // btnAsagial
            //
            this.btnAsagial.Location = new System.Drawing.Point(395, 0);
            this.btnAsagial.Name = "btnAsagial";
            this.btnAsagial.Size = new System.Drawing.Size(46, 32);
            this.btnAsagial.TabIndex = 5;
            this.btnAsagial.TabStop = false;
            this.btnAsagial.Text = "–";
            this.btnAsagial.Click += new System.EventHandler(this.btnAsagial_Click);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(36, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Antrenman Adı";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(36, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Antrenman Detayı";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSporcu
            //
            this.lblSporcu.Location = new System.Drawing.Point(36, 76);
            this.lblSporcu.Name = "lblSporcu";
            this.lblSporcu.Size = new System.Drawing.Size(120, 25);
            this.lblSporcu.TabIndex = 7;
            this.lblSporcu.Text = "Sporcu";
            this.lblSporcu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(139, 25);
            this.lblBaslik.TabIndex = 11;
            this.lblBaslik.Text = "Antrenman Ekle";
            //
            // AntrenmanEkleForm
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnkapat;
            this.ClientSize = new System.Drawing.Size(487, 308);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblSporcu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnkapat);
            this.Controls.Add(this.btnAsagial);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.lblGun);
            this.Controls.Add(this.cmbGun);
            this.Controls.Add(this.txtDetaylar);
            this.Controls.Add(this.txtAntrenmanName);
            this.Controls.Add(this.comboSporcular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AntrenmanEkleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spor Istanbul | Antrenman Ekle";
            this.Load += new System.EventHandler(this.AntrenmanEkleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboSporcular;
        private System.Windows.Forms.TextBox txtAntrenmanName;
        private System.Windows.Forms.TextBox txtDetaylar;
        private System.Windows.Forms.ComboBox cmbGun;
        private System.Windows.Forms.Label lblGun;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Button btnAsagial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSporcu;
        private System.Windows.Forms.Label lblBaslik;
    }
}
