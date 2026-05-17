using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using quanlynhasach.Controllers;

namespace quanlynhasach
{
    public partial class UC_LichSuHoatDong : UserControl
    {
        private LichSuController lsController = new LichSuController();

        public UC_LichSuHoatDong()
        {
            InitializeComponent();

            FormatDataGridView(dgvLichSu);
            LoadData();
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
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 63, 159);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }

        // HÀM MỚI: Tách logic đổ DataTable và Việt hóa cột ra dùng chung để tránh code rác và chống lỗi Crash
        private void HienThiDuLieuLenBang(DataTable dt)
        {
            // Đổ trực tiếp dữ liệu thay vì dùng vòng lặp Add thủ công
            dgvLichSu.DataSource = dt;

            // Việt hóa lại tiêu đề các cột
            if (dgvLichSu.Columns.Contains("MaLog")) dgvLichSu.Columns["MaLog"].HeaderText = "Mã";
            if (dgvLichSu.Columns.Contains("HoTen")) dgvLichSu.Columns["HoTen"].HeaderText = "Nhân viên";
            if (dgvLichSu.Columns.Contains("TaiKhoan")) dgvLichSu.Columns["TaiKhoan"].HeaderText = "Tài khoản";

            if (dgvLichSu.Columns.Contains("HanhDong"))
            {
                dgvLichSu.Columns["HanhDong"].HeaderText = "Hành động thực hiện";
                dgvLichSu.Columns["HanhDong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvLichSu.Columns.Contains("ThoiGian"))
            {
                dgvLichSu.Columns["ThoiGian"].HeaderText = "Thời gian";
                dgvLichSu.Columns["ThoiGian"].Width = 180;
                dgvLichSu.Columns["ThoiGian"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = lsController.GetDanhSachLichSu();
                HienThiDuLieuLenBang(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử hoạt động: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Mốc 'Từ ngày' không được lớn hơn 'Đến ngày'!", "Lỗi chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DataTable dt = lsController.GetLichSuTheoKhoangThoiGian(tuNgay, denNgay);
                HienThiDuLieuLenBang(dt); // Gọi hàm hiển thị chung, hết sạch lỗi "Cannot clear this list"!
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
            }
        }
    }
}