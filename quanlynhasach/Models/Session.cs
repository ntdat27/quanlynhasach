using System;

namespace quanlynhasach.Models // (Hoặc namespace tương ứng của bạn)
{
    // Class này để static (tĩnh) để có thể gọi ở bất kỳ Form nào mà không cần dùng lệnh "new"
    public static class Session
    {
        public static int MaNV { get; set; }
        public static string HoTen { get; set; }
        public static string TaiKhoan { get; set; }
        public static string ChucVu { get; set; }

        // Hàm này dùng để xóa thông tin khi bấm Đăng xuất
        public static void Clear()
        {
            MaNV = 0;
            HoTen = "";
            TaiKhoan = "";
            ChucVu = "";
        }
    }
}