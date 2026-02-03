using System;
using System.Data;
using MySql.Data.MySqlClient; 

namespace PROJEKT_72413.Database
{
    // Implementacja interfejsu 
    public class ObslugaBazy : IUslugaDanych
    {
        // Połączenie z bazą
        private string connectionString = "server=localhost;database=biuro_podrozy;uid=root;pwd=admin123;";

        // Implementacja metody wykonującej zmiany w bazie 
        public void WykonajPolecenie(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Otwarcie połączenia
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery(); // Wykonanie komendy SQL
            }
        }

        // Implementacja metody pobierającej dane 
        public DataTable PobierzTabele(string sql)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); 
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt); // Wypełnienie tabeli wynikami z bazy
            }
            return dt; 
        }
    }
}  