using System.Security.Cryptography;

public static class EncryptionHelper
{
    private static readonly string EncryptionKey = GenerateRandomKey(256);

    public static string Encrypt(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Convert.FromBase64String(EncryptionKey);
            aesAlg.IV = GenerateRandomIV();
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                return Convert.ToBase64String(aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Convert.FromBase64String(EncryptionKey);
            aesAlg.IV = cipherBytes.Take(16).ToArray();
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes, 16, cipherBytes.Length - 16))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    private static byte[] GenerateRandomIV()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateIV();
            return aesAlg.IV;
        }
    }

    private static string GenerateRandomKey(int keySizeInBits)
    {
        int keySizeInBytes = keySizeInBits / 8;
        byte[] keyBytes = new byte[keySizeInBytes];
        RandomNumberGenerator.Fill(keyBytes);
        return Convert.ToBase64String(keyBytes);
    }
}
