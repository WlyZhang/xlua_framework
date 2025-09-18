using OBS;
using OBS.Model;
using System;
using System.IO;
using UnityEngine;

public class DownloadOBS
{
    /// <summary>
    /// 下载OBS流资源
    /// </summary>
    /// <param name="clientOBS"></param>
    /// <param name="configOBS"></param>
    /// <param name="objectKey"></param>
    /// <param name="savePath"></param>
    /// <returns></returns>
    public string GetObject(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, string savePath = "")
    {
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();

        string dest = string.Empty;

        // 下载对象
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
                    // 将对象的数据流写入文件中
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
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();

        // 下载对象
        try
        {
            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
            };

            // 以传输字节数为基准反馈下载进度
            request.ProgressType = ProgressTypeEnum.ByBytes;
            // 每下载1MB数据反馈下载进度
            request.ProgressInterval = 1024 * 1024;

            // 注册下载进度回调函数
            request.DownloadProgress += delegate (object sender, TransferStatus status) {
                // 获取下载平均速率
                Debug.Log($"AverageSpeed: {status.AverageSpeed / 1024} KB/S");
                // 获取下载进度百分比
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
                    // 将对象的数据流写入文件中
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
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();

        // 下载对象
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
                            // 将对象的数据流写入文件中
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