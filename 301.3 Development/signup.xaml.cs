using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class signup : Page
    {
        private readonly UserStore userStore;

        public signup()
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

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = isPasswordVisible ? txtPasswordVisible.Text : txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = userStore.AddUser(username, password, fullName: null);
            if (!ok)
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Account created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService?.Navigate(new login());
        }
    }
}