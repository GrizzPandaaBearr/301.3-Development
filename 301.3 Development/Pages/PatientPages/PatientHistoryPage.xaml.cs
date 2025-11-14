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

namespace _301._3_Development.Pages.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientHistoryPage.xaml
    /// </summary>
    public partial class PatientHistoryPage : Page
    {
        private readonly UserDTO _user;

        public PatientHistoryPage(UserDTO user)
        {
            InitializeComponent();
            _user = user;

            HistoryText.Text = user.Patient?.MedicalHistory ?? "No history available.";
        }
    }
}
