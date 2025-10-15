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

namespace _301._3_Development
{
    public partial class recordupdate : Page
    {
        private List<Patient> allPatients;

        public recordupdate()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            allPatients = new List<Patient>
            {
            };

            PatientList.ItemsSource = allPatients;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.ToLower();

            var filtered = allPatients
                .Where(p => p.Name.ToLower().Contains(query))
                .ToList();

            PatientList.ItemsSource = filtered;
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is string patientName)
            {
                var selected = allPatients.FirstOrDefault(p => p.Name == patientName);

                if (selected != null)
                {
                }
            }
        }
    }
}