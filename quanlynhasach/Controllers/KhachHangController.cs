using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class KhachHangController
    {
        private DatabaseHelper db = new DatabaseHelper();

        // Hàm lấy toàn bộ danh sách khách hàng
        public List<KhachHang> GetAllKhachHang()
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = "SELECT MaKH, HoTen, SoDienThoai, TongTienDaMua FROM khachhang";
            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new KhachHang
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    TongTienDaMua = row["TongTienDaMua"] != DBNull.Value ? Convert.ToDecimal(row["TongTienDaMua"]) : 0
                });
            }
            return list;
        }

        // HÀM ĐƯỢC BỔ SUNG LẠI: Tìm kiếm khách hàng bằng SĐT dùng cho màn hình POS/Bán hàng
        public KhachHang GetKhachHangBySdt(string sdt)
        {
            string query = "SELECT MaKH, HoTen, SoDienThoai, TongTienDaMua FROM khachhang WHERE SoDienThoai = @sdt";
            MySqlParameter[] parameters = {
                new MySqlParameter("@sdt", sdt.Trim())
            };
            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhachHang
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    TongTienDaMua = row["TongTienDaMua"] != DBNull.Value ? Convert.ToDecimal(row["TongTienDaMua"]) : 0
                };
            }
            return null; // Trả về null nếu không tìm thấy số điện thoại này
        }

        // Hàm tìm kiếm khách hàng phục vụ bộ lọc tìm kiếm trên giao diện quản lý
        public DataTable SearchKhachHang(string tenKH, string sdt)
        {
            string query = "SELECT MaKH, HoTen, SoDienThoai, TongTienDaMua FROM khachhang WHERE HoTen LIKE @tenKH AND SoDienThoai LIKE @sdt";
            MySqlParameter[] parameters = {
                new MySqlParameter("@tenKH", "%" + tenKH + "%"),
                new MySqlParameter("@sdt", "%" + sdt + "%")
            };
            return db.ExecuteQuery(query, parameters);
        }

        // Hàm thêm mới khách hàng
        public bool AddKhachHang(KhachHang kh)
        {
            string query = "INSERT INTO khachhang (HoTen, SoDienThoai, TongTienDaMua) VALUES (@HoTen, @SoDienThoai, @TongTienDaMua)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@HoTen", kh.HoTen),
                new MySqlParameter("@SoDienThoai", kh.SoDienThoai),
                new MySqlParameter("@TongTienDaMua", kh.TongTienDaMua)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        // Hàm cập nhật thông tin khách hàng
        public bool UpdateKhachHang(KhachHang kh)
        {
            string query = "UPDATE khachhang SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, TongTienDaMua = @TongTienDaMua WHERE MaKH = @MaKH";
            MySqlParameter[] parameters = {
                new MySqlParameter("@HoTen", kh.HoTen),
                new MySqlParameter("@SoDienThoai", kh.SoDienThoai),
                new MySqlParameter("@TongTienDaMua", kh.TongTienDaMua),
                new MySqlParameter("@MaKH", kh.MaKH)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        // Hàm xóa khách hàng
        public bool DeleteKhachHang(int maKH)
        {
            try
            {
                string query = "DELETE FROM khachhang WHERE MaKH = @MaKH";
                MySqlParameter[] parameters = { new MySqlParameter("@MaKH", maKH) };
                return db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch
            {
                return false;
            }
        }

        // Hàm check trùng SĐT khi thêm hoặc sửa dữ liệu khách
        public bool KiemTraTrungSdt(string sdt, int maKHExempt = 0)
        {
            string query = "SELECT COUNT(*) FROM khachhang WHERE SoDienThoai = @sdt AND MaKH != @maKH";
            MySqlParameter[] parameters = {
                new MySqlParameter("@sdt", sdt),
                new MySqlParameter("@maKH", maKHExempt)
            };
            DataTable dt = db.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            return false;
        }
    }
}