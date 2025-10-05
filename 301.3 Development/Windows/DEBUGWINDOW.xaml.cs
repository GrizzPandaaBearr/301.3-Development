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
using System.Windows.Shapes;
using _301._3_Development.Scripts;

namespace _301._3_Development.Windows
{
    /// <summary>
    /// Interaction logic for DEBUGWINDOW.xaml
    /// </summary>
    public partial class DEBUGWINDOW : Window
    {
        public DEBUGWINDOW()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataHandler handler = new DataHandler();

            List<Patient> patients = handler.GetPatients();

            PatientDataGrid.ItemsSource = patients;

        }
    }
}
