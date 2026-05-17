using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class SachController
    {
        private DatabaseHelper db = new DatabaseHelper();

        public List<Sach> GetAllSach()
        {
            List<Sach> list = new List<Sach>();
            string query = "SELECT * FROM Sach";
            DataTable dt = db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToSach(row));
            }
            return list;
        }

        public List<Sach> SearchSach(string keyword)
        {
            List<Sach> listSach = new List<Sach>();
            string query = @"
                SELECT s.* FROM Sach s
                LEFT JOIN theloai tl ON s.MaTL = tl.MaTL
                LEFT JOIN tacgia tg ON s.MaTG = tg.MaTG
                WHERE s.TenSach LIKE @keyword 
                   OR tl.TenTL LIKE @keyword 
                   OR tg.TenTG LIKE @keyword";

            MySqlParameter[] parameters = {
                new MySqlParameter("@keyword", "%" + keyword + "%")
            };

            DataTable dt = db.ExecuteQuery(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                listSach.Add(MapRowToSach(row));
            }
            return listSach;
        }

        private Sach MapRowToSach(DataRow row)
        {
            return new Sach
            {
                MaSach = Convert.ToInt32(row["MaSach"]),
                TenSach = row["TenSach"].ToString(),
                MaTL = row["MaTL"] != DBNull.Value ? Convert.ToInt32(row["MaTL"]) : 0,
                MaTG = row["MaTG"] != DBNull.Value ? Convert.ToInt32(row["MaTG"]) : 0,
                MaNXB = row["MaNXB"] != DBNull.Value ? Convert.ToInt32(row["MaNXB"]) : 0,
                NamXB = row["NamXB"] != DBNull.Value ? Convert.ToInt32(row["NamXB"]) : 0,
                GiaNhap = row["GiaNhap"] != DBNull.Value ? Convert.ToInt32(row["GiaNhap"]) : 0,
                GiaBan = row["GiaBan"] != DBNull.Value ? Convert.ToInt32(row["GiaBan"]) : 0,
                SoLuongTon = row["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(row["SoLuongTon"]) : 0
            };
        }

        // HÀM MỚI NÂNG CẤP: Quản lý giao dịch nhập kho chặt chẽ, sinh chứng từ PhieuNhap
        // HÀM ĐÃ SỬA LỖI TÊN CỘT: Quản lý giao dịch nhập kho chặt chẽ, sinh chứng từ PhieuNhap
        // HÀM ĐÃ SỬA LỖI TÊN CỘT VÀ THÊM MaNCC: Quản lý giao dịch nhập kho chặt chẽ, sinh chứng từ PhieuNhap
        public bool NhapHangVaoKho(int maSach, int soLuongNhap, int giaNhap, int maNV)
        {
            // Lấy chuỗi kết nối an toàn từ DatabaseHelper
            DatabaseHelper dbHelper = new DatabaseHelper();
            string connectionString = dbHelper.ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. SỬA LỖI CHÍ MẠNG: Bổ sung thêm cột MaNCC (Mã Nhà Cung Cấp) vào câu lệnh INSERT
                        string queryPhieuNhap = "INSERT INTO phieunhap (MaNV, MaNCC, TongTien) VALUES (@maNV, @maNCC, @tongTien); SELECT LAST_INSERT_ID();";
                        int tongTienPhieu = soLuongNhap * giaNhap;
                        int maPN = 0;

                        using (MySqlCommand cmd = new MySqlCommand(queryPhieuNhap, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@maNV", maNV);

                            // Tạm thời gán cứng Mã Nhà Cung Cấp = 1 để qua cửa MySQL
                            cmd.Parameters.AddWithValue("@maNCC", 1);

                            cmd.Parameters.AddWithValue("@tongTien", tongTienPhieu);
                            maPN = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 2. Tạo Chi Tiết Phiếu Nhập
                        string queryChiTiet = "INSERT INTO chitietphieunhap (MaPN, MaSach, SoLuongNhap, GiaNhap, ThanhTien) VALUES (@maPN, @maSach, @soLuongNhap, @giaNhap, @thanhTien)";
                        using (MySqlCommand cmd = new MySqlCommand(queryChiTiet, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@maPN", maPN);
                            cmd.Parameters.AddWithValue("@maSach", maSach);
                            cmd.Parameters.AddWithValue("@soLuongNhap", soLuongNhap);
                            cmd.Parameters.AddWithValue("@giaNhap", giaNhap);
                            cmd.Parameters.AddWithValue("@thanhTien", tongTienPhieu);
                            cmd.ExecuteNonQuery();
                        }

                        // 3. Cập nhật số lượng tồn kho và giá vốn (GiaNhap) mới cho cuốn sách đó
                        // 3. CẬP NHẬT KHO BẰNG PHƯƠNG PHÁP BÌNH QUÂN GIA QUYỀN
                        // Cập nhật giá nhập mới trước (trộn giá cũ và giá mới), sau đó mới cộng dồn số lượng
                        string queryUpdateSach = @"
    UPDATE sach 
    SET GiaNhap = ROUND(((SoLuongTon * GiaNhap) + (@soLuong * @giaNhap)) / (SoLuongTon + @soLuong)), 
        SoLuongTon = SoLuongTon + @soLuong 
    WHERE MaSach = @maSach";

                        using (MySqlCommand cmd = new MySqlCommand(queryUpdateSach, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@soLuong", soLuongNhap);
                            cmd.Parameters.AddWithValue("@giaNhap", giaNhap);
                            cmd.Parameters.AddWithValue("@maSach", maSach);
                            cmd.ExecuteNonQuery();
                        }

                        // 4. Ghi nhận lịch sử cho quản lý theo dõi
                        LichSuController.GhiLog(maNV, $"Lập phiếu nhập hàng ID {maPN}: Thêm +{soLuongNhap} cuốn sách (Mã: {maSach}) vào kho.");

                        // Hoàn tất giao dịch
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // In lỗi ra Output (Console) của Visual Studio để dễ debug nếu có lỗi khác
                        Console.WriteLine("Lỗi thực thi nhập hàng: " + ex.Message);
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool AddSach(Sach s)
        {
            string query = "INSERT INTO Sach (TenSach, MaTL, MaTG, MaNXB, NamXB, GiaNhap, SoLuongTon, GiaBan) " +
                           "VALUES (@tenSach, @maTL, @maTG, @maNXB, @namXB, @giaNhap, @soLuongTon, @giaBan)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@tenSach", s.TenSach),
                new MySqlParameter("@maTL", s.MaTL),
                new MySqlParameter("@maTG", s.MaTG),
                new MySqlParameter("@maNXB", s.MaNXB),
                new MySqlParameter("@namXB", s.NamXB),
                new MySqlParameter("@giaNhap", s.GiaNhap),
                new MySqlParameter("@soLuongTon", s.SoLuongTon),
                new MySqlParameter("@giaBan", s.GiaBan)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateSach(Sach s)
        {
            string query = "UPDATE Sach SET TenSach = @tenSach, MaTL = @maTL, MaTG = @maTG, " +
                           "MaNXB = @maNXB, NamXB = @namXB, GiaNhap = @giaNhap, " +
                           "SoLuongTon = @soLuongTon, GiaBan = @giaBan WHERE MaSach = @maSach";
            MySqlParameter[] parameters = {
                new MySqlParameter("@tenSach", s.TenSach),
                new MySqlParameter("@maTL", s.MaTL),
                new MySqlParameter("@maTG", s.MaTG),
                new MySqlParameter("@maNXB", s.MaNXB),
                new MySqlParameter("@namXB", s.NamXB),
                new MySqlParameter("@giaNhap", s.GiaNhap),
                new MySqlParameter("@soLuongTon", s.SoLuongTon),
                new MySqlParameter("@giaBan", s.GiaBan),
                new MySqlParameter("@maSach", s.MaSach)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteSach(int maSach)
        {
            string query = "DELETE FROM Sach WHERE MaSach = @maSach";
            MySqlParameter[] parameters = {
                new MySqlParameter("@maSach", maSach)
            };
            bool result = db.ExecuteNonQuery(query, parameters) > 0;

            if (result && quanlynhasach.Models.Session.MaNV > 0)
            {
                LichSuController.GhiLog(quanlynhasach.Models.Session.MaNV, $"Xóa sách có mã ID: {maSach}");
            }

            return result;
        }
    }
}