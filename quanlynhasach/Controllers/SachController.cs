using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
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
            string query = @"
                SELECT s.* FROM Sach s
                LEFT JOIN theloai tl ON s.MaTL = tl.MaTL
                LEFT JOIN tacgia tg ON s.MaTG = tg.MaTG
                WHERE s.TenSach LIKE @keyword 
                   OR tl.TenTL LIKE @keyword 
                   OR tg.TenTG LIKE @keyword";

            MySqlParameter[] parameters = {
                new MySqlParameter("@keyword", "%" + keyword + "%")
            };

            DataTable dt = db.ExecuteQuery(query, parameters);
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

        public bool NhapHang(int maSach, int soLuongThem)
        {
            try
            {
                string query = "UPDATE Sach SET SoLuongTon = SoLuongTon + @soLuongThem WHERE MaSach = @maSach";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@soLuongThem", soLuongThem),
                    new MySqlParameter("@maSach", maSach)
                };
                return db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch { return false; }
        }

        public bool AddSach(Sach s)
        {
            string query = "INSERT INTO Sach (TenSach, MaTL, MaTG, MaNXB, NamXB, GiaNhap, SoLuongTon, GiaBan) " +
                           "VALUES (@tenSach, @maTL, @maTG, @maNXB, @namXB, @giaNhap, @soLuongTon, @giaBan)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@tenSach", s.TenSach),
                new MySqlParameter("@maTL", s.MaTL),
                new MySqlParameter("@maTG", s.MaTG),
                new MySqlParameter("@maNXB", s.MaNXB),
                new MySqlParameter("@namXB", s.NamXB),
                new MySqlParameter("@giaNhap", s.GiaNhap),
                new MySqlParameter("@soLuongTon", s.SoLuongTon),
                new MySqlParameter("@giaBan", s.GiaBan)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateSach(Sach s)
        {
            string query = "UPDATE Sach SET TenSach = @tenSach, MaTL = @maTL, MaTG = @maTG, " +
                           "MaNXB = @maNXB, NamXB = @namXB, GiaNhap = @giaNhap, " +
                           "SoLuongTon = @soLuongTon, GiaBan = @giaBan WHERE MaSach = @maSach";
            MySqlParameter[] parameters = {
                new MySqlParameter("@tenSach", s.TenSach),
                new MySqlParameter("@maTL", s.MaTL),
                new MySqlParameter("@maTG", s.MaTG),
                new MySqlParameter("@maNXB", s.MaNXB),
                new MySqlParameter("@namXB", s.NamXB),
                new MySqlParameter("@giaNhap", s.GiaNhap),
                new MySqlParameter("@soLuongTon", s.SoLuongTon),
                new MySqlParameter("@giaBan", s.GiaBan),
                new MySqlParameter("@maSach", s.MaSach)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteSach(int maSach)
        {
            string query = "DELETE FROM Sach WHERE MaSach = @maSach";
            MySqlParameter[] parameters = {
                new MySqlParameter("@maSach", maSach)
            };
            bool result = db.ExecuteNonQuery(query, parameters) > 0;

            // THÊM ĐOẠN NÀY:
            if (result && quanlynhasach.Models.Session.MaNV > 0)
            {
                LichSuController.GhiLog(quanlynhasach.Models.Session.MaNV, $"Xóa sách có mã ID: {maSach}");
            }

            return result;
        }
    }
}