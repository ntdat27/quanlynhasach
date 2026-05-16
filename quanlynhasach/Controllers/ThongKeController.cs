using System;
using System.Data;
using quanlynhasach.Data;

namespace quanlynhasach.Controllers
{
    public class ThongKeController
    {
        private DatabaseHelper db = new DatabaseHelper();

        public int GetTongKhachHang()
        {
            try
            {
                DataTable dt = db.ExecuteQuery("SELECT COUNT(*) FROM KhachHang");
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { return 0; }
        }

        public int GetTongNhanVien()
        {
            try
            {
                DataTable dt = db.ExecuteQuery("SELECT COUNT(*) FROM NhanVien");
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { return 0; }
        }

        public int GetTongDauSach()
        {
            try
            {
                DataTable dt = db.ExecuteQuery("SELECT COUNT(*) FROM Sach");
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { return 0; }
        }
        public decimal GetDoanhThuHomNay()
        {
            try
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                string query = $"SELECT SUM(ThanhToan) FROM hoadon WHERE NgayLap >= '{today} 00:00:00' AND NgayLap <= '{today} 23:59:59'";

                DataTable dt = db.ExecuteQuery(query);

                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    return Convert.ToDecimal(dt.Rows[0][0]);
                }
                return 0; 
            }
            catch { return 0; }
        }

        public int GetSoDonHangHomNay()
        {
            try
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                string query = $"SELECT COUNT(MaHD) FROM hoadon WHERE NgayLap >= '{today} 00:00:00' AND NgayLap <= '{today} 23:59:59'";

                DataTable dt = db.ExecuteQuery(query);

                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
                return 0;
            }
            catch { return 0; }
        }


        public DataTable GetTop5SachBanChay()
        {
            try
            {
                string query = @"
                    SELECT s.TenSach AS 'Tên Sách', SUM(c.SoLuongBan) AS 'Tổng Số Lượng Đã Bán'
                    FROM chitiethoadon c
                    JOIN sach s ON c.MaSach = s.MaSach
                    GROUP BY c.MaSach, s.TenSach
                    ORDER BY SUM(c.SoLuongBan) DESC
                    LIMIT 5";

                return db.ExecuteQuery(query);
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetThongTinChungHoaDon(int maHD)
        {
            string query = $@"
                SELECT h.MaHD, h.NgayLap, h.TongTien, h.PhanTramGiam, h.TienGiam, h.ThanhToan,
                       IFNULL(k.HoTen, N'Khách vãng lai') AS TenKhach,
                       IFNULL(k.SoDienThoai, '') AS SdtKhach,
                       IFNULL(nv.HoTen, N'Không xác định') AS TenNhanVien,
                       IFNULL(tv.TenHang, N'Khách vãng lai') AS TenHang
                FROM hoadon h
                LEFT JOIN khachhang k ON h.MaKH = k.MaKH
                LEFT JOIN hangthanhvien tv ON k.MaHang = tv.MaHang
                LEFT JOIN nhanvien nv ON h.MaNV = nv.MaNV
                WHERE h.MaHD = {maHD}";
            return db.ExecuteQuery(query);
        }

        public DataTable GetDanhSachSachTrongHoaDon(int maHD)
        {
            string query = $@"
                SELECT s.TenSach AS 'Tên Sách', c.SoLuongBan AS 'SL',
                       c.GiaBan AS 'Đơn Giá', c.ThanhTien AS 'Thành Tiền'
                FROM chitiethoadon c
                JOIN sach s ON c.MaSach = s.MaSach
                WHERE c.MaHD = {maHD}";
            return db.ExecuteQuery(query);
        }
        public DataTable GetSachSapHetHang(int mucBaoDong = 5)
        {
            try
            {
                string query = $"SELECT MaSach AS 'Mã', TenSach AS 'Tên Sách', SoLuongTon AS 'Tồn Kho' " +
                               $"FROM sach WHERE SoLuongTon <= {mucBaoDong} " +
                               $"ORDER BY SoLuongTon ASC";
                return db.ExecuteQuery(query);
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}