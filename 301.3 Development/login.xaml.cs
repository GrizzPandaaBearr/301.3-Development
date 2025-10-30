using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.models;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class login : Page
    {
        private readonly AesGcmEncryptionService _encService;

        public login()
        {
            InitializeComponent();
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            var users = EncryptedStorage.LoadEncrypted<UserDTO>(_encService);
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var decrypted = _encService.DecryptString(user.EncryptedPassword);
                if (decrypted == password)
                {
                    MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NavigationService?.Navigate(new mainscreen());
                    return;
                }
            }
            catch
            {
                
            }

            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnGoToSignup_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new signup());

        }
    }
}