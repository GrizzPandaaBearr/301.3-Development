using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using _301._3_Development.Scripts;
using _301._3_Development.Pages.PatientPages;
using _301._3_Development.Pages.AdminPages;
using _301._3_Development.Pages.DoctorPages;
using _301._3_Development.models;
using _301._3_Development.Services;
using UserControl = System.Windows.Controls.UserControl;
using _301._3_Development.Scripts.Session;

namespace _301._3_Development.Controls
{
    /// <summary>
    /// Interaction logic for HamburgerMenu.xaml
    /// </summary>
    public partial class HamburgerMenu : UserControl
    {
        public event EventHandler Logout;
        private readonly UserDTO _user;
        public bool collapsed {  get; set; }
        private List<Button> _buttons;

        private Frame mainFrame {  get; set; }

        ButtonEventHandler ButtonEventHandler { get; set; }
        public HamburgerMenu(UserDTO user, Frame _mainFrame)
        {
            InitializeComponent();
            _user = user;

            //ButtonEventHandler handler = new ButtonEventHandler(mainFrame);

            mainFrame = _mainFrame;
            _buttons = SetButtonEvents(user.Role);

            collapsed = true;

            BuildBurger();
        }

        private void BuildBurger() // creates hamburger menu appropriate to role of user. i.e: admin, patient, doctor.
        {
            
            AddButtons();
        }

        private List<Button> SetButtonEvents(string role)
        {
            var events = role switch
            {
                "Admin" => SetAdminControls(),
                "Doctor" => SetDoctorControls(),
                "Patient" => SetPatientControls(),
                _ => new List<(string, Action)>()
            };

            var buttons = new List<Button>();

            foreach (var (label, action) in events)
            {
                var btn = new Button
                {
                    Content = label,
                    Height = 40,
                    Margin = new Thickness(5),
                };

                btn.Click += (_, _) => action();
                buttons.Add(btn);
            }

            return buttons;
        }


        private void AddButtons()
        {
            int row = 3;

            foreach (var btn in _buttons)
            {
                Grid.SetRow(btn, row++);
                HamburgerControlGrid.Children.Add(btn);
            }
        }


        private List<(string Label, Action ClickAction)> SetPatientControls()
        {
            return new()
            {
                ("Profile", () => mainFrame.Content = new PatientProfilePage(_user)),
                ("Appointments", () => mainFrame.Content = new PatientAppointmentsPage(_user)),
                ("Medical History", () => mainFrame.Content = new PatientHistoryPage(_user)),
                ("Book Appointment", () => mainFrame.Content = new PatientBookAppointmentPage(_user))
            };
        }
        private List<(string Label, Action ClickAction)> SetDoctorControls()
        {
            return new()
            {
                ("My Patients", () => mainFrame.Content = new DoctorPatientsPage(_user)),
                ("Schedule", () => mainFrame.Content = new DoctorSchedulePage(_user))
            };
        }
        private List<(string Label, Action ClickAction)> SetAdminControls()
        {
            return new()
            {
                ("User Management", () => mainFrame.Content = new AdminUsersPage(_user)),
                ("System Logs", () => mainFrame.Content = new AdminLogsPage(_user)),
                ("Add User", () => mainFrame.Content = new UserCreator(_user))
            };
        }
        private void ActivateHamburger_Click(object sender, RoutedEventArgs e)
        {
            collapsed = !collapsed;

            if (collapsed)
            {
                HamburgerControlGrid.Width = 50;
                Grid.SetColumnSpan(ActivateHamburger, 4);
                Grid.SetColumn(ActivateHamburger, 0);
            }
            else
            {
                HamburgerControlGrid.Width = 225;
                Grid.SetColumn(ActivateHamburger, 3);
                Grid.SetColumnSpan(ActivateHamburger, 1);
            }
        }

        public void PatientInformationRoute(object sender, RoutedEventArgs e)
        {
            patientdataform dataform = new patientdataform();

            mainFrame.Content = dataform;

            Debug.Write($"You Pressed routed button: {((FrameworkElement)e.Source).Name}");
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            Logout?.Invoke(this, EventArgs.Empty);

        }
    }
}
