using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.Security;
using _301._3_Development.Services;
using _301._3_Development.models;
using _301._3_Development.Scripts;

namespace _301._3_Development
{
    public partial class recordupdate : Page
    {
        private readonly AesGcmEncryptionService _encService;
        private List<dynamic> _patients = new();

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
                PatientList.ItemsSource = null;
                return;
            }

            PatientList.ItemsSource = _patients;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = (SearchBox.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(query) || query.Equals("search by name", System.StringComparison.OrdinalIgnoreCase))
            {
                PatientList.ItemsSource = _patients;
                return;
            }

            var filtered = _patients
                .Where(p => {
                    try { return p?.Name?.ToString().ToLower().Contains(query.ToLower()) == true; }
                    catch { return false; }
                })
                .ToList();

            PatientList.ItemsSource = filtered;
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;

            var patient = btn.DataContext;
            if (patient == null) return;

            var finalPage = new finalform(patient);
            NavigationService?.Navigate(finalPage);
        }
    }
}