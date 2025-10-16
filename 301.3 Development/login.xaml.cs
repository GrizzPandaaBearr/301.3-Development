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

namespace _301._3_Development
{
    public partial class login : Page
    {
        private readonly EncryptionService _enc;

        public login()
        {
            InitializeComponent();
            _enc = new EncryptionService(App.AppEncryptionKey);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (username == App.RegisteredUsername && App.RegisteredPasswordEncrypted != null)
            {
                var decrypted = _enc.DecryptString(App.RegisteredPasswordEncrypted);
                if (decrypted == password)
                {
                    MessageBox.Show("Login successful!");
                    NavigationService.Navigate(new mainscreen());
                    return;
                }
            }
            if (username == App.AdminUsername && password == App.AdminPassword)
            {
                MessageBox.Show("Welcome Admin!", "Admin Login", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new adminscreen());
                return;
            }
            else
            {
                MessageBox.Show("Invalid credentials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}