using System.IO;
using UnityEngine;


public class DirectoryType
{
    /// <summary>
    /// 发布平台
    /// </summary>
    public enum BuildPlatform
    {
        None,
        Windows,
        Linux,
        MacOS,
        iOS,
        Android,
        WebGL,
    }

    /// <summary>
    /// 发布平台属性
    /// </summary>
    public static string Platform
    {
        get
        {
            string plat = string.Empty;
            if (Application.platform == RuntimePlatform.WindowsPlayer)
                plat = BuildPlatform.Windows.ToString();
            else if (Application.platform == RuntimePlatform.LinuxPlayer)
                plat = BuildPlatform.Linux.ToString();
            else if (Application.platform == RuntimePlatform.OSXPlayer)
                plat = BuildPlatform.MacOS.ToString();
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                plat = BuildPlatform.iOS.ToString();
            else if (Application.platform == RuntimePlatform.Android)
                plat = BuildPlatform.Android.ToString();
            else if (Application.platform == RuntimePlatform.WebGLPlayer)
                plat = BuildPlatform.WebGL.ToString();
            else
            {
                plat = BuildPlatform.None.ToString();
            }

            return plat;
        }
        set
        {
            Platform = value;
        }
    }

    /// <summary>
    /// 资源主目录
    /// </summary>
    public static string RootPath = Application.dataPath;

    /// <summary>
    /// Editor资源加载目录
    /// </summary>
    public const string AssetsPath = "Assets";

    /// <summary>
    /// 逻辑代码开发目录
    /// </summary>
    public const string DeveloperPath = "GameDev";

    /// <summary>
    /// 资源下载目录
    /// </summary>
    public const string DownloadPath = "Download";


    /// <summary>
    /// 游戏模块名称
    /// </summary>
    public const string ModuleName = "Game";


    // ART Type Module Path
    public const string SourceMatePath = "ART";


    /// <summary>
    /// 资源预设包目录
    /// </summary>
    public const string Package = "Package";


    /// <summary>
    /// 代码脚本目录
    /// </summary>
    public const string Scripts = "Scripts";


    /// <summary> 
    /// 项目加载路径
    /// </summary>
    public static string StreamingAssetsPath = Path.Combine(Application.streamingAssetsPath, ModuleName, Platform, "AssetBundles");

    /// <summary>
    /// 缓存加载路径
    /// </summary>
    public static string CacheAssetsPath = Path.Combine(Application.persistentDataPath, ModuleName, Platform, "AssetBundles");


    /// <summary>
    /// Editor资源加载路径
    /// </summary>
    public static string EditorAssetsPath = Path.Combine(AssetsPath, DownloadPath, Package);


    /// <summary>
    /// C#类文件路径
    /// </summary>
    public static string BuildLibraryPath = Path.Combine(RootPath, DeveloperPath, Scripts);


    /// <summary>
    /// 开发者模式Lua代码路径
    /// </summary>
    public static string LuaDevPath = Path.Combine($"{RootPath}/../{LuaProjectPath}", ModuleName, "src");

    /// <summary>
    /// 内部Lua代码模式
    /// </summary>
    public static string LuaResPath = Path.Combine(RootPath, DownloadPath, Package, ArtType.Lua.ToString(), "Resources");

    /// <summary>
    /// Lua代码打包路径名称
    /// </summary>
    public static string LuaBundleName = "src/lua";

    /// <summary>
    /// Lua项目代码路径
    /// </summary>
    public static string LuaProjectPath = "LuaProject";

    /// <summary>
    /// Typescript -> Puerts 项目代码路径
    /// </summary>
    public static string TSProjectPath = "TSProject";

    /// <summary>
    /// 美术资源类型
    /// </summary>
    public enum ArtType
    {
        None,
        UGUI,
        Model,
        Scene,
        Texture,
        Sprite,
        Effect,
        Audio,
        Video,
        Config,
        Text,
        Shader,
        Material,
        Lua,
    }
}
