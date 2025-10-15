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
    public partial class signup : Page
    {
        public signup()
        {
            InitializeComponent();
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter Username and Password.", "Try Again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            App.RegisteredUsername = username;
            App.RegisteredPassword = password;

            MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService.Navigate(new login());
        }

        private void BtnGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new login());
        }
    }
}