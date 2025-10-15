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
using _301._3_Development.Windows;
using _301._3_Development.Scripts.Repos;
using _301._3_Development.Scripts;
using Microsoft.VisualBasic.ApplicationServices;

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

            Patient patient = SetPatient();

            PatientRepo repo = new PatientRepo();
            if (repo.AddPatient(patient))
            {
                System.Windows.MessageBox.Show("Successfully added Patient to database");
                return;
            }
            
            /*UserRepo userRepo = new UserRepo();
            userRepo.AddUser(user);*/



            Debug.WriteLine("Failed to update database");
        }
        private Patient SetPatient()
        {
            Patient patient = new Patient();
            patient.Birth_Place = _BirthPlace;
            patient.Birth_Date = _BirthDate.ToShortDateString();
            patient.Sex = _Gender;
            patient.DoctorID = 1; //default !!!!!
            patient.Medical_History = "Null";
            
            Random random = new Random();
            patient.FirstName = _Name.Split()[0];
            patient.LastName = _Name.Split()[1];
            patient.Email = random.Next(1, 1000).ToString();
            patient.Phone = _Phone;
            patient.Role = Scripts.User.UserRole.Patient;
            patient.PasswordHash = "SecretPassword";

            return patient;
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

            
            /*if (!CheckFormRequest())
            {
                return;

            }*/
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

            DateTime? dT = calendarSelect.CalendarSelector.SelectedDate;

            if(dT == null)
            {
                Debug.WriteLine("CalendarSelector == Null: " + dT);
                return;
            }

            DateTime selectedDate = (DateTime)dT;
            RoutePickedDate(btn.Name, selectedDate);


        }
        private void RoutePickedDate(string btn, DateTime date)
        {
            switch(btn)
            {
                case "PickBirthdayBtn":
                    Debug.WriteLine("Routed BirthDate:", btn);
                    _BirthDate = date;
                    break;
                case "PickAppointmentBtn":
                    Debug.WriteLine(" Routed AppointmentDate:", btn);
                    _AppointmentDate = date;
                    break;
            }
        }
    }
}
