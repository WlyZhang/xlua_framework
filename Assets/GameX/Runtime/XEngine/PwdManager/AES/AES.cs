using System;
using System.IO;
using System.Security.Cryptography;

public class AES
{
    /// <summary>
    /// ���ܷ���
    /// </summary>
    /// <param name="data">����Ҫ���ܵ��ı�����</param>
    /// <param name="key">byte[] key = new byte[32];��256 λ��Կ��</param>
    /// <param name="iv">yte[] iv = new byte[16];  ��128 λ��ʼ��������</param>
    /// <returns></returns>
    public static byte[] Encoder(byte[] data, byte[] key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.FlushFinalBlock();
                }
                return msEncrypt.ToArray();
            }
        }
    }

    /// <summary>
    /// ���ܷ���
    /// </summary>
    /// <param name="cipherText">����Ҫ���ܵ��ı�����</param>
    /// <param name="key">byte[] key = new byte[32];��256 λ��Կ��</param>
    /// <param name="iv">yte[] iv = new byte[16];  ��128 λ��ʼ��������</param>
    /// <returns></returns>
    public static byte[] Decoder(byte[] cipherText, byte[] key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (MemoryStream plaintextStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int read;
                        while ((read = csDecrypt.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            plaintextStream.Write(buffer, 0, read);
                        }
                        return plaintextStream.ToArray();
                    }
                }
            }
        }
    }
}