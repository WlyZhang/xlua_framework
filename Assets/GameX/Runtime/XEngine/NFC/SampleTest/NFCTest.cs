using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFCTest : MonoBehaviour
{
    // 在需要NFC功能的场景中
    void Start()
    {
        NFCManager.Instance.InitNFC();
        NFCManager.Instance.OnTagScanned += HandleTag;
    }

    void HandleTag(string tagId)
    {
        Debug.Log($"主程序收到标签: {tagId}");
        // 执行游戏逻辑（如解锁道具/加载场景）
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
