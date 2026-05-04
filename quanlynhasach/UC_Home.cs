using quanlynhasach.Controllers;
using System;
using ClosedXML.Excel;
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

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = thongKeController.GetTop5SachBanChay();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Mở hộp thoại cho khách chọn nơi lưu file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu báo cáo thống kê";
            sfd.FileName = "BaoCao_TopSach_" + DateTime.Now.ToString("ddMMyyyy");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 3. Khởi tạo file Excel bằng ClosedXML
                    using (var workbook = new XLWorkbook())
                    {
                        // Tạo 1 Sheet tên là "Top Sách"
                        var worksheet = workbook.Worksheets.Add("Top Sách");

                        // --- TRANG TRÍ TIÊU ĐỀ BÁO CÁO ---
                        worksheet.Cell("A1").Value = "BÁO CÁO TOP 5 SÁCH BÁN CHẠY NHẤT";
                        worksheet.Range("A1:B1").Merge(); // Gộp ô A1 và B1 lại với nhau
                        worksheet.Cell("A1").Style.Font.Bold = true; // In đậm
                        worksheet.Cell("A1").Style.Font.FontSize = 16; // Chữ to
                        worksheet.Cell("A1").Style.Font.FontColor = XLColor.White; // Chữ trắng
                        worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.DarkBlue; // Nền xanh đậm
                        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Căn giữa

                        // Thêm ngày xuất báo cáo
                        worksheet.Cell("A2").Value = "Ngày lập: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Range("A2:B2").Merge();
                        worksheet.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell("A2").Style.Font.Italic = true; // In nghiêng

                        // --- ĐỔ DỮ LIỆU TỪ BẢNG VÀO EXCEL ---
                        // Bắt đầu chèn bảng từ ô A4. Hàm InsertTable siêu xịn tự động làm đủ trò!
                        var table = worksheet.Cell(4, 1).InsertTable(dt);

                        // Áp dụng Theme (Giao diện) cho bảng giống hệt chức năng Format as Table trong Excel
                        table.Theme = XLTableTheme.TableStyleMedium2; // Sẽ ra viền và màu xanh nhạt xen kẽ cực kỳ chuyên nghiệp

                        // Tự động căn giãn độ rộng tất cả các cột cho vừa khít với chữ
                        worksheet.Columns().AdjustToContents();

                        // 4. Lưu file và thông báo
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Đã xuất file Excel thành công!", "Tuyệt vời", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}