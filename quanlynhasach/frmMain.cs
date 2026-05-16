using ReaLTaiizor.Colors; 
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;   
using System;
using System.Windows.Forms;
using static ReaLTaiizor.Controls.HopeTabPage;

namespace quanlynhasach
{
    public partial class frmMain : MaterialForm
    {
        public frmMain()
        {
            InitializeComponent();
            SetupMaterialTheme();
            this.WindowState = FormWindowState.Maximized;

            PhanQuyenHeThong();
            AddUserControl(new UC_Home()); 

    
            this.Text = $"Hệ Thống Nhà Sách - Xin chào: {Session.HoTen} ({Session.ChucVu})";
        }

        private void PhanQuyenHeThong()
        {
            if (Session.ChucVu == "Nhân viên")
            {
                btnQuanLySach.Visible = false;
                btnMenuNhanVien.Visible = false;
                btnMenuHome.Visible = false;
            }

        }

        private void SetupMaterialTheme()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new MaterialColorScheme(
                System.Drawing.Color.FromArgb(63, 81, 181),   
                System.Drawing.Color.FromArgb(48, 63, 159),  
                System.Drawing.Color.FromArgb(197, 202, 233), 
                System.Drawing.Color.FromArgb(255, 64, 129), 
                System.Drawing.Color.White                   
            );
        }

        private void AddUserControl(UserControl uc)
        {
            pnlContent.Controls.Clear(); 
            uc.Dock = DockStyle.Fill;   
            pnlContent.Controls.Add(uc); 
            uc.BringToFront();
        }

        private void btnMenuHome_Click(object sender, EventArgs e)
        {
            AddUserControl(new UC_Home());

        }

        private void btnMenuPOS_Click(object sender, EventArgs e)
        {
            UC_POS posScreen = new UC_POS();
            AddUserControl(posScreen);
        }

        private void btnQuanLySach_Click(object sender, EventArgs e)
        {
            UC_QuanLySach sachScreen = new UC_QuanLySach();
            AddUserControl(sachScreen);
        }

        private void btnMenuNhanVien_Click(object sender, EventArgs e)
        {
            AddUserControl(new UC_QuanLyNhanVien());
        }

        private void btnMenuKhachHang_Click(object sender, EventArgs e)
        {
            AddUserControl(new UC_QuanLyKhachHang());
        }

        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            AddUserControl(new UC_QuanLyHoaDon());
        }
    }
}