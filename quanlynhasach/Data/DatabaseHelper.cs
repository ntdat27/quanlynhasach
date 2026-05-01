using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlynhasach.Data
{
    public class DatabaseHelper
    {
        // Nhớ thay đổi thông số host, user và password cho khớp với MySQL local của bạn
        private readonly string connectionString = "Server=localhost;Database=quanlynhasach;Uid=root;Pwd=;";

        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Bổ sung thêm hàm ExecuteNonQuery để dành cho các lệnh INSERT, UPDATE, DELETE sau này
        public int ExecuteNonQuery(string query)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result; // Trả về số dòng bị ảnh hưởng
        }
        // Hàm mới: Thực thi lệnh và trả về 1 ID vừa được tạo
        public int ExecuteScalar(string query)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object obj = cmd.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                    {
                        result = Convert.ToInt32(obj);
                    }
                }
            }
            return result;
        }
    }
}
