using MySql.Data.MySqlClient;
using System.Data;

namespace PROJEKT_72413.Database
{
    public class DatabaseService : IDataService
    {
        private string connectionString = "server=localhost;database=biuro_podrozy;uid=root;pwd=admin123;";

        public bool TestConnection()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open(); return true;
                }
            }
            catch { return false; }
        }

        public DataTable GetTable(string sql)
        {
            DataTable dt = new DataTable();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                new MySqlDataAdapter(sql, conn).Fill(dt);
            }
            return dt;
        }

        public int ExecuteCommand(string sql)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return new MySqlCommand(sql, conn).ExecuteNonQuery();
            }
        }
    }
}