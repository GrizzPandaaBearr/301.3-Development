using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;

namespace _301._3_Development.Security
{
    /// <summary>
    /// Generates a 256-bit key and stores it protected by DPAPI (CurrentUser).
    /// Returns the raw key bytes for use with AES.
    /// </summary>
    public static class KeyManager
    {
        private static readonly string KeyFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "_301._3_Development",
            "enc.key");

        public static byte[] GetOrCreatekey()
        {
            var dir = Path.GetDirectoryName(KeyFilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(KeyFilePath))
            {
                var protectedBytes = File.ReadAllBytes(KeyFilePath);
                try
                {
                    var key = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
                    if (key.Length == 32)
                        return key;
                }
                catch { }
                File.Delete(KeyFilePath);
            }

            using var rng = RandomNumberGenerator.Create();
            var newKey = new byte[32];
            rng.GetBytes(newKey);

            var protectedKey = ProtectedData.Protect(newKey, null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(KeyFilePath, protectedKey);

            return newKey;
        }

        public static void DeleteStoredKey()
        {
            if (File.Exists(KeyFilePath)) File.Delete(KeyFilePath);
        }
    }
}