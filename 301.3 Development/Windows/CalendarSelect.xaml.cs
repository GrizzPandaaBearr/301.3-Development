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
using System.Windows.Shapes;

namespace _301._3_Development.Windows
{
    /// <summary>
    /// Interaction logic for CalendarSelect.xaml
    /// </summary>
    public partial class CalendarSelect : Window
    {
        public DateTime _DateTime { get; set; }
        public CalendarSelect()
        {
            InitializeComponent();
        }


        private void CalendarSelector_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) /// this function is redundant as Selected date gets passed through using Dialogue box (see PatientRegistration.xaml.cs)
        {
            Debug.WriteLine(CalendarSelector.SelectedDate);
        }
    }
}
