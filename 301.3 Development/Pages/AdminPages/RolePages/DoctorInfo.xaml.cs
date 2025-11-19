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
    /// Interaction logic for DoctorInfo.xaml
    /// </summary>
    public partial class DoctorInfo : Page
    {
        public DoctorInfo()
        {
            InitializeComponent();
        }
        public DoctorDTO GetDTO()
        {
            int x = 0;

            if (Int32.TryParse(TxtExperience.Text, out x))
            {
                MessageBox.Show("Experience years is not an integer, returning default (0)");
            }
            DoctorDTO doctor = new DoctorDTO
            {
                Specialization = TxtSpecialization.Text,
                LicenseNumber = TxtLicenceNumber.Text,
                OfficePhone = TxtPhoneNumber.Text,
                ExperienceYears = x
            };
            return doctor;
        }
    }
}
