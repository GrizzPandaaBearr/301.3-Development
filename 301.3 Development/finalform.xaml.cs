using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.models;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class finalform : Page
    {
        private readonly AesGcmEncryptionService _encService;

        public finalform()
        {
            InitializeComponent();
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            Bitmap formImage = new Bitmap(this.Width, this.Height);



            var patient = new
            {
                Name = txtPatientName.Text,
                ContactNo = txtContactNo.Text,
                PassportNo = txtPassportNo.Text,
                AppointmentDate = txtAppointmentDate.Text,
                PlaceOfBirth = txtPlaceOfBirth.Text,
                DateOfBirth = txtDateOfBirth.Text,
                Sex = txtSex.Text,

                EmergencyName = txtEmergencyName.Text,
                EmergencyContact = txtEmergencyContact.Text,
                EmergencyRelation = txtEmergencyRelation.Text,

                PickupYes = chkPickupYes,
                PickupNo = chkPickupNo,

                ComeBy = txtComeBy.Text,
                ETA = txtETA.Text,

                Inpatient = chkInpatient,
                Outpatient = chkOutpatient,
                MedicalCheckUp = chkMedicalCheckup,

                Doctor = txtDoctor.Text,
                Remark = txtRemark.Text
            };

            try
            {
                PatientStorage.SavePatientData(patient, _encService);

            }
            catch
            {
                MessageBox.Show("Error saving patient data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
