using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class recordupdate : Page
    {
        private readonly AesGcmEncryptionService _encService;
        private List<dynamic> _patients = new List<dynamic>();

        public recordupdate()
        {
            InitializeComponent();
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
            LoadPatientRecords();
        }

        private void LoadPatientRecords()
        {
            _patients = PatientStorage.LoadPatientData(_encService);

            if (_patients == null || !_patients.Any())
            {
                MessageBox.Show("No patient records found.");
                return;
            }

            PatientList.ItemsSource = _patients;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.Trim().ToLower();

            if (_patients == null || _patients.Count == 0)
                return;

            if (string.IsNullOrWhiteSpace(searchText) || searchText == "search by name")
            {
                PatientList.ItemsSource = _patients;
            }
            else
            {
                var filtered = _patients
                    .Where(p => p?.Name?.ToString().ToLower().Contains(searchText) == true)
                    .ToList();
                PatientList.ItemsSource = filtered;
            }
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var patient = button?.DataContext;

            if (patient == null)
                return;

            var finalFormPage = new finalform(patient);
            NavigationService?.Navigate(finalFormPage);
        }
    }
}