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
using System.Xml.Linq;

namespace _301._3_Development
{
    public partial class patientdataform : Page
    {
        public patientdataform()
        {
            InitializeComponent();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            PatientData patient = new PatientData
            {
                Name = txtName.Text,
                ContactNo = txtContactNo.Text,
                PassportNo = txtPassportNo.Text,
                AppointmentDate = txtAppointmentDate.Text,
                PlaceOfBirth = txtPlaceOfBirth.Text,
                DateOfBirth = txtDOB.Text,
                Sex = txtSex.Text
            };

            if (string.IsNullOrWhiteSpace(patient.Name) || string.IsNullOrWhiteSpace(patient.ContactNo) || string.IsNullOrWhiteSpace(patient.PassportNo) ||
                string.IsNullOrWhiteSpace(patient.AppointmentDate) || string.IsNullOrWhiteSpace(patient.PlaceOfBirth) || string.IsNullOrWhiteSpace(patient.DateOfBirth) ||
                string.IsNullOrWhiteSpace(patient.Sex))
            {
                MessageBox.Show("All Informations are required! Please check again!", "Validation Error!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.NavigationService?.Navigate(new emergencycontact(patient));
        }
    }

    public class PatientData
    {
        public required string Name { get; set; }
        public required string ContactNo { get; set; }
        public required string PassportNo { get; set; }
        public required string AppointmentDate { get; set; }
        public required string PlaceOfBirth { get; set; }
        public required string DateOfBirth { get; set; }
        public required string Sex { get; set; }
    }
}
