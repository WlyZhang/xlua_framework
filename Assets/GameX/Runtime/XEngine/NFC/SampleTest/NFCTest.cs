using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFCTest : MonoBehaviour
{
    // ����ҪNFC���ܵĳ�����
    void Start()
    {
        NFCManager.Instance.InitNFC();
        NFCManager.Instance.OnTagScanned += HandleTag;
    }

    void HandleTag(string tagId)
    {
        Debug.Log($"�������յ���ǩ: {tagId}");
        // ִ����Ϸ�߼������������/���س�����
    }

    void OnEnable()
    {
        NFCManager.Instance.StartListening();
    }

    void OnDisable()
    {
        NFCManager.Instance.StopListening();
    }



}
