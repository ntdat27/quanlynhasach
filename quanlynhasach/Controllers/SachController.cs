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
            string query = "SELECT MaSach, TenSach, SoLuongTon, GiaBan FROM Sach";

            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Sach s = new Sach();
                s.MaSach = Convert.ToInt32(row["MaSach"]);
                s.TenSach = row["TenSach"].ToString();
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
            // Tìm kiếm tương đối bằng toán tử LIKE (dấu % đại diện cho chuỗi bất kỳ)
            string query = $"SELECT MaSach, TenSach, SoLuongTon, GiaBan FROM Sach WHERE TenSach LIKE '%{keyword}%' OR MaSach LIKE '%{keyword}%'";

            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Sach s = new Sach();
                s.MaSach = Convert.ToInt32(row["MaSach"]);
                s.TenSach = row["TenSach"].ToString();
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