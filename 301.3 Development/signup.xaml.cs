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
using _301._3_Development.Security;
using _301._3_Development.Services;
using _301._3_Development.models;
using System.Collections.Generic;

namespace _301._3_Development
{
    public partial class signup : Page
    {
        private readonly EncryptionService _enc;

        public signup()
        {
            InitializeComponent();
            _enc = new EncryptionService(App.AppEncryptionKey);
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter Username and Password.", "Try Again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var enc = new EncryptionService(App.AppEncryptionKey);
            var users = EncryptedStorage.LoadUsersEncrypted<UserDTO>(enc);
            string encryptedPassword = _enc.EncryptString(password);

            users.Add(new UserDTO
            {
                Username = username,
                EncryptedPassword = enc.EncryptString(password)
            });

            App.RegisteredUsername = username;
            App.RegisteredPasswordEncrypted = encryptedPassword;

            EncryptedStorage.SaveUsersEncrypted(users, enc);

            MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new login());
        }
    }
}