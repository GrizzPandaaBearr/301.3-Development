using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class adminscreen : Page
    {
        private readonly string logsFile;

        public adminscreen()
        {
            InitializeComponent();

            var dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyApp");
            Directory.CreateDirectory(dataPath);
            logsFile = Path.Combine(dataPath, "userLogs.enc");
        }

        private void BtnReadUserLogs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(logsFile))
                {
                    MessageBox.Show("No logs found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                string encryptedData = File.ReadAllText(logsFile);
                string decrypted = EncryptionService.Decrypt(encryptedData);

                MessageBox.Show($"Decrypted Logs:\n{decrypted}", "Logs", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading logs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnAddNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new newadmin());
        }

        private void BtnUpdateRecords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newData = "Example user log entry: Admin viewed patient records at " + DateTime.Now;
                string encrypted = EncryptionService.Encrypt(newData);
                File.WriteAllText(logsFile, encrypted);

                MessageBox.Show("Logs updated & encrypted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving encrypted logs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
