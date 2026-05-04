using quanlynhasach.Controllers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace quanlynhasach
{
    public partial class UC_Home : UserControl
    {
        private ThongKeController thongKeController = new ThongKeController();

        public UC_Home()
        {
            InitializeComponent();
            FormatDataGridView(dgvTopSach);
            FormatDataGridView(dgvSapHetHang);
            LoadThongKe();
        }
        private void FormatDataGridView(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 35;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 63, 159);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }
        private void LoadThongKe()
        {
            lblTongKhach.Text = thongKeController.GetTongKhachHang().ToString();
            lblTongNhanVien.Text = thongKeController.GetTongNhanVien().ToString();
            lblTongSach.Text = thongKeController.GetTongDauSach().ToString();
            // 1. Hiển thị doanh thu hôm nay
            decimal doanhThu = thongKeController.GetDoanhThuHomNay();
            lblDoanhThuHomNay.Text = doanhThu.ToString("N0") + " VNĐ"; // Format N0 để có dấu phẩy phân cách hàng nghìn (VD: 1,200,000 VNĐ)

            // 2. Hiển thị số đơn hàng hôm nay
            int soDon = thongKeController.GetSoDonHangHomNay();
            lblSoDonHomNay.Text = soDon.ToString() + " đơn";

            // 3. Đổ dữ liệu vào bảng Top 5 Sách bán chạy
            DataTable dtTopSach = thongKeController.GetTop5SachBanChay();
            dgvTopSach.DataSource = dtTopSach;
            DataTable dtHetHang = thongKeController.GetSachSapHetHang();
            dgvSapHetHang.DataSource = dtHetHang;
            if (dgvSapHetHang.Columns.Count > 0)
            {
                dgvSapHetHang.Columns["Tên Sách"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSapHetHang.Columns["Mã"].Width = 50;
                dgvSapHetHang.Columns["Tồn Kho"].Width = 80;
                dgvSapHetHang.Columns["Tồn Kho"].DefaultCellStyle.ForeColor = System.Drawing.Color.Red; // Tô đỏ cột số lượng
                dgvSapHetHang.Columns["Tồn Kho"].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            // Làm đẹp cho bảng (Nếu có dữ liệu)
            if (dgvTopSach.Columns.Count > 0)
            {
                dgvTopSach.Columns["Tên Sách"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvTopSach.Columns["Tổng Số Lượng Đã Bán"].Width = 200;
                dgvTopSach.Columns["Tổng Số Lượng Đã Bán"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}