using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;

namespace _301._3_Development.Services
{
    public class UserRecord
    {
        public required string Username { get; set; }
        public required string Salt { get; set; }
        public required string PasswordHash { get; set; }
        public required string FullName { get; set; }
    }

    public class UserStore
    {
        private readonly string filePath;

        public UserStore(string filePath)
        {
            this.filePath = filePath;
        }

        private static byte[] GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        private static string HashPassword(string password, byte[] salt, int iterations = 100_000)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hash);
        }

        public List<UserRecord> LoadAll()
        {
            if (!File.Exists(filePath))
                return new List<UserRecord>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<UserRecord>>(json) ?? new List<UserRecord>();
        }

        public void SaveAll(List<UserRecord> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public bool AddUser(string username, string plainPassword, string fullName = null)
        {
            var users = LoadAll();
            if (users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return false;

            var salt = GenerateSalt();
            var hash = HashPassword(plainPassword, salt);

            users.Add(new UserRecord
            {
                Username = username,
                FullName = fullName,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = hash
            });

            SaveAll(users);
            return true;
        }

        public bool ValidateCredentials(string username, string plainPassword)
        {
            var users = LoadAll();
            var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return false;

            var salt = Convert.FromBase64String(user.Salt);
            var computedHash = HashPassword(plainPassword, salt);

            return computedHash == user.PasswordHash;
        }
    }
}
