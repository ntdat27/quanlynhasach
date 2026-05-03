using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlynhasach.Models
{
    public class Sach
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }

        // Các trường Số (Dùng để lưu xuống Database)
        public int MaTL { get; set; }
        public int MaTG { get; set; }
        public int MaNXB { get; set; }

        // MỚI THÊM: Các trường Chữ (Dùng để hiển thị lên DataGridView cho đẹp)
        public string TenTL { get; set; }
        public string TenTG { get; set; }
        public string TenNXB { get; set; }

        public int NamXB { get; set; }
        public int GiaNhap { get; set; }
        public int GiaBan { get; set; }
        public int SoLuongTon { get; set; }
    }
}