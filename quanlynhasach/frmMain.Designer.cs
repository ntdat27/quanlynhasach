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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMenuPOS = new ReaLTaiizor.Controls.MaterialButton();
            this.btnQuanLySach = new ReaLTaiizor.Controls.MaterialButton();
            this.btnMenuNhanVien = new ReaLTaiizor.Controls.MaterialButton();
            this.pnlContent.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(3, 64);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(994, 633);
            this.pnlContent.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnMenuNhanVien);
            this.panel1.Controls.Add(this.btnQuanLySach);
            this.panel1.Controls.Add(this.btnMenuPOS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 633);
            this.panel1.TabIndex = 0;
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
            this.btnMenuNhanVien.Size = new System.Drawing.Size(124, 36);
            this.btnMenuNhanVien.TabIndex = 3;
            this.btnMenuNhanVien.Text = "Quản Lý Nhân Viên";
            this.btnMenuNhanVien.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnMenuNhanVien.UseAccentColor = false;
            this.btnMenuNhanVien.UseVisualStyleBackColor = true;
            this.btnMenuNhanVien.Click += new System.EventHandler(this.btnMenuNhanVien_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlContent);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.pnlContent.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.MaterialButton btnMenuPOS;
        private ReaLTaiizor.Controls.MaterialButton btnQuanLySach;
        private ReaLTaiizor.Controls.MaterialButton btnMenuNhanVien;
    }
}

