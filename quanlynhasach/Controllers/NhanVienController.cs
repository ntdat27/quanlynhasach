using System;
using System.Collections.Generic;
using System.Data;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class NhanVienController
    {
        private DatabaseHelper db = new DatabaseHelper();
        public bool DangNhap(string taiKhoan, string matKhau)
        {
            try
            {
                string query = $"SELECT MaNV, TaiKhoan, HoTen, ChucVu FROM NhanVien WHERE TaiKhoan = '{taiKhoan}' AND MatKhau = '{matKhau}'";
                DataTable dt = db.ExecuteQuery(query);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    
                    Session.MaNV = Convert.ToInt32(row["MaNV"]);
                    Session.TaiKhoan = row["TaiKhoan"].ToString();
                    Session.HoTen = row["HoTen"].ToString();
                    Session.ChucVu = row["ChucVu"].ToString();
                    
                    return true; 
                }
                return false; 
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();
            string query = "SELECT MaNV, HoTen, SoDienThoai, TaiKhoan, MatKhau, ChucVu FROM NhanVien";
            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVien
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    TaiKhoan = row["TaiKhoan"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    ChucVu = row["ChucVu"].ToString()
                });
            }
            return list;
        }
        public bool KiemTraTrung(string taiKhoan, string soDienThoai, int maNVIgnore = 0)
        {
            string query = $@"SELECT MaNV FROM NhanVien 
                              WHERE (TaiKhoan = '{taiKhoan}' OR SoDienThoai = '{soDienThoai}') 
                              AND MaNV != {maNVIgnore}";
            DataTable dt = db.ExecuteQuery(query);
            return dt.Rows.Count > 0; 
        }
        public bool AddNhanVien(NhanVien nv)
        {
            try
            {
                string query = $"INSERT INTO NhanVien (HoTen, SoDienThoai, TaiKhoan, MatKhau, ChucVu) VALUES (N'{nv.HoTen}', '{nv.SoDienThoai}', '{nv.TaiKhoan}', '{nv.MatKhau}', N'{nv.ChucVu}')";
                return db.ExecuteNonQuery(query) > 0;
            }
            catch { return false; }
        }

        public bool UpdateNhanVien(NhanVien nv)
        {
            try
            {
                string query = $"UPDATE NhanVien SET HoTen = N'{nv.HoTen}', SoDienThoai = '{nv.SoDienThoai}', TaiKhoan = '{nv.TaiKhoan}', MatKhau = '{nv.MatKhau}', ChucVu = N'{nv.ChucVu}' WHERE MaNV = {nv.MaNV}";
                return db.ExecuteNonQuery(query) > 0;
            }
            catch { return false; }
        }

        public bool DeleteNhanVien(int maNV)
        {
            try
            {
                string query = $"DELETE FROM NhanVien WHERE MaNV = {maNV}";
                return db.ExecuteNonQuery(query) > 0;
            }
            catch { return false; }
        }
        public DataTable SearchNhanVien(string tenNV, string sdt)
        {
            try
            {
                string query = $@"
                    SELECT MaNV, HoTen, SoDienThoai, TaiKhoan, MatKhau, ChucVu 
                    FROM NhanVien 
                    WHERE HoTen LIKE N'%{tenNV}%' 
                      AND SoDienThoai LIKE '%{sdt}%'";

                quanlynhasach.Data.DatabaseHelper db = new quanlynhasach.Data.DatabaseHelper();
                return db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tìm kiếm nhân viên: " + ex.Message);
                return new DataTable();
            }
        }
    }
}