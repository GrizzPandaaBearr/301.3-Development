using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using _301._3_Development.Security;

namespace _301._3_Development.Services
{
    public static class PatientStorage
    {
        private static readonly string FilePath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
            "_301.3_Development",
            "patients.enc"
        );

        public static void SavePatientData(object patientData, AesGcmEncryptionService encryptionService)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

            var allPatients = LoadPatientData(encryptionService);
            allPatients.Add(patientData);

            var json = JsonSerializer.Serialize(allPatients);

            var encryptedBytes = encryptionService.Encrypt(json);
            File.WriteAllBytes(FilePath, encryptedBytes);
        }

        public static List<dynamic> LoadPatientData(AesGcmEncryptionService encryptionService)
        {
            if (!File.Exists(FilePath))
                return new List<dynamic>();

            try
            {
                var encryptedBytes = File.ReadAllBytes(FilePath);
                var json = encryptionService.Decrypt(encryptedBytes);
                return JsonSerializer.Deserialize<List<dynamic>>(json) ?? new List<dynamic>();
            }
            catch
            {
                return new List<dynamic>();
            }
        }
    }
}