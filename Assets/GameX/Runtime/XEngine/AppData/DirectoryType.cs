using System.IO;
using UnityEngine;


public class DirectoryType
{
    /// <summary>
    /// ����ƽ̨
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
    /// ����ƽ̨����
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
    /// ��Դ��Ŀ¼
    /// </summary>
    public static string RootPath = Application.dataPath;

    /// <summary>
    /// Editor��Դ����Ŀ¼
    /// </summary>
    public const string AssetsPath = "Assets";

    /// <summary>
    /// �߼����뿪��Ŀ¼
    /// </summary>
    public const string DeveloperPath = "GameDev";

    /// <summary>
    /// ��Դ����Ŀ¼
    /// </summary>
    public const string DownloadPath = "Download";


    /// <summary>
    /// ��Ϸģ������
    /// </summary>
    public const string ModuleName = "Game";


    // ART Type Module Path
    public const string SourceMatePath = "ART";


    /// <summary>
    /// ��ԴԤ���Ŀ¼
    /// </summary>
    public const string Package = "Package";


    /// <summary>
    /// ����ű�Ŀ¼
    /// </summary>
    public const string Scripts = "Scripts";


    /// <summary> 
    /// ��Ŀ����·��
    /// </summary>
    public static string StreamingAssetsPath = Path.Combine(Application.streamingAssetsPath, ModuleName, Platform, "AssetBundles");

    /// <summary>
    /// �������·��
    /// </summary>
    public static string CacheAssetsPath = Path.Combine(Application.persistentDataPath, ModuleName, Platform, "AssetBundles");


    /// <summary>
    /// Editor��Դ����·��
    /// </summary>
    public static string EditorAssetsPath = Path.Combine(AssetsPath, DownloadPath, Package);


    /// <summary>
    /// C#���ļ�·��
    /// </summary>
    public static string BuildLibraryPath = Path.Combine(RootPath, DeveloperPath, Scripts);


    /// <summary>
    /// ������ģʽLua����·��
    /// </summary>
    public static string LuaDevPath = Path.Combine($"{RootPath}/../{LuaProjectPath}", ModuleName, "src");

    /// <summary>
    /// �ڲ�Lua����ģʽ
    /// </summary>
    public static string LuaResPath = Path.Combine(RootPath, DownloadPath, Package, ArtType.Lua.ToString(), "Resources");

    /// <summary>
    /// Lua������·������
    /// </summary>
    public static string LuaBundleName = "src/lua";

    /// <summary>
    /// Lua��Ŀ����·��
    /// </summary>
    public static string LuaProjectPath = "LuaProject";

    /// <summary>
    /// Typescript -> Puerts ��Ŀ����·��
    /// </summary>
    public static string TSProjectPath = "TSProject";

    /// <summary>
    /// ������Դ����
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
