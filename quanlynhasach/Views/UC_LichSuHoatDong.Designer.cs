namespace quanlynhasach
{
    partial class UC_LichSuHoatDong
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.btnLamMoi = new ReaLTaiizor.Controls.MaterialButton();
            this.materialCard4 = new ReaLTaiizor.Controls.MaterialCard();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new ReaLTaiizor.Controls.PoisonDateTime();
            this.dtpTuNgay = new ReaLTaiizor.Controls.PoisonDateTime();
            this.btnLoc = new ReaLTaiizor.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.materialCard4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvLichSu.Location = new System.Drawing.Point(0, 300);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.Size = new System.Drawing.Size(1000, 400);
            this.dgvLichSu.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLamMoi.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLamMoi.Depth = 0;
            this.btnLamMoi.HighEmphasis = true;
            this.btnLamMoi.Icon = null;
            this.btnLamMoi.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnLamMoi.Location = new System.Drawing.Point(824, 53);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLamMoi.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLamMoi.Size = new System.Drawing.Size(83, 36);
            this.btnLamMoi.TabIndex = 35;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLamMoi.UseAccentColor = false;
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // materialCard4
            // 
            this.materialCard4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard4.Controls.Add(this.btnLamMoi);
            this.materialCard4.Controls.Add(this.label2);
            this.materialCard4.Controls.Add(this.label1);
            this.materialCard4.Controls.Add(this.dtpDenNgay);
            this.materialCard4.Controls.Add(this.dtpTuNgay);
            this.materialCard4.Controls.Add(this.btnLoc);
            this.materialCard4.Depth = 0;
            this.materialCard4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.materialCard4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard4.Location = new System.Drawing.Point(14, 23);
            this.materialCard4.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialCard4.Name = "materialCard4";
            this.materialCard4.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard4.Size = new System.Drawing.Size(925, 100);
            this.materialCard4.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(219, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Đến Ngày";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "Từ Ngày";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(223, 54);
            this.dtpDenNgay.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 29);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(17, 54);
            this.dtpTuNgay.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 29);
            this.dtpTuNgay.TabIndex = 2;
            // 
            // btnLoc
            // 
            this.btnLoc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoc.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLoc.Depth = 0;
            this.btnLoc.HighEmphasis = true;
            this.btnLoc.Icon = null;
            this.btnLoc.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnLoc.Location = new System.Drawing.Point(430, 53);
            this.btnLoc.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLoc.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLoc.Size = new System.Drawing.Size(64, 36);
            this.btnLoc.TabIndex = 9;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLoc.UseAccentColor = false;
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // UC_LichSuHoatDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialCard4);
            this.Controls.Add(this.dgvLichSu);
            this.Name = "UC_LichSuHoatDong";
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.materialCard4.ResumeLayout(false);
            this.materialCard4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLichSu;
        private ReaLTaiizor.Controls.MaterialButton btnLamMoi;
        private ReaLTaiizor.Controls.MaterialCard materialCard4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.PoisonDateTime dtpDenNgay;
        private ReaLTaiizor.Controls.PoisonDateTime dtpTuNgay;
        private ReaLTaiizor.Controls.MaterialButton btnLoc;
    }
}
