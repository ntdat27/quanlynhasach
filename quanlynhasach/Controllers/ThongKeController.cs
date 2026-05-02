using System;
using System.Data;
using quanlynhasach.Data;

namespace quanlynhasach.Controllers
{
    public class ThongKeController
    {
        private DatabaseHelper db = new DatabaseHelper();

        public int GetTongKhachHang()
        {
            try
            {
                // Đếm tổng số dòng trong bảng KhachHang
                DataTable dt = db.ExecuteQuery("SELECT COUNT(*) FROM KhachHang");
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { return 0; }
        }

        public int GetTongNhanVien()
        {
            try
            {
                DataTable dt = db.ExecuteQuery("SELECT COUNT(*) FROM NhanVien");
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { return 0; }
        }

        public int GetTongDauSach()
        {
            try
            {
                // Giả sử bảng sách của bạn tên là Sach
                DataTable dt = db.ExecuteQuery("SELECT COUNT(*) FROM Sach");
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { return 0; }
        }
    }
}