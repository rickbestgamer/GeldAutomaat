﻿using GeldAutomaat.Classes;
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
    /// Interaction logic for BalancePage.xaml
    /// </summary>
    public partial class BalancePage : Page
    {
        Storyboard MainCardAnimationDisapear;
        Storyboard MainCardAnimationApear;
        public BalancePage()
        {
            InitializeComponent();
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            string sql = "SELECT `Balance` FROM `rekeningen` WHERE `idRekeningen` = @idRekeningen";
            MySqlCommand command = new MySqlCommand(sql, DBData.connection);
            command.Parameters.Add("@idRekeningen", MySqlDbType.Int32).Value = User.UserD.Id;

            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows) return;

            while (reader.Read())
            {
                BalanceTxBl.Text = "€ " + Convert.ToString(reader["Balance"]);
            }
            reader.Close();

            BtnL4.MouseDown += Prevpage;

            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
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
