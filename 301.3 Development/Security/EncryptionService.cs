using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


namespace _301._3_Development.Security{
    public class EncryptionService
    {
        private readonly byte[] key;

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
        }
    }
