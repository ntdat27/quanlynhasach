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

        private void btnMenuPOS_Click(object sender, EventArgs e)
        {
            // Đảm bảo bạn đã tạo file UC_POS.cs thì dòng này mới không bị gạch đỏ nhé
            UC_POS posScreen = new UC_POS();
            AddUserControl(posScreen);
        }

        private void btnQuanLySach_Click(object sender, EventArgs e)
        {
            UC_QuanLySach sachScreen = new UC_QuanLySach();
            AddUserControl(sachScreen);
        }
    }
}