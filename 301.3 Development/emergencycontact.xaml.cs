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
    public partial class emergencycontact : Page
    {
        private PatientData _patient;

        public emergencycontact(PatientData patient)
        {
            InitializeComponent();
            _patient = patient;

            MessageBox.Show($"Patient: {_patient.Name}");
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new patientdataform());
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string contactNo = txtContactNo.Text;
            string relationship = txtRelationship.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(contactNo))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EmergencyContactInfo contactInfo = new EmergencyContactInfo
            {
                Name = name,
                ContactNo = contactNo,
                Relationship = relationship
            };

            this.NavigationService?.Navigate(new informationform());
        }
    }

    public class EmergencyContactInfo
    {
        public required string Name { get; set; }
        public required string ContactNo { get; set; }
        public required string Relationship { get; set; }
    }
}
