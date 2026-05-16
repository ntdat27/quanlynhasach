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

            // KHẮC PHỤC CHÍ MẠNG: Khai báo mảng cố định thứ tự xuất hiện từ trên xuống dưới
            string[] danhSachMenuChuan = {
                "btnMenuPOS",
                "btnQuanLyHoaDon",
                "btnMenuKhachHang",
                "btnQuanLySach",
                "btnMenuNhanVien",
                "btnMenuHome",
                "btnLichSuHeThong"
            };

            // Duyệt theo mảng chuẩn để xếp giao diện chứ không duyệt ngẫu nhiên theo Controls nữa
            foreach (string tenNut in danhSachMenuChuan)
            {
                if (pnlSidebar.Controls.ContainsKey(tenNut))
                {
                    Control ctrl = pnlSidebar.Controls[tenNut];
                    ctrl.AutoSize = false;
                    if (ctrl is ReaLTaiizor.Controls.MaterialButton mBtn)
                    {
                        mBtn.AutoSizeMode = AutoSizeMode.GrowOnly;
                    }

                    ctrl.Width = pnlSidebar.Width - 20;
                    ctrl.Height = 45;

                    ctrl.Location = new System.Drawing.Point(10, toaDoY);
                    toaDoY += 55; // Khoảng cách đều giữa các nút
                }
            }

            // 1. Xử lý nút ĐỔI MẬT KHẨU (Ép nằm cách đáy 75px - luôn nằm TRÊN nút Đăng xuất)
            // Check cả 2 trường hợp đặt tên đề phòng bạn viết là btnDoiMatKhau hoặc btnDoiMatKhauMenu
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
                btnDMK.Location = new System.Drawing.Point(10, pnlSidebar.Height - btnDMK.Height - 75);
                btnDMK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }

            // 2. Xử lý nút ĐĂNG XUẤT (Ép xuống đáy cùng, cách đáy 20px)
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
                btnDX.Location = new System.Drawing.Point(10, pnlSidebar.Height - btnDX.Height - 20);
                btnDX.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            frm.ShowDialog();
        }
    }
}