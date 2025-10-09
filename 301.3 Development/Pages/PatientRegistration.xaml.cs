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
using Brushes = System.Windows.Media.Brushes;
using TextBox = System.Windows.Controls.TextBox;

namespace _301._3_Development.Pages
{
    /// <summary>
    /// Interaction logic for PatientRegistration.xaml
    /// </summary>
    public partial class PatientRegistration : Page
    {
        public PatientRegistration()
        {
            InitializeComponent();
        }

        private void SendRegistrationForm_btn_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox>() { NameBox, PhoneBox, PassportBox, BirthPlaceBox, GenderBox};
            if (CheckFormRequest(textBoxes))
            {
                messagebox.Text = "IT IS WORKING";
            }
        }
        private bool CheckFormRequest(List<TextBox> textBoxes)
        {
            bool isValid = true;

            

            string msg = "msg: ";

            foreach (TextBox textBox in textBoxes)
            {
                if(textBox.Text == "")
                {
                    isValid = false;
                    textBox.BorderBrush = Brushes.Red;
                    msg += textBox.Name + " ";
                }
            }

            if (!isValid) { messagebox.Text = msg; }

            return isValid;
        }
    }
}
