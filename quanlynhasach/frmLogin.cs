using quanlynhasach.Controllers;
using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.Windows.Forms;

namespace quanlynhasach
{
    public partial class frmLogin : MaterialForm
    {
        private NhanVienController nvController = new NhanVienController();

        public frmLogin()
        {
            InitializeComponent();
            SetupMaterialTheme();

            // Cấu hình form Login cơ bản
            this.StartPosition = FormStartPosition.CenterScreen; // Luôn ra giữa màn hình
            this.MaximizeBox = false; // Tắt nút phóng to (Form Login không cần phóng to)
            this.Sizable = false;     // Không cho kéo giãn lôm côm
            this.Text = "ĐĂNG NHẬP HỆ THỐNG";
        }

        private void SetupMaterialTheme()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Dùng tông màu Xanh Indigo đồng nhất với frmMain
            materialSkinManager.ColorScheme = new MaterialColorScheme(
                System.Drawing.Color.FromArgb(63, 81, 181),
                System.Drawing.Color.FromArgb(48, 63, 159),
                System.Drawing.Color.FromArgb(197, 202, 233),
                System.Drawing.Color.FromArgb(255, 64, 129),
                System.Drawing.Color.White
            );
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtTaiKhoan.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            // 1. Bắt lỗi để trống
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Gọi Controller xuống Database kiểm tra
            if (nvController.DangNhap(user, pass))
            {
                // Đăng nhập thành công -> Ẩn form Login đi
                this.Hide();

                // Mở màn hình chính lên
                frmMain main = new frmMain();
                main.ShowDialog();

                // Khi người dùng tắt frmMain (ShowDialog kết thúc) -> Tắt luôn ứng dụng
                this.Close();
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtMatKhau.Focus(); // Trỏ chuột lại vào ô mật khẩu cho người dùng gõ lại
            }
        }
    }
}