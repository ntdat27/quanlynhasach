using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlynhasach
{
    public static class Session
    {
        public static int MaNV { get; set; } = 1;
        public static string HoTen { get; set; } = "Nguyễn Tiến Đạt";

        public static string ChucVu { get; set; } = "Admin";
        public static string TaiKhoan { get; internal set; }
        public static object CurrentUser { get; internal set; }
    }
}