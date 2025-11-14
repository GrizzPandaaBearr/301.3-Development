using _301._3_Development.models;
using _301._3_Development.Scripts.Session;
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

namespace _301._3_Development.Pages.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientAppointmentsPage.xaml
    /// </summary>
    public partial class PatientAppointmentsPage : Page
    {
        private readonly UserDTO _user;

        public PatientAppointmentsPage(UserDTO user)
        {
            InitializeComponent();
            _user = user;

            PatientNameText.Text = $"{_user.Name_First}'s Appointments";

            this.Loaded += PatientAppointmentsPage_Loaded;
            // Later: API call to load appointments
        
        }
        private async void PatientAppointmentsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await BindToList();
        }
        public async Task<List<AppointmentDTO>> FetchAppointmentsAsync(int patientId)
        {
            var appointments = await SessionManager.Instance.Api.GetAsync<List<AppointmentDTO>>($"Appointments/patient/{patientId}");
            return appointments;
        }
        public async Task BindToList()
        {
            var appointments = await FetchAppointmentsAsync(SessionManager.Instance.CurrentUser.UserID);
            AppointmentsListView.ItemsSource = appointments;
        }
        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var selected = AppointmentsListView.SelectedItem as AppointmentDTO;
            if (selected == null) return;
            if (selected.Status == "Completed" || selected.Status == "Cancelled") { MessageBox.Show("Appointment Is Unavailabe"); return; }
            bool ok = await SessionManager.Instance.Api.CancelAppointment(selected.AppointmentID);

            if (ok)
            {
                MessageBox.Show("Appointment canceled");
                await LoadAppointments();
            }
        }
        private async Task LoadAppointments()
        {
            try
            {
                int patientId = _user.UserID;

                var appts = await FetchAppointmentsAsync(patientId);

                AppointmentsListView.ItemsSource = appts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load appointments: {ex.Message}");
            }
        }
    }
}
