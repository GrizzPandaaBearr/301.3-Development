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
using _301._3_Development.models;

namespace _301._3_Development
{
    public partial class mainscreen : Page
    {
        public mainscreen(UserDTO user)
        {
            InitializeComponent();
            MainWindow.a
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigate to Registration Form Page.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NavigationService.Navigate(new patientdataform());
        }

        private void BtnViewData_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigate to View Data Page.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //NavigationService.Navigate(new finalform());
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Data exported to image.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //implement export logic here
        }
    }
}