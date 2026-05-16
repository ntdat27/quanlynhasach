namespace quanlynhasach.Models
{
    public class KhachHang
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public decimal TongTienDaMua { get; set; } 

        public string TenHang
        {
            get
            {
                if (TongTienDaMua >= 50000000) return "Hạng Kim Cương";
                if (TongTienDaMua >= 15000000) return "Hạng Vàng";
                if (TongTienDaMua >= 5000000) return "Hạng Bạc";
                return "Khách hàng thân thiết";
            }
        }

        public int PhanTramGiam
        {
            get
            {
                if (TongTienDaMua >= 50000000) return 10; // Kim cương giảm 10%
                if (TongTienDaMua >= 15000000) return 5;  // Vàng giảm 5    %
                if (TongTienDaMua >= 5000000) return 2;  // Bạc giảm 2%
                return 0;                                 // Thân thiết giảm 0%
            }
        }
    }
}