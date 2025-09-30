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
    /// ��������Ȩ����
    /// </summary>
    private XConfig config;

    /// <summary>
    /// ����ģʽ
    /// </summary>
    public LoaderType LoaderType = LoaderType.DevelopLoader;

    /// <summary>
    /// �汾����ģʽ
    /// </summary>
    public BuildType BuildType = BuildType.Debug;

    #region Lua Loader API

    /// <summary>
    /// Lua�������ӿ�ʵ��
    /// </summary>
    public ILuaLoader Loader;

    /// <summary>
    /// Lua�����ʵ��
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
    /// ��ʼ������
    /// </summary>
    /// <returns></returns>
    public override async UniTask<bool> Init()
    {
        bool isInit = false;

        while (this.config == null)
        {
            if(!isInit)
            {
                Debug.Log("<color=red>��������XEngine����Ȩ��Ϣ</color>");
                isInit = true;
            }

            await UniTask.Yield();
        }

        // ��ʼ����Դ���ع�����
        AssetLoadManager.Create();

        // ��ʼ�� HTTP ���������
        HttpCenter.Create();

        // ��ʼ��Lua�����
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
    /// ���ѡ��Lua������
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