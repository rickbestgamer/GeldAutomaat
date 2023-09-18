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
        public List<Transactions> transactions = new List<Transactions>();
        public DBData()
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

        public class Transactions
        {
            public Transactions() { }

            public int index { get; set; }
    }
}
