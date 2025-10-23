using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using _301._3_Development.models;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class finalform : Page
    {
        private readonly AesGcmEncryptionService _encService;
        private dynamic _patientData;
        public finalform(dynamic patient)
        {
            InitializeComponent();
            _patientData = patient;

            if (_patientData != null)
                DisplayPatientData();

            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
        }

        private void DisplayPatientData()
        {
            txtPatientName.Text = _patientData.Name ?? "";
            txtContactNo.Text = _patientData.ContactNo?.ToString() ?? "";
            txtPassportNo.Text = _patientData.PassportNo?.ToString() ?? "";
            txtAppointmentDate.Text = _patientData.AppointmentDate?.Tostring ?? "";
        }
        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
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
