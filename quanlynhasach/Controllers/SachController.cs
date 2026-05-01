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
    }
}