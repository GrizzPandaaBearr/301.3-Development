using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.models;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class signup : Page
    {
        private readonly AesGcmEncryptionService _encService;

        public signup()
        {
            InitializeComponent();
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text?.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please provide username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var users = EncryptedStorage.LoadEncrypted<UserDTO>(_encService);
            if (users.Any(u => u.Username == username))
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var passwordHash = PasswordHasher.HashPassword(password);

            var user = new UserDTO
            {
                Username = username,
                PasswordHash = passwordHash
            };

            users.Add(user);
            EncryptedStorage.SaveEncrypted(users, _encService);

            NavigationService?.Navigate(new login());
        }

        private void BtnGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new login());
        }
    }
}
