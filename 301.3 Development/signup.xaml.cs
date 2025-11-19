using _301._3_Development.models;
using _301._3_Development.Pages.AdminPages.RolePages;
using _301._3_Development.Scripts;
using _301._3_Development.Scripts.Session;
using _301._3_Development.Security;
using _301._3_Development.Services;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace _301._3_Development
{
    public partial class signup : Page
    {
        private readonly AesGcmEncryptionService _encService;
        public event EventHandler LoginSuccess;
        private PatientInfo _patientInfo;

        public signup()
        {
            InitializeComponent();
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
            _patientInfo = new PatientInfo();
            FrameRole.Content = _patientInfo;
        }
        private void btnTogglePassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            PasswordValidator passwordValidator = new PasswordValidator();
            // Simple input validation
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) ||
                string.IsNullOrWhiteSpace(TxtLastName.Text) ||
                string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                string.IsNullOrWhiteSpace(TxtPassword.Password))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }
            string email = TxtEmail.Text;
            string password = TxtPassword.Password;

            if (!IsEmailValid(TxtEmail.Text)) { MessageBox.Show("Invalid Email Format."); return; }

            if (!passwordValidator.Validate(password, email)) { MessageBox.Show(passwordValidator.GetError()); }
            var request = new RegisterRequest
            {
                FirstName = TxtFirstName.Text,
                LastName = TxtLastName.Text,
                Email = TxtEmail.Text,
                Phone = TxtPhone.Text,
                Password = TxtPassword.Password,
                Role = "Patient",
                Patient = _patientInfo.GetDTO()
                
            };

            try
            {
                var response = await SessionManager.Instance.Api.PostAsync<RegisterResponse>(
                    "auth/register", request);

                if (response != null)
                {
                    MessageBox.Show($"Registration successful: {response.Message}");

                    // Optional: start session automatically
                    if (response.User != null && !string.IsNullOrEmpty(response.Token))
                    {
                        /*SessionManager.Instance.StartSession(response.User, response.Token);
                        MessageBox.Show("Session started for user!");*/
                        NavigationService?.Navigate(new login());
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }
        private bool IsEmailValid(string email)
        {
            var emailTrim = email.Trim();

            if (emailTrim.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == emailTrim;
            }
            catch
            {
                return false;
            }
        }

        private void BtnGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new login());
        }
    }
}
