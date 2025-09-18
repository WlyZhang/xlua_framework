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
        /// ������Ļת��
        /// </summary>
        /// <param name="isPortrait">��true��������false������</param>
        public static void SetScreen(bool isPortrait)
        {
            Screen.orientation = ScreenOrientation.AutoRotation;//���÷���Ϊ�Զ�(������Ҫ�Զ���ת��Ļ�����κ����õķ���)
            Screen.autorotateToLandscapeRight = !isPortrait;           //�Զ���ת���Һ���
            Screen.autorotateToLandscapeLeft = !isPortrait;            //�Զ���ת�������
            Screen.autorotateToPortrait = isPortrait;                //�Զ���ת������
            Screen.autorotateToPortraitUpsideDown = isPortrait;      //�Զ���ת����������
            Screen.sleepTimeout = SleepTimeout.NeverSleep;      //˯��ʱ��Ϊ�Ӳ�˯��
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
        /// ���ݡ�Tag����ȡԤ���б�
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
        /// �������&�ԳƼ��ܹ�����
        /// </summary>
        public static class EncryptionTool
        {
            // �ԳƼ�����Կ���豣֤��ȫ�������ʾ���򵥵Ĺ̶��ַ�����ʵ�ʿɸ�������ã�
            private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890abcdef");
            // �ԳƼ��ܳ�ʼ�������豣֤��ȫ�������ʾ���򵥵Ĺ̶��ַ�����ʵ�ʿɸ�������ã�
            private static readonly byte[] IV = Encoding.UTF8.GetBytes("abcdef1234567890");

            /// <summary>
            /// ʹ��AES�㷨���жԳƼ���
            /// </summary>
            /// <param name="plainText">��������</param>
            /// <returns>���ܺ���ֽ�����</returns>
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
            /// ʹ��AES�㷨���жԳƽ���
            /// </summary>
            /// <param name="cipherText">���ܺ���ֽ�����</param>
            /// <returns>���ܺ����������</returns>
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
            /// �����ַ�����MD5��ϣֵ
            /// </summary>
            /// <param name="input">������ַ���</param>
            /// <returns>MD5��ϣֵ��ʮ�������ַ�����ʽ��</returns>
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
            /// ��ȡ��Ļ��ͼ������ΪPNGͼƬ
            /// </summary>
            /// <param name="path">���ý�ͼ�ļ�·��</param>
            /// <param name="tx2dName">���ý�ͼ������(ע:��������׺)</param>
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