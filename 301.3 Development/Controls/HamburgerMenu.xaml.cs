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
using _301._3_Development.Services;
using UserControl = System.Windows.Controls.UserControl;

namespace _301._3_Development.Controls
{
    /// <summary>
    /// Interaction logic for HamburgerMenu.xaml
    /// </summary>
    public partial class HamburgerMenu : UserControl
    {
        public bool collapsed {  get; set; }
        private List<Button> _buttons;

        private Frame mainFrame {  get; set; }

        ButtonEventHandler ButtonEventHandler { get; set; }
        public HamburgerMenu(User.UserRole role, Frame _mainFrame)
        {
            InitializeComponent();

            //ButtonEventHandler handler = new ButtonEventHandler(mainFrame);

            mainFrame = _mainFrame;
            _buttons = SetButtonEvents(role);

            collapsed = true;

            BuildBurger();
        }

        private void BuildBurger() // creates hamburger menu appropriate to role of user. i.e: admin, patient, doctor.
        {
            
            AddButtons();
        }

        private List<Button> SetButtonEvents(User.UserRole role)
        {
            List<Button> buttons = new List<Button>();

            List<(string Label, Action ClickAction)> evntList = new List<(string Label, Action ClickAction)>();
            switch (role)
            {
                case User.UserRole.Admin:
                    //btnList = SetAdminControl();
                    break;
                case User.UserRole.Doctor:
                    //btnList = SetDoctorControl();
                    break;
                case User.UserRole.Patient:
                    evntList = SetPatientControl();
                    break;
                default:
                    Debug.Write($"Set buttons failed: role = {role}");
                    break;
            }

            foreach (var (label, action) in evntList)
            {
                var btn = new Button
                {
                    Content = label,

                };
                btn.Click += (_, _) => action();

                buttons.Add(btn);
            }

            return buttons;
        }


        private void AddButtons()
        {
            int i = 3;
            foreach(Button btn in _buttons)
            {
                btn.Name = $"Btn{i}";
                HamburgerControlGrid.Children.Add(btn);
                Grid.SetRow(btn, i);
                
                i++;
            }
        }



        private List<Button> SetAdminControl()
        {
            List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();
            // create buttons
            Button button = new Button();
            button.Content = "placeholder";
            // add buttons to list
            btnList.Add(button);

            // set buttons

            return btnList;
        }
        private List<Button> SetDoctorControl()
        {
            List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();
            // create buttons
            Button button = new Button();
            button.Content = "placeholder";
            // add buttons to list
            btnList.Add(button);

            // set buttons

            return btnList;
        }
        private List<(string Label, Action ClickAction)> SetPatientControl()
        {
            List<(string Label, Action ClickAction)> buttons = new();

            buttons.Add(("Profile", () => mainFrame.Content = new informationform()));
            // create buttons
            
            // add buttons to list

            // set buttons

            return buttons;
        }

        private void ActivateHamburger_Click(object sender, RoutedEventArgs e)
        {
            if(collapsed)
            {
                Grid.SetColumn(ActivateHamburger, 3);
                Grid.SetColumnSpan(ActivateHamburger, 1);
                HamburgerControlGrid.Width = 225;
                collapsed = false;
                return;
            }
            Grid.SetColumnSpan(ActivateHamburger, 4);
            Grid.SetColumn(ActivateHamburger, 0);

            HamburgerControlGrid.Width = 50;
            collapsed = true;


        }

        public void PatientInformationRoute(object sender, RoutedEventArgs e)
        {
            patientdataform dataform = new patientdataform();

            mainFrame.Content = dataform;

            Debug.Write($"You Pressed routed button: {((FrameworkElement)e.Source).Name}");
        }
    }
}
