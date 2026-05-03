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
                // Dùng đúng các cột trong bảng NhanVien (Sửa maGV thành MaNV)
                string query = $"SELECT MaNV, TaiKhoan, HoTen, ChucVu FROM NhanVien WHERE TaiKhoan = '{taiKhoan}' AND MatKhau = '{matKhau}'";
                DataTable dt = db.ExecuteQuery(query);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    
                    // Nạp thông tin vào Session chuẩn xác nhất
                    Session.MaNV = Convert.ToInt32(row["MaNV"]);
                    Session.TaiKhoan = row["TaiKhoan"].ToString();
                    Session.HoTen = row["HoTen"].ToString();
                    Session.ChucVu = row["ChucVu"].ToString();
                    
                    return true; // Đăng nhập thành công
                }
                return false; // Sai tài khoản/mật khẩu
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
            return dt.Rows.Count > 0; // Trả về true nếu bị trùng
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
    }
}