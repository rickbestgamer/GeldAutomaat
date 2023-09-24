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

            BalanceButton.Tag = BtnL1;
            RekOverviewButton.Tag = BtnL2;
            QuitButton.Tag = BtnL4;
            WithdrawlButton.Tag = BtnR1;
            DepositButton.Tag = BtnR2;
            TransactionButton.Tag = BtnR3;
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

            BtnL1.MouseDown += Balance;
            BtnL2.MouseDown += RekOverview;
            BtnL4.MouseDown += PrevPage;
            BtnR1.MouseDown += Withdawl; ;
            BtnR2.MouseDown += Deposit; ;
            BtnR3.MouseDown += Transaction; ;
            BtnR4.MouseDown += ChangePin;

            HelloText.Text = "Hallo " + User.UserD.Name;
            ChoiceCard.Opacity = 0;
            ChoiceCard.Visibility = Visibility.Visible;
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            Storyboard.SetTarget(MainCardAnimationApear, ChoiceCard);
            MainCardAnimationApear.Begin();
        }

        private void Transaction(object sender, MouseButtonEventArgs e)
        {
            TransactioinPage transactioinPage = new TransactioinPage();
            NavigationService.Navigate(transactioinPage);
        }

        private void Deposit(object sender, MouseButtonEventArgs e)
        {
            DepositePage depositePage = new DepositePage();
            NavigationService.Navigate(depositePage);
        }

        private void Withdawl(object sender, MouseButtonEventArgs e)
        {
            WithdrawlPage withdrawlPage = new WithdrawlPage();
            NavigationService.Navigate(withdrawlPage);
        }

        private void ChangePin(object sender, MouseButtonEventArgs e)
        {
            ChangePinPage changePinPage = new ChangePinPage();
            NavigationService.Navigate(changePinPage);
        }

        private void Balance(object sender, MouseButtonEventArgs e)
        {
            BalancePage balancePage = new BalancePage();
            NavigationService.Navigate(balancePage);
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
