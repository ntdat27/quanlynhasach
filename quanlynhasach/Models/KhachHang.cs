namespace quanlynhasach.Models
{
    public class KhachHang
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public int DiemTichLuy { get; set; }
        public int MaHang { get; set; }

        // Cột phụ thêm vào để nối bảng (JOIN), giúp hiển thị tên hạng lên giao diện
        public string TenHang { get; set; }
        public int PhanTramGiam { get; set; }
    }
}