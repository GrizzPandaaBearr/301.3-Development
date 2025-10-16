using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using _301._3_Development.Security;


namespace _301._3_Development.Services
{
    public static class EncryptedStorage
    {
        private static readonly string UsersFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "_301._3_Development", 
            "users.enc");

        public static void SaveUsersEncrypted<T>(IEnumerable<T> data, EncryptionService enc)
        {
            var json = JsonSerializer.Serialize(data);
            var cipher = enc.EncryptString(json);
            File.WriteAllText(UsersFile, cipher);
        }

        public static List<T> LoadUsersEncrypted<T>(EncryptionService enc)
        {
            if (!File.Exists(UsersFile)) return new List<T>();
            var ciphar = File.ReadAllText(UsersFile);
            var json = enc.DecryptString(ciphar);
            return JsonSerializer.Deserialize<List<T>>(json);
        }
    }
}
