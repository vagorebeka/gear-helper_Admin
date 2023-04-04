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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void listItems()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(itemUrl).Result;
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(result);
                listBox.Items.Clear();
                foreach (var item in items)
                {
                    listBox.Items.Add(item);
                }
            }
        }

        private void itemList_Click(object sender, RoutedEventArgs e)
        {
            listItems();
        }

        private void userList_Click(object sender, RoutedEventArgs e)
        {

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
