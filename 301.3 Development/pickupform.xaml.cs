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
    public partial class pickupform : Page
    {
        public pickupform()
        {
            InitializeComponent();
        }

        private void Pickup_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == chkYes && chkYes.IsChecked == true)
                chkNo.IsChecked = false;
            else if (sender == chkNo && chkNo.IsChecked == true)
                chkYes.IsChecked = false;
        }

        private void Pickup_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.GoBack();
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            bool? pickupNeeded = null;
            if (chkYes.IsChecked == true) pickupNeeded = true;
            else if (chkNo.IsChecked == true) pickupNeeded = false;

            string comeBy = txtComeBy.Text;
            string eta = txtETA.Text;

            if (pickupNeeded == null)
            {
                MessageBox.Show("Please select whether pickup is needed (Yes or No).",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (pickupNeeded == true && (string.IsNullOrWhiteSpace(comeBy) || string.IsNullOrWhiteSpace(eta)))
            {
                MessageBox.Show("Please fill in 'Come by' and 'ETA' if pickup is needed.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PickupData pickup = new PickupData
            {
                PickupNeeded = pickupNeeded.Value,
                ComeBy = comeBy,
                ETA = eta
            };

            MessageBox.Show($"Pickup Needed: {pickup.PickupNeeded}\nCome by: {pickup.ComeBy}\nETA: {pickup.ETA}",
                            "Pickup Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }

    public class PickupData
    {
        public bool PickupNeeded { get; set; }
        public required string ComeBy { get; set; }
        public required string ETA { get; set; }
    }
}