using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class login : Page
    {
        private readonly UserStore userStore;

        public login()
        {
            InitializeComponent();

            string secret = Environment.GetEnvironmentVariable("FORM_MASTER_PASSPHRASE");
            if (string.IsNullOrEmpty(secret))
            {
                secret = "dev-default-passphrase-change-me";
            }

            var dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyApp");
            Directory.CreateDirectory(dataPath);
            var usersFile = Path.Combine(dataPath, "users.json");

            userStore = new UserStore(usersFile, secret);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (userStore.ValidateCredentials(username, password))
            {
                MessageBox.Show("Login successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new mainscreen());
            }
            else
            {
                MessageBox.Show("Invalid username/password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}