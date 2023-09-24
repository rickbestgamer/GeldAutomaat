using GeldAutomaat.Classes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
    /// Interaction logic for DepositePage.xaml
    /// </summary>
    public partial class DepositePage : Page
    {
        public DepositePage()
        {
            InitializeComponent();
            BtnR4.MouseDown += Prevpage;
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
            if (e.Key != Key.Return) return;
            if (TransactionAmount.Text == "") return;
            if (Convert.ToInt64(TransactionAmount.Text) <= 10 && Convert.ToInt64(TransactionAmount.Text) > 2000) return;



            string sql = "SELECT `Balance` FROM `rekeningen` WHERE `rekeningen`.`idRekeningen` = @idRekeningen";
            MySqlCommand command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Clear();
            command.Parameters.Add("@idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            sql = "UPDATE `rekeningen` SET `Balance` = @Balance WHERE `rekeningen`.`idRekeningen` = @idRekeningen";
            command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Add("@Balance", MySqlDbType.Int64).Value = (int)reader["Balance"] + Convert.ToInt64(TransactionAmount.Text)- 2;
            command.Parameters.Add("@idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;
            reader.Close();
            command.ExecuteNonQuery();
            NavigationService.GoBack();

            sql = "INSERT INTO `transacties` (`Type`, `Amount`, `TimeStamp`, `Rekeningen_idRekeningen`) VALUES ('0', @Amount, current_timestamp(), @Rekeningen_idRekeningen);";
            command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Add("@Amount", MySqlDbType.Int64).Value = Convert.ToInt64(TransactionAmount.Text) - 2;
            command.Parameters.Add("@Rekeningen_idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;

            command.ExecuteNonQuery();
        }
    }
}
