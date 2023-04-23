using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace GearHelper_Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string itemUrl = "http://localhost:8000/api/item";
        private readonly string userUrl = "http://localhost:8000/api/user";
        ListBox listBox = new ListBox();

        TextBox nameBox = new TextBox();
        TextBox stat1amountBox = new TextBox();
        TextBox stat2amountBox = new TextBox();
        TextBox stat3amountBox = new TextBox();
        ComboBox stat1ComboBox = new ComboBox();
        ComboBox stat2ComboBox = new ComboBox();
        ComboBox stat3ComboBox = new ComboBox();
        ComboBox slotBox = new ComboBox();
        ComboBox materialBox = new ComboBox();
        int material = 0;

        TextBox emailBox = new TextBox();
        CheckBox isAdminBox = new CheckBox();

        Label label = new Label();
        Button cancelButton = new Button();

        List<String> itemNameList = new List<String>();

        MessageBoxButton okButton = MessageBoxButton.OK;
        String errorCaption = "Error";

        public MainWindow()
        {
            InitializeComponent();
        }

        // GENERAL

        private List<String> itemNamesToList()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(itemUrl).Result;
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(result);
                foreach (var item in items)
                {
                    itemNameList.Add(item.Name.ToLower().Trim());
                }
            }
            return itemNameList;
        }

        private void addListBox()
        {
            stackPanel.Children.Clear();
            stackPanel.Children.Add(listBox);
            listBox.Name = "listBox";
            listBox.FontFamily = new FontFamily("Consolas");
        }

        private void addCancelButton()
        {
            cancelButton.Content = "Cancel";
            cancelButton.Width = 70;
            cancelButton.HorizontalAlignment = HorizontalAlignment.Left;
            cancelButton.Margin = new Thickness(0, 5, 5, 5);
            cancelButton.Click += CancelButton_Click;
        }
        
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
            stackPanel.Children.Add(defaultText);
        }
        
        // MESSAGEBOXES

        private void nothingSelected()
        {
            String message = "Please choose an item or user";
            String caption = "Error";
            MessageBox.Show(message, caption, okButton);
        }

        private void somethingWentWrong()
        {
            String message = "Something went wrong. Please try again";
            MessageBox.Show(message, errorCaption, okButton);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            String message = "Are you sure?";
            String caption = "Exit";
            MessageBoxButton yesNoButton = MessageBoxButton.YesNo;
            if (MessageBox.Show(message, caption, yesNoButton) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        // FORMS

        private void comboBoxAddItems()
        {
            // TODO: add statistic class and clean up this code
            stat1ComboBox.Items.Add("AGI");
            stat1ComboBox.Items.Add("INT");
            stat1ComboBox.Items.Add("STA");
            stat1ComboBox.Items.Add("STR");
            stat1ComboBox.Items.Add("SPI");
            stat2ComboBox.Items.Add("AGI");
            stat2ComboBox.Items.Add("INT");
            stat2ComboBox.Items.Add("STA");
            stat2ComboBox.Items.Add("STR");
            stat2ComboBox.Items.Add("SPI");
            stat3ComboBox.Items.Add("AGI");
            stat3ComboBox.Items.Add("INT");
            stat3ComboBox.Items.Add("STA");
            stat3ComboBox.Items.Add("STR");
            stat3ComboBox.Items.Add("SPI");

            slotBox.Items.Add("head");
            slotBox.Items.Add("torso");
            slotBox.Items.Add("legs");

            materialBox.Items.Add("cloth");
            materialBox.Items.Add("leather");
            materialBox.Items.Add("plate");
        }

        private void clearItemForm()
        {
            nameBox.Clear();
            stat1ComboBox.Items.Clear();
            stat2ComboBox.Items.Clear();
            stat3ComboBox.Items.Clear();
            stat1amountBox.Clear();
            stat2amountBox.Clear();
            stat3amountBox.Clear();
            slotBox.Items.Clear();
            materialBox.Items.Clear();
        }
        
        // LIST

        private void itemList_Click(object sender, RoutedEventArgs e)
        {
            clearItemForm();
            listItems();
        }
        
        private void userList_Click(object sender, RoutedEventArgs e)
        {
            listUsers();
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

        // ADD
        
        private void newItem_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
            comboBoxAddItems();
            addCancelButton();

            Label nameLabel = new Label();
            nameLabel.Content = "Name";
            Label stat1Label = new Label();
            stat1Label.Content = "Stat1";
            Label stat1amountLabel = new Label();
            stat1amountLabel.Content = "Stat1 amount";
            Label stat2Label = new Label();
            stat2Label.Content = "Stat2";
            Label stat2amountLabel = new Label();
            stat2amountLabel.Content = "Stat2 amount";
            Label stat3Label = new Label();
            stat3Label.Content = "Stat3";
            Label stat3amountLabel = new Label();
            stat3amountLabel.Content = "Stat3 amount";
            Label slotLabel = new Label();
            slotLabel.Content = "Slot";
            Label materialLabel = new Label();
            materialLabel.Content = "Material";
            Button addItem = new Button();
            addItem.Content = "Add item";
            addItem.Width = 70;
            addItem.HorizontalAlignment = HorizontalAlignment.Left;
            addItem.Margin = new Thickness(0,15,5,5);

            stackPanel.Children.Add(nameLabel);
            nameBox.Text = "";
            nameBox.IsEnabled = true;
            stackPanel.Children.Add(nameBox);
            stackPanel.Children.Add(stat1Label);
            stat1ComboBox.SelectedItem = null;
            stackPanel.Children.Add(stat1ComboBox);
            stackPanel.Children.Add(stat1amountLabel);
            stat1amountBox.Text = "";
            stackPanel.Children.Add(stat1amountBox);
            stackPanel.Children.Add(stat2Label);
            stat2ComboBox.SelectedItem = null;
            stackPanel.Children.Add(stat2ComboBox);
            stackPanel.Children.Add(stat2amountLabel);
            stat2amountBox.Text = "";
            stackPanel.Children.Add(stat2amountBox);
            stackPanel.Children.Add(stat3Label);
            stat3ComboBox.SelectedItem = null;
            stackPanel.Children.Add(stat3ComboBox);
            stackPanel.Children.Add(stat3amountLabel);
            stat3amountBox.Text = "";
            stackPanel.Children.Add(stat3amountBox);
            stackPanel.Children.Add(slotLabel);
            slotBox.SelectedItem = null;
            stackPanel.Children.Add(slotBox);
            stackPanel.Children.Add(materialLabel);
            materialBox.SelectedItem = null;
            stackPanel.Children.Add(materialBox);
            stackPanel.Children.Add(addItem);
            stackPanel.Children.Add(label);
            stackPanel.Children.Add(cancelButton);

            addItem.Click += addItem_Click;
        }
        
        private async void addItem_Click(object sender, RoutedEventArgs e)
        {
            if (nameTry() && statChoiceTry() && statAmountsTry() && slotTry() && materialTry())
            {
                materialConvert();
                var values = new Dictionary<String, String>
                {
                    {"name", nameBox.Text },
                    {"stat1", stat1ComboBox.Text },
                    {"stat1amount", stat1amountBox.Text },
                    {"stat2", stat2ComboBox.Text },
                    {"stat2amount", stat2amountBox.Text },
                    {"stat3", stat3ComboBox.Text },
                    {"stat3amount", stat3amountBox.Text },
                    {"slot", slotBox.Text },
                    {"material", material.ToString() }
                };
                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                HttpResponseMessage response = await client.PostAsync(itemUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    String message = "Item added";
                    String caption = "Success";
                    MessageBox.Show(message, caption, okButton);
                    clearItemForm();
                    listItems();
                }
                else
                {
                    somethingWentWrong();
                }
            }
        }

        private bool statChoiceTry()
        {
            bool ok = true;
            if (stat1ComboBox.Text == "" || stat2ComboBox.Text == "" || stat3ComboBox.Text == "")
            {
                ok = false;
                String message = "Please choose all three statistics";
                MessageBox.Show(message, errorCaption, okButton);
            }
            else if (stat1ComboBox.Text == stat2ComboBox.Text || stat1ComboBox.Text == stat3ComboBox.Text || stat2ComboBox.Text == stat3ComboBox.Text)
            {
                ok = false;
                String message = "Please choose three different statistics";
                MessageBox.Show(message, errorCaption, okButton);
            }
            return ok;
        }
        
        private bool statAmountsTry()
        {
            bool num = true;
            try
            {
                int.Parse(stat1amountBox.Text);
            }
            catch
            {
                num = false;
                String message = "Please add a numeric value to stat 1 amount";
                MessageBox.Show(message, errorCaption, okButton);
            }
            try
            {
                int.Parse(stat2amountBox.Text);
            }
            catch
            {
                num = false;
                String message = "Please add a numeric value to stat 2 amount";
                MessageBox.Show(message, errorCaption, okButton);
            }
            try
            {
                int.Parse(stat3amountBox.Text);
            }
            catch
            {
                num = false;
                String message = "Please add a numeric value to stat 3 amount";
                MessageBox.Show(message, errorCaption, okButton);
            }
            return num;
        }

        private bool nameTry()
        {
            bool nameOk = true;
            if (nameBox.Text == "")
            {
                nameOk = false;
                String message = "Please add the name of the item";
                MessageBox.Show(message, errorCaption, okButton);
            }
            else if (itemNamesToList().Contains(nameBox.Text.ToLower().Trim()))
            {
                nameOk = false;
                String message = "Please add a unique item name, an item with this name already exists in the database";
                MessageBox.Show(message, errorCaption, okButton);
            }
            return nameOk;
        }

        private bool slotTry()
        {
            bool slotOk = true;
            if (slotBox.Text == "")
            {
                slotOk = false;
                String message = "Please choose a slot";
                MessageBox.Show(message, errorCaption, okButton);
            }
            return slotOk;
        }

        private bool materialTry()
        {
            bool materialOk = true;
            if (materialBox.Text == "")
            {
                materialOk = false;
                String message = "Please choose a material";
                MessageBox.Show(message, errorCaption, okButton);
            }
            return materialOk;
        }

        private int materialConvert()
        {
            if (materialBox.Text == "cloth")
            {
                material = 1;
            }
            else if (materialBox.Text == "leather")
            {
                material = 2;
            }
            else
            {
                material = 3;
            }
            return material;
        }

        // MODIFY

        private void modifytBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null)
            {
                nothingSelected();
            }
            else
            {
                try
                {
                    Item itemToModify = (Item)listBox.SelectedItem;
                    modifyItem();
                }
                catch
                {
                    User userToModify = (User)listBox.SelectedItem;
                    modifyUser();
                }
            }
        }

        private void modifyItem()
        {
            Item itemToModify = (Item)listBox.SelectedItem;
            stackPanel.Children.Clear();
            comboBoxAddItems();
            addCancelButton();

            Label nameLabel = new Label();
            nameLabel.Content = "Name";
            Label stat1Label = new Label();
            stat1Label.Content = "Stat1";
            Label stat1amountLabel = new Label();
            stat1amountLabel.Content = "Stat1 amount";
            Label stat2Label = new Label();
            stat2Label.Content = "Stat2";
            Label stat2amountLabel = new Label();
            stat2amountLabel.Content = "Stat2 amount";
            Label stat3Label = new Label();
            stat3Label.Content = "Stat3";
            Label stat3amountLabel = new Label();
            stat3amountLabel.Content = "Stat3 amount";
            Label slotLabel = new Label();
            slotLabel.Content = "Slot";
            Label materialLabel = new Label();
            materialLabel.Content = "Material";
            Button modifyItem = new Button();
            modifyItem.Content = "Modify item";
            modifyItem.Width = 70;
            modifyItem.HorizontalAlignment = HorizontalAlignment.Left;
            modifyItem.Margin = new Thickness(0, 15, 5, 5);

            stackPanel.Children.Add(nameLabel);
            stackPanel.Children.Add(nameBox);
            nameBox.Text = itemToModify.Name.ToString();
            nameBox.IsEnabled = true;
            stackPanel.Children.Add(stat1Label);
            stackPanel.Children.Add(stat1ComboBox);
            stat1ComboBox.SelectedItem = itemToModify.Stat1;
            stackPanel.Children.Add(stat1amountLabel);
            stackPanel.Children.Add(stat1amountBox);
            stat1amountBox.Text = itemToModify.Stat1amount.ToString();
            stackPanel.Children.Add(stat2Label);
            stackPanel.Children.Add(stat2ComboBox);
            stat2ComboBox.SelectedItem = itemToModify.Stat2;
            stackPanel.Children.Add(stat2amountLabel);
            stackPanel.Children.Add(stat2amountBox);
            stat2amountBox.Text = itemToModify.Stat2amount.ToString();
            stackPanel.Children.Add(stat3Label);
            stackPanel.Children.Add(stat3ComboBox);
            stat3ComboBox.SelectedItem = itemToModify.Stat3;
            stackPanel.Children.Add(stat3amountLabel);
            stackPanel.Children.Add(stat3amountBox);
            stat3amountBox.Text = itemToModify.Stat3amount.ToString();
            stackPanel.Children.Add(slotLabel);
            stackPanel.Children.Add(slotBox);
            slotBox.SelectedItem = itemToModify.Slot;
            stackPanel.Children.Add(materialLabel);
            materialBox.Text = convertMaterialBack(itemToModify);
            stackPanel.Children.Add(materialBox);
            stackPanel.Children.Add(modifyItem);
            stackPanel.Children.Add(label);
            stackPanel.Children.Add(cancelButton);

            modifyItem.Click += ModifyItem_Click;
        }

        private async void ModifyItem_Click(object sender, RoutedEventArgs e)
        {
            Item itemToModify = (Item)listBox.SelectedItem;
            if (nameTry() && statChoiceTry() && statAmountsTry() && slotTry() && materialTry())
            {
                materialConvert();
                var values = new Dictionary<String, String>
                {
                    {"name", nameBox.Text },
                    {"stat1", stat1ComboBox.Text },
                    {"stat1amount", stat1amountBox.Text },
                    {"stat2", stat2ComboBox.Text },
                    {"stat2amount", stat2amountBox.Text },
                    {"stat3", stat3ComboBox.Text },
                    {"stat3amount", stat3amountBox.Text },
                    {"slot", slotBox.Text },
                    {"material", material.ToString() }
                };
                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                String url = itemUrl + "/" + itemToModify.Id;

                HttpResponseMessage response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    String message = "Item successfully modified";
                    String caption = "Success";
                    MessageBox.Show(message, caption, okButton);
                    clearItemForm();
                    listItems();
                }
                else
                {
                    somethingWentWrong();
                }
            }
        }

        private void modifyUser()
        {
            User userToModify = (User)listBox.SelectedItem;
            stackPanel.Children.Clear();
            addCancelButton();

            Label nameLabel = new Label();
            nameLabel.Content = "username";
            nameBox.Text = userToModify.Name;
            nameBox.IsEnabled = false;
            Label emailLabel = new Label();
            emailLabel.Content = "email";
            emailBox.Text = userToModify.Email;
            Label isAdminLabel = new Label();
            isAdminLabel.Content = "admin";
            if (userToModify.Admin)
            {
                isAdminBox.IsChecked = true;
            }
            else
            {
                isAdminBox.IsChecked = false;
            }
            Button modifyUser = new Button();
            modifyUser.Content = "Modify user";
            modifyUser.Width = 70;
            modifyUser.HorizontalAlignment = HorizontalAlignment.Left;
            modifyUser.Margin = new Thickness(0, 15, 5, 5);

            stackPanel.Children.Add(nameLabel);
            stackPanel.Children.Add(nameBox);
            stackPanel.Children.Add(emailLabel);
            stackPanel.Children.Add(emailBox);
            stackPanel.Children.Add(isAdminLabel);
            stackPanel.Children.Add(isAdminBox);
            stackPanel.Children.Add(modifyUser);
            stackPanel.Children.Add(cancelButton);
            stackPanel.Children.Add(label);

            modifyUser.Click += ModifyUser_Click;
        }

        private async void ModifyUser_Click(object sender, RoutedEventArgs e)
        {
            User userToModify = (User)listBox.SelectedItem;
            String isAdmin = "0";
            if ((bool)isAdminBox.IsChecked)
            {
                isAdmin = "1";
            }

            var values = new Dictionary<String, string>
                {
                    {"email", emailBox.Text },
                    {"admin", isAdmin }
                };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            String url = userUrl + "/" + userToModify.Id;

            HttpResponseMessage response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                String message = "User successfully modified";
                String caption = "Success";
                MessageBox.Show(message, caption, okButton);
                listUsers();
            }
            else
            {
                somethingWentWrong();
            }
        }
        
        private String convertMaterialBack(Item itemToModify)
        {
            String materialToReturn = "";
            if (itemToModify.Material == 1)
            {
                materialToReturn = "cloth";
            }
            else if (itemToModify.Material == 2)
            {
                materialToReturn = "leather";
            }
            else
            {
                materialToReturn = "plate";
            }
            return materialToReturn;
        }

        // DELETE

        private async void delete(object o)
        {
            String message;
            String successCaption = "Success";
            try
            {
                Item itemToDelete = (Item)o;
                using (client)
                {
                    String address = itemUrl + "/" + itemToDelete.Id.ToString();
                    HttpResponseMessage response = await client.DeleteAsync(address);
                    if (response.IsSuccessStatusCode)
                    {
                        message = String.Format("Item #{0} deleted", itemToDelete.Id);
                        MessageBox.Show(message, successCaption, okButton);
                        listItems();
                    }
                    else
                    {
                        somethingWentWrong();
                    }
                }
                listItems();
            } catch
            {
                User userToDelete = (User)o;
                int userId = userToDelete.Id;
                using (client)
                {
                    String address = userUrl + "/" + userId.ToString();
                    HttpResponseMessage response = await client.DeleteAsync(address);
                    if (response.IsSuccessStatusCode)
                    {
                        message = String.Format("User #{0} deleted", userToDelete.Id);
                        MessageBox.Show(message, successCaption, okButton);
                        listUsers();
                    }
                    else
                    {
                        somethingWentWrong();
                    }
                }
            }
            listUsers();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            String message;
            String caption;
            if (listBox.SelectedItem != null)
            {
                message = "Are you sure?";
                caption = "Delete";
                MessageBoxButton yesNoButton = MessageBoxButton.YesNo;
                if (MessageBox.Show(message, caption, yesNoButton) == MessageBoxResult.Yes)
                {
                    delete(listBox.SelectedItem);
                }
            }
            else
            {
                nothingSelected();
            }
        }
    }
    }

// TODO?: make/remove as admin button when user is selected + are you sure? + put user to change admin status

// a modifyról a newra és vissza a modifyra megmarad a kijelölés (is this a problem?)

// TODO: user saját magát nem törölheti

// backend: idegen kulcsok