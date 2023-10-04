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
    /// Interaction logic for ChoicePage.xaml
    /// </summary>
    public partial class ChoicePage : Page
    {
        Storyboard MainCardAnimationDisapear;
        Storyboard MainCardAnimationApear;
        public ChoicePage()
        {
            InitializeComponent();
            ChoiceCard.Opacity = 0;
            HelloText.Text = "Hallo " + User.UserD.Name;
            ChoiceCard.Visibility = Visibility.Visible;
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            Storyboard.SetTarget(MainCardAnimationApear, ChoiceCard);
            MainCardAnimationApear.Begin();

            BtnL1.MouseDown += Balance;
            BtnL4.MouseDown += PrevPage;
            BtnR1.MouseDown += Withdawl;
            BtnR2.MouseDown += Deposit;
            BtnR3.MouseDown += Transaction;
            BtnR4.MouseDown += ChangePin;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigating += BackAnimation;
        }

        private void BackAnimation(object sender, NavigatingCancelEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationApear, ChoiceCard);
            MainCardAnimationApear.Begin();
        }

        private async void Transaction(object sender, MouseButtonEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            TransactioinPage transactioinPage = new TransactioinPage();
            NavigationService.Navigate(transactioinPage);
        }

        private async void Deposit(object sender, MouseButtonEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            DepositePage depositePage = new DepositePage();
            NavigationService.Navigate(depositePage);
        }

        private async void Withdawl(object sender, MouseButtonEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            WithdrawlPage withdrawlPage = new WithdrawlPage();
            NavigationService.Navigate(withdrawlPage);
        }

        private async void ChangePin(object sender, MouseButtonEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            ChangePinPage changePinPage = new ChangePinPage();
            NavigationService.Navigate(changePinPage);
        }

        private async void PrevPage(object sender, MouseButtonEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            NavigationService.GoBack();
            NavigationService.GoBack();
        }

        private async void Balance(object sender, MouseButtonEventArgs e)
        {
            Storyboard.SetTarget(MainCardAnimationDisapear, ChoiceCard);
            MainCardAnimationDisapear.Begin();
            await Task.Delay(500);
            BalancePage balancePage = new BalancePage();
            NavigationService.Navigate(balancePage);
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
