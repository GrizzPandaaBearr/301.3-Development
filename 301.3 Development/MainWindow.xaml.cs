using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _301._3_Development.Scripts;
using _301._3_Development.Windows;

namespace _301._3_Development
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patientdataform patientdataform = new Patientdataform();
            patientdataform.
        }

        private void ShowPatients_Click(object sender, RoutedEventArgs e)
        {
            DEBUGWINDOW dEBUGWINDOW = new DEBUGWINDOW();
            dEBUGWINDOW.ShowDialog();
            Visibility = Visibility.Hidden;
        }
    }
}