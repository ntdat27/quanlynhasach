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

            // Mặc định khi vừa mở lên là xem dữ liệu của Ngày Hôm Nay
            if (dtpTuNgay != null && dtpDenNgay != null)
            {
                dtpTuNgay.Value = DateTime.Now;
                dtpDenNgay.Value = DateTime.Now;
            }

            LoadThongKe(DateTime.Now, DateTime.Now);
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

        // Đã cập nhật hàm LoadThongKe nhận vào khoảng thời gian
        private void LoadThongKe(DateTime tuNgay, DateTime denNgay)
        {
            lblTongKhach.Text = thongKeController.GetTongKhachHang().ToString();
            lblTongNhanVien.Text = thongKeController.GetTongNhanVien().ToString();
            lblTongSach.Text = thongKeController.GetTongDauSach().ToString();

            // Gọi hàm thống kê động có khoảng thời gian
            decimal doanhThu = thongKeController.GetDoanhThu(tuNgay, denNgay);
            lblDoanhThuHomNay.Text = doanhThu.ToString("N0") + " VNĐ";

            int soDon = thongKeController.GetSoDonHang(tuNgay, denNgay);
            lblSoDonHomNay.Text = soDon.ToString() + " đơn";

            // Phần biểu đồ Top và Cảnh báo giữ nguyên logic
            DataTable dtTopSach = thongKeController.GetTop5SachBanChay();
            dgvTopSach.DataSource = dtTopSach;

            DataTable dtHetHang = thongKeController.GetSachSapHetHang();
            dgvSapHetHang.DataSource = dtHetHang;

            if (dgvSapHetHang.Columns.Count > 0)
            {
                dgvSapHetHang.Columns["Tên Sách"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSapHetHang.Columns["Mã"].Width = 50;
                dgvSapHetHang.Columns["Tồn Kho"].Width = 80;
                dgvSapHetHang.Columns["Tồn Kho"].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                dgvSapHetHang.Columns["Tồn Kho"].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            if (dgvTopSach.Columns.Count > 0)
            {
                dgvTopSach.Columns["Tên Sách"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvTopSach.Columns["Tổng Số Lượng Đã Bán"].Width = 200;
                dgvTopSach.Columns["Tổng Số Lượng Đã Bán"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // Sự kiện khi bấm nút Lọc Dữ Liệu
        private void btnLoc_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            // Kiểm tra tính hợp lệ của ngày
            if (tuNgay > denNgay)
            {
                MessageBox.Show("Khoảng thời gian không hợp lệ! 'Từ ngày' không được lớn hơn 'Đến ngày'.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm tải lại dữ liệu theo ngày được chọn
            LoadThongKe(tuNgay, denNgay);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = thongKeController.GetTop5SachBanChay();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu báo cáo thống kê";
            sfd.FileName = "BaoCao_TopSach_" + DateTime.Now.ToString("ddMMyyyy");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Top Sách");

                        worksheet.Cell("A1").Value = "BÁO CÁO TOP 5 SÁCH BÁN CHẠY NHẤT";
                        worksheet.Range("A1:B1").Merge();
                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("A1").Style.Font.FontSize = 16;
                        worksheet.Cell("A1").Style.Font.FontColor = XLColor.White;
                        worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.DarkBlue;
                        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        worksheet.Cell("A2").Value = "Ngày lập: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Range("A2:B2").Merge();
                        worksheet.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell("A2").Style.Font.Italic = true;

                        var table = worksheet.Cell(4, 1).InsertTable(dt);

                        table.Theme = XLTableTheme.TableStyleMedium2;

                        worksheet.Columns().AdjustToContents();

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