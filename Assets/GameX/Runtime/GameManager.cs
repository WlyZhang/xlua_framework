using CXGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using XLua;


public enum BuildType : byte
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    Debug,

    /// <summary>
    /// ����ģʽ
    /// </summary>
    Release,
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// ����ģʽ
    /// </summary>
    public BuildType BuildType { get; set; }

    /// <summary>
    /// ����ģʽ
    /// </summary>
    public LoaderType LoaderType { get; set; }

    /// <summary>
    /// Lua��ڽű�
    /// </summary>
    public string mainScript = "app";

    /// <summary>
    /// �ű���׺
    /// </summary>
    public string suffix = ".lua";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitComponent();
    }

    private async void InitComponent()
    {
        XConfig config = new XConfig();
        config.AppId = "AppId";
        config.ModuleName = "Game";
        config.Token = "token";
        config.DevelopLicense = "123456";

        XEngine engine = new XEngine(config);
        await engine.Init();
        engine.SetupLoader(LoaderType);

        Debug.Log("Loader OK.");
    }
}