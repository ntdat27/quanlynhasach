namespace quanlynhasach
{
    partial class UC_QuanLySach
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
            this.materialCard1 = new ReaLTaiizor.Controls.MaterialCard();
            this.lblMaSach = new System.Windows.Forms.Label();
            this.txtSoLuong = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            this.txtGiaBan = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            this.txtTenSach = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            this.btnThem = new ReaLTaiizor.Controls.MaterialButton();
            this.btnLamMoi = new ReaLTaiizor.Controls.MaterialButton();
            this.btnXoa = new ReaLTaiizor.Controls.MaterialButton();
            this.btnSua = new ReaLTaiizor.Controls.MaterialButton();
            this.dgvDanhSachSach = new System.Windows.Forms.DataGridView();
            this.materialCard1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachSach)).BeginInit();
            this.SuspendLayout();
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.btnSua);
            this.materialCard1.Controls.Add(this.btnXoa);
            this.materialCard1.Controls.Add(this.btnLamMoi);
            this.materialCard1.Controls.Add(this.btnThem);
            this.materialCard1.Controls.Add(this.lblMaSach);
            this.materialCard1.Controls.Add(this.txtSoLuong);
            this.materialCard1.Controls.Add(this.txtGiaBan);
            this.materialCard1.Controls.Add(this.txtTenSach);
            this.materialCard1.Depth = 0;
            this.materialCard1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(0, 0);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(1000, 398);
            this.materialCard1.TabIndex = 0;
            // 
            // lblMaSach
            // 
            this.lblMaSach.AutoSize = true;
            this.lblMaSach.Location = new System.Drawing.Point(273, 125);
            this.lblMaSach.Name = "lblMaSach";
            this.lblMaSach.Size = new System.Drawing.Size(53, 13);
            this.lblMaSach.TabIndex = 3;
            this.lblMaSach.Text = "Mã Sách:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.AnimateReadOnly = false;
            this.txtSoLuong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSoLuong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSoLuong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtSoLuong.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtSoLuong.Depth = 0;
            this.txtSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSoLuong.HideSelection = true;
            this.txtSoLuong.Hint = "Tồn kho";
            this.txtSoLuong.LeadingIcon = null;
            this.txtSoLuong.Location = new System.Drawing.Point(17, 125);
            this.txtSoLuong.MaxLength = 32767;
            this.txtSoLuong.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.PasswordChar = '\0';
            this.txtSoLuong.PrefixSuffixText = null;
            this.txtSoLuong.ReadOnly = false;
            this.txtSoLuong.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSoLuong.SelectedText = "";
            this.txtSoLuong.SelectionLength = 0;
            this.txtSoLuong.SelectionStart = 0;
            this.txtSoLuong.ShortcutsEnabled = true;
            this.txtSoLuong.Size = new System.Drawing.Size(250, 48);
            this.txtSoLuong.TabIndex = 2;
            this.txtSoLuong.TabStop = false;
            this.txtSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSoLuong.TrailingIcon = null;
            this.txtSoLuong.UseSystemPasswordChar = false;
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.AnimateReadOnly = false;
            this.txtGiaBan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtGiaBan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtGiaBan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtGiaBan.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtGiaBan.Depth = 0;
            this.txtGiaBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtGiaBan.HideSelection = true;
            this.txtGiaBan.Hint = "Giá bán";
            this.txtGiaBan.LeadingIcon = null;
            this.txtGiaBan.Location = new System.Drawing.Point(17, 71);
            this.txtGiaBan.MaxLength = 32767;
            this.txtGiaBan.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.PasswordChar = '\0';
            this.txtGiaBan.PrefixSuffixText = null;
            this.txtGiaBan.ReadOnly = false;
            this.txtGiaBan.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtGiaBan.SelectedText = "";
            this.txtGiaBan.SelectionLength = 0;
            this.txtGiaBan.SelectionStart = 0;
            this.txtGiaBan.ShortcutsEnabled = true;
            this.txtGiaBan.Size = new System.Drawing.Size(250, 48);
            this.txtGiaBan.TabIndex = 1;
            this.txtGiaBan.TabStop = false;
            this.txtGiaBan.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGiaBan.TrailingIcon = null;
            this.txtGiaBan.UseSystemPasswordChar = false;
            // 
            // txtTenSach
            // 
            this.txtTenSach.AnimateReadOnly = false;
            this.txtTenSach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTenSach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTenSach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtTenSach.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtTenSach.Depth = 0;
            this.txtTenSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTenSach.HideSelection = true;
            this.txtTenSach.Hint = "Tên sách";
            this.txtTenSach.LeadingIcon = null;
            this.txtTenSach.Location = new System.Drawing.Point(17, 17);
            this.txtTenSach.MaxLength = 32767;
            this.txtTenSach.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.PasswordChar = '\0';
            this.txtTenSach.PrefixSuffixText = null;
            this.txtTenSach.ReadOnly = false;
            this.txtTenSach.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTenSach.SelectedText = "";
            this.txtTenSach.SelectionLength = 0;
            this.txtTenSach.SelectionStart = 0;
            this.txtTenSach.ShortcutsEnabled = true;
            this.txtTenSach.Size = new System.Drawing.Size(250, 48);
            this.txtTenSach.TabIndex = 0;
            this.txtTenSach.TabStop = false;
            this.txtTenSach.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTenSach.TrailingIcon = null;
            this.txtTenSach.UseSystemPasswordChar = false;
            // 
            // btnThem
            // 
            this.btnThem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnThem.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnThem.Depth = 0;
            this.btnThem.HighEmphasis = true;
            this.btnThem.Icon = null;
            this.btnThem.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnThem.Location = new System.Drawing.Point(293, 20);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnThem.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnThem.Name = "btnThem";
            this.btnThem.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnThem.Size = new System.Drawing.Size(64, 36);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnThem.UseAccentColor = false;
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLamMoi.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLamMoi.Depth = 0;
            this.btnLamMoi.HighEmphasis = true;
            this.btnLamMoi.Icon = null;
            this.btnLamMoi.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnLamMoi.Location = new System.Drawing.Point(509, 20);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLamMoi.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLamMoi.Size = new System.Drawing.Size(83, 36);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLamMoi.UseAccentColor = false;
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnXoa.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnXoa.Depth = 0;
            this.btnXoa.HighEmphasis = true;
            this.btnXoa.Icon = null;
            this.btnXoa.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnXoa.Location = new System.Drawing.Point(437, 20);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnXoa.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnXoa.Size = new System.Drawing.Size(64, 36);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnXoa.UseAccentColor = false;
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSua.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSua.Depth = 0;
            this.btnSua.HighEmphasis = true;
            this.btnSua.Icon = null;
            this.btnSua.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnSua.Location = new System.Drawing.Point(365, 20);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSua.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnSua.Name = "btnSua";
            this.btnSua.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSua.Size = new System.Drawing.Size(64, 36);
            this.btnSua.TabIndex = 7;
            this.btnSua.Text = "SỬA";
            this.btnSua.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSua.UseAccentColor = false;
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // dgvDanhSachSach
            // 
            this.dgvDanhSachSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachSach.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDanhSachSach.Location = new System.Drawing.Point(0, 398);
            this.dgvDanhSachSach.Name = "dgvDanhSachSach";
            this.dgvDanhSachSach.Size = new System.Drawing.Size(1000, 302);
            this.dgvDanhSachSach.TabIndex = 1;
            // 
            // UC_QuanLySach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDanhSachSach);
            this.Controls.Add(this.materialCard1);
            this.Name = "UC_QuanLySach";
            this.Size = new System.Drawing.Size(1000, 700);
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.MaterialCard materialCard1;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit txtSoLuong;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit txtGiaBan;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit txtTenSach;
        private System.Windows.Forms.Label lblMaSach;
        private ReaLTaiizor.Controls.MaterialButton btnSua;
        private ReaLTaiizor.Controls.MaterialButton btnXoa;
        private ReaLTaiizor.Controls.MaterialButton btnLamMoi;
        private ReaLTaiizor.Controls.MaterialButton btnThem;
        private System.Windows.Forms.DataGridView dgvDanhSachSach;
    }
}
