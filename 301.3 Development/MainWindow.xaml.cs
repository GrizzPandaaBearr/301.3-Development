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
using _301._3_Development.Scripts.Session;
using System.Diagnostics;


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
            var loginpage = new login();
            loginpage.LoginSuccess += Login_Success;

            _mainFrame.Content = loginpage;

        }

        private void Login_Success(object sender, EventArgs e)
        {
            ShowMain();
        }

        private void ShowMain()
        {
            if(SessionManager.Instance.CurrentUser != null)
            {
                Debug.WriteLine($"LOGIN SUCCESS Current user is {SessionManager.Instance.CurrentUser.FirstName} \nRole: {SessionManager.Instance.CurrentUser.Role}");
                _mainFrame.Content = new mainscreen();
                ActivateHamburger();
            }
        }

        private void ActivateHamburger()
        {
            HamburgerMenu hamburger = new HamburgerMenu(SessionManager.Instance.CurrentUser.Role, _mainFrame);
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