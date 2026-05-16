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

        public KhachHang GetKhachHangBySdt(string sdt)
        {
            string query = @"SELECT kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiemTichLuy, kh.MaHang, 
                                     tv.TenHang, tv.PhanTramGiam 
                              FROM KhachHang kh 
                              LEFT JOIN HangThanhVien tv ON kh.MaHang = tv.MaHang 
                              WHERE kh.SoDienThoai = @sdt";

            MySqlParameter[] parameters = { new MySqlParameter("@sdt", sdt) };
            DataTable dt = db.ExecuteQuery(query, parameters);

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
            string query = "SELECT MaKH FROM KhachHang WHERE SoDienThoai = @sdt AND MaKH != @maKHIgnore";
            MySqlParameter[] parameters = {
                new MySqlParameter("@sdt", sdt),
                new MySqlParameter("@maKHIgnore", maKHIgnore)
            };
            DataTable dt = db.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0;
        }

        public bool AddKhachHang(KhachHang kh)
        {
            string query = "INSERT INTO KhachHang (HoTen, SoDienThoai, DiemTichLuy, MaHang) VALUES (@hoTen, @soDienThoai, @diem, @maHang)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@hoTen", kh.HoTen),
                new MySqlParameter("@soDienThoai", kh.SoDienThoai),
                new MySqlParameter("@diem", kh.DiemTichLuy),
                new MySqlParameter("@maHang", kh.MaHang)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateKhachHang(KhachHang kh)
        {
            string query = "UPDATE KhachHang SET HoTen=@hoTen, SoDienThoai=@soDienThoai, DiemTichLuy=@diem, MaHang=@maHang WHERE MaKH=@maKH";
            MySqlParameter[] parameters = {
                new MySqlParameter("@hoTen", kh.HoTen),
                new MySqlParameter("@soDienThoai", kh.SoDienThoai),
                new MySqlParameter("@diem", kh.DiemTichLuy),
                new MySqlParameter("@maHang", kh.MaHang),
                new MySqlParameter("@maKH", kh.MaKH)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteKhachHang(int maKH)
        {
            string query = "DELETE FROM KhachHang WHERE MaKH=@maKH";
            MySqlParameter[] parameters = { new MySqlParameter("@maKH", maKH) };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable SearchKhachHang(string tenKH, string sdt)
        {
            try
            {
                string query = @"
                    SELECT k.MaKH, k.HoTen, k.SoDienThoai, k.DiemTichLuy, 
                           t.TenHang, t.PhanTramGiam
                    FROM khachhang k
                    LEFT JOIN hangthanhvien t ON k.MaHang = t.MaHang
                    WHERE k.HoTen LIKE @tenKH 
                      AND k.SoDienThoai LIKE @sdt";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@tenKH", "%" + tenKH + "%"),
                    new MySqlParameter("@sdt", "%" + sdt + "%")
                };

                return db.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tìm kiếm: " + ex.Message);
                return new DataTable();
            }
        }
    }
}