using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using _301._3_Development.Security;

namespace _301._3_Development.Services
{
    public static class PatientStorage
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "_301.3_Development",
            "patients.enc"
        );

        public static void SavePatientData(object patientData, AesGcmEncryptionService encryptionService)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

            var allPatients = LoadPatientData(encryptionService);
            allPatients.Add(patientData);

            var json = JsonSerializer.Serialize(allPatients, new JsonSerializerOptions { WriteIndented = false });
            var cipherText = encryptionService.EncryptString(json);

            File.WriteAllText(FilePath, cipherText);
        }

        public static List<dynamic> LoadPatientData(AesGcmEncryptionService encryptionService)
        {
            if (!File.Exists(FilePath))
                return new List<dynamic>();

            try
            {
                var cipherText = File.ReadAllText(FilePath);
                var json = encryptionService.DecryptString(cipherText);

                var list = JsonSerializer.Deserialize<List<dynamic>>(json);
                return list ?? new List<dynamic>();
            }
            catch
            {
                return new List<dynamic>();
            }
        }

        public static void SaveAllPatients(IEnumerable<dynamic> patients, AesGcmEncryptionService encryptionService)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            var json = JsonSerializer.Serialize(patients, new JsonSerializerOptions { WriteIndented = false });
            var cipherText = encryptionService.EncryptString(json);
            File.WriteAllText(FilePath, cipherText);
        }
    }
}