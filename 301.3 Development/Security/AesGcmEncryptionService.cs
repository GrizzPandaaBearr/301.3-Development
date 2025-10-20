using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _301._3_Development.Security
{
    public class AesGcmEncryptionService
    {
        private readonly byte[] _key;

        public AesGcmEncryptionService(byte[] key)
        {
            if (key == null || key.Length != 32) throw new ArgumentException("Key must be 32 bytes (256-bit).");
            _key = key;
        }

        public string EncryptString(string plainText)
        {
            if (plainText == null) return null;

            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            var nonce = new byte[12];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(nonce);

            var cipherBytes = new byte[plainBytes.Length];
            var tag = new byte[16];

            using var aesGcm = new AesGcm(_key);
            aesGcm.Encrypt(nonce, plainBytes, cipherBytes, tag, null);

            using var ms = new MemoryStream();
            ms.Write(nonce, 0, nonce.Length);
            ms.Write(tag, 0, tag.Length);
            ms.Write(cipherBytes, 0, cipherBytes.Length);

            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptString(string base64Payload)
        {
            if (string.IsNullOrEmpty(base64Payload)) return null;

            var allBytes = Convert.FromBase64String(base64Payload);
            if (allBytes.Length < 12 + 16) throw new ArgumentException("Invalid payload");

            var nonce = new byte[12];
            Array.Copy(allBytes, 0, nonce, 0, 12);

            var tag = new byte[16];
            Array.Copy(allBytes, 12, tag, 0, 16);

            var cipherLen = allBytes.Length - 12 - 16;
            var cipherBytes = new byte[cipherLen];
            Array.Copy(allBytes, 28, cipherBytes, 0, cipherLen);

            var plainBytes = new byte[cipherLen];
            using var aesGcm = new AesGcm(_key);
            aesGcm.Decrypt(nonce, cipherBytes, tag, plainBytes, null);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}