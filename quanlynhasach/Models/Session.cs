using System;

namespace quanlynhasach.Models
{
    public static class Session
    {
        public static int MaNV { get; set; }
        public static string TaiKhoan { get; set; }
        public static string HoTen { get; set; }
        public static string ChucVu { get; set; }

        public static void Clear()
        {
            MaNV = 0;
            TaiKhoan = "";
            HoTen = "";
            ChucVu = "";
        }
    }
}