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
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;

using _301._3_Development.Scripts;
using _301._3_Development.Windows;

namespace _301._3_Development.Pages
{
    /// <summary>
    /// Interaction logic for PatientRegistration.xaml
    /// </summary>
    public partial class PatientRegistration : Page
    {
        string _Name;
        string _Phone;
        string _Passport;
        string _BirthPlace;
        string _Gender;
        DateTime _BirthDate;
        DateTime _AppointmentDate;
        public PatientRegistration()
        {
            InitializeComponent();
        }

        private void NewUserRequest()
        {
            DataHandler dataHandler = new DataHandler();
            Patient patient = new Patient();

            patient.Name_First = _Name.Split()[0];
            patient.Phone = _Phone;
            patient.Birth_Place = _BirthPlace;
            patient.Appointment_Date = _AppointmentDate.ToString();
            patient.Sex = _Gender[0]; // this is horrid
            patient.Name_Last = _Name.Split()[1];
            patient.Birth_Date = _BirthDate.ToString();

            bool requestResult = dataHandler.NewPatient(patient);

            Debug.WriteLine("DID IT WORK????",requestResult);
        }

        private void GetSetForm()
        {
            _Name = NameBox.Text;
            _Phone= PhoneBox.Text;
            _Passport= PassportBox.Text;
            _BirthPlace= BirthPlaceBox.Text;
            _Gender = GenderBox.Text;
        }

        private void SendRegistrationForm_btn_Click(object sender, RoutedEventArgs e)
        {

            
            if (!CheckFormRequest())
            {
                return;

            }
            GetSetForm();
            NewUserRequest();
        }
        private bool CheckFormRequest()
        {
            bool isValid = true;

            


            List<TextBox> textBoxes = new List<TextBox>() { NameBox, PhoneBox, PassportBox, BirthPlaceBox, GenderBox};

            

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

        private void PickDateBtn(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;

            Debug.WriteLine(btn.Name);

            CalendarSelect calendarSelect = new CalendarSelect();
            calendarSelect.ShowDialog();

            Debug.WriteLine(calendarSelect.CalendarSelector.SelectedDate.ToString());

        }
        private void RoutePickedDate(string btn, DateTime date)
        {
            switch(btn)
            {
                case "PickBirthdayBtn":
                    _BirthDate = date;
                    break;
                case "PickAppointmentBtn":
                    _AppointmentDate = date;
                    break;
            }
        }
    }
}
