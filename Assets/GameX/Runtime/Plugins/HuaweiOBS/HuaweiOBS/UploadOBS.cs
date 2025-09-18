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
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �ϴ���
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
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �ϴ���
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
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �ϴ��ļ�
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
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �ϴ��ļ�
        try
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = configOBS.BucketName,  //������Ŀ��Ͱ��
                ObjectKey = objectKey,   //�����������(�������Ƕ�����Ͱ�е�����·������folder/test.txt��·���в�����Ͱ��)
                FilePath = localfile,   //���ϴ��ı����ļ�·������Ҫָ����������ļ���
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
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �ϴ��ļ�
        try
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
                FilePath = filePath,
            };
            // �Դ����ֽ���Ϊ��׼�����ϴ�����
            request.ProgressType = ProgressTypeEnum.ByBytes;
            // ÿ�ϴ�1MB���ݷ����ϴ�����
            request.ProgressInterval = 1024 * 1024;

            // ע���ϴ����Ȼص�����
            request.UploadProgress += delegate (object sender, TransferStatus status) {

                if (callback != null)
                {
                    callback(sender, status);
                }
                // ��ȡ�ϴ�ƽ������
                Debug.Log($"AverageSpeed: {status.AverageSpeed / 1024} KB/S");
                // ��ȡ�ϴ����Ȱٷֱ�
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
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �����ļ���
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