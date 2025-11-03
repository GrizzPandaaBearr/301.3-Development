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
using UserControl = System.Windows.Controls.UserControl;

namespace _301._3_Development.Controls
{
    /// <summary>
    /// Interaction logic for HamburgerMenu.xaml
    /// </summary>
    public partial class HamburgerMenu : UserControl
    {
        public bool collapsed {  get; set; }
        private List<System.Windows.Controls.Button> _buttons;
        private List<System.Windows.Controls.RowDefinition> _buttonSlots;
        public HamburgerMenu()
        {
            InitializeComponent();

            BuildBurger("patient");
        }

        private void BuildBurger(string role) // creates hamburger menu appropriate to role of user. i.e: admin, patient, doctor.
        {
            SetButtons(role);
            AddButtons();
        }

        private void SetButtons(string role)
        {
            switch (role)
            {
                case "admin":
                    SetAdminControl();
                    break;
                case "doctor":
                    SetDoctorControl();
                    break;
                case "patient":
                    SetPatientControl();
                    break;
                default:
                    Debug.Write($"Set buttons failed: role = {role}");
                    break;
            }
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

        private int SetAdminControl()
        {
            List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();
            // create buttons
            Button button = new Button();
            button.Content = "placeholder";
            // add buttons to list
            btnList.Add(button);

            // set buttons
            _buttons = btnList;

            return 1;
        }
        private int SetDoctorControl()
        {
            List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();
            // create buttons
            Button button = new Button();
            button.Content = "placeholder";
            // add buttons to list
            btnList.Add(button);

            // set buttons
            _buttons = btnList;

            return 1;
        }
        private int SetPatientControl()
        {
            List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();
            // create buttons
            Button button = new Button();
            button.Content = "placeholder";
            // add buttons to list
            btnList.Add(button);

            // set buttons
            _buttons = btnList;

            return 1;
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

            HamburgerControlGrid.Width = double.NaN;
            collapsed = true;


        }
    }
}
