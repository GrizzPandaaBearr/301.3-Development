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
using _301._3_Development.Pages;
using _301._3_Development.models;
using _301._3_Development.Controls;


namespace _301._3_Development
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserDTO? userDTO;
        public MainWindow()
        {
            InitializeComponent();

            _mainFrame.NavigationService.Navigate(new login());
        }
        public void ActivateHamburger(string username)
        {
            HamburgerMenu hamburger = new HamburgerMenu(username);
            hamburgerGrid.Children.Add(hamburger);
        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            /*PatientRegistration patientRegistration = new PatientRegistration();
            patientRegistration.ShowDialog();*/
            _mainFrame.NavigationService.Navigate(new PatientRegistration());
        }

        private void ShowPatients_Click(object sender, RoutedEventArgs e)
        {
            /*DEBUGWINDOW dEBUGWINDOW = new DEBUGWINDOW();
            dEBUGWINDOW.ShowDialog();*/
            _mainFrame.NavigationService.Navigate(new DataPage());
        }

        private void ShowPAGE_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemovePatients_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new RemovePatients());
        }
    }
}