using System;
using System.IO;
using System.Text.Json;
using _301._3_Development.Security;

namespace _301._3_Development.Services
{
    public static class PatientStorage
    {
        private static readonly string PatientsFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "_301._3_Development",
            "patients_aesgcm.enc");

        public static void SavePatientData<T>(T data, AesGcmEncryptionService enc)
        {
            var json = JsonSerializer.Serialize(data);
            var cipher = enc.EncryptString(json);

            var dir = Path.GetDirectoryName(PatientsFile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            File.WriteAllText(PatientsFile, cipher);
        }

        public static T LoadPatientData<T>(AesGcmEncryptionService enc)
        {
            if (!File.Exists(PatientsFile)) return default;
            var cipher = File.ReadAllText(PatientsFile);
            var json = enc.DecryptString(cipher);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}