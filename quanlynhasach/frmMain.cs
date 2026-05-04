using ReaLTaiizor.Colors; // Keep this for other color-related types
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;   // Chứa các Enum: Primary, Accent, TextShade
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

            // 1. Phân quyền ngay khi form được khởi tạo
            PhanQuyenHeThong();
            AddUserControl(new UC_Home()); // <--- THÊM DÒNG NÀY VÀO ĐÂY

            // 2. Mặc định mở màn hình TRANG CHỦ thay vì để trống
            // (Đảm bảo bạn đã tạo file UC_Home.cs)

            // 3. Hiển thị tên nhân viên đăng nhập lên tiêu đề của Form
            // (Đảm bảo class Session đã được tạo và có dữ liệu từ lúc Login)
            this.Text = $"Hệ Thống Nhà Sách - Xin chào: {Session.HoTen} ({Session.ChucVu})";
        }

        private void PhanQuyenHeThong()
        {
            // Nếu chức vụ là "Nhân viên" thì ẩn các nút quản lý đi
            // Tên nút phải khớp đúng với tên (Name) bạn đặt trong Designer
            if (Session.ChucVu == "Nhân viên")
            {
                btnQuanLySach.Visible = false;
                btnMenuNhanVien.Visible = false;
                btnMenuKhachHang.Visible = false;
            }

        }

        private void SetupMaterialTheme()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Viết đầy đủ đường dẫn System.Drawing.Color để C# không báo lỗi
            materialSkinManager.ColorScheme = new MaterialColorScheme(
                System.Drawing.Color.FromArgb(63, 81, 181),   // primary: Xanh Indigo 500
                System.Drawing.Color.FromArgb(48, 63, 159),   // darkPrimary: Xanh Indigo 700
                System.Drawing.Color.FromArgb(197, 202, 233), // lightPrimary: Xanh Indigo 100
                System.Drawing.Color.FromArgb(255, 64, 129),  // accent: Hồng Pink 200
                System.Drawing.Color.White                    // textShade: Chữ màu trắng
            );
        }

        private void AddUserControl(UserControl uc)
        {
            pnlContent.Controls.Clear(); // Xóa sạch màn hình cũ
            uc.Dock = DockStyle.Fill;    // Phóng to màn hình mới lấp đầy vùng trống
            pnlContent.Controls.Add(uc); // Gắn vào
            uc.BringToFront();
        }

        // =====================================
        // CÁC SỰ KIỆN CHUYỂN TRANG BÊN SIDEBAR
        // =====================================

        // Nút Trang Chủ mới thêm
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