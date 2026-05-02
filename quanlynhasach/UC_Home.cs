using System;
using System.Windows.Forms;
using quanlynhasach.Controllers;

namespace quanlynhasach
{
    public partial class UC_Home : UserControl
    {
        private ThongKeController tkController = new ThongKeController();

        public UC_Home()
        {
            InitializeComponent();
            LoadThongKe(); // Gọi hàm tải dữ liệu ngay khi màn hình vừa bật lên
        }

        private void LoadThongKe()
        {
            try
            {
                lblTongKhach.Text = tkController.GetTongKhachHang().ToString();
                lblTongNhanVien.Text = tkController.GetTongNhanVien().ToString();
                lblTongSach.Text = tkController.GetTongDauSach().ToString();
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi nếu lỡ label chưa được tạo trên giao diện
                Console.WriteLine(ex.Message);
            }
        }
    }
}