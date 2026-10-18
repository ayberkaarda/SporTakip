namespace SporTakip
{
    partial class OlcumGrafikForm
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
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblSporcu = new System.Windows.Forms.Label();
            this.lblTur = new System.Windows.Forms.Label();
            this.cmbTur = new System.Windows.Forms.ComboBox();
            this.lblOzet = new System.Windows.Forms.Label();
            this.formsPlot = new ScottPlot.WinForms.FormsPlot();
            this.lblBos = new System.Windows.Forms.Label();
            this.btnkapat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblBaslik
            //
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(18, 14);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(132, 25);
            this.lblBaslik.TabIndex = 4;
            this.lblBaslik.Text = "Ölçüm Grafiği";
            //
            // lblSporcu
            //
            this.lblSporcu.AutoEllipsis = true;
            this.lblSporcu.BackColor = System.Drawing.Color.Transparent;
            this.lblSporcu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(188)))), ((int)(((byte)(228)))));
            this.lblSporcu.Location = new System.Drawing.Point(160, 19);
            this.lblSporcu.Name = "lblSporcu";
            this.lblSporcu.Size = new System.Drawing.Size(420, 20);
            this.lblSporcu.TabIndex = 5;
            this.lblSporcu.Text = "Sporcu";
            //
            // lblTur
            //
            this.lblTur.BackColor = System.Drawing.Color.Transparent;
            this.lblTur.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTur.ForeColor = System.Drawing.Color.White;
            this.lblTur.Location = new System.Drawing.Point(20, 60);
            this.lblTur.Name = "lblTur";
            this.lblTur.Size = new System.Drawing.Size(86, 25);
            this.lblTur.TabIndex = 6;
            this.lblTur.Text = "Ölçüm türü";
            this.lblTur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbTur
            //
            this.cmbTur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTur.FormattingEnabled = true;
            this.cmbTur.Location = new System.Drawing.Point(110, 60);
            this.cmbTur.Name = "cmbTur";
            this.cmbTur.Size = new System.Drawing.Size(200, 25);
            this.cmbTur.TabIndex = 0;
            this.cmbTur.SelectedIndexChanged += new System.EventHandler(this.cmbTur_SelectedIndexChanged);
            //
            // lblOzet
            //
            this.lblOzet.BackColor = System.Drawing.Color.Transparent;
            this.lblOzet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(188)))), ((int)(((byte)(228)))));
            this.lblOzet.Location = new System.Drawing.Point(330, 60);
            this.lblOzet.Name = "lblOzet";
            this.lblOzet.Size = new System.Drawing.Size(410, 25);
            this.lblOzet.TabIndex = 7;
            this.lblOzet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // formsPlot
            //
            this.formsPlot.Location = new System.Drawing.Point(20, 100);
            this.formsPlot.Name = "formsPlot";
            this.formsPlot.Size = new System.Drawing.Size(720, 360);
            this.formsPlot.TabIndex = 1;
            //
            // lblBos
            //
            this.lblBos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(34)))), ((int)(((byte)(88)))));
            this.lblBos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(188)))), ((int)(((byte)(228)))));
            this.lblBos.Location = new System.Drawing.Point(20, 100);
            this.lblBos.Name = "lblBos";
            this.lblBos.Size = new System.Drawing.Size(720, 360);
            this.lblBos.TabIndex = 2;
            this.lblBos.Text = "Bu tür için ölçüm yok";
            this.lblBos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBos.Visible = false;
            //
            // btnkapat
            //
            this.btnkapat.BackColor = System.Drawing.Color.Transparent;
            this.btnkapat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnkapat.FlatAppearance.BorderSize = 0;
            this.btnkapat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnkapat.ForeColor = System.Drawing.Color.White;
            this.btnkapat.Location = new System.Drawing.Point(714, 0);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(46, 32);
            this.btnkapat.TabIndex = 3;
            this.btnkapat.TabStop = false;
            this.btnkapat.Text = "×";
            this.btnkapat.UseVisualStyleBackColor = false;
            //
            // OlcumGrafikForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(22)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnkapat;
            this.ClientSize = new System.Drawing.Size(760, 480);
            this.Controls.Add(this.lblBos);
            this.Controls.Add(this.formsPlot);
            this.Controls.Add(this.lblOzet);
            this.Controls.Add(this.cmbTur);
            this.Controls.Add(this.lblTur);
            this.Controls.Add(this.lblSporcu);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnkapat);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OlcumGrafikForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spor Istanbul | Ölçüm Grafiği";
            this.Load += new System.EventHandler(this.OlcumGrafikForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblSporcu;
        private System.Windows.Forms.Label lblTur;
        private System.Windows.Forms.ComboBox cmbTur;
        private System.Windows.Forms.Label lblOzet;
        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.Label lblBos;
        private System.Windows.Forms.Button btnkapat;
    }
}
