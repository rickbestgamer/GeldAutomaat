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
    /// Interaction logic for ChangePinPage.xaml
    /// </summary>
    public partial class ChangePinPage : Page
    {
        Storyboard MainCardAnimationDisapear;
        Storyboard MainCardAnimationApear;
        public ChangePinPage()
        {
            InitializeComponent();
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");

            Storyboard.SetTarget(MainCardAnimationApear, ChoiceCard);
            MainCardAnimationApear.Begin();
            BtnL4.MouseDown += Prevpage;

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

        private async void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || NewPin.Text != PinRepeat.Text) return;

            string sql = "UPDATE `rekeningen` SET `RekeningPin` = @RekeningPin, `RekeningPinLength` = @RekeningPinLength WHERE `rekeningen`.`idRekeningen` = @idRekeningen";

            MySqlCommand command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Add("@RekeningPin", MySqlDbType.String).Value = DBData.HashPin(NewPin.Text);
            command.Parameters.Add("@RekeningPinLength", MySqlDbType.Int64).Value = NewPin.Text.Length;
            command.Parameters.Add("@idRekeningen", MySqlDbType.Int64).Value = User.UserD.Id;

            command.ExecuteNonQuery();

            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            NavigationService.GoBack();

        }

        private void CheckNum(object sender, KeyEventArgs e)
        {
            DBData.CheckNum(e);
        }
    }
}
