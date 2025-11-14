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

namespace _301._3_Development.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorSchedulePage.xaml
    /// </summary>
    public partial class DoctorSchedulePage : Page
    {
        private readonly UserDTO _user;

        public DoctorSchedulePage(UserDTO user)
        {
            InitializeComponent();
            _user = user;
            this.Loaded += OnLoaded;
            Header.Text = $"{user.Name_First} {user.Name_Last}'s Schedule";
        }
        private async void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadAppointments();
        }

        private async void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            var selected = AppointmentsList.SelectedItem as AppointmentDTO;
            if (selected == null) return;

            string newStatus = (StatusCombo.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(newStatus)) return;

            bool ok = await SessionManager.Instance.Api.UpdateAppointmentStatus(selected.AppointmentID, newStatus);

            if (ok)
            {
                MessageBox.Show("Status updated.");
                await LoadAppointments(); // reload
            }
        }
        private async Task LoadAppointments()
        {
            try
            {
                int doctorId = _user.UserID;

                var appts = await SessionManager.Instance.Api
                    .GetAsync<List<AppointmentDTO>>($"Appointments/doctor/{doctorId}");

                AppointmentsList.ItemsSource = appts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load appointments: {ex.Message}");
            }
        }
    }
}
