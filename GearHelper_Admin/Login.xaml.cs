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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT role from tbl_login WHERE Username = @username and password=@password";
            string returnValue = "";
            using (SqlConnection con = new SqlConnection(
            new SqlConnectionStringBuilder()
            {
                DataSource = "localhost",
                InitialCatalog = "gear-helper",
                UserID = "sa",
                Password = ""
            }.ConnectionString
            ))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    sqlcmd.Parameters.Add("@username", SqlDbType.VarChar).Value = textBoxUsername.Text;
                    sqlcmd.Parameters.Add("@password", SqlDbType.VarChar).Value = passwordBox1;
                    con.Open();
                    returnValue = (string)sqlcmd.ExecuteScalar();
                }
            }
            //EDIT to avoid NRE 
            if (String.IsNullOrEmpty(returnValue))
            {
                MessageBox.Show("Incorrect username or password");
                return;
            }
            returnValue = returnValue.Trim();
            if (returnValue == "Admin")
            {
                MessageBox.Show("You are logged in as an Admin");
                Window mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
        }
    }
}