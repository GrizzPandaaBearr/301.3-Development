using _301._3_Development.Scripts.Session;
using _301._3_Development.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                var data = await SessionManager.Instance.Api.GetAsync<object>($"Auth/patient/export/{patientId}");
                

                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);

            }
        }

    }
}
