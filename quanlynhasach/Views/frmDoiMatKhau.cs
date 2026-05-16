using quanlynhasach.Controllers;
using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.Windows.Forms;
using quanlynhasach.Models;

namespace quanlynhasach
{
    public partial class frmDoiMatKhau : MaterialForm
    {
        private NhanVienController nvController = new NhanVienController();

        public frmDoiMatKhau()
        {
            InitializeComponent();
            this.Text = "Đổi Mật Khẩu Cá Nhân";
            SetupMaterialTheme();

            this.StartPosition = FormStartPosition.CenterScreen; 
            this.MaximizeBox = false; 
            this.Sizable = false;    
            this.Text = "ĐĂNG NHẬP HỆ THỐNG";
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

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string mkCu = txtMatKhauCu.Text.Trim();
            string mkMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtXacNhanMatKhau.Text.Trim();

            // 1. Kiểm tra xem đã nhập đủ thông tin chưa
            if (string.IsNullOrEmpty(mkCu) || string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu mới và xác nhận mật khẩu mới có khớp nhau không
            if (mkMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu mới và Xác nhận mật khẩu không trùng khớp nhau!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Kiểm tra độ dài mật khẩu mới cho an toàn (Ví dụ: tối thiểu 3 ký tự)
            if (mkMoi.Length < 3)
            {
                MessageBox.Show("Mật khẩu mới phải có độ dài tối thiểu từ 3 ký tự trở lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Gọi Controller xử lý xuống CSDL thông qua Session.MaNV đang lưu ngầm
            bool result = nvController.DoiMatKhau(Session.MaNV, mkCu, mkMoi);

            if (result)
            {
                // Ghi nhận hành động vào lịch sử hệ thống
                LichSuController.GhiLog(Session.MaNV, "Đổi mật khẩu cá nhân thành công");

                MessageBox.Show("Thay đổi mật khẩu thành công!", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form popup lại
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không chính xác! Vui lòng kiểm tra lại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close(); // Bấm hủy thì đóng form

        }
    }
}