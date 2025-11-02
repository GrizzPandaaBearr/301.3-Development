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
        private List<System.Windows.Controls.Button> _buttons;
        public HamburgerMenu()
        {
            InitializeComponent();
        }

        private void BuildBurger(string role) // creates hamburger menu appropriate to role of user. i.e: admin, patient, doctor.
        {
            

        }

        private void SetButtons(string role)
        {
            switch (role)
            {
                case "admin":
                    SetAdminControl();
                    break;
                case "doctor":
                    SetAdminControl();
                    break;
                case "patient":
                    SetAdminControl();
                    break;
                default:
                    Debug.Write($"Set buttons failed: role = {role}");
                    break;
            }
        }

        private int SetAdminControl()
        {
            List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();
            // create buttons
            Button button = new Button();

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

            // add buttons to list
            btnList.Add(button);

            // set buttons
            _buttons = btnList;

            return 1;
        }

    }
}
