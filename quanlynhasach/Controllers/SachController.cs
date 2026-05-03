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
            List<Sach> list = new List<Sach>();
            DatabaseHelper db = new DatabaseHelper();
            string query = "SELECT * FROM Sach"; // Hoặc câu query của bạn
            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Sach s = new Sach();
                s.MaSach = Convert.ToInt32(row["MaSach"]);
                s.TenSach = row["TenSach"].ToString();

                // MẸO CHỐNG LỖI DBNULL: Nếu dữ liệu trong DB là NULL thì gán bằng 0, ngược lại thì Convert
                s.MaTL = row["MaTL"] != DBNull.Value ? Convert.ToInt32(row["MaTL"]) : 0;
                s.MaTG = row["MaTG"] != DBNull.Value ? Convert.ToInt32(row["MaTG"]) : 0;
                s.MaNXB = row["MaNXB"] != DBNull.Value ? Convert.ToInt32(row["MaNXB"]) : 0;

                // Cột NamXB đang bị NULL của bạn sẽ được an toàn đi qua đây
                s.NamXB = row["NamXB"] != DBNull.Value ? Convert.ToInt32(row["NamXB"]) : 0;
                s.GiaNhap = row["GiaNhap"] != DBNull.Value ? Convert.ToInt32(row["GiaNhap"]) : 0;
                s.GiaBan = row["GiaBan"] != DBNull.Value ? Convert.ToInt32(row["GiaBan"]) : 0;
                s.SoLuongTon = row["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(row["SoLuongTon"]) : 0;

                list.Add(s);
            }
            return list;
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
                // Đã bổ sung đầy đủ NamXB và GiaNhap vào câu lệnh INSERT
                string query = $"INSERT INTO Sach (TenSach, MaTL, MaTG, MaNXB, NamXB, GiaNhap, SoLuongTon, GiaBan) " +
                               $"VALUES (N'{s.TenSach}', {s.MaTL}, {s.MaTG}, {s.MaNXB}, {s.NamXB}, {s.GiaNhap}, {s.SoLuongTon}, {s.GiaBan})";

                DatabaseHelper db = new DatabaseHelper();
                return db.ExecuteNonQuery(query) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi thêm sách: " + ex.Message);
                return false;
            }
        }

        // 2. Cập nhật Sách
        public bool UpdateSach(Sach s)
        {
            try
            {
                // Đã bổ sung đầy đủ NamXB và GiaNhap vào câu lệnh UPDATE
                string query = $"UPDATE Sach SET " +
                               $"TenSach = N'{s.TenSach}', " +
                               $"MaTL = {s.MaTL}, " +
                               $"MaTG = {s.MaTG}, " +
                               $"MaNXB = {s.MaNXB}, " +
                               $"NamXB = {s.NamXB}, " +
                               $"GiaNhap = {s.GiaNhap}, " +
                               $"SoLuongTon = {s.SoLuongTon}, " +
                               $"GiaBan = {s.GiaBan} " +
                               $"WHERE MaSach = {s.MaSach}";

                DatabaseHelper db = new DatabaseHelper();
                return db.ExecuteNonQuery(query) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi sửa sách: " + ex.Message);
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