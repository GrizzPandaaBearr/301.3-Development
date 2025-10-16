using _301._3_Development.Models;
using _301._3_Development.Security;
using _301._3_Development.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace _301._3_Development
{
    public partial class finalform : Page
    {

        public finalform(Patient patient)
        {
            InitializeComponent();

            txtPatientName.Text = patient.Name;
            txtContactNo.Text = patient.ContactNo;
            txtPassportNo.Text = patient.PassportNo;

            txtEmergencyName.Text = patient.EmergencyName;
            txtEmergencyContact.Text = patient.EmergencyContact;
            txtEmergencyRelation.Text = patient.EmergencyRelation;

            chkPickupYes.IsChecked = patient.PickupYes;
            chkPickupNo.IsChecked = patient.PickupNo;
            txtComeBy.Text = patient.ComeBy;
            txtETA.Text = patient.ETA;

            chkInpatient.IsChecked = patient.Inpatient;
            chkOutpatient.IsChecked = patient.Outpatient;
            chkMedicalCheckup.IsChecked = patient.MedicalCheckUp;

            txtDoctor.Text = patient.Doctor;
            txtRemark.Text = patient.Remark;
        }

        public finalform()
        {
            InitializeComponent();

            var enc = new EncryptionService(App.AppEncryptionKey);
            PatientStorage.SavePatient(patientFormData, enc);
            var data = PatientStorage.LoadPatient<PatientFormData>(enc);
        }

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
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(rtb));
                        encoder.Save(fs);
                    }

                    MessageBox.Show("Form exported successfully!", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Error while exporting form.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}