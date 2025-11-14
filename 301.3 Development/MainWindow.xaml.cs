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
        private void Log_out(object sender, EventArgs e)
        {
            SessionManager.Instance.EndSession();
            DeactivateHamburger();
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
                Debug.WriteLine($"LOGIN SUCCESS Current user is {SessionManager.Instance.CurrentUser.Name_First} \nRole: {SessionManager.Instance.CurrentUser.Role}");
                _mainFrame.Content = new mainscreen();
                ActivateHamburger();
            }
        }

        private void ActivateHamburger()
        {
            HamburgerMenu hamburger = new HamburgerMenu(SessionManager.Instance.CurrentUser, _mainFrame);
            hamburger.Logout += Log_out;
            hamburgerGrid.Children.Add(hamburger);
        }

        private void DeactivateHamburger()
        {
            hamburgerGrid.Children.Clear();
        }

    }
}