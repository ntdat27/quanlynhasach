using System;
using System.Collections.Generic;
using System.Data;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class KhachHangController
    {
        private DatabaseHelper db = new DatabaseHelper();

        // Lấy 1 khách hàng bằng SĐT (Đã đổi tên bảng thành HangThanhVien)
        public KhachHang GetKhachHangBySdt(string sdt)
        {
            string query = $@"SELECT kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiemTichLuy, kh.MaHang, 
                                     tv.TenHang, tv.PhanTramGiam 
                              FROM KhachHang kh 
                              LEFT JOIN HangThanhVien tv ON kh.MaHang = tv.MaHang 
                              WHERE kh.SoDienThoai = '{sdt}'";

            DataTable dt = db.ExecuteQuery(query);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhachHang
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    DiemTichLuy = Convert.ToInt32(row["DiemTichLuy"]),
                    MaHang = Convert.ToInt32(row["MaHang"]),
                    TenHang = row["TenHang"].ToString(),
                    PhanTramGiam = Convert.ToInt32(row["PhanTramGiam"])
                };
            }
            return null;
        }

        // Lấy danh sách toàn bộ khách hàng (Đã đổi tên bảng thành HangThanhVien)
        public List<KhachHang> GetAllKhachHang()
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = @"SELECT kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiemTichLuy, kh.MaHang, 
                                    tv.TenHang, tv.PhanTramGiam 
                             FROM KhachHang kh 
                             LEFT JOIN HangThanhVien tv ON kh.MaHang = tv.MaHang";

            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new KhachHang
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    DiemTichLuy = Convert.ToInt32(row["DiemTichLuy"]),
                    MaHang = Convert.ToInt32(row["MaHang"]),
                    TenHang = row["TenHang"].ToString(),
                    PhanTramGiam = Convert.ToInt32(row["PhanTramGiam"])
                });
            }
            return list;
        }
        public bool KiemTraTrungSdt(string sdt, int maKHIgnore = 0)
        {
            string query = $"SELECT MaKH FROM KhachHang WHERE SoDienThoai = '{sdt}' AND MaKH != {maKHIgnore}";
            DataTable dt = db.ExecuteQuery(query);
            return dt.Rows.Count > 0;
        }

        // Thêm khách hàng (Đã bỏ cột PhanTramGiam ở bảng KhachHang cho đúng DB của bạn)
        public bool AddKhachHang(KhachHang kh)
        {
            string query = $"INSERT INTO KhachHang (HoTen, SoDienThoai, DiemTichLuy, MaHang) " +
                           $"VALUES (N'{kh.HoTen}', '{kh.SoDienThoai}', {kh.DiemTichLuy}, {kh.MaHang})";
            return db.ExecuteNonQuery(query) > 0;
        }

        public bool UpdateKhachHang(KhachHang kh)
        {
            string query = $"UPDATE KhachHang SET HoTen=N'{kh.HoTen}', SoDienThoai='{kh.SoDienThoai}', " +
                           $"DiemTichLuy={kh.DiemTichLuy}, MaHang={kh.MaHang} WHERE MaKH={kh.MaKH}";
            return db.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteKhachHang(int maKH)
        {
            string query = $"DELETE FROM KhachHang WHERE MaKH={maKH}";
            return db.ExecuteNonQuery(query) > 0;
        }
        public DataTable SearchKhachHang(string tenKH, string sdt)
        {
            try
            {
                // Nối với bảng hangthanhvien để lấy được Tên Hạng và Giảm Giá y như lúc Load tất cả
                string query = $@"
                    SELECT k.MaKH, k.HoTen, k.SoDienThoai, k.DiemTichLuy, 
                           t.TenHang, t.PhanTramGiam
                    FROM khachhang k
                    LEFT JOIN hangthanhvien t ON k.MaHang = t.MaHang
                    WHERE k.HoTen LIKE N'%{tenKH}%' 
                      AND k.SoDienThoai LIKE '%{sdt}%'";

                DatabaseHelper db = new DatabaseHelper();
                return db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tìm kiếm: " + ex.Message);
                return new DataTable();
            }
        }
    }
}