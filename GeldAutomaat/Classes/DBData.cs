using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GeldAutomaat.Pages;
using MySql.Data.MySqlClient;

namespace GeldAutomaat.Classes
{
    internal class DBData
    {
        public static MySqlConnection connection;
        public static string myConnectionString = "server=localhost;uid=root;" +
                "pwd=;database=geldautomaat";
        public static void GetConnection()
        {
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = myConnectionString;
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool CheckUserLogin(string UserName, string UserPin)
        {
            if (connection == null || UserName == "" || UserPin == "") return false;

            string sql = "Select * FROM `rekeningen` WHERE `RekeningNummer` = @RekeningNummer AND `RekeningPin` = @RekeningPin";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add("@RekeningNummer", MySqlDbType.String).Value = UserName;
            command.Parameters.Add("@RekeningPin", MySqlDbType.String).Value = UserPin;
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) { ReadReader(reader); reader.Close(); return true; }

            reader.Close();
            return false;
        }

        private static void ReadReader(MySqlDataReader reader)
        {
            while (reader.Read())
            {
                User.UserD.Id = (int)reader["idRekeningen"];
                User.UserD.Name = (string)reader["Name"];
                User.UserD.Role = (sbyte)reader["Role"];
            }
        }

        public static void GetAllUsers(Grid grid)
        {
            string sql = "SELECT * FROM `rekeningen`";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { reader.Close(); return; }

            grid.RowDefinitions.Add(new RowDefinition());

            CreateHeaderTextBlock(grid, "Naam");
            CreateHeaderTextBlock(grid, "Rekening nummer");
            CreateHeaderTextBlock(grid, "Pin");
            CreateHeaderTextBlock(grid, "Saldo");
            CreateHeaderTextBlock(grid, "Rol");
            CreateHeaderTextBlock(grid, "Actions");




            while (reader.Read())
            {
                if ((int)reader["idRekeningen"] == User.UserD.Id) continue;
                string
                    role = "",
                    pin = "";
                if ((sbyte)reader["Role"] == 0) role = "User";
                if ((sbyte)reader["Role"] == 1) role = "Admin";
                if ((sbyte)reader["Active"] == 0) role = "Inactive";

                for (int i = 0; i < (int)reader["RekeningPinLength"]; i++)
                {
                    pin += "*";
                }

                grid.RowDefinitions.Add(new RowDefinition());
                CreateTextBlock(grid, (string)reader["Name"], (int)reader["idRekeningen"], 1);
                CreateTextBlock(grid, (string)reader["RekeningNummer"], (int)reader["idRekeningen"], 2);
                CreateTextBlock(grid, pin, (int)reader["idRekeningen"], 3);
                CreateTextBlock(grid, Convert.ToString(reader["Balance"]), (int)reader["idRekeningen"], 4);
                CreateTextBlock(grid, role, (int)reader["idRekeningen"], 5);
                CreateActionsTextBlock(grid, (int)reader["idRekeningen"], (sbyte)reader["Active"]);
            }
            reader.Close();
        }

        private static void CreateHeaderTextBlock(Grid grid, string text)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            TextBlock ReknummerTextBlock = new TextBlock { Text = text, Background = Brushes.White, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetColumn(ReknummerTextBlock, grid.ColumnDefinitions.Count - 1);
            Grid.SetRow(ReknummerTextBlock, grid.RowDefinitions.Count - 1);
            grid.Children.Add(ReknummerTextBlock);
        }

        private static void CreateActionsTextBlock(Grid grid, int id, int active)
        {
            ArrayList arrayList = new ArrayList { id, grid };
            string state = "Inactive";
            if (active != 0) state = "Active";

            Grid actionGrid = new Grid { ColumnDefinitions = { new ColumnDefinition(), new ColumnDefinition() } };
            Button editButton = new Button { Tag = arrayList, Content = "Edit"};
            Button stateButton = new Button { Tag = id, Content = state};
            editButton.Click += EditUser;
            stateButton.Click += EditState;

            Grid.SetColumn(editButton, 0);
            Grid.SetColumn(stateButton, 1);

            actionGrid.Children.Add(editButton);
            actionGrid.Children.Add(stateButton);

            Grid.SetColumn(actionGrid, grid.ColumnDefinitions.Count - 1);
            Grid.SetRow(actionGrid, grid.RowDefinitions.Count - 1);
            grid.Children.Add(actionGrid);
        }

        private static void EditUser(object sender, RoutedEventArgs e)
        {
            int id = 0;
            Grid grid = null;
            ArrayList textBoxes = new ArrayList();

            foreach (var item in (ArrayList)((Button)sender).Tag)
            {
                if (item is int) id = (int)item;
                if (item is Grid) grid = (Grid)item;
            }

            foreach (var item in grid.Children)
            {
                if (!(item is TextBlock)) continue;
                if (!(((TextBlock)item).Tag is int)) continue;
                if ((int)((TextBlock)item).Tag != id) continue;
                CreateTextBox((TextBlock)item, textBoxes);
            }
            foreach (var item in textBoxes)
            {
                grid.Children.Add((UIElement)item);
            }

            ArrayList TagList = new ArrayList { textBoxes, id, grid };

            ((Button)sender).Content = "Confirm";
            ((Button)sender).Click -= EditUser;
            ((Button)sender).Click += ConfirmUser;
            ((Button)sender).Tag = TagList;

        }

        private static void ConfirmUser(object sender, RoutedEventArgs e)
        {
            int id = 0;
            Grid grid = null;
            ArrayList textBoxes = new ArrayList();

            foreach (var item in (ArrayList)((Button)sender).Tag)
            {
                if (item is int) id = (int)item;
                if (item is Grid) grid = (Grid)item;
                if (item is ArrayList) textBoxes = (ArrayList)item;
            }

            foreach (var item in textBoxes)
            {
                if (item is TextBox)
                {
                    foreach (var item1 in grid.Children)
                    {
                        if (!(item1 is TextBlock)) continue;
                        if (!(((TextBlock)item1).Tag is int)) continue;
                        if ((int)((TextBlock)item1).Tag != id) continue;
                        if (Grid.GetColumn((TextBlock)item1) != Grid.GetColumn((TextBox)item) && Grid.GetRow((TextBlock)item1) != Grid.GetRow((TextBox)item)) continue;
                    }
                }
                if (item is ComboBox)
                {
                    Debug.WriteLine(((ComboBox)item).Text);
                }
                grid.Children.Remove((UIElement)item);
            }
                ((Button)sender).Content = "Edit";
                ((Button)sender).Click += EditUser;
                ((Button)sender).Click -= ConfirmUser;

        }

        private static void CreateTextBox(TextBlock textBlock, ArrayList textBoxes)
        {
            if (Grid.GetColumn(textBlock) == 3) return; 
            if (Grid.GetColumn(textBlock) == 4)
            {
                int index = 0;
                ComboBox comboBox = new ComboBox();
                comboBox.Items.Add(new TextBlock { Text = "User" });
                comboBox.Items.Add(new TextBlock { Text = "Admin"});
                if (textBlock.Text == "Admin") index = 1;

                Grid.SetColumn(comboBox, Grid.GetColumn(textBlock));
                Grid.SetRow(comboBox, Grid.GetRow(textBlock));

                comboBox.SelectedIndex = index;
                textBoxes.Add(comboBox);
                return;
            }
            TextBox textBox = new TextBox();
            textBox.Text = textBlock.Text;

            Grid.SetColumn(textBox, Grid.GetColumn(textBlock));
            Grid.SetRow(textBox, Grid.GetRow(textBlock));

            textBoxes.Add(textBox);
        }

        private static void EditState(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            byte dbState = 0;
            string state = "Inactive";
            string sql = "UPDATE `rekeningen` SET `Active` = @STATE WHERE `rekeningen`.`idRekeningen` = @Id";
            MySqlCommand command = new MySqlCommand(sql, connection);

            if ((string)((Button)sender).Content == "Inactive") { state = "Active"; dbState = 1; }
            command.Parameters.Add("@STATE", MySqlDbType.Byte).Value = dbState;
            command.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
            command.ExecuteNonQuery();
            ((Button)sender).Content = state;
        }

        private static void CreateTextBlock(Grid grid, string text, int id, int row)
        {
            TextBlock ReknummerTextBlock = new TextBlock { Text = text, Tag = id, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetColumn(ReknummerTextBlock, row - 1);
            Grid.SetRow(ReknummerTextBlock, grid.RowDefinitions.Count - 1);
            grid.Children.Add(ReknummerTextBlock);
        }
    }

    public class User
    {
        public static User UserD = new User();
        public User() { }

        private static int id;
        private static string name;
        private static int role;
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Role { get { return role; } set { role = value; } }
    }

    public class Transactions
    {
        public static List<Transactions> transactions = new List<Transactions>();
        public Transactions() { }

        public int Index { get; set; }
        public int Amount { get; set; }
    }
}
