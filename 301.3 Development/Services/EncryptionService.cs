using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace _301._3_Development.Services
{
    public static class EncryptionService
    {
        private static readonly string EncryptionKey = "Your32ByteSuperSecretAESKey_2025!!";

        public static string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
                sw.Write(plainText);

            // Example saving patient record JSON encrypted:
            string master = Environment.GetEnvironmentVariable("FORM_MASTER_PASSPHRASE") ?? "dev-default";
            string patientJson = JsonSerializer.Serialize(patientObject);
            string encrypted = EncryptionService.EncryptString(patientJson, master);
            File.WriteAllText(Path.Combine(dataPath, "patient_R001.enc"), encrypted);

            // To read:
            string encryptedRead = File.ReadAllText(...);
            string decryptedJson = EncryptionService.DecryptString(encryptedRead, master);
            var patient = JsonSerializer.Deserialize<Patient>(decryptedJson);

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}