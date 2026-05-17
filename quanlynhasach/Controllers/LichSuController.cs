using System;
using System.Data;
using MySql.Data.MySqlClient;
using quanlynhasach.Data;

namespace quanlynhasach.Controllers
{
    public class LichSuController
    {
        private DatabaseHelper db = new DatabaseHelper();

        public static void GhiLog(int maNV, string hanhDong)
        {
            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                string query = "INSERT INTO lichsuhoatdong (MaNV, HanhDong) VALUES (@maNV, @hanhDong)";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@maNV", maNV),
                    new MySqlParameter("@hanhDong", hanhDong)
                };
                dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch
            {
                // Bỏ qua lỗi
            }
        }

        public DataTable GetDanhSachLichSu()
        {
            string query = @"
                SELECT l.MaLog, n.HoTen, n.TaiKhoan, l.HanhDong, l.ThoiGian 
                FROM lichsuhoatdong l
                JOIN nhanvien n ON l.MaNV = n.MaNV
                ORDER BY l.ThoiGian DESC
                LIMIT 100";

            return db.ExecuteQuery(query);
        }

        public DataTable GetLichSuTheoKhoangThoiGian(DateTime tuNgay, DateTime denNgay)
        {
            DatabaseHelper db = new DatabaseHelper();

            // ĐÃ FIX: Thêm cột nv.TaiKhoan vào chuỗi SELECT để đồng bộ cấu trúc với hàm GetDanhSachLichSu
            string query = @"SELECT ls.MaLog, nv.HoTen, nv.TaiKhoan, ls.HanhDong, ls.ThoiGian 
                     FROM lichsuhoatdong ls
                     LEFT JOIN nhanvien nv ON ls.MaNV = nv.MaNV
                     WHERE ls.ThoiGian >= @tuNgay AND ls.ThoiGian <= @denNgay
                     ORDER BY ls.ThoiGian DESC";

            MySqlParameter[] parameters = {
                new MySqlParameter("@tuNgay", tuNgay),
                new MySqlParameter("@denNgay", denNgay)
            };

            return db.ExecuteQuery(query, parameters);
        }
    }
}