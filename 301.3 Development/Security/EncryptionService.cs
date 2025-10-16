using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Input;


namespace _301._3_Development.Security
{
    public class EncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(byte[] key)
        {
            if (key == null || key.Length != 32) throw new ArgumentException("key must be 32 bytes (256-bit).");
            _key = key;
        }

        public string EncryptString(string plainText)
        {
            if (plainText == null) return null;

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.GenerateIV();
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, iv);
            using var ms = new MemoryStream();

            ms.Write(iv, 0, iv.Length);
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs, Encoding.UTF8))
            {
                sw.Write(plainText);
            }

            var result = ms.ToArray();
            return Convert.ToBase64String(result);
        }

        public string DecryptString(string base64CipherText)
        {
            if (string.IsNullOrEmpty(base64CipherText))
                return null;

            var allBytes = Convert.FromBase64String(base64CipherText);
            if (allBytes.Length < 16) throw new ArgumentException("Cipher text too short");

            var iv = new byte[16];
            Array.Copy(allBytes, 0, iv, 0, 16);

            var cipherBytes = new byte[allBytes.Length - 16];
            Array.Copy(allBytes, 16, cipherBytes, 0, cipherBytes.Length);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs, Encoding.UTF8);
            return sr.ReadToEnd();

        }
    }
}