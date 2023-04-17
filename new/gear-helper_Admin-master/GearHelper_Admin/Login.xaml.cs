using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GearHelper_Admin
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void ValidateUser()
        {
            string query = "SELECT * from tbl_login WHERE name = @name and password=@password";
            User user;
            /*SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder()
            {
                Data Source = "localhost";
            InitialCatalog = "gear-helper";
            UserID = "root";
            }.ConnectionString;*/
            using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;User ID=username;Password=password;Database=gear-helper;")) //created a user with this data in phpmyadmin
            {
                using (MySqlCommand sqlcmd = new MySqlCommand(query, con))
                {
                    sqlcmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = usernameBox.Text;
                    sqlcmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = passwordBox.Text;
                    con.Open();
                    user = (User)sqlcmd.ExecuteScalar();
                }
            }
            if (user.Admin)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
            else
            {
                infoLabel.Content = "Incorrect username or password";
                return;
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateUser();
        }
    }
}
