using _301._3_Development.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using _301._3_Development.Scripts;

namespace _301._3_Development
{
    public partial class finalform : Page
    {

        public finalform(Patient patient)
        {
            InitializeComponent();

            txtPatientName.Text = $" {patient.FirstName} {patient.LastName}";
            txtContactNo.Text = patient.Phone;
            txtPassportNo.Text = "placeholder"; // sort it out later by god

            txtEmergencyName.Text = "patient.EmergencyName";
            txtEmergencyContact.Text = "patient.EmergencyContact";
            txtEmergencyRelation.Text = "patient.EmergencyRelation";

            chkPickupYes.IsChecked = false;
            chkPickupNo.IsChecked = false;
            txtComeBy.Text = "patient.ComeBy";
            txtETA.Text = "patient.ETA";

            chkInpatient.IsChecked = false;
            chkOutpatient.IsChecked = false;
            chkMedicalCheckup.IsChecked = false;

            txtDoctor.Text = patient.DoctorID.ToString();
            txtRemark.Text = patient.Medical_History;
        }

        /*public finalform()
        {
            InitializeComponent();
        }*/

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap(
                    (int)this.ActualWidth,
                    (int)this.ActualHeight,
                    96d, 96d, PixelFormats.Pbgra32);

                rtb.Render(this);

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png",
                    Title = "Save Registration Form"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileAccess.Read))
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(rtb));
                        encoder.Save(fs);
                    }

                    MessageBox.Show("Form exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Error while exporting form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}