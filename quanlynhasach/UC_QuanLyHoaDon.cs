using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlynhasach
{
    public partial class UC_QuanLyHoaDon : UserControl
    {
        public UC_QuanLyHoaDon()
        {
            InitializeComponent();

            // 1. Sơn lại màu cho cái bảng bớt "phèn"
            FormatDataGridView(dgvHoaDon);

            // 2. (Tùy chọn) Có thể set mặc định Từ Ngày là ngày mùng 1 đầu tháng cho dễ xem
            // dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // 3. TỰ ĐỘNG CHẠY LOAD DỮ LIỆU: Ép hệ thống tự bấm nút Lọc 1 lần khi vừa mở tab
            btnLocHoaDon_Click(null, null);
        }
        private void FormatDataGridView(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White; // Nền trắng nhìn cho sang
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false; // Tắt cái cột mũi tên vô duyên bên trái cùng
            dgv.RowTemplate.Height = 35;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 63, 159);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }

        private void btnLocHoaDon_Click(object sender, EventArgs e)
        {
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
                string sdtTimKiem = txtTimSdt.Text.Trim(); // Lấy SĐT gõ trong ô

                if (tuNgay > denNgay) { MessageBox.Show("Ngày lỗi!"); return; }

                try
                {
                    quanlynhasach.Data.DatabaseHelper db = new quanlynhasach.Data.DatabaseHelper();

                    // Nâng cấp Query: LEFT JOIN KhachHang để tìm theo SĐT
                    // Dùng IFNULL để nếu khách vãng lai (không có MaKH) thì để trống
                    string query = $@"
                    SELECT h.MaHD AS 'Mã HĐ', h.NgayLap AS 'Ngày Lập', 
                           IFNULL(k.SoDienThoai, 'Khách vãng lai') AS 'SĐT Khách',
                           h.TongTien AS 'Tổng Tiền', h.PhanTramGiam AS 'Giảm (%)', 
                           h.TienGiam AS 'Tiền Giảm', h.ThanhToan AS 'Thanh Toán'
                    FROM hoadon h
                    LEFT JOIN khachhang k ON h.MaKH = k.MaKH
                    WHERE h.NgayLap >= '{tuNgay:yyyy-MM-dd HH:mm:ss}' 
                      AND h.NgayLap <= '{denNgay:yyyy-MM-dd HH:mm:ss}'";

                    // Nếu có nhập SĐT thì thêm điều kiện lọc
                    if (!string.IsNullOrEmpty(sdtTimKiem))
                    {
                        query += $" AND k.SoDienThoai LIKE '%{sdtTimKiem}%'";
                    }

                    query += " ORDER BY h.NgayLap DESC";

                    DataTable dt = db.ExecuteQuery(query);
                    dgvHoaDon.DataSource = dt;

                    // Căn chỉnh như cũ...
                    if (dgvHoaDon.Columns.Count > 0)
                    {
                        dgvHoaDon.Columns["Mã HĐ"].Width = 60;
                        dgvHoaDon.Columns["Ngày Lập"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvHoaDon.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
                        dgvHoaDon.Columns["Tiền Giảm"].DefaultCellStyle.Format = "N0";
                        dgvHoaDon.Columns["Thanh Toán"].DefaultCellStyle.Format = "N0";
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void dgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy Mã HĐ ở cột đầu tiên của dòng vừa click
                int maHD = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["Mã HĐ"].Value);

                // Mở form Chi tiết và truyền Mã HĐ sang
                frmChiTietHoaDon fChiTiet = new frmChiTietHoaDon(maHD);
                fChiTiet.ShowDialog(); // ShowDialog để nó hiện đè lên, bắt người dùng xem xong phải tắt đi
            }
        }
    }
}
