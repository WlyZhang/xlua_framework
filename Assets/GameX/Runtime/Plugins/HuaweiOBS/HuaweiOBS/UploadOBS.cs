using OBS.Model;
using OBS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UploadOBS
{
    public void PutObject(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, string str)
    {
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();
        // 上传流
        try
        {
            Stream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(str));
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
                InputStream = stream,
            };
            PutObjectResponse response = client.PutObject(request);
            Debug.Log($"put object response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void PutObject(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, byte[] bytes)
    {
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();
        // 上传流
        try
        {
            Stream stream = new MemoryStream(bytes);
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
                InputStream = stream,
            };
            PutObjectResponse response = client.PutObject(request);
            Debug.Log($"put object response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void PutObjectByPath(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, string filePath)
    {
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();
        // 上传文件
        try
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
                FilePath = filePath,

            };
            PutObjectResponse response = client.PutObject(request);
            Debug.Log($"put object response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void PutObjectByPathAsync(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, string localfile)
    {
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();
        // 上传文件
        try
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = configOBS.BucketName,  //待传入目标桶名
                ObjectKey = objectKey,   //待传入对象名(对象名是对象在桶中的完整路径，如folder/test.txt，路径中不包含桶名)
                FilePath = localfile,   //待上传的本地文件路径，需要指定到具体的文件名
            };
            client.BeginPutObject(request, delegate (IAsyncResult ar) {
                try
                {
                    PutObjectResponse response = client.EndPutObject(ar);
                    Debug.Log($"put object response: {response.StatusCode}");
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
            Debug.LogError($"Message: {ex.Message}");
        }
    }

    public void PutObjectProgress(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey, string filePath, Action<object, TransferStatus> callback = null, int dateTime = -1)
    {
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();
        // 上传文件
        try
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
                FilePath = filePath,
            };
            // 以传输字节数为基准反馈上传进度
            request.ProgressType = ProgressTypeEnum.ByBytes;
            // 每上传1MB数据反馈上传进度
            request.ProgressInterval = 1024 * 1024;

            // 注册上传进度回调函数
            request.UploadProgress += delegate (object sender, TransferStatus status) {

                if (callback != null)
                {
                    callback(sender, status);
                }
                // 获取上传平均速率
                Debug.Log($"AverageSpeed: {status.AverageSpeed / 1024} KB/S");
                // 获取上传进度百分比
                Debug.Log($"TransferPercentage: {status.TransferPercentage}");
            };
            PutObjectResponse response = client.PutObject(request);
            Debug.Log($"put object response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void CreateDirectory(ClientOBS clientOBS, string bucketName, string dir)
    {
        // 创建ObsClient实例
        ObsClient client = clientOBS.GetClientOBS();
        // 创建文件夹
        try
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = bucketName,
                ObjectKey = dir,
            };
            PutObjectResponse response = client.PutObject(request);
            Debug.Log($"put object response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }
}