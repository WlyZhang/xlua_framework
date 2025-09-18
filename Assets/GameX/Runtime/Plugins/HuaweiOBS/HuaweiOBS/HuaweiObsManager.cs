using Newtonsoft.Json;
using OBS.Model;
using System.IO;
using UnityEngine;

public class HuaweiObsManager
{
    /// <summary>
    /// 配置组件
    /// </summary>
    public static ConfigOBS ConfigOBS;

    /// <summary>
    /// HuaweiOBS 客户端组件
    /// </summary>
    public static ClientOBS ClientOBS;

    /// <summary>
    /// 上传组件
    /// </summary>
    public static UploadOBS UploadOBS;

    /// <summary>
    /// 下载组件
    /// </summary>
    public static DownloadOBS DownloadOBS;

    /// <summary>
    /// 对象桶管理组件
    /// </summary>
    public static BucketOBS BucketOBS;

    /// <summary>
    /// 对象文件管理组件
    /// </summary>
    public static FileOBS FileOBS;

    public static void Create()
    {
        //初始化配置文件
        InitConfig();

        //初始化客户端
        InitClient();
    }

    /// <summary>
    /// 初始化配置文件
    /// </summary>
    public static void InitConfig()
    {
#if UNITY_EDITOR
        string configName = "config";
        string path = $"{Application.streamingAssetsPath}/{configName}.json";

        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            ConfigOBS = JsonConvert.DeserializeObject<ConfigOBS>(json);
        }
#else
        //这里可以动态获取 HuaweiObsConfig 配置信息
#endif

    }

    /// <summary>
    /// 初始化客户端
    /// </summary>
    public static void InitClient()
    {
        //初始化客户端
        ClientOBS = new ClientOBS();
        ClientOBS.Init(ConfigOBS);

        //初始化上传组件
        UploadOBS = new UploadOBS();

        //初始化下载组件
        DownloadOBS = new DownloadOBS();

        //初始化对象桶
        BucketOBS = new BucketOBS();

        //初始化对象文件
        FileOBS = new FileOBS();
    }
}