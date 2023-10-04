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
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Page
    {
                Storyboard MainCardAnimationDisapear;
                Storyboard MainCardAnimationApear;
        public LandingPage()
        {
            InitializeComponent();
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            MainCard.Opacity = 0;
            MainCard.Visibility = Visibility.Visible;
            WelcomeCard.Opacity = 0;
            WelcomeCard.Visibility = Visibility.Visible;
            LoadLogo.Opacity = 1;
            LoadLogo.Visibility = Visibility.Visible;
            Storyboard.SetTarget(MainCardAnimationApear, MainCard);
            MainCardAnimationApear.Begin();
        }

        private async void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainCard.Opacity == 1)
            {

                Storyboard.SetTarget(MainCardAnimationDisapear, MainCard);
                MainCardAnimationDisapear.Begin();
                await Task.Delay(500);
                Storyboard.SetTarget(MainCardAnimationApear, WelcomeCard);
                MainCardAnimationApear.Begin();
                await Task.Delay(500);
                Storyboard.SetTarget(MainCardAnimationDisapear, LoadLogo);
                MainCardAnimationDisapear.Begin();
                await Task.Delay(500);
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }
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
