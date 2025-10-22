using System;
using System.IO;
using System.Security.Cryptography;

namespace _301._3_Development.Security
{
    public static class KeyManager
    {
        private static readonly string KeyFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "_301._3_Development",
            "aesgcm.key");

        public static byte[] GetOrCreateKey()
        {
            var dir = Path.GetDirectoryName(KeyFilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(KeyFilePath))
            {
                try
                {
                    var protectedBytes = File.ReadAllBytes(KeyFilePath);
                    var key = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
                    if (key != null && key.Length == 32) return key;
                }
                catch
                {
                }
                File.Delete(KeyFilePath);
            }

            var keyBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(keyBytes);

            var protectedKey = ProtectedData.Protect(keyBytes, null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(KeyFilePath, protectedKey);
            return keyBytes;
        }

        public static void DeleteStoredKey()
        {
            if (File.Exists(KeyFilePath)) File.Delete(KeyFilePath);
        }
    }
}