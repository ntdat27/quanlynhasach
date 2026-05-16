using System;
using System.Data;
using MySql.Data.MySqlClient;
using quanlynhasach.Data;

namespace quanlynhasach.Controllers
{
    public class LichSuController
    {
        private DatabaseHelper db = new DatabaseHelper();

        // Hàm ghi log tĩnh (static) - Gọi trực tiếp từ mọi nơi cực kỳ tiện lợi
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
                // Bỏ qua lỗi nếu có để không làm gián đoạn các chức năng chính của phần mềm
            }
        }

        // Hàm lấy danh sách log để hiển thị lên DataGridView cho Admin xem
        public DataTable GetDanhSachLichSu()
        {
            string query = @"
                SELECT l.MaLog, n.HoTen, n.TaiKhoan, l.HanhDong, l.ThoiGian 
                FROM lichsuhoatdong l
                JOIN nhanvien n ON l.MaNV = n.MaNV
                ORDER BY l.ThoiGian DESC
                LIMIT 100"; // Chỉ lấy 100 hành động gần nhất cho nhẹ app

            return db.ExecuteQuery(query);
        }
    }
}