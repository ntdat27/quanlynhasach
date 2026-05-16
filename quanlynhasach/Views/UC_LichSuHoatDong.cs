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

            // Định dạng bảng hiển thị giống chuẩn các form trước của bạn
            FormatDataGridView(dgvLichSu);

            // Tự động tải dữ liệu khi mở màn hình
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
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 63, 159); // Màu xanh đậm giống hệ thống của bạn
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }

        private void LoadData()
        {
            try
            {
                // Gọi Controller lấy dữ liệu từ database về
                DataTable dt = lsController.GetDanhSachLichSu();
                dgvLichSu.DataSource = dt;

                // Việt hóa lại tiêu đề các cột đổ từ database lên cho đẹp mắt
                if (dgvLichSu.Columns.Contains("MaLog")) dgvLichSu.Columns["MaLog"].HeaderText = "Mã";
                if (dgvLichSu.Columns.Contains("HoTen")) dgvLichSu.Columns["HoTen"].HeaderText = "Nhân viên";
                if (dgvLichSu.Columns.Contains("TaiKhoan")) dgvLichSu.Columns["TaiKhoan"].HeaderText = "Tài khoản";

                if (dgvLichSu.Columns.Contains("HanhDong"))
                {
                    dgvLichSu.Columns["HanhDong"].HeaderText = "Hành động thực hiện";
                    dgvLichSu.Columns["HanhDong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Tự động kéo dãn cột hành động
                }

                if (dgvLichSu.Columns.Contains("ThoiGian"))
                {
                    dgvLichSu.Columns["ThoiGian"].HeaderText = "Thời gian";
                    dgvLichSu.Columns["ThoiGian"].Width = 180;
                    dgvLichSu.Columns["ThoiGian"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"; // Định dạng ngày giờ Việt Nam
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử hoạt động: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData(); // Bấm nút thì tải lại dữ liệu mới nhất
        }
    }
}