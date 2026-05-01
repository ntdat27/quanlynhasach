namespace quanlynhasach
{
    partial class UC_POS
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtTimKiem = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            this.dgvSach = new System.Windows.Forms.DataGridView();
            this.materialCard1 = new ReaLTaiizor.Controls.MaterialCard();
            this.txtSoDienThoai = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            this.btnThanhToan = new ReaLTaiizor.Controls.MaterialButton();
            this.lblTongTien = new ReaLTaiizor.Controls.MaterialLabel();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();
            this.materialCard1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtTimKiem);
            this.splitContainer1.Panel1.Controls.Add(this.dgvSach);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.materialCard1);
            this.splitContainer1.Panel2.Controls.Add(this.dgvGioHang);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 700);
            this.splitContainer1.SplitterDistance = 606;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.AnimateReadOnly = false;
            this.txtTimKiem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTimKiem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTimKiem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtTimKiem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtTimKiem.Depth = 0;
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTimKiem.HideSelection = true;
            this.txtTimKiem.Hint = "Nhập mã sách, tên sách hoặc quét mã vạch..";
            this.txtTimKiem.LeadingIcon = null;
            this.txtTimKiem.Location = new System.Drawing.Point(150, 3);
            this.txtTimKiem.MaxLength = 32767;
            this.txtTimKiem.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PasswordChar = '\0';
            this.txtTimKiem.PrefixSuffixText = null;
            this.txtTimKiem.ReadOnly = false;
            this.txtTimKiem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.SelectionLength = 0;
            this.txtTimKiem.SelectionStart = 0;
            this.txtTimKiem.ShortcutsEnabled = true;
            this.txtTimKiem.Size = new System.Drawing.Size(342, 48);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TabStop = false;
            this.txtTimKiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTimKiem.TrailingIcon = null;
            this.txtTimKiem.UseSystemPasswordChar = false;
            // 
            // dgvSach
            // 
            this.dgvSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSach.GridColor = System.Drawing.SystemColors.Control;
            this.dgvSach.Location = new System.Drawing.Point(3, 57);
            this.dgvSach.Name = "dgvSach";
            this.dgvSach.ReadOnly = true;
            this.dgvSach.Size = new System.Drawing.Size(600, 643);
            this.dgvSach.TabIndex = 1;
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.txtSoDienThoai);
            this.materialCard1.Controls.Add(this.btnThanhToan);
            this.materialCard1.Controls.Add(this.lblTongTien);
            this.materialCard1.Depth = 0;
            this.materialCard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(0, 413);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(390, 287);
            this.materialCard1.TabIndex = 1;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.AnimateReadOnly = false;
            this.txtSoDienThoai.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSoDienThoai.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSoDienThoai.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtSoDienThoai.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtSoDienThoai.Depth = 0;
            this.txtSoDienThoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSoDienThoai.HideSelection = true;
            this.txtSoDienThoai.Hint = "Nhập SĐT giảm giá";
            this.txtSoDienThoai.LeadingIcon = null;
            this.txtSoDienThoai.Location = new System.Drawing.Point(17, 17);
            this.txtSoDienThoai.MaxLength = 32767;
            this.txtSoDienThoai.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PasswordChar = '\0';
            this.txtSoDienThoai.PrefixSuffixText = null;
            this.txtSoDienThoai.ReadOnly = false;
            this.txtSoDienThoai.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.SelectionLength = 0;
            this.txtSoDienThoai.SelectionStart = 0;
            this.txtSoDienThoai.ShortcutsEnabled = true;
            this.txtSoDienThoai.Size = new System.Drawing.Size(250, 48);
            this.txtSoDienThoai.TabIndex = 3;
            this.txtSoDienThoai.TabStop = false;
            this.txtSoDienThoai.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSoDienThoai.TrailingIcon = null;
            this.txtSoDienThoai.UseSystemPasswordChar = false;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnThanhToan.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnThanhToan.Depth = 0;
            this.btnThanhToan.HighEmphasis = true;
            this.btnThanhToan.Icon = null;
            this.btnThanhToan.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnThanhToan.Location = new System.Drawing.Point(199, 213);
            this.btnThanhToan.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnThanhToan.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnThanhToan.Size = new System.Drawing.Size(114, 36);
            this.btnThanhToan.TabIndex = 2;
            this.btnThanhToan.Text = "Thanh Toán";
            this.btnThanhToan.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnThanhToan.UseAccentColor = false;
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Depth = 0;
            this.lblTongTien.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTongTien.Location = new System.Drawing.Point(17, 90);
            this.lblTongTien.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(46, 19);
            this.lblTongTien.TabIndex = 0;
            this.lblTongTien.Text = "0 VND";
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(2, 57);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.Size = new System.Drawing.Size(385, 350);
            this.dgvGioHang.TabIndex = 0;
            // 
            // UC_POS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UC_POS";
            this.Size = new System.Drawing.Size(1000, 700);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit txtTimKiem;
        private System.Windows.Forms.DataGridView dgvSach;
        private ReaLTaiizor.Controls.MaterialButton btnThanhToan;
        private ReaLTaiizor.Controls.MaterialCard materialCard1;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private ReaLTaiizor.Controls.MaterialLabel lblTongTien;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit txtSoDienThoai;
    }
}
