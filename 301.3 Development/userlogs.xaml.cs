using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using _301._3_Development.models;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class userlogs : Page
    {
        private ObservableCollection<UserLog> allLogs;

        public userlogs()
        {
            InitializeComponent();
            LoadUserLogs();

            SearchBox.TextChanged += SearchBox_TextChanged;

            FinishButton.Click += FinishButton_Click;
        }

        private void LoadUserLogs()
        {
            allLogs = new ObservableCollection<UserLog>
            {

            };

            LogsDataGrid.ItemsSource = allLogs;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText) || searchText == "search by name")
            {
                LogsDataGrid.ItemsSource = allLogs;
                return;
            }

            var filteredLogs = allLogs
                .Where(log => log.User.ToLower().Contains(searchText))
                .ToList();

            LogsDataGrid.ItemsSource = filteredLogs;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var enc = new EncryptionService(App.AppEncryptionKey);
            var users = EncryptedStorage.LoadUsersEncrypted<UserDTO>(enc);

            LogsDataGrid.ItemsSource = users;
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new adminscreen());
        }

       
    }
    public class UserLog
    {
        public required string User { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public required string Action { get; set; }
        public required string LastEdited { get; set; }
    }
}