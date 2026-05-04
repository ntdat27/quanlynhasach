namespace quanlynhasach
{
    partial class frmMain
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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnMenuHome = new ReaLTaiizor.Controls.MaterialButton();
            this.btnMenuKhachHang = new ReaLTaiizor.Controls.MaterialButton();
            this.btnMenuNhanVien = new ReaLTaiizor.Controls.MaterialButton();
            this.btnQuanLySach = new ReaLTaiizor.Controls.MaterialButton();
            this.btnMenuPOS = new ReaLTaiizor.Controls.MaterialButton();
            this.btnQuanLyHoaDon = new ReaLTaiizor.Controls.MaterialButton();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(203, 64);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(794, 633);
            this.pnlContent.TabIndex = 0;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlSidebar.Controls.Add(this.btnQuanLyHoaDon);
            this.pnlSidebar.Controls.Add(this.btnMenuHome);
            this.pnlSidebar.Controls.Add(this.btnMenuKhachHang);
            this.pnlSidebar.Controls.Add(this.btnMenuNhanVien);
            this.pnlSidebar.Controls.Add(this.btnQuanLySach);
            this.pnlSidebar.Controls.Add(this.btnMenuPOS);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(3, 64);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(200, 633);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnMenuHome
            // 
            this.btnMenuHome.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenuHome.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnMenuHome.Depth = 0;
            this.btnMenuHome.HighEmphasis = true;
            this.btnMenuHome.Icon = null;
            this.btnMenuHome.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnMenuHome.Location = new System.Drawing.Point(4, 578);
            this.btnMenuHome.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnMenuHome.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnMenuHome.Name = "btnMenuHome";
            this.btnMenuHome.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnMenuHome.Size = new System.Drawing.Size(64, 36);
            this.btnMenuHome.TabIndex = 4;
            this.btnMenuHome.Text = "HOME";
            this.btnMenuHome.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnMenuHome.UseAccentColor = false;
            this.btnMenuHome.UseVisualStyleBackColor = true;
            this.btnMenuHome.Click += new System.EventHandler(this.btnMenuHome_Click);
            // 
            // btnMenuKhachHang
            // 
            this.btnMenuKhachHang.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenuKhachHang.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnMenuKhachHang.Depth = 0;
            this.btnMenuKhachHang.HighEmphasis = true;
            this.btnMenuKhachHang.Icon = null;
            this.btnMenuKhachHang.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnMenuKhachHang.Location = new System.Drawing.Point(16, 150);
            this.btnMenuKhachHang.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnMenuKhachHang.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnMenuKhachHang.Name = "btnMenuKhachHang";
            this.btnMenuKhachHang.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnMenuKhachHang.Size = new System.Drawing.Size(180, 36);
            this.btnMenuKhachHang.TabIndex = 4;
            this.btnMenuKhachHang.Text = "Quản Lý Khách Hàng";
            this.btnMenuKhachHang.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnMenuKhachHang.UseAccentColor = false;
            this.btnMenuKhachHang.UseVisualStyleBackColor = true;
            this.btnMenuKhachHang.Click += new System.EventHandler(this.btnMenuKhachHang_Click);
            // 
            // btnMenuNhanVien
            // 
            this.btnMenuNhanVien.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenuNhanVien.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnMenuNhanVien.Depth = 0;
            this.btnMenuNhanVien.HighEmphasis = true;
            this.btnMenuNhanVien.Icon = null;
            this.btnMenuNhanVien.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnMenuNhanVien.Location = new System.Drawing.Point(16, 102);
            this.btnMenuNhanVien.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnMenuNhanVien.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnMenuNhanVien.Name = "btnMenuNhanVien";
            this.btnMenuNhanVien.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnMenuNhanVien.Size = new System.Drawing.Size(163, 36);
            this.btnMenuNhanVien.TabIndex = 3;
            this.btnMenuNhanVien.Text = "Quản Lý Nhân Viên";
            this.btnMenuNhanVien.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnMenuNhanVien.UseAccentColor = false;
            this.btnMenuNhanVien.UseVisualStyleBackColor = true;
            this.btnMenuNhanVien.Click += new System.EventHandler(this.btnMenuNhanVien_Click);
            // 
            // btnQuanLySach
            // 
            this.btnQuanLySach.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuanLySach.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnQuanLySach.Depth = 0;
            this.btnQuanLySach.HighEmphasis = true;
            this.btnQuanLySach.Icon = null;
            this.btnQuanLySach.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnQuanLySach.Location = new System.Drawing.Point(16, 54);
            this.btnQuanLySach.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnQuanLySach.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnQuanLySach.Name = "btnQuanLySach";
            this.btnQuanLySach.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnQuanLySach.Size = new System.Drawing.Size(124, 36);
            this.btnQuanLySach.TabIndex = 2;
            this.btnQuanLySach.Text = "Quản Lý Sách";
            this.btnQuanLySach.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnQuanLySach.UseAccentColor = false;
            this.btnQuanLySach.UseVisualStyleBackColor = true;
            this.btnQuanLySach.Click += new System.EventHandler(this.btnQuanLySach_Click);
            // 
            // btnMenuPOS
            // 
            this.btnMenuPOS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenuPOS.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnMenuPOS.Depth = 0;
            this.btnMenuPOS.HighEmphasis = true;
            this.btnMenuPOS.Icon = null;
            this.btnMenuPOS.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnMenuPOS.Location = new System.Drawing.Point(16, 6);
            this.btnMenuPOS.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnMenuPOS.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnMenuPOS.Name = "btnMenuPOS";
            this.btnMenuPOS.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnMenuPOS.Size = new System.Drawing.Size(95, 36);
            this.btnMenuPOS.TabIndex = 1;
            this.btnMenuPOS.Text = "Bán Hàng";
            this.btnMenuPOS.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnMenuPOS.UseAccentColor = false;
            this.btnMenuPOS.UseVisualStyleBackColor = true;
            this.btnMenuPOS.Click += new System.EventHandler(this.btnMenuPOS_Click);
            // 
            // btnQuanLyHoaDon
            // 
            this.btnQuanLyHoaDon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuanLyHoaDon.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnQuanLyHoaDon.Depth = 0;
            this.btnQuanLyHoaDon.HighEmphasis = true;
            this.btnQuanLyHoaDon.Icon = null;
            this.btnQuanLyHoaDon.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnQuanLyHoaDon.Location = new System.Drawing.Point(16, 198);
            this.btnQuanLyHoaDon.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnQuanLyHoaDon.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnQuanLyHoaDon.Name = "btnQuanLyHoaDon";
            this.btnQuanLyHoaDon.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnQuanLyHoaDon.Size = new System.Drawing.Size(180, 36);
            this.btnQuanLyHoaDon.TabIndex = 5;
            this.btnQuanLyHoaDon.Text = "QUẢN LÝ HÓA ĐƠN";
            this.btnQuanLyHoaDon.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnQuanLyHoaDon.UseAccentColor = false;
            this.btnQuanLyHoaDon.UseVisualStyleBackColor = true;
            this.btnQuanLyHoaDon.Click += new System.EventHandler(this.btnQuanLyHoaDon_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlSidebar;
        private ReaLTaiizor.Controls.MaterialButton btnMenuPOS;
        private ReaLTaiizor.Controls.MaterialButton btnQuanLySach;
        private ReaLTaiizor.Controls.MaterialButton btnMenuNhanVien;
        private ReaLTaiizor.Controls.MaterialButton btnMenuKhachHang;
        private ReaLTaiizor.Controls.MaterialButton btnMenuHome;
        private ReaLTaiizor.Controls.MaterialButton btnQuanLyHoaDon;
    }
}

