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
    /// Interaction logic for DoctorPatientsPage.xaml
    /// </summary>
    public partial class DoctorPatientsPage : Page
    {
        private readonly UserDTO _user;

        public DoctorPatientsPage(UserDTO user)
        {
            InitializeComponent();
            _user = user;

            DoctorNameText.Text = $"{user.Name_First} {user.Name_Last} — Patients";
            this.Loaded += DoctorPatientsPage_Loaded;
        }
        private async void DoctorPatientsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadPatients();
        }
        public async Task<List<DoctorPatientDTO>> FetchMyPatientsAsync()
        {
            int doctorId = _user.UserID;

            return await SessionManager.Instance.Api.GetAsync<List<DoctorPatientDTO>>($"Appointments/doctor/{doctorId}/patients");
        }
        private async Task LoadPatients()
        {
            var patients = await FetchMyPatientsAsync();
            PatientsListView.ItemsSource = patients;
        }
    }
}
