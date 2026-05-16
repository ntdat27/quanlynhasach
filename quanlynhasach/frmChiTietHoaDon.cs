using quanlynhasach.Controllers; 
using ReaLTaiizor.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace quanlynhasach
{
    public partial class frmChiTietHoaDon : MaterialForm
    {
        private ThongKeController tkController = new ThongKeController();
        private int _maHD;

        public frmChiTietHoaDon(int maHD)
        {
            InitializeComponent();
            _maHD = maHD;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CHI TIẾT HÓA ĐƠN #" + maHD;
            FormatDataGridView(dgvChiTietSach);
            LoadChiTiet();
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

        private void LoadChiTiet()
        {
            DataTable dtChung = tkController.GetThongTinChungHoaDon(_maHD);
            if (dtChung.Rows.Count > 0)
            {
                DataRow row = dtChung.Rows[0];
                lblNgay.Text = "Ngày lập: " + Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy HH:mm");

                string tenKhach = row["TenKhach"].ToString();
                string hangTV = row["TenHang"].ToString();
                lblKhach.Text = $"Khách hàng: {tenKhach} - [{hangTV}]";

                lblSdt.Text = "SĐT: " + row["SdtKhach"].ToString();
                lblNhanVien.Text = "Thu ngân: " + row["TenNhanVien"].ToString();

                decimal tongTien = Convert.ToDecimal(row["TongTien"]);
                decimal phanTram = Convert.ToDecimal(row["PhanTramGiam"]);
                decimal tienGiam = Convert.ToDecimal(row["TienGiam"]);
                decimal thanhToan = Convert.ToDecimal(row["ThanhToan"]);

                lblTongTienHang.Text = $"Tổng tiền hàng: {tongTien:N0} VNĐ";

                if (phanTram > 0)
                {
                    lblGiamGia.Text = $"Giảm giá ({phanTram}%): -{tienGiam:N0} VNĐ";
                }
                else
                {
                    lblGiamGia.Text = "Giảm giá (0%): 0 VNĐ";
                }

                lblThanhToan.Text = $"Khách cần trả: {thanhToan:N0} VNĐ";
            }
            DataTable dtSach = tkController.GetDanhSachSachTrongHoaDon(_maHD);
            dgvChiTietSach.DataSource = dtSach;

            if (dgvChiTietSach.Columns.Count > 0)
            {
                dgvChiTietSach.Columns["Tên Sách"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvChiTietSach.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                dgvChiTietSach.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
            }
        }
    }
}