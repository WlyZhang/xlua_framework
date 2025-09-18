using System;
using System.IO;
using System.Security.Cryptography;

public class AES
{
    /// <summary>
    /// 加密方法
    /// </summary>
    /// <param name="data">这是要加密的文本内容</param>
    /// <param name="key">byte[] key = new byte[32];【256 位密钥】</param>
    /// <param name="iv">yte[] iv = new byte[16];  【128 位初始化向量】</param>
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
    /// 解密方法
    /// </summary>
    /// <param name="cipherText">这是要解密的文本内容</param>
    /// <param name="key">byte[] key = new byte[32];【256 位密钥】</param>
    /// <param name="iv">yte[] iv = new byte[16];  【128 位初始化向量】</param>
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