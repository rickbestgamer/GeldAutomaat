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

        private void Login(object sender, RoutedEventArgs e)
        {
            DBData.CheckUserLogin(TxBlRek.Text, TxBlPin.Text);
            //if (TxBlRek.Text != "" && TxBlPin.Text != "")
            //{
            //    Storyboard.SetTarget(MainCardAnimationDisapear, LoginGrid);
            //    MainCardAnimationDisapear.Begin();
            //    Storyboard.SetTarget(MainCardAnimationDisapear, LeftLoginButton);
            //    ((Ellipse)LeftLoginButton.Tag).MouseDown += Login;
            //    MainCardAnimationDisapear.Begin();
            //    Storyboard.SetTarget(MainCardAnimationDisapear, RightLoginButton);
            //    ((Ellipse)RightLoginButton.Tag).MouseDown += Login;
            //    MainCardAnimationDisapear.Begin();
            //    await Task.Delay(500);
            //    ChoicePage choicePage = new ChoicePage();
            //    NavigationService.Navigate(choicePage);
            //}
        }
    }
}
