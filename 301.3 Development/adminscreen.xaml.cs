using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace _301._3_Development
{
    public partial class adminscreen : Page
    {
        private readonly string logsFile;

        public adminscreen()
        {
            InitializeComponent();

        }

        private void BtnReadLogs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(logsFile))
                {
                    MessageBox.Show("No logs found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnAddNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new newadmin());
        }

        private void BtnUpdateRecords_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new recordupdate());
        }
    }
}
