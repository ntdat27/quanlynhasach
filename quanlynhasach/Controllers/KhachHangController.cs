using System;
using System.Data;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class KhachHangController
    {
        private DatabaseHelper db = new DatabaseHelper();

        public KhachHang GetKhachHangBySdt(string sdt)
        {
            // Kết hợp (JOIN) 2 bảng để biết khách hàng này thuộc hạng nào, được giảm bao nhiêu %
            string query = $@"SELECT kh.MaKH, kh.HoTen, kh.SoDienThoai, htv.PhanTramGiam 
                              FROM KhachHang kh 
                              JOIN HangThanhVien htv ON kh.MaHang = htv.MaHang 
                              WHERE kh.SoDienThoai = '{sdt}'";

            DataTable dt = db.ExecuteQuery(query);

            // Nếu tìm thấy khách hàng
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhachHang
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    PhanTramGiam = Convert.ToInt32(row["PhanTramGiam"])
                };
            }

            return null; // Không tìm thấy
        }
    }
}