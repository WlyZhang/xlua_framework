using OBS.Model;
using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class TestOBS : MonoBehaviour
{
    private ClientOBS client;

    private ConfigOBS config;

    private Canvas canvas;

    private void Awake()
    {
        HuaweiObsManager.Create();

        config = HuaweiObsManager.ConfigOBS;

        client = HuaweiObsManager.ClientOBS;
    }

    private void Start()
    {
        //root资源文件在obs桶根目录
        StartCoroutine(LoadAsset("root"));
    }


    IEnumerator LoadAsset(string objectKey)
    {
        string path = HuaweiObsManager.FileOBS.GetObject(client, config, objectKey);
        
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        yield return ab;

        GameObject go = ab.LoadAsset<GameObject>(objectKey);
        Instantiate(go, Vector3.zero, Quaternion.identity);

        Debug.Log("HuaweiOBS AssetBundle资源.", ab.name, "加载完成.");
    }
}