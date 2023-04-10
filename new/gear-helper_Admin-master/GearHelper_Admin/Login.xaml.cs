using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GearHelper_Admin
{
    /// <summary>  
    /// Interaction logic for MainWindow.xaml  
    /// </summary>   
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string user = usernameBox.Text;
            string pass = passwordBox.Text;
            using (SqlConnection conn = new SqlConnection("server=localhost; database=gear-helper; user id=root"))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from mysqlcsharp where username = @Username and password = @Password", conn);
                command.Parameters.AddWithValue("@Username", user);
                command.Parameters.AddWithValue("@Password", pass);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read() == true)
                {
                    MessageBox.Show("OK");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password");
                }
                conn.Close();
            }
        }
    }
}