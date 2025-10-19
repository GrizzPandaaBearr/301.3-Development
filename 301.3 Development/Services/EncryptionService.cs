using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class EncryptionService
{
    private const int KeySizeBytes = 32;
    private const int NonceSizeBytes = 12;
    private const int SaltSizeBytes = 16;
    private const int Pbkdf2IterationsDefault = 200_000;

    private const int PackageVersion = 1;

    public record WrappedKeyPackage(
        int Version,
        string KdfSaltBase64,
        int KdfIterations,
        string KekNonceBase64,
        string WrappedKeyCiphertextBase64,
        string WrappedKeyTagBase64
    );

    public record EncryptedDataPackage(
        int Version,
        string EncryptedDataBase64,
        string DataNonceBase64,
        string DataTagBase64,
        WrappedKeyPackage WrappedKey
    );
    private static byte[] GenerateRandomBytes(int length)
    {
        var b = new byte[length];
        RandomNumberGenerator.Fill(b);
        return b;
    }

    private static byte[] DeriveKeyFromPassword(string password, byte[] salt, int iterations, int keyBytes = KeySizeBytes)
    {
        using var derive = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        return derive.GetBytes(keyBytes);
    }

    private static (byte[] ciphertext, byte[] nonce, byte[] tag) WrapKey(byte[] kek, byte[] cek)
    {
        var nonce = GenerateRandomBytes(NonceSizeBytes);
        var ciphertext = new byte[cek.Length];
        var tag = new byte[16];


        try
        {
            using var aesGcm = new AesGcm(kek);
            aesGcm.Encrypt(nonce, cek, ciphertext, tag);
            return (ciphertext, nonce, tag);
        }
        finally
        {
        }
    }

    private static byte[] UnwrapKey(byte[] kek, byte[] ciphertext, byte[] nonce, byte[] tag)
    {
        var cek = new byte[ciphertext.Length];
        using var aesGcm = new AesGcm(kek);
        aesGcm.Decrypt(nonce, ciphertext, tag, cek);
        return cek;
    }

    private static (byte[] ciphertext, byte[] nonce, byte[] tag) EncryptWithCek(byte[] cek, byte[] plain)
    {
        var nonce = GenerateRandomBytes(NonceSizeBytes);
        var ciphertext = new byte[plain.Length];
        var tag = new byte[16];
        using var aesGcm = new AesGcm(cek);
        aesGcm.Encrypt(nonce, plain, ciphertext, tag);
        return (ciphertext, nonce, tag);
    }

    private static byte[] DecryptWithCek(byte[] cek, byte[] ciphertext, byte[] nonce, byte[] tag)
    {
        var plain = new byte[ciphertext.Length];
        using var aesGcm = new AesGcm(cek);
        aesGcm.Decrypt(nonce, ciphertext, tag, plain);
        return plain;
    }

    public static string EncryptDataWithPassword(byte[] plainBytes, string password, int pbkdf2Iterations = Pbkdf2IterationsDefault)
    {
        var cek = GenerateRandomBytes(KeySizeBytes);

        var (ciphertext, dataNonce, dataTag) = EncryptWithCek(cek, plainBytes);

        var kdfSalt = GenerateRandomBytes(SaltSizeBytes);
        var kek = DeriveKeyFromPassword(password, kdfSalt, pbkdf2Iterations);

        var (wrappedCipher, wrappedNonce, wrappedTag) = WrapKey(kek, cek);

        var wrapped = new WrappedKeyPackage(
        Version: PackageVersion,
        KdfSaltBase64: Convert.ToBase64String(kdfSalt),
        KdfIterations: pbkdf2Iterations,
        KekNonceBase64: Convert.ToBase64String(wrappedNonce),
        WrappedKeyCiphertextBase64: Convert.ToBase64String(wrappedCipher),
        WrappedKeyTagBase64: Convert.ToBase64String(wrappedTag)
        );


        var pkg = new EncryptedDataPackage(
        Version: PackageVersion,
        EncryptedDataBase64: Convert.ToBase64String(ciphertext),
        DataNonceBase64: Convert.ToBase64String(dataNonce),
        DataTagBase64: Convert.ToBase64String(dataTag),
        WrappedKey: wrapped
        );

        Clear(cek);
        Clear(kek);


        var json = JsonSerializer.Serialize(pkg);
        return json;
    }
    public static byte[] DecryptDataWithPassword(string encryptedPackageJson, string password)
    {
        var pkg = JsonSerializer.Deserialize<EncryptedDataPackage>(encryptedPackageJson);
        if (pkg == null) throw new ArgumentException("Invalid package");

        var wrapped = pkg.WrappedKey;
        var kdfSalt = Convert.FromBase64String(wrapped.KdfSaltBase64);
        var kdfIterations = wrapped.KdfIterations;
        var wrappedCipher = Convert.FromBase64String(wrapped.WrappedKeyCiphertextBase64);
        var wrappedNonce = Convert.FromBase64String(wrapped.KekNonceBase64);
        var wrappedTag = Convert.FromBase64String(wrapped.WrappedKeyTagBase64);

        var kek = DeriveKeyFromPassword(password, kdfSalt, kdfIterations);

        byte[] cek = null;
        try
        {
            cek = UnwrapKey(kek, wrappedCipher, wrappedNonce, wrappedTag);

            var ciphertext = Convert.FromBase64String(pkg.EncryptedDataBase64);
            var dataNonce = Convert.FromBase64String(pkg.DataNonceBase64);
            var dataTag = Convert.FromBase64String(pkg.DataTagBase64);


            var plain = DecryptWithCek(cek, ciphertext, dataNonce, dataTag);


            return plain;
        }
        finally
        {
            if (cek != null) Clear(cek);
            Clear(kek);
        }
    }

    private static void Clear(byte[] buffer)
    {
        if (buffer == null) return;
        for (int i = 0; i < buffer.Length; i++) buffer[i] = 0;
    }

    public static string PrettyPrintPackage(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        var tree = doc.RootElement.Clone();
        return JsonSerializer.Serialize(tree, opts);
    }
}