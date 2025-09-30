using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;

public class XEngine : BaseManager
{
    public static XEngine Instance { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public override bool IsLoad { get; set; }


    /// <summary>
    /// 开发者授权配置
    /// </summary>
    private XConfig config;

    /// <summary>
    /// 加载模式
    /// </summary>
    public LoaderType LoaderType = LoaderType.DevelopLoader;

    /// <summary>
    /// 版本发布模式
    /// </summary>
    public BuildType BuildType = BuildType.Debug;

    #region Lua Loader API

    /// <summary>
    /// Lua加载器接口实例
    /// </summary>
    public ILuaLoader Loader;

    /// <summary>
    /// Lua虚拟机实例
    /// </summary>
    public Lua Lua = null;

    #endregion


    public XEngine(XConfig config)
    {
        if (config == null)
            return;

        Instance = this;
        this.config = config;
    }

    /// <summary>
    /// 初始化引擎
    /// </summary>
    /// <returns></returns>
    public override async UniTask<bool> Init()
    {
        bool isInit = false;

        while (this.config == null)
        {
            if(!isInit)
            {
                Debug.Log("<color=red>请先配置XEngine的授权信息</color>");
                isInit = true;
            }

            await UniTask.Yield();
        }

        // 初始化资源加载管理器
        AssetLoadManager.Create();

        // 初始化 HTTP 请求管理器
        HttpCenter.Create();

        // 初始化Lua虚拟机
        Lua = Lua.Create();

        IsLoad = true;

        return IsLoad;
    }

    public override void Update(float time)
    {
        
    }

    public override void Destroy()
    {

    }


    /// <summary>
    /// 检测选择Lua加载器
    /// </summary>
    public void SetupLoader(LoaderType LoaderType)
    {
        switch (LoaderType)
        {
            case LoaderType.DevelopLoader:
                ILuaLoader loadDev = new DevelopLoader();
                loadDev.LoadScript(LoaderType);
                break;
            case LoaderType.ResourcesLoader:
                ILuaLoader loadRes = new ResourcesLoader();
                loadRes.LoadScript(LoaderType);
                break;
            case LoaderType.HotUpdateLoader:
                ILuaLoader loadHot = new HotUpdateLoader();
                loadHot.LoadScript(LoaderType);
                break;
        }
    }
}