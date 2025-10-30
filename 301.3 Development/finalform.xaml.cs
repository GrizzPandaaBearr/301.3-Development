using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using _301._3_Development.Scripts;
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
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
            _patientData = patient;
            DisplayPatientData();
        }

        private void DisplayPatientData()
        {
            if (_patientData == null) return;

            txtPatientName.Text = _patientData.Name?.ToString() ?? "";
            txtContactNo.Text = _patientData.ContactNo?.ToString() ?? "";
            txtPassportNo.Text = _patientData.PassportNo?.ToString() ?? "";
            txtAppointmentDate.Text = _patientData.AppointmentDate?.ToString() ?? "";
            txtPlaceOfBirth.Text = _patientData.PlaceOfBirth?.ToString() ?? "";
            txtDateOfBirth.Text = _patientData.DateOfBirth?.ToString() ?? "";
            txtSex.Text = _patientData.Sex?.ToString() ?? "";

            txtEmergencyName.Text = _patientData.EmergencyName?.ToString() ?? _patientData.Emergency?.Name?.ToString() ?? "";
            txtEmergencyContact.Text = _patientData.EmergencyContact?.ToString() ?? _patientData.Emergency?.Contact?.ToString() ?? "";
            txtEmergencyRelation.Text = _patientData.EmergencyRelationship?.ToString() ?? _patientData.Emergency?.Relationship?.ToString() ?? "";

            chkPickupYes.IsChecked = _patientData.PickupYes == true;
            chkPickupNo.IsChecked = _patientData.PickupNo == true;
            txtComeBy.Text = _patientData.ComeBy?.ToString() ?? "";
            txtETA.Text = _patientData.ETA?.ToString() ?? "";

            chkInpatient.IsChecked = _patientData.Inpatient == true;
            chkOutpatient.IsChecked = _patientData.Outpatient == true;
            chkMedicalCheckup.IsChecked = _patientData.MedicalCheckUp == true || _patientData.MedicalCheckup == true;
            txtDoctor.Text = _patientData.Doctor?.ToString() ?? "";
            txtRemark.Text = _patientData.Remark?.ToString() ?? "";
        }
    }
}
