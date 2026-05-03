using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlynhasach
{
    public static class Session
    {
        // Gán cứng dữ liệu mặc định để "đánh lừa" hệ thống là đã đăng nhập rồi
        public static int MaNV { get; set; } = 1;
        public static string HoTen { get; set; } = "Nguyễn Tiến Đạt";

        // Bạn có thể đổi thành "Nhân viên" để test thử xem các nút quản lý có bị ẩn đi không nhé
        public static string ChucVu { get; set; } = "Admin";
        public static string TaiKhoan { get; internal set; }
    }
}