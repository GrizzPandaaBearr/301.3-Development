/*using _301._3_Development.Scripts.Session;
using _301._3_Development.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _301._3_Development.Scripts
{
    public class ExportManager
    {
        public async Task ExportPatientInfoAsync(int patientId)
        {
            var saveDialog = new SaveFileDialog
            {
                Title = "Save Patient Information",
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FileName = $"patient_{patientId}_info.json"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveDialog.FileName;

                try
                {
                    string json = await SessionManager.Instance.Api.GetAsync<string>($"/Export/patient/{patientId}");

                    // Save file
                    await File.WriteAllTextAsync(path, json);

                    MessageBox.Show(
                        $"Export complete!\nSaved to:\n{path}",
                        "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Failed to export patient data:\n" + ex.Message,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
        }

    }
}
*/