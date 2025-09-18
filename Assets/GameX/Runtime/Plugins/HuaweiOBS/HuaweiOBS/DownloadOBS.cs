using OBS;
using OBS.Model;
using System;
using System.IO;
using UnityEngine;

public class DownloadOBS
{
    /// <summary>
    /// ����OBS����Դ
    /// </summary>
    /// <param name="clientOBS"></param>
    /// <param name="configOBS"></param>
    /// <param name="objectKey"></param>
    /// <param name="savePath"></param>
    /// <returns></returns>
    public string GetObject(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, string savePath = "")
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();

        string dest = string.Empty;

        // ���ض���
        try
        {
            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
            };

            using (GetObjectResponse response = client.GetObject(request))
            {
                if (string.IsNullOrEmpty(savePath))
                {
                    dest = $"{Application.persistentDataPath}/{configOBS.BucketName}/{objectKey}";
                }
                else
                {
                    dest = savePath;
                }

                if (!File.Exists(dest) && !string.IsNullOrEmpty(dest))
                {
                    // �������������д���ļ���
                    response.WriteResponseStreamToFile(dest);
                }

                Debug.Log($"Get object response: {response.StatusCode}");
            }
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }

        return dest;
    }

    public void GetObjectProgress(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, Action<object, TransferStatus> callback = null, string savePath = "")
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();

        // ���ض���
        try
        {
            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
            };

            // �Դ����ֽ���Ϊ��׼�������ؽ���
            request.ProgressType = ProgressTypeEnum.ByBytes;
            // ÿ����1MB���ݷ������ؽ���
            request.ProgressInterval = 1024 * 1024;

            // ע�����ؽ��Ȼص�����
            request.DownloadProgress += delegate (object sender, TransferStatus status) {
                // ��ȡ����ƽ������
                Debug.Log($"AverageSpeed: {status.AverageSpeed / 1024} KB/S");
                // ��ȡ���ؽ��Ȱٷֱ�
                Debug.Log($"TransferPercentage: {status.TransferPercentage}");

                if(callback != null)
                {
                    callback(sender, status);
                }
            };

            using (GetObjectResponse response = client.GetObject(request))
            {
                string dest = savePath;
                if (!File.Exists(dest) && !string.IsNullOrEmpty(dest))
                {
                    // �������������д���ļ���
                    response.WriteResponseStreamToFile(dest);
                }
                Debug.Log($"Get object response: {response.StatusCode}");
            }
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void GetObjectAsync(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, Action<string> callback, string savePath = "")
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();

        // ���ض���
        try
        {
            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
            };
            client.BeginGetObject(request, delegate (IAsyncResult ar) {
                try
                {
                    using (GetObjectResponse response = client.EndGetObject(ar))
                    {
                        string dest = string.Empty;
                        if (string.IsNullOrEmpty(savePath))
                        {
                            dest = $"{Application.persistentDataPath}/{clientOBS.GetConfigOBS().BucketName}";
                        }
                        else
                        {
                            dest = savePath;
                        }

                        if (!File.Exists(dest) && !string.IsNullOrEmpty(dest))
                        {
                            // �������������д���ļ���
                            response.WriteResponseStreamToFile(dest);
                        }

                        callback(dest);
                        Debug.Log($"Get object response: {response.StatusCode}");
                    }
                }
                catch (ObsException ex)
                {
                    Debug.LogError($"ErrorCode: {ex.ErrorCode}");
                    Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
                }
            }, null);
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }
}