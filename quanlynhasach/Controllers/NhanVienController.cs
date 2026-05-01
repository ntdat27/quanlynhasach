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

        public bool AddNhanVien(NhanVien nv)
        {
            string query = $"INSERT INTO NhanVien (HoTen, SoDienThoai, TaiKhoan, MatKhau, ChucVu) " +
                           $"VALUES ('{nv.HoTen}', '{nv.SoDienThoai}', '{nv.TaiKhoan}', '{nv.MatKhau}', '{nv.ChucVu}')";
            return db.ExecuteNonQuery(query) > 0;
        }

        public bool UpdateNhanVien(NhanVien nv)
        {
            string query = $"UPDATE NhanVien SET HoTen='{nv.HoTen}', SoDienThoai='{nv.SoDienThoai}', " +
                           $"TaiKhoan='{nv.TaiKhoan}', MatKhau='{nv.MatKhau}', ChucVu='{nv.ChucVu}' WHERE MaNV={nv.MaNV}";
            return db.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteNhanVien(int maNV)
        {
            string query = $"DELETE FROM NhanVien WHERE MaNV={maNV}";
            return db.ExecuteNonQuery(query) > 0;
        }
    }
}