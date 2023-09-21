using GeldAutomaat.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeldAutomaat.Pages
{
    /// <summary>
    /// Interaction logic for WithdrawlPage.xaml
    /// </summary>
    public partial class WithdrawlPage : Page
    {
        public WithdrawlPage()
        {
            InitializeComponent();

            BtnL4.MouseDown += Prevpage;

        }

        private void Prevpage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Ellipse)sender).Fill = Brushes.Gray;
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Ellipse)sender).Fill = Brushes.Black;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("1");
            if (e.Key != Key.Return) return;
            Debug.WriteLine("2");
            if (TransactionAmount.Text == "") return;
            Debug.WriteLine("3");
            if (Convert.ToInt64(TransactionAmount.Text) <= 0 && Convert.ToInt64(TransactionAmount.Text) > 500) return;
            Debug.WriteLine("4");

            string sql = "INSERT INTO `transacties` (`Type`, `Amount`, `TimeStamp`, `Rekeningen_idRekeningen`) VALUES ('1', @Amount, current_timestamp(), @Rekeningen_idRekeningen);";
            MySqlCommand command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Add("@Amount", MySqlDbType.Int64).Value = Convert.ToInt64(TransactionAmount.Text);
            command.Parameters.Add("@Rekeningen_idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;

            command.ExecuteNonQuery();

            sql = "SELECT `Balance` FROM `rekeningen` WHERE `rekeningen`.`idRekeningen` = @idRekeningen";
            command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Clear();
            command.Parameters.Add("@idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;
            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                if ((int)reader["Balance"] < Convert.ToInt64(TransactionAmount.Text)) { reader.Close(); return; }

                sql = "UPDATE `rekeningen` SET `Balance` = @Balance WHERE `rekeningen`.`idRekeningen` = @idRekeningen";
                command = new MySqlCommand(sql, DBData.connection);
                command.Parameters.Add("@Balance", MySqlDbType.Int64).Value = (int)reader["Balance"] - Convert.ToInt64(TransactionAmount.Text);
                command.Parameters.Add("@idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;
                reader.Close();
                command.ExecuteNonQuery();
            NavigationService.GoBack();
                return;
            }


        }
    }
}
