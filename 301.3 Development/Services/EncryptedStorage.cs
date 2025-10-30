using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using _301._3_Development.Security;

namespace _301._3_Development.Services
{
    public static class EncryptedStorage
    {
        // path to appdata on local users machine: /appdata/roaming/_301._3_Development
        private static readonly string UsersFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "_301._3_Development",
            "users_aesgcm.enc");

        public static void SaveEncrypted<T>(IEnumerable<T> data, AesGcmEncryptionService enc)
        {
            var json = JsonSerializer.Serialize(data);
            var cipher = enc.EncryptString(json);

            var dir = Path.GetDirectoryName(UsersFile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            //debugging
            Debug.WriteLine("");
            //

            File.WriteAllText(UsersFile, cipher);
        }

        public static List<T> LoadEncrypted<T>(AesGcmEncryptionService enc) // need to rework this
        {
            if (!File.Exists(UsersFile)) return new List<T>();
            var cipher = File.ReadAllText(UsersFile);
            var json = enc.DecryptString(cipher);
            
            //debugging
            Debug.WriteLine("");
            //
            
            return JsonSerializer.Deserialize<List<T>>(json);
        }
    }
}