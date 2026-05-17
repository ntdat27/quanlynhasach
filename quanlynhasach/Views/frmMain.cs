using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;
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

            // 1. Phân quyền và "Trục xuất" hoàn toàn các nút thừa ra khỏi menu
            PhanQuyenHeThong();

            // 2. Sắp xếp lại những nút còn sót lại dồn từ trên xuống dưới
            ChinhDinhDangMenu();

            this.Text = $"Hệ Thống Nhà Sách - Xin chào: {Session.HoTen} ({Session.ChucVu})";
        }

        private void PhanQuyenHeThong()
        {
            string role = quanlynhasach.Models.Session.ChucVu;

            if (role == "Nhân viên")
            {
                // Xóa sổ hoàn toàn các nút khỏi thanh Sidebar (Không để lại vết mờ xám)
                pnlSidebar.Controls.Remove(btnQuanLySach);
                pnlSidebar.Controls.Remove(btnMenuNhanVien);
                pnlSidebar.Controls.Remove(btnMenuHome);
                pnlSidebar.Controls.Remove(btnLichSuHeThong);

                AddUserControl(new UC_POS());
            }
            else if (role == "Quản lý kho" || role == "Kho")
            {
                // Xóa sổ các nút của role Thủ kho
                pnlSidebar.Controls.Remove(btnMenuPOS);
                pnlSidebar.Controls.Remove(btnQuanLyHoaDon);
                pnlSidebar.Controls.Remove(btnMenuKhachHang);
                pnlSidebar.Controls.Remove(btnMenuNhanVien);
                pnlSidebar.Controls.Remove(btnMenuHome);

                AddUserControl(new UC_QuanLySach());
            }
            else
            {
                // Admin giữ nguyên không xóa nút nào
                AddUserControl(new UC_Home());
            }
        }

        private void ChinhDinhDangMenu()
        {
            int toaDoY = 10; // Bắt đầu cách mép trên 10px

            // Danh sách các nút theo thứ tự chuẩn từ trên xuống
            string[] danhSachMenuChuan = {
                "btnMenuPOS",
                "btnQuanLyHoaDon",
                "btnMenuKhachHang",
                "btnQuanLySach",
                "btnMenuNhanVien",
                "btnMenuHome",
                "btnLichSuHeThong"
            };

            foreach (string tenNut in danhSachMenuChuan)
            {
                // Chỉ sắp xếp những nút thực sự còn tồn tại trên Panel (không bị hàm bên trên xóa)
                if (pnlSidebar.Controls.ContainsKey(tenNut))
                {
                    Control ctrl = pnlSidebar.Controls[tenNut];

                    // Tắt AutoSize để ép kích thước theo ý muốn
                    ctrl.AutoSize = false;
                    if (ctrl is ReaLTaiizor.Controls.MaterialButton mBtn)
                    {
                        mBtn.AutoSizeMode = AutoSizeMode.GrowOnly;
                    }

                    ctrl.Width = pnlSidebar.Width - 20;
                    ctrl.Height = 45;

                    // Xếp vị trí và cộng dồn tọa độ Y để nút tiếp theo xếp ngay bên dưới
                    ctrl.Location = new Point(10, toaDoY);
                    toaDoY += 55;
                }
            }

            // Xử lý nút ĐỔI MẬT KHẨU (Ép ghim sát đáy, cách đáy 75px)
            string nameDMK = pnlSidebar.Controls.ContainsKey("btnDoiMatKhau") ? "btnDoiMatKhau" : "btnDoiMatKhauMenu";
            if (pnlSidebar.Controls.ContainsKey(nameDMK))
            {
                Control btnDMK = pnlSidebar.Controls[nameDMK];
                btnDMK.AutoSize = false;
                if (btnDMK is ReaLTaiizor.Controls.MaterialButton mBtnDMK)
                {
                    mBtnDMK.AutoSizeMode = AutoSizeMode.GrowOnly;
                }

                btnDMK.Width = pnlSidebar.Width - 20;
                btnDMK.Height = 45;
                btnDMK.Location = new Point(10, pnlSidebar.Height - btnDMK.Height - 75);
                btnDMK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }

            // Xử lý nút ĐĂNG XUẤT (Ép ghim sát đáy cùng, cách đáy 20px)
            if (pnlSidebar.Controls.ContainsKey("btnDangXuat"))
            {
                Control btnDX = pnlSidebar.Controls["btnDangXuat"];
                btnDX.AutoSize = false;
                if (btnDX is ReaLTaiizor.Controls.MaterialButton mBtnDX)
                {
                    mBtnDX.AutoSizeMode = AutoSizeMode.GrowOnly;
                }

                btnDX.Width = pnlSidebar.Width - 20;
                btnDX.Height = 45;
                btnDX.Location = new Point(10, pnlSidebar.Height - btnDX.Height - 20);
                btnDX.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void SetupMaterialTheme()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new MaterialColorScheme(
                Color.FromArgb(63, 81, 181),
                Color.FromArgb(48, 63, 159),
                Color.FromArgb(197, 202, 233),
                Color.FromArgb(255, 64, 129),
                Color.White
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
            AddUserControl(new UC_POS());
        }

        private void btnQuanLySach_Click(object sender, EventArgs e)
        {
            AddUserControl(new UC_QuanLySach());
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
            this.Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            this.Close();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            frm.ShowDialog();
        }
    }
}