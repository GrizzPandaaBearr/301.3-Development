using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace _301._3_Development.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) 
                throw new ArgumentNullException("Password cannot be empty");

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            var result = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
            Buffer.BlockCopy(key ,0, result, SaltSize, KeySize);

            return Convert.ToBase64String(result);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash)) return false;
            var allbBytes = Convert.FromBase64String(storedHash);

            if (allbBytes.Length != SaltSize + KeySize) 
                return false;

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(allbBytes, 0, salt, 0, SaltSize);

            var storedKey = new byte[KeySize];
            Buffer.BlockCopy(allbBytes, SaltSize, storedKey, 0, KeySize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var computedKey = pbkdf2.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(storedKey, computedKey);
        }
    }
}
