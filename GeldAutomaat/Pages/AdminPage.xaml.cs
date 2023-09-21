using GeldAutomaat.Classes;
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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        Storyboard MainCardAnimationDisapear;
        Storyboard MainCardAnimationApear;
        public AdminPage()
        {
            InitializeComponent();

            RequestButton.Tag = BtnL1;
            RekOverviewButton.Tag = BtnL2;
            QuitButton.Tag = BtnL4;
            WithdrawlButton.Tag = BtnR1;
            ChangePinButton.Tag = BtnR4;

            foreach (var item in ChoiceCard.Children)
            {
                if (item is Border)
                {
                    if (((Border)item).Tag is Ellipse)
                    {
                        ((Ellipse)((Border)item).Tag).MouseEnter += Ellipse_MouseEnter;
                        ((Ellipse)((Border)item).Tag).MouseLeave += Ellipse_MouseLeave;
                    }
                }
            }

            BtnL2.MouseDown += RekOverview;
            BtnL4.MouseDown += PrevPage; ;

            HelloText.Text = "Hello " + User.UserD.Name;
            ChoiceCard.Opacity = 0;
            ChoiceCard.Visibility = Visibility.Visible;
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            Storyboard.SetTarget(MainCardAnimationApear, ChoiceCard);
            MainCardAnimationApear.Begin();
        }

        private void PrevPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.GoBack();
        }

        private void RekOverview(object sender, MouseButtonEventArgs e)
        {
            UserOverview userOverview = new UserOverview();
            NavigationService.Navigate(userOverview);
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
