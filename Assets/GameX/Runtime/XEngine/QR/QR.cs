using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using ZXing;
using ZXing.QrCode;
using Cysharp.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class QR
{
	/// <summary>
	/// QR���л�
	/// </summary>
	/// <param name="data"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static Texture2D Encode(string data, int width, int height, bool isSave = true, string QR_Name = "key")
    {
		Texture2D encoded = new Texture2D(width, height, TextureFormat.RGBA32, false);
		var colors = GetColor32(data, width, height);
		encoded.SetPixels32(colors);
		encoded.Apply();

		if(isSave)
        {
			DateTime now = DateTime.UtcNow; // ��ȡ��ǰ UTC ʱ��
			long timestamp = (long)(now - new DateTime(1970, 1, 1)).TotalSeconds; // ����� 1970-01-01 ���������

			string path = $"{Application.streamingAssetsPath}/config/";
			string name = $"{QR_Name}.png";
			byte[] bytes = encoded.EncodeToPNG();

			ByteToFile(path, name, bytes, bytes.Length);
        }

		return encoded;
	}

	
	/// <summary>
	/// ��ȡ��������
	/// </summary>
	/// <param name="textForEncoding"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	private static Color32[] GetColor32(string textForEncoding, int width, int height)
	{
		BarcodeWriter writer = new BarcodeWriter
		{
			Format = BarcodeFormat.QR_CODE,
			Options = new QrCodeEncodingOptions
			{
				Height = height,
				Width = width,
				Margin = 2,
				PureBarcode = true
			}
		};
		return writer.Write(textForEncoding);
	}


	/// <summary>
	/// QR�����л�
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static async UniTask<string> Decode(string path)
	{
		Texture2D texture = await LoadQR(path);

		BarcodeReader mReader = new BarcodeReader();
		Color32[] colors = texture.GetPixels32();
		var result = mReader.Decode(colors, texture.width, texture.height);
		if (result != null)
		{
			Debug.Log(result.Text);

			return result.Text;
		}
		return null;
	}

	/// <summary>
	/// ����QR
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	private static async UniTask<Texture2D> LoadQR(string path)
    {
		WWW www = new WWW(path);

		while(!www.isDone)
        {
			await UniTask.Yield();
        }

		Texture2D texture = www.texture;

		return texture;
    }

	/// <summary>
	/// �����ļ�
	/// </summary>
	/// <param name="path"></param>
	/// <param name="info"></param>
	/// <param name="length"></param>
	private static void ByteToFile(string path, string name, byte[] info, int length)
	{
		if(!Directory.Exists(path))
        {
			Directory.CreateDirectory(path);
        }

		//�ļ�����Ϣ  
		//StreamWriter sw;  
		Stream sw;
		FileInfo t = new FileInfo(path + name);
		t.Delete();
		sw = t.Create();
		//���е���ʽд����Ϣ  
		//sw.WriteLine(info);  
		sw.Write(info, 0, length);
		//�ر���  
		sw.Close();
		//������  
		sw.Dispose();

#if UNITY_EDITOR
		AssetDatabase.Refresh();
#endif

		Debug.Log($"����QR�ɹ�������·����<color=yellow>{path + name}</color>");
	}
}
