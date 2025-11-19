using _301._3_Development.models;
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

namespace _301._3_Development.Pages.AdminPages.RolePages
{
    /// <summary>
    /// Interaction logic for PatientInfo.xaml
    /// </summary>
    public partial class PatientInfo : Page
    {
        public PatientInfo()
        {
            InitializeComponent();
        }
        public PatientDTO GetDTO()
        {
            PatientDTO patient = new PatientDTO
            {
                BirthPlace = TxtBirthPlace.Text,
                //Dredded calendar = CalBirthDay,
                Sex = (ComboGender.SelectedItem as ComboBoxItem)?.Tag.ToString(),
                MedicalHistory = TxtMedicalHistory.Text
            };
            return patient;
        }
    }
}
