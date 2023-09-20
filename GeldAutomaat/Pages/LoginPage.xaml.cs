using GeldAutomaat.Classes;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeldAutomaat.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
            Storyboard MainCardAnimationDisapear;
            Storyboard MainCardAnimationApear;
        public LoginPage()
        {
            InitializeComponent();
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            LoginGrid.Opacity = 0;
            LoginGrid.Visibility = Visibility.Visible;
            LeftLoginButton.Opacity = 0;
            LeftLoginButton.Tag = BtnL4;
            LeftLoginButton.Visibility = Visibility.Visible;
            RightLoginButton.Opacity = 0;
            RightLoginButton.Tag = BtnR4;
            RightLoginButton.Visibility = Visibility.Visible;
            Storyboard.SetTarget(MainCardAnimationApear, LoginGrid);
            MainCardAnimationApear.Begin();
            Storyboard.SetTarget(MainCardAnimationApear, LeftLoginButton);
            ((Ellipse)LeftLoginButton.Tag).MouseDown += Login;
            MainCardAnimationApear.Begin();
            Storyboard.SetTarget(MainCardAnimationApear, RightLoginButton);
            ((Ellipse)RightLoginButton.Tag).MouseDown += Login;
            MainCardAnimationApear.Begin();
            DBData.GetConnection();
        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Ellipse)sender).Fill = Brushes.Gray;
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Ellipse)sender).Fill = Brushes.Black;
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            if (!DBData.CheckUserLogin(TxBlRek.Text, TxBlPin.Text)) return;
            Storyboard.SetTarget(MainCardAnimationDisapear, LoginGrid);
            MainCardAnimationDisapear.Begin();
            Storyboard.SetTarget(MainCardAnimationDisapear, LeftLoginButton);
            MainCardAnimationDisapear.Begin();
            Storyboard.SetTarget(MainCardAnimationDisapear, RightLoginButton);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            ChoicePage choicePage = new ChoicePage();
            if (User.UserD.Role == 0) { NavigationService.Navigate(choicePage); return; }
            AdminPage adminPage = new AdminPage();
            if (User.UserD.Role == 1) { NavigationService.Navigate(adminPage); return; }
            
        }

        private void CheckEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Login(sender, e);
        }
    }
}
