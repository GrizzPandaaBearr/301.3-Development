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
                //Email = txtUsername.Text?.Trim(),
                //Password = txtPassword.Password
                Email = "123@gmail.com",
                Password = "Pass"
            };

            try
            {
                Debug.WriteLine($"Fetching user from server: {request.Email} {request.Password}");
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

            /*string email = txtUsername.Text?.Trim();
            string password = txtPassword.Password;
*/
            //Connect to database instead of local storage




            ///

            /*var users = EncryptedStorage.LoadEncrypted<UserDTO>(_encService);
            var user = users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool valid = PasswordHasher.VerifyPassword(password, user.PasswordHash);
            if (valid)
            {
                MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                byte[] sessionkey = KeyManager.GetOrCreateKey();
                if (sessionkey == null)
                {
                    Debug.WriteLine("Session failed to create key");
                    return;
                }

                //experimental
                User dummy = new User();
                dummy.SetDummyUser();
                SessionManager.Instance.StartSession(dummy,sessionkey);
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }
        private void BtnGoToSignup_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new signup());
        }
    }
}
