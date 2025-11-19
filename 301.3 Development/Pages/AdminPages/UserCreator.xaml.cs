using _301._3_Development.models;
using _301._3_Development.Scripts.Session;
using _301._3_Development.Pages.AdminPages.RolePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace _301._3_Development.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for UserCreator.xaml
    /// </summary>
    public partial class UserCreator : Page
    {
        private readonly UserDTO _userDTO;

        private PatientInfo _patientInfo;
        private DoctorInfo _doctorInfo;
        private AdminInfo _adminInfo;
        public UserCreator(UserDTO user)
        {
            _userDTO = user;
            InitializeComponent();
            _patientInfo = new PatientInfo();
            _doctorInfo = new DoctorInfo();
            _adminInfo = new AdminInfo();
        }
        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Simple input validation
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) ||
                string.IsNullOrWhiteSpace(TxtLastName.Text) ||
                string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                string.IsNullOrWhiteSpace(TxtPassword.Password))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            var request = new RegisterRequest
            {
                FirstName = TxtFirstName.Text,
                LastName = TxtLastName.Text,
                Email = TxtEmail.Text,
                Phone = TxtPhone.Text,
                Password = TxtPassword.Password,
                Role = ComboRole.SelectionBoxItem.ToString()
            };
            string role = (ComboRole.SelectedItem as ComboBoxItem)?.Tag.ToString();
            switch (role)
            {
                case "Patient":
                    request.Patient=_patientInfo.GetDTO();
                    break;
                case "Doctor":
                    request.Doctor = _doctorInfo.GetDTO();
                    break;
                case "Admin":
                    request.Admin = _adminInfo.GetDTO();
                    break;
                default:
                    break;
            }

            MessageBox.Show(request.ToString() );

            try
            {
                var response = await SessionManager.Instance.Api.PostAsync<RegisterResponse>(
                    "auth/register", request);

                if (response != null)
                {
                    MessageBox.Show($"Registration successful: {response.Message}");

                    return;
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

        private void ComboRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string role = (ComboRole.SelectedItem as ComboBoxItem)?.Tag.ToString();
            switch( role )
            {
                case "Patient":
                    FrameRole.Content = _patientInfo;
                    break;
                case "Doctor":
                    FrameRole.Content = _doctorInfo;
                    break;
                case "Admin":
                    FrameRole.Content = _adminInfo;
                    break;
                default:
                    MessageBox.Show("Something went wrong");
                    break;
            }
        }
    }
}
