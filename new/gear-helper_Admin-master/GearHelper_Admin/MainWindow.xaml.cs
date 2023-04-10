using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace GearHelper_Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string itemUrl = "http://localhost:8000/api/item";
        private string userUrl = "http://localhost:8000/api/user";
        ListBox listBox = new ListBox();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addListBox()
        {
            stackPanel.Children.Clear();
            stackPanel.Children.Add(listBox);
            listBox.Name = "listBox";
            listBox.FontFamily = new FontFamily("Consolas");
        }

        private void listItems()
        {
            addListBox();
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(itemUrl).Result;
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(result);
                listBox.Items.Clear();
                listBox.Items.Add(String.Format("{0,-3} {1,-25} {2,-7} {3,-7} {4,-7} {5,-8} {6}", "ID", "name", "stat1", "stat2", "stat3", "slot", "material"));
                foreach (var item in items)
                {
                    listBox.Items.Add(item);
                }
            }
        }

        private void listUsers()
        {
            addListBox();
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(userUrl).Result;
                List<User> users = JsonConvert.DeserializeObject<List<User>>(result);
                listBox.Items.Clear();
                listBox.Items.Add(String.Format("{0,-3} {1,-15} {2,-35} {3,-5}", "ID", "username", "email", "admin"));
                foreach (var user in users)
                {
                    listBox.Items.Add(user);
                }
            }
        }

        private void itemList_Click(object sender, RoutedEventArgs e)
        {
            listItems();
        }

        private void userList_Click(object sender, RoutedEventArgs e)
        {
            listUsers();
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            //listBox.Items.Clear();
            //textBlock.Text = "teszt";
        }
    }
}
