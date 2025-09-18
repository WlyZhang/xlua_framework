using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CXGame
{
    public class API
    {

        /// <summary>
        /// 设置屏幕转向
        /// </summary>
        /// <param name="isPortrait">【true竖屏】【false横屏】</param>
        public static void SetScreen(bool isPortrait)
        {
            Screen.orientation = ScreenOrientation.AutoRotation;//设置方向为自动(根据需要自动旋转屏幕朝向任何启用的方向。)
            Screen.autorotateToLandscapeRight = !isPortrait;           //自动旋转到右横屏
            Screen.autorotateToLandscapeLeft = !isPortrait;            //自动旋转到左横屏
            Screen.autorotateToPortrait = isPortrait;                //自动旋转到纵向
            Screen.autorotateToPortraitUpsideDown = isPortrait;      //自动旋转到纵向上下
            Screen.sleepTimeout = SleepTimeout.NeverSleep;      //睡眠时间为从不睡眠
            Screen.fullScreen = true;

            CanvasScaler canvasScaler = GameObject.FindObjectOfType<CanvasScaler>();

            if (canvasScaler)
            {
                if (isPortrait)
                {
                    Screen.SetResolution(1080, 1920, true);
                    canvasScaler.referenceResolution = new Vector2(1080, 1920);
                }
                else
                {
                    Screen.SetResolution(1920, 1080, true);
                    canvasScaler.referenceResolution = new Vector2(1920, 1080);
                }
            }
        }


        /// <summary>
        /// 根据【Tag】获取预设列表
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<GameObject> GetMapObjects(string tag)
        {
            GameObject[] goArr = GameObject.FindGameObjectsWithTag(tag);

            List<GameObject> list = new List<GameObject>(goArr);

            return list;
        }


        //==================================================================================================================================
        //==================================================================================================================================

        /// <summary>
        /// 单项加密&对称加密工具类
        /// </summary>
        public static class EncryptionTool
        {
            // 对称加密密钥（需保证安全，这里仅示例简单的固定字符串，实际可更灵活配置）
            private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890abcdef");
            // 对称加密初始向量（需保证安全，这里仅示例简单的固定字符串，实际可更灵活配置）
            private static readonly byte[] IV = Encoding.UTF8.GetBytes("abcdef1234567890");

            /// <summary>
            /// 使用AES算法进行对称加密
            /// </summary>
            /// <param name="plainText">明文内容</param>
            /// <returns>加密后的字节数组</returns>
            public static byte[] AESEncrypt(string plainText)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

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
                        return msEncrypt.ToArray();
                    }
                }
            }

            /// <summary>
            /// 使用AES算法进行对称解密
            /// </summary>
            /// <param name="cipherText">加密后的字节数组</param>
            /// <returns>解密后的明文内容</returns>
            public static string AESDecrypt(byte[] cipherText)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
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

            /// <summary>
            /// 计算字符串的MD5哈希值
            /// </summary>
            /// <param name="input">输入的字符串</param>
            /// <returns>MD5哈希值（十六进制字符串形式）</returns>
            public static string CalculateMD5Hash(string input)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("x2"));
                    }

                    return sb.ToString();
                }
            }


            /// <summary>
            /// 获取屏幕截图并保存为PNG图片
            /// </summary>
            /// <param name="path">设置截图文件路径</param>
            /// <param name="tx2dName">设置截图的名称(注:不包含后缀)</param>
            public static void ScreenCutSave()
            {
                Texture2D tx2d = ScreenCapture.CaptureScreenshotAsTexture();

                byte[] bytes = tx2d.EncodeToPNG();

                string project = "GameX";

                string time = EncryptionTool.CalculateMD5Hash(DateTime.Now.ToString().ToLower());

                string picName = string.Join("-", project, time);

                string path = $"{Application.streamingAssetsPath}/pictures";

                string savePath = Path.Combine(path, picName, ".png");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                File.WriteAllBytes(savePath, bytes);

#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            }
        }
    }
}