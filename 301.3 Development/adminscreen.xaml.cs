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

namespace _301._3_Development
{
    public partial class adminscreen : Page
    {
        public adminscreen()
        {
            InitializeComponent();
        }

        private void BtnAddAdmin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigate to Add Admin Page.", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new newadmin());
        }

        private void BtnUpdateRecords_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigate to Update Records Page.", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnReadLogs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reading User Logs", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Data exported to Excel.", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}