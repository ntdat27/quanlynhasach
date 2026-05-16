using System;
using System.Collections.Generic;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class HoaDonController
    {
        private DatabaseHelper db = new DatabaseHelper();

        public bool ThanhToan(int? maKH, int maNV, int tongTien, int phanTramGiam, int tienGiam, int thanhToan, List<ChiTietHoaDon> danhSachChiTiet)
        {
            try
            {
                string khachHangId = maKH.HasValue ? maKH.Value.ToString() : "NULL";

                string queryHoaDon = $"INSERT INTO HoaDon (MaKH, MaNV, TongTien, PhanTramGiam, TienGiam, ThanhToan) " +
                                     $"VALUES ({khachHangId}, {maNV}, {tongTien}, {phanTramGiam}, {tienGiam}, {thanhToan}); " +
                                     $"SELECT LAST_INSERT_ID();";

                int maHD = db.ExecuteScalar(queryHoaDon);

                foreach (var item in danhSachChiTiet)
                {
                    string queryChiTiet = $"INSERT INTO ChiTietHoaDon (MaHD, MaSach, SoLuongBan, GiaBan, ThanhTien) " +
                                          $"VALUES ({maHD}, {item.MaSach}, {item.SoLuongBan}, {item.GiaBan}, {item.ThanhTien})";
                    db.ExecuteNonQuery(queryChiTiet);

                    string queryTruKho = $"UPDATE Sach SET SoLuongTon = SoLuongTon - {item.SoLuongBan} WHERE MaSach = {item.MaSach}";
                    db.ExecuteNonQuery(queryTruKho);
                }
                return true; 
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}