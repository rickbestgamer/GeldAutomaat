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
        public LandingPage()
        {
            InitializeComponent();
        }

        private async void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainCard.Opacity != 0)
            {
                Storyboard storyboard = (Storyboard)FindResource("MainCardAnimationDisapear");
                Storyboard.SetTarget(storyboard, MainCard);
                storyboard.Begin();
                await Task.Delay(500);
                storyboard = (Storyboard)FindResource("MainCardAnimationApear");
                Storyboard.SetTarget(storyboard, WelcomeCard);
                storyboard.Begin();
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
