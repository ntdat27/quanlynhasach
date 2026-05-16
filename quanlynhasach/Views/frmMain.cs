using ReaLTaiizor.Colors; 
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;   
using System;
using System.Windows.Forms;
using static ReaLTaiizor.Controls.HopeTabPage;
using quanlynhasach.Models;

namespace quanlynhasach
{
    public partial class frmMain : MaterialForm
    {
        public frmMain()
        {
            InitializeComponent();
            SetupMaterialTheme();
            this.WindowState = FormWindowState.Maximized;
            ChinhDinhDangMenu(); // GỌI HÀM NÀY Ở ĐÂY ĐỂ CĂN ĐỀU MENU
            PhanQuyenHeThong();
            AddUserControl(new UC_Home()); 

    
            this.Text = $"Hệ Thống Nhà Sách - Xin chào: {Session.HoTen} ({Session.ChucVu})";
        }
        private void ChinhDinhDangMenu()
        {
            int toaDoY = 10; // Căn cách lề trên 10px

            foreach (Control ctrl in pnlSidebar.Controls)
            {
                // Bỏ qua nút Đăng xuất, chỉ xếp hàng cho các nút Menu chính
                if (ctrl is ReaLTaiizor.Controls.MaterialButton btn && btn.Name != "btnDangXuat")
                {
                    btn.AutoSize = false;
                    btn.AutoSizeMode = AutoSizeMode.GrowOnly;

                    btn.Width = pnlSidebar.Width - 20;
                    btn.Height = 45;

                    btn.Location = new System.Drawing.Point(10, toaDoY);
                    toaDoY += 55;
                }
            }

            // Xử lý riêng cho nút Đăng Xuất (Ép nó xuống sát đáy Sidebar)
            if (pnlSidebar.Controls.ContainsKey("btnDangXuat"))
            {
                Control btnDX = pnlSidebar.Controls["btnDangXuat"];
                if (btnDX is ReaLTaiizor.Controls.MaterialButton btnDangXuat)
                {
                    btnDangXuat.AutoSize = false;
                    btnDangXuat.AutoSizeMode = AutoSizeMode.GrowOnly;

                    btnDangXuat.Width = pnlSidebar.Width - 20;
                    btnDangXuat.Height = 45;

                    // Tính toán tọa độ Y ép xuống đáy (cách đáy 20px)
                    btnDangXuat.Location = new System.Drawing.Point(10, pnlSidebar.Height - btnDangXuat.Height - 20);

                    // Neo chặt (Anchor) vào đáy để khi bạn phóng to/thu nhỏ cửa sổ, nút tự động trôi theo đáy
                    btnDangXuat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                }
            }
        }
        private void PhanQuyenHeThong()
        {
            if (Session.ChucVu == "Nhân viên")
            {
                btnQuanLySach.Visible = false;
                btnMenuNhanVien.Visible = false;
                btnMenuHome.Visible = false;
                btnLichSuHeThong.Visible = false;
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

        private void btnLichSuHeThong_Click(object sender, EventArgs e)
        {
            AddUserControl(new UC_LichSuHoatDong());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            quanlynhasach.Models.Session.Clear();

            // Mở lại màn hình đăng nhập
            this.Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            this.Close();
        }
    }
}