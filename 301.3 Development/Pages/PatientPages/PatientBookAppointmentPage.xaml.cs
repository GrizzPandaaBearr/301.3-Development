using _301._3_Development.models;
using _301._3_Development.Scripts.Session;
using _301._3_Development.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace _301._3_Development.Pages.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientBookAppointmentPage.xaml
    /// </summary>
    public partial class PatientBookAppointmentPage : Page
    {
        private readonly UserDTO _userDTO;
        public PatientBookAppointmentPage(UserDTO userDTO)
        {
            InitializeComponent();
            _userDTO = userDTO;
            this.Loaded += PatientBookAppointmentsPage_Loaded;
        }
        
        private async void PatientBookAppointmentsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await BindComboToDoctor();
        }

        public async Task<List<DoctorDTO>> FetchDoctorsAsync()
        {
            try
            {
                var doctors = await SessionManager.Instance.Api.GetAsync<List<DoctorDTO>>("Doctors");
                
                return doctors;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
                   
        }
        public async Task BindComboToDoctor()
        {
            var doctors = await FetchDoctorsAsync();
            foreach(var doctor in doctors)
            {
                Debug.WriteLine(doctor.Name_First);
            }
            DoctorComboBox.ItemsSource = doctors;
            DoctorComboBox.DisplayMemberPath = "Name_First"; // can use a property in DTO combining first + last name
            DoctorComboBox.SelectedValuePath = "DoctorID";
        }

        private async void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem is not DoctorDTO doctor)
            {
                MessageBox.Show("Please choose a doctor.");
                return;
            }

            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select a date.");
                return;
            }

            var dto = new
            {
                PatientID = _userDTO.UserID,
                DoctorID = doctor.DoctorID,
                Date = DatePicker.SelectedDate.Value,
                Reason = ReasonTextBox.Text
            };
            try
            {
                var response = await SessionManager.Instance.Api.PostAsync<RegisterResponse>(
                    "auth/book", dto);

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
    }
}
