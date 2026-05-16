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

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtTaiKhoan.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nvController.DangNhap(user, pass))
            {
                this.Hide();

                frmMain main = new frmMain();
                main.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtMatKhau.Focus(); 
            }
        }
    }
}