using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.models;
using _301._3_Development.Scripts;
using _301._3_Development.Scripts.Session;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class login : Page
    {
        private readonly AesGcmEncryptionService _encService;
        public event EventHandler LoginSuccess;

        public login()
        {
            InitializeComponent();
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e) // check what role user is before redirecting
        {
            

            var request = new
            {
                Email = txtUsername.Text?.Trim(),
                Password = txtPassword.Password
            };

            try
            {
                var response = await SessionManager.Instance.Api.PostAsync<LoginResponse>("auth/login", request);
                if (response != null)
                {
                    SessionManager.Instance.StartSession(response.User, response.Token);
                    LoginSuccess?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Invalid Login");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error: " + ex.Message);
            }

            
        }
        private void BtnGoToSignup_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new signup());
        }
    }
}
