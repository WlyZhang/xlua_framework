using OBS.Model;
using OBS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileOBS
{
    /// <summary>
    /// �оٶ���
    /// </summary>
    /// <param name="obs"></param>
    /// <returns></returns>
    public List<ObsObject> ListObjects(ClientOBS clientOBS, ConfigOBS configOBS)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();

        // �оٶ���
        ListObjectsRequest request = new ListObjectsRequest();
        request.BucketName = configOBS.BucketName;
        ListObjectsResponse response = client.ListObjects(request);

        List<ObsObject> objects = new List<ObsObject>();
        foreach (ObsObject Object in response.ObsObjects)
        {
            objects.Add(Object);
            Debug.Log($"ObjectKey={Object.ObjectKey}, Size={Object.Size}");
        }

        return objects;
    }


    /// <summary>
    /// ɾ������
    /// </summary>
    /// <param name="obs"></param>
    public void DeleteObject(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();

        //ɾ������
        try
        {
            DeleteObjectRequest request = new DeleteObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
            };
            client.DeleteObject(request);
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }


    /// <summary>
    /// �ж϶����Ƿ����
    /// </summary>
    /// <param name="obs"></param>
    /// <returns></returns>
    public bool HeadObject(ClientOBS clientOBS, ConfigOBS configOBS, string objectKey)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();

        //�ж϶����Ƿ����
        bool response = false;

        try
        {
            HeadObjectRequest request = new HeadObjectRequest()
            {
                BucketName = configOBS.BucketName,
                ObjectKey = objectKey,
            };
            response = client.HeadObject(request);
            Debug.Log($"Head object response: {response}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"Exception errorcode: {ex.ErrorCode}, when head object.");
            Debug.Log($"Exception errormessage: {ex.ErrorMessage}");
        }

        return response;
    }

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
}