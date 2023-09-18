using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace GeldAutomaat.Classes
{
    internal class DBData
    {
        public MySqlConnection connection;
        readonly string myConnectionString = "server=localhost;uid=root;" +
                "pwd=;database=geldautomaat";
        public static void GetConnection()
        {
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = myConnectionString;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CheckUserLogin()
        {
            if (connection == null) return;
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
