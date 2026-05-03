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

        // Lấy tất cả sách
        public List<Sach> GetAllSach()
        {
            List<Sach> listSach = new List<Sach>();
            // Dùng LEFT JOIN để lấy tên từ các bảng phụ
            string query = @"
                SELECT s.MaSach, s.TenSach, 
                       s.MaTL, tl.TenTL, 
                       s.MaTG, tg.TenTG, 
                       s.MaNXB, nxb.TenNXB, 
                       s.NamXB, s.GiaNhap, s.SoLuongTon, s.GiaBan 
                FROM Sach s
                LEFT JOIN theloai tl ON s.MaTL = tl.MaTL
                LEFT JOIN tacgia tg ON s.MaTG = tg.MaTG
                LEFT JOIN nhaxuatban nxb ON s.MaNXB = nxb.MaNXB";

            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Sach s = new Sach();
                s.MaSach = Convert.ToInt32(row["MaSach"]);
                s.TenSach = row["TenSach"].ToString();

                // Lấy Mã (để lưu)
                s.MaTL = row["MaTL"] != DBNull.Value ? Convert.ToInt32(row["MaTL"]) : 0;
                s.MaTG = row["MaTG"] != DBNull.Value ? Convert.ToInt32(row["MaTG"]) : 0;
                s.MaNXB = row["MaNXB"] != DBNull.Value ? Convert.ToInt32(row["MaNXB"]) : 0;

                // Lấy Tên (để hiển thị)
                s.TenTL = row["TenTL"].ToString();
                s.TenTG = row["TenTG"].ToString();
                s.TenNXB = row["TenNXB"].ToString();

                s.NamXB = Convert.ToInt32(row["NamXB"]);
                s.GiaNhap = Convert.ToInt32(row["GiaNhap"]);
                s.SoLuongTon = Convert.ToInt32(row["SoLuongTon"]);
                s.GiaBan = Convert.ToInt32(row["GiaBan"]);

                listSach.Add(s);
            }
            return listSach;
        }

        // TÌM KIẾM SÁCH (Mới thêm)
        public List<Sach> SearchSach(string keyword)
        {
            List<Sach> listSach = new List<Sach>();
            string query = $@"
                SELECT s.MaSach, s.TenSach, 
                       s.MaTL, tl.TenTL, 
                       s.MaTG, tg.TenTG, 
                       s.MaNXB, nxb.TenNXB, 
                       s.NamXB, s.GiaNhap, s.SoLuongTon, s.GiaBan 
                FROM Sach s
                LEFT JOIN theloai tl ON s.MaTL = tl.MaTL
                LEFT JOIN tacgia tg ON s.MaTG = tg.MaTG
                LEFT JOIN nhaxuatban nxb ON s.MaNXB = nxb.MaNXB
                WHERE s.TenSach LIKE '%{keyword}%' 
                   OR tl.TenTL LIKE '%{keyword}%' 
                   OR tg.TenTG LIKE '%{keyword}%'";
            // Tìm theo cả tên Tác giả và Thể loại cho VIP!

            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Sach s = new Sach();
                s.MaSach = Convert.ToInt32(row["MaSach"]);
                s.TenSach = row["TenSach"].ToString();
                s.MaTL = row["MaTL"] != DBNull.Value ? Convert.ToInt32(row["MaTL"]) : 0;
                s.MaTG = row["MaTG"] != DBNull.Value ? Convert.ToInt32(row["MaTG"]) : 0;
                s.MaNXB = row["MaNXB"] != DBNull.Value ? Convert.ToInt32(row["MaNXB"]) : 0;
                s.TenTL = row["TenTL"].ToString();
                s.TenTG = row["TenTG"].ToString();
                s.TenNXB = row["TenNXB"].ToString();
                s.NamXB = Convert.ToInt32(row["NamXB"]);
                s.GiaNhap = Convert.ToInt32(row["GiaNhap"]);
                s.SoLuongTon = Convert.ToInt32(row["SoLuongTon"]);
                s.GiaBan = Convert.ToInt32(row["GiaBan"]);

                listSach.Add(s);
            }
            return listSach;
        }
        public bool AddSach(Sach s)
        {
            try
            {
                // Tạm thời gán cứng MaTL=1, MaTG=1, MaNXB=1, GiaNhap=0 để test CRUD cơ bản trước
                string query = $"INSERT INTO Sach (TenSach, MaTL, MaTG, MaNXB, GiaNhap, GiaBan, SoLuongTon) " +
                               $"VALUES ('{s.TenSach}', 1, 1, 1, 0, {s.GiaBan}, {s.SoLuongTon})";

                return db.ExecuteNonQuery(query) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 2. Cập nhật Sách
        public bool UpdateSach(Sach s)
        {
            try
            {
                string query = $"UPDATE Sach SET TenSach = '{s.TenSach}', GiaBan = {s.GiaBan}, SoLuongTon = {s.SoLuongTon} " +
                               $"WHERE MaSach = {s.MaSach}";

                return db.ExecuteNonQuery(query) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 3. Xóa Sách
        public bool DeleteSach(int maSach)
        {
            try
            {
                // Lưu ý: Nếu sách này đã từng được bán (nằm trong ChiTietHoaDon) thì sẽ không xóa được do vướng Khóa ngoại.
                // Thực tế người ta thường dùng cột "TrangThai" để ẩn đi thay vì xóa cứng. Nhưng ở đây ta cứ làm xóa cơ bản trước.
                string query = $"DELETE FROM Sach WHERE MaSach = {maSach}";

                return db.ExecuteNonQuery(query) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}