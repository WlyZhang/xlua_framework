using OBS;
using OBS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketOBS
{
    public void CreateBucket(ClientOBS client, string bucketName)
    {
        CreateBucketRequest request = new CreateBucketRequest();
        request.BucketName = bucketName;
        client.GetClientOBS().CreateBucket(request);
    }

    public void ListBuckets(ClientOBS clientOBS)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �о�Ͱ
        try
        {
            ListBucketsRequest request = new ListBucketsRequest();
            ListBucketsResponse response = client.ListBuckets(request);
            request.IsQueryLocation = true;
            foreach (ObsBucket bucket in response.Buckets)
            {
                Debug.Log(bucket.BucketName, bucket.CreationDate, bucket.Location);
            }
        }
        catch (ObsException ex)
        {
            Debug.LogError(ex.ErrorCode, ex.ErrorMessage);
        }
    }

    public void DeleteBucket(ClientOBS clientOBS, string bucketname)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        //ɾ��Ͱ
        try
        {
            DeleteBucketRequest request = new DeleteBucketRequest
            {
                BucketName = bucketname,
            };
            DeleteBucketResponse response = client.DeleteBucket(request);
            Debug.Log($"Delete bucket response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void HasBucket(ClientOBS clientOBS, string bucketname)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        // �ж�Ͱ�Ƿ����
        try
        {
            HeadBucketRequest request = new HeadBucketRequest
            {
                BucketName = bucketname,
            };
            bool exists = client.HeadBucket(request);
            Debug.Log($"Bucket exists: {exists}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"StatusCode: {ex.StatusCode}");
        }
    }

    public void SetBucketAcl(ClientOBS clientOBS, string bucketname)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        //����Ͱ����Ȩ��
        try
        {
            //Ͱ����������Ϣ
            Owner owner = new Owner
            {
                Id = "ownerid",//�����ߵ�DomainId
            };
            AccessControlList acl = new AccessControlList();
            acl.Owner = owner;

            Grant item = new Grant()
            {
                Grantee = new GroupGrantee()
                {
                    GroupGranteeType = GroupGranteeEnum.AllUsers
                },
                Permission = PermissionEnum.FullControl
            };

            IList<Grant> grants = new List<Grant>();
            grants.Add(item);
            acl.Grants = grants;

            SetBucketAclRequest request = new SetBucketAclRequest()
            {
                BucketName = bucketname,
                AccessControlList = acl
            };

            SetBucketAclResponse response = client.SetBucketAcl(request);
            Debug.Log($"Set bucket acl response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.LogError($"ErrorCode: {ex.ErrorCode}");
            Debug.LogError($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void SetBucketPolicy(ClientOBS clientOBS, string bucketname, string policy)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        try
        {
            SetBucketPolicyRequest request = new SetBucketPolicyRequest
            {
                BucketName = bucketname,
                Policy = policy,
            };
            SetBucketPolicyResponse response = client.SetBucketPolicy(request);
            Debug.Log($"Set bucket policy response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void GetBucketPolicy(ClientOBS clientOBS, string bucketname)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        try
        {
            GetBucketPolicyRequest request = new GetBucketPolicyRequest
            {
                BucketName = bucketname,
            };
            GetBucketPolicyResponse response = client.GetBucketPolicy(request);
            Debug.Log($"Get bucket policy response: {response.StatusCode}");
            Debug.Log($"Get bucket policy response: {response.Policy}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void DeleteBucketPolicy(ClientOBS clientOBS, string bucketname)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        try
        {
            DeleteBucketPolicyRequest request = new DeleteBucketPolicyRequest
            {
                BucketName = bucketname,
            };
            DeleteBucketPolicyResponse response = client.DeleteBucketPolicy(request);
            Debug.Log($"Delete bucket policy response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void GetBucketLocation(ClientOBS clientOBS, string bucketname)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        try
        {
            GetBucketLocationRequest request = new GetBucketLocationRequest
            {
                BucketName = bucketname,
            };
            GetBucketLocationResponse response = client.GetBucketLocation(request);
            Debug.Log($"Get bucket location response: {response.StatusCode}");
            Debug.Log($"Location: {response.Location}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void GetBucketStorageInfo(ClientOBS clientOBS, string bucketname)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        try
        {
            GetBucketStorageInfoRequest request = new GetBucketStorageInfoRequest
            {
                BucketName = "bucketname",
            };
            GetBucketStorageInfoResponse response = client.GetBucketStorageInfo(request);
            Debug.Log($"Get bucket storageinfo response: {response.StatusCode}");
            Debug.Log($"ObjectNumber: {response.ObjectNumber}");
            Debug.Log($"Size: {response.Size}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void SetBucketQuota(ClientOBS clientOBS, string bucketname, long storageQuota = 0L)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        try
        {
            SetBucketQuotaRequest request = new SetBucketQuotaRequest
            {
                BucketName = bucketname,
                StorageQuota = storageQuota,
            };
            SetBucketQuotaResponse response = client.SetBucketQuota(request);
            Debug.Log($"Set bucket quota response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }

    public void SetBucketStoragePolicy(ClientOBS clientOBS, string bucketname, StorageClassEnum storageClass = StorageClassEnum.Cold)
    {
        // ����ObsClientʵ��
        ObsClient client = clientOBS.GetClientOBS();
        try
        {
            SetBucketStoragePolicyRequest request = new SetBucketStoragePolicyRequest
            {
                BucketName = bucketname,
                StorageClass = storageClass,
            };
            SetBucketStoragePolicyResponse response = client.SetBucketStoragePolicy(request);
            Debug.Log($"Set bucket storage policy response: {response.StatusCode}");
        }
        catch (ObsException ex)
        {
            Debug.Log($"ErrorCode: {ex.ErrorCode}");
            Debug.Log($"ErrorMessage: {ex.ErrorMessage}");
        }
    }
}
