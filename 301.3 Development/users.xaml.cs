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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _301._3_Development
{
    public partial class users : Page
    {
        private List<users> allUsers;

        public users()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            allUsers = new List<users>
            {
            };

            UserList.ItemsSource = allUsers;
        }
        
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.ToLower();
            var filtered = allUsers
                .Where(u => u.Username.ToLower().Contains(query) || u.Name.ToLower().Contains(query))
                .ToList();

            UserList.ItemsSource = filtered;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new userlogs());
        }

        public class Users
        {
            public required string Name { get; set; }
            public required string Username { get; set; }
            public required string Password { get; set; }
        }

    }
}