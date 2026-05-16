// [Thay thế toàn bộ nội dung file Controllers/SachController.cs]
using System;
using System.Collections.Generic;
using System.Data;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class SachController
    {
        private DatabaseHelper db = new DatabaseHelper();

        public List<Sach> GetAllSach()
        {
            List<Sach> list = new List<Sach>();
            string query = "SELECT * FROM Sach";
            DataTable dt = db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToSach(row));
            }
            return list;
        }

        public List<Sach> SearchSach(string keyword)
        {
            List<Sach> listSach = new List<Sach>();
            string query = $@"
                SELECT s.* FROM Sach s
                LEFT JOIN theloai tl ON s.MaTL = tl.MaTL
                LEFT JOIN tacgia tg ON s.MaTG = tg.MaTG
                WHERE s.TenSach LIKE '%{keyword}%' 
                   OR tl.TenTL LIKE '%{keyword}%' 
                   OR tg.TenTG LIKE '%{keyword}%'";

            DataTable dt = db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                listSach.Add(MapRowToSach(row));
            }
            return listSach;
        }

        private Sach MapRowToSach(DataRow row)
        {
            return new Sach
            {
                MaSach = Convert.ToInt32(row["MaSach"]),
                TenSach = row["TenSach"].ToString(),
                MaTL = row["MaTL"] != DBNull.Value ? Convert.ToInt32(row["MaTL"]) : 0,
                MaTG = row["MaTG"] != DBNull.Value ? Convert.ToInt32(row["MaTG"]) : 0,
                MaNXB = row["MaNXB"] != DBNull.Value ? Convert.ToInt32(row["MaNXB"]) : 0,
                NamXB = row["NamXB"] != DBNull.Value ? Convert.ToInt32(row["NamXB"]) : 0,
                GiaNhap = row["GiaNhap"] != DBNull.Value ? Convert.ToInt32(row["GiaNhap"]) : 0,
                GiaBan = row["GiaBan"] != DBNull.Value ? Convert.ToInt32(row["GiaBan"]) : 0,
                SoLuongTon = row["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(row["SoLuongTon"]) : 0
            };
        }

        // CHỨC NĂNG QUAN TRỌNG: NHẬP HÀNG (Cộng dồn số lượng)
        public bool NhapHang(int maSach, int soLuongThem)
        {
            try
            {
                string query = $"UPDATE Sach SET SoLuongTon = SoLuongTon + {soLuongThem} WHERE MaSach = {maSach}";
                return db.ExecuteNonQuery(query) > 0;
            }
            catch { return false; }
        }

        public bool AddSach(Sach s)
        {
            string safeTen = s.TenSach.Replace("'", "''");
            string query = $"INSERT INTO Sach (TenSach, MaTL, MaTG, MaNXB, NamXB, GiaNhap, SoLuongTon, GiaBan) " +
                           $"VALUES (N'{safeTen}', {s.MaTL}, {s.MaTG}, {s.MaNXB}, {s.NamXB}, {s.GiaNhap}, {s.SoLuongTon}, {s.GiaBan})";
            return db.ExecuteNonQuery(query) > 0;
        }

        public bool UpdateSach(Sach s)
        {
            string safeTen = s.TenSach.Replace("'", "''");
            string query = $"UPDATE Sach SET TenSach = N'{safeTen}', MaTL = {s.MaTL}, MaTG = {s.MaTG}, " +
                           $"MaNXB = {s.MaNXB}, NamXB = {s.NamXB}, GiaNhap = {s.GiaNhap}, " +
                           $"SoLuongTon = {s.SoLuongTon}, GiaBan = {s.GiaBan} WHERE MaSach = {s.MaSach}";
            return db.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteSach(int maSach) => db.ExecuteNonQuery($"DELETE FROM Sach WHERE MaSach = {maSach}") > 0;
    }
}