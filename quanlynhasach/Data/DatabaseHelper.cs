using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace quanlynhasach.Data
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Server=localhost;Database=quanlynhasach;Uid=root;Pwd=;";

        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public int ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
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