using System.Linq;
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
            string username = txtUsername.Text?.Trim();
            string password = txtPassword.Password;

            var users = EncryptedStorage.LoadEncrypted<UserDTO>(_encService);
            var user = users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool valid = PasswordHasher.VerifyPassword(password, user.PasswordHash);
            if (valid)
            {
                MessageBox.Show("Login successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new mainscreen());
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
