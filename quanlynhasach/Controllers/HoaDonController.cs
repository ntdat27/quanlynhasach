using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class HoaDonController
    {
        // Sử dụng chuỗi kết nối trực tiếp để có thể mở Transaction
        private readonly string connectionString = "Server=localhost;Database=quanlynhasach;Uid=root;Pwd=;";

        public bool ThanhToan(int? maKH, int maNV, int tongTien, int phanTramGiam, int tienGiam, int thanhToan, List<ChiTietHoaDon> danhSachChiTiet)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Bắt đầu một Transaction
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Tạo Hóa Đơn và lấy ID
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

                        // 2. Chạy vòng lặp thêm Chi Tiết Hóa Đơn & Trừ Tồn Kho
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

                        // 3. TÍCH ĐIỂM CHO KHÁCH HÀNG
                        if (maKH.HasValue && thanhToan > 0)
                        {
                            int diemDuocCong = thanhToan / 10000;
                            string queryTichDiem = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @diemDuocCong WHERE MaKH = @maKH";
                            using (MySqlCommand cmd = new MySqlCommand(queryTichDiem, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@diemDuocCong", diemDuocCong);
                                cmd.Parameters.AddWithValue("@maKH", maKH.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Nếu mọi thứ suôn sẻ, Commit (lưu thực sự) toàn bộ dữ liệu
                        LichSuController.GhiLog(maNV, $"Thanh toán thành công Hóa đơn ID: {maHD} - Số tiền: {thanhToan:N0}đ");
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Nếu có bất kỳ lỗi nào, Rollback (hoàn tác) toàn bộ
                        transaction.Rollback();
                        Console.WriteLine("Lỗi khi thanh toán: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}