using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using quanlynhasach.Models;
using quanlynhasach.Data; // THÊM: Để gọi lớp DatabaseHelper trung tâm

namespace quanlynhasach.Controllers
{
    public class HoaDonController
    {
        // ĐÃ XÓA chuỗi kết nối kết nối cứng ở đây để đảm bảo tính an toàn dữ liệu

        public bool ThanhToan(int? maKH, int maNV, int tongTien, int phanTramGiam, int tienGiam, int thanhToan, List<ChiTietHoaDon> danhSachChiTiet)
        {
            // Lấy chuỗi kết nối tập trung từ DatabaseHelper thay vì tự khai báo lại
            DatabaseHelper dbHelper = new DatabaseHelper();
            string connectionString = dbHelper.ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Bắt đầu một Transaction bảo toàn tính vẹn toàn dữ liệu
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Tạo Hóa Đơn và lấy ID tự tăng vừa sinh ra
                        string queryHoaDon = "INSERT INTO HoaDon (MaKH, MaNV, TongTien, PhanTramGiam, TienGiam, ThanhToan) " +
                                             "VALUES (@maKH, @maNV, @tongTien, @phanTramGiam, @tienGiam, @thanhToan); " +
                                             "SELECT LAST_INSERT_ID();";
                        int maHD = 0;
                        using (MySqlCommand cmd = new MySqlCommand(queryHoaDon, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@maKH", maKH.HasValue ? (object)maKH.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@maNV", maNV);
                            cmd.Parameters.AddWithValue("@tongTien", tongTien);
                            cmd.Parameters.AddWithValue("@phanTramGiam", phanTramGiam);
                            cmd.Parameters.AddWithValue("@tienGiam", tienGiam);
                            cmd.Parameters.AddWithValue("@thanhToan", thanhToan);

                            maHD = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 2. Chạy vòng lặp thêm Chi Tiết Hóa Đơn & Trừ Tồn Kho từng món sách
                        foreach (var item in danhSachChiTiet)
                        {
                            string queryChiTiet = "INSERT INTO ChiTietHoaDon (MaHD, MaSach, SoLuongBan, GiaBan, ThanhTien) " +
                                                  "VALUES (@maHD, @maSach, @soLuongBan, @giaBan, @thanhTien)";
                            using (MySqlCommand cmd = new MySqlCommand(queryChiTiet, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@maHD", maHD);
                                cmd.Parameters.AddWithValue("@maSach", item.MaSach);
                                cmd.Parameters.AddWithValue("@soLuongBan", item.SoLuongBan);
                                cmd.Parameters.AddWithValue("@giaBan", item.GiaBan);
                                cmd.Parameters.AddWithValue("@thanhTien", item.ThanhTien);
                                cmd.ExecuteNonQuery();
                            }

                            string queryTruKho = "UPDATE Sach SET SoLuongTon = SoLuongTon - @soLuongBan WHERE MaSach = @maSach";
                            using (MySqlCommand cmd = new MySqlCommand(queryTruKho, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@soLuongBan", item.SoLuongBan);
                                cmd.Parameters.AddWithValue("@maSach", item.MaSach);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 3. ĐÃ ĐỔI: Cộng dồn số tiền thanh toán thực tế vào Tổng Chi Tiêu để tự động nâng Hạng thành viên động
                        if (maKH.HasValue && thanhToan > 0)
                        {
                            string queryTichLuyDoanhSo = "UPDATE khachhang SET TongTienDaMua = TongTienDaMua + @soTienMua WHERE MaKH = @maKH";
                            using (MySqlCommand cmd = new MySqlCommand(queryTichLuyDoanhSo, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@soTienMua", thanhToan);
                                cmd.Parameters.AddWithValue("@maKH", maKH.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Nếu mọi thứ chạy thông suốt từ đầu đến cuối không lỗi, Commit chính thức lưu dữ liệu vào DB
                        LichSuController.GhiLog(maNV, $"Thanh toán thành công Hóa đơn ID: {maHD} - Số tiền thực thu: {thanhToan:N0}đ");
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Nếu dính bất kỳ lỗi gì ở bất kỳ bước nào, Rollback hủy bỏ toàn bộ luồng hóa đơn để tránh sai lệch kho
                        transaction.Rollback();
                        Console.WriteLine("Lỗi khi thanh toán hóa đơn: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}