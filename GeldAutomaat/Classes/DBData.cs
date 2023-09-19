using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace GeldAutomaat.Classes
{
    internal class DBData
    {
        public static MySqlConnection connection;
        public static string myConnectionString = "server=localhost;uid=root;" +
                "pwd=;database=geldautomaat";
        public static void GetConnection()
        {
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = myConnectionString;
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CheckUserLogin(string UserName, string UserPin)
        {
            if (connection == null || UserName == "" || UserPin == "") return;
            string sql = "Select * FROM `rekeningen` WHERE `RekeningNummer` = @RekeningNummer AND `RekeningPin` = @RekeningPin";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add("@RekeningNummer", MySqlDbType.String).Value = UserName;
            command.Parameters.Add("@RekeningPin", MySqlDbType.String).Value = UserPin;
            MySqlDataReader reader = command.ExecuteReader();
            Debug.WriteLine("1");
            if (reader.HasRows) { ReadReader(reader); reader.Close(); return; }
            reader.Close();
            sql = "Select * FROM `admin` WHERE `AdminName` = @AdminName  AND `AdminPin` = @AdminPin";
            command = new MySqlCommand(sql, connection);
            command.Parameters.Add("@AdminName", MySqlDbType.String).Value = UserName;
            command.Parameters.Add("@AdminPin", MySqlDbType.String).Value = UserPin;
            reader = command.ExecuteReader();
            Debug.WriteLine("2");
            if (reader.HasRows) { ReadReader(reader); reader.Close(); return; }
            reader.Close();
        }

        private static void ReadReader(MySqlDataReader reader)
        {
            Debug.WriteLine("3");
            while (reader.Read())
            {

            }
        }
    }

    public class User
    {
        public static User UserD = new User();
        public User() { }

        private static int id;
        private static string name;
        private static bool isAdmin;
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public bool IsAdmin { get { return isAdmin; } set { isAdmin = value; } }
    }

    public class Transactions
    {
        public static List<Transactions> transactions = new List<Transactions>();
        public Transactions() { }

        public int Index { get; set; }
        public int Amount { get; set; }
    }
}
