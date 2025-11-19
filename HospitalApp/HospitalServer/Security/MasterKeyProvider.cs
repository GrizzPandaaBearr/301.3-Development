
using System.Security.Cryptography;
public static class MasterKeyProvider
{
    private const string KeyFile = "master_key.bin";

    public static byte[] GetMasterKey()
    {
        if (File.Exists(KeyFile))
            return File.ReadAllBytes(KeyFile);

        var key = RandomNumberGenerator.GetBytes(32); // AES-256
        File.WriteAllBytes(KeyFile, key);
        return key;
    }
}
