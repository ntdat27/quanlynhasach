using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using quanlynhasach.Data;
using quanlynhasach.Models;

namespace quanlynhasach.Controllers
{
    public class NhanVienController
    {
        private DatabaseHelper db = new DatabaseHelper();

        // 1. Hàm băm mật khẩu chuẩn bảo mật SHA-256
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển mật khẩu thành mảng byte và băm
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển mảng byte trở lại thành chuỗi Hex (ký tự dạng số và chữ a-f)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool DangNhap(string taiKhoan, string matKhau)
        {
            try
            {
                // Băm mật khẩu người dùng vừa nhập vào ô TextBox
                string hashedPass = HashPassword(matKhau);

                // Đem mật khẩu ĐÃ BĂM đi so sánh với mật khẩu trong Database
                string query = "SELECT MaNV, TaiKhoan, HoTen, ChucVu FROM NhanVien WHERE TaiKhoan = @taiKhoan AND MatKhau = @matKhau";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@taiKhoan", taiKhoan),
                    new MySqlParameter("@matKhau", hashedPass)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    Session.MaNV = Convert.ToInt32(row["MaNV"]);
                    Session.TaiKhoan = row["TaiKhoan"].ToString();
                    Session.HoTen = row["HoTen"].ToString();
                    Session.ChucVu = row["ChucVu"].ToString();
                    LichSuController.GhiLog(Session.MaNV, "Đăng nhập vào hệ thống");

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();
            string query = "SELECT MaNV, HoTen, SoDienThoai, TaiKhoan, MatKhau, ChucVu FROM NhanVien";
            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVien
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    TaiKhoan = row["TaiKhoan"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    ChucVu = row["ChucVu"].ToString()
                });
            }
            return list;
        }

        public bool KiemTraTrung(string taiKhoan, string soDienThoai, int maNVIgnore = 0)
        {
            string query = @"SELECT MaNV FROM NhanVien 
                             WHERE (TaiKhoan = @taiKhoan OR SoDienThoai = @soDienThoai) 
                             AND MaNV != @maNVIgnore";
            MySqlParameter[] parameters = {
                new MySqlParameter("@taiKhoan", taiKhoan),
                new MySqlParameter("@soDienThoai", soDienThoai),
                new MySqlParameter("@maNVIgnore", maNVIgnore)
            };

            DataTable dt = db.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0;
        }

        public bool AddNhanVien(NhanVien nv)
        {
            try
            {
                // Mã hóa mật khẩu trước khi Insert vào bảng
                string hashedPass = HashPassword(nv.MatKhau);

                string query = "INSERT INTO NhanVien (HoTen, SoDienThoai, TaiKhoan, MatKhau, ChucVu) VALUES (@hoTen, @soDienThoai, @taiKhoan, @matKhau, @chucVu)";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@hoTen", nv.HoTen),
                    new MySqlParameter("@soDienThoai", nv.SoDienThoai),
                    new MySqlParameter("@taiKhoan", nv.TaiKhoan),
                    new MySqlParameter("@matKhau", hashedPass),
                    new MySqlParameter("@chucVu", nv.ChucVu)
                };
                return db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch { return false; }
        }

        public bool UpdateNhanVien(NhanVien nv)
        {
            try
            {
                string hashedPass = nv.MatKhau;
                // Kiểm tra: Chuỗi băm SHA-256 luôn dài 64 ký tự. 
                // Nếu độ dài khác 64, nghĩa là Admin vừa gõ một mật khẩu mới tinh (vd: "1234"), ta cần băm nó ra.
                // Nếu độ dài đã là 64, nghĩa là Admin chỉ cập nhật tên/sdt mà không đổi mật khẩu, ta giữ nguyên.
                if (nv.MatKhau.Length != 64)
                {
                    hashedPass = HashPassword(nv.MatKhau);
                }

                string query = "UPDATE NhanVien SET HoTen = @hoTen, SoDienThoai = @soDienThoai, TaiKhoan = @taiKhoan, MatKhau = @matKhau, ChucVu = @chucVu WHERE MaNV = @maNV";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@hoTen", nv.HoTen),
                    new MySqlParameter("@soDienThoai", nv.SoDienThoai),
                    new MySqlParameter("@taiKhoan", nv.TaiKhoan),
                    new MySqlParameter("@matKhau", hashedPass),
                    new MySqlParameter("@chucVu", nv.ChucVu),
                    new MySqlParameter("@maNV", nv.MaNV)
                };
                return db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch { return false; }
        }

        public bool DeleteNhanVien(int maNV)
        {
            try
            {
                string query = "DELETE FROM NhanVien WHERE MaNV = @maNV";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@maNV", maNV)
                };
                return db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch { return false; }
        }

        public DataTable SearchNhanVien(string tenNV, string sdt)
        {
            try
            {
                string query = @"
                    SELECT MaNV, HoTen, SoDienThoai, TaiKhoan, MatKhau, ChucVu 
                    FROM NhanVien 
                    WHERE HoTen LIKE @tenNV 
                      AND SoDienThoai LIKE @sdt";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@tenNV", "%" + tenNV + "%"),
                    new MySqlParameter("@sdt", "%" + sdt + "%")
                };

                return db.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tìm kiếm nhân viên: " + ex.Message);
                return new DataTable();
            }
        }
    }
}