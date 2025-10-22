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
    public partial class informationform : Page
    {
        public informationform()
        {
            InitializeComponent();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService?.GoBack();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            bool isInpatient = chkInpatient.IsChecked == true;
            bool isOutpatient = chkOutpatient.IsChecked == true;
            bool isCheckup = chkCheckup.IsChecked == true;

            string doctor = txtDoctor.Text;
            string remark = txtRemark.Text;

            if (!isInpatient && !isOutpatient && !isCheckup)
            {
                MessageBox.Show("Please select at least one type (Inpatient, Outpatient, or Medical Check-Up).",
                                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(doctor))
            {
                MessageBox.Show("Please enter the Doctor’s name.",
                                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            InformationData info = new InformationData
            {
                Inpatient = isInpatient,
                Outpatient = isOutpatient,
                Checkup = isCheckup,
                Doctor = doctor,
                Remark = remark
            };

            this.NavigationService?.Navigate(new pickupform());
        }
    }

    public class InformationData
    {
        public bool Inpatient { get; set; }
        public bool Outpatient { get; set; }
        public bool Checkup { get; set; }
        public required string Doctor { get; set; }
        public string Remark { get; set; }
    }
}