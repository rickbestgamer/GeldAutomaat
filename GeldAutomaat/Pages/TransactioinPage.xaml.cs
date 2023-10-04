using GeldAutomaat.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeldAutomaat.Pages
{
    /// <summary>
    /// Interaction logic for TransactioinPage.xaml
    /// </summary>
    public partial class TransactioinPage : Page
    {
        Storyboard MainCardAnimationDisapear;
        Storyboard MainCardAnimationApear;
        public TransactioinPage()
        {
            InitializeComponent();
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            BtnL4.MouseDown += Prevpage;
            BtnR4.MouseDown += Prevpage;

            string sql = "SELECT * FROM `transacties` WHERE `transacties`.`Rekeningen_idRekeningen` = @Rekeningen_idRekeningen ORDER BY `transacties`.`idTransacties` DESC LIMIT 3";
            MySqlCommand command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Add("@Rekeningen_idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;
            MySqlDataReader reader = command.ExecuteReader();

            int index = 0;

            while (reader.Read())
            {
                string sType = "Opname";
                if ((sbyte)reader["Type"] == 1) sType = "Storting" ;
                TextBlock Timestamp = new TextBlock { Text = Convert.ToString(reader["TimeStamp"]) };
                Grid.SetColumn(Timestamp, 0);
                Grid.SetRow(Timestamp, index);

                TextBlock amount = new TextBlock { Text = Convert.ToString(reader["Amount"]) };
                Grid.SetColumn(amount, 1);
                Grid.SetRow(amount, index);

                TextBlock type = new TextBlock{ Text = sType };
                Grid.SetColumn(type, 2);
                Grid.SetRow(type, index);

                TransactionList.Children.Add(Timestamp);
                TransactionList.Children.Add(amount);
                TransactionList.Children.Add(type);

                index++;
            }
            reader.Close();

            Storyboard.SetTarget(MainCardAnimationApear, ChoiceCard);
            MainCardAnimationApear.Begin();
        }

        private async void Prevpage(object sender, MouseButtonEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
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
    }
}
