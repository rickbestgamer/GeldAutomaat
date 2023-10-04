using GeldAutomaat.Classes;
using System;
using System.Collections;
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
    /// Interaction logic for UserOverview.xaml
    /// </summary>
    public partial class UserOverview : Page
    {
        ArrayList NewUserElements = new ArrayList();
        Storyboard MainCardAnimationDisapear;
        Storyboard MainCardAnimationApear;
        public UserOverview()
        {
            InitializeComponent();
            MainCardAnimationDisapear = (Storyboard)FindResource("MainCardAnimationDisapear");
            MainCardAnimationApear = (Storyboard)FindResource("MainCardAnimationApear");
            DBData.GetAllUsers(UserList);

            AddUserButton.Tag = BtnR4;
            QuitButton.Tag = BtnL4;
            BtnR4.MouseDown += AddUser;
            BtnL4.MouseDown += Prevpage;

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

        private void AddUser(object sender, MouseButtonEventArgs e)
        {
            if (NewUserElements != null)
            {
                foreach (var item in NewUserElements)
                {
                    if (UserList.Children.Contains((UIElement)item)) return;
                }
            }

            UserList.RowDefinitions.Add(new RowDefinition());


            TextBox nameTextBox = new TextBox();
            TextBox rekeningTextBox = new TextBox();
            TextBox rekeningPin = new TextBox();

            rekeningTextBox.KeyDown += CheckNum;
            rekeningPin.KeyDown += CheckNum;

            Grid.SetColumn(nameTextBox, 0);
            Grid.SetColumn(rekeningTextBox, 1);
            Grid.SetColumn(rekeningPin, 2);
            Grid.SetRow(nameTextBox, UserList.RowDefinitions.Count - 1);
            Grid.SetRow(rekeningTextBox, UserList.RowDefinitions.Count - 1);
            Grid.SetRow(rekeningPin, UserList.RowDefinitions.Count - 1);

            NewUserElements.Add(nameTextBox);
            NewUserElements.Add(rekeningTextBox);
            NewUserElements.Add(rekeningPin);

            DBData.CreateActionsTextBlock(UserList, 0, 1, NewUserElements);

            UserList.Children.Add(nameTextBox);
            UserList.Children.Add(rekeningTextBox);
            UserList.Children.Add(rekeningPin);
        }

        private void CheckNum(object sender, KeyEventArgs e)
        {
            DBData.CheckNum(e);
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
