using System.Security.Cryptography;
using System.Text;

public class EncryptionService
{
    private readonly byte[] _masterKey;

    public EncryptionService(byte[] masterKey)
    {
        if (masterKey.Length != 32)
            throw new ArgumentException("Master key must be 32 bytes for AES-256");
        _masterKey = masterKey;
    }

    // Encrypt plaintext
    public string Encrypt(string plaintext)
    {
        if (string.IsNullOrEmpty(plaintext)) return plaintext;

        using var aes = Aes.Create();
        aes.Key = _masterKey;
        aes.GenerateIV(); // unique IV for each encryption
        using var encryptor = aes.CreateEncryptor();

        var plainBytes = Encoding.UTF8.GetBytes(plaintext);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        // prepend IV to ciphertext for storage
        var result = new byte[aes.IV.Length + cipherBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
        Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);

        return Convert.ToBase64String(result);
    }

    // Decrypt ciphertext
    public string Decrypt(string encrypted)
    {
        if (string.IsNullOrEmpty(encrypted)) return encrypted;

        var fullBytes = Convert.FromBase64String(encrypted);
        var iv = new byte[16];
        var cipher = new byte[fullBytes.Length - 16];

        Buffer.BlockCopy(fullBytes, 0, iv, 0, 16);
        Buffer.BlockCopy(fullBytes, 16, cipher, 0, cipher.Length);

        using var aes = Aes.Create();
        aes.Key = _masterKey;
        aes.IV = iv;
        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }
}
