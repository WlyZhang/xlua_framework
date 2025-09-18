using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using BuildPlatform = DirectoryType.BuildPlatform;

public class BuildAssets
{
    /// <summary>
    /// 脚本列表
    /// </summary>
    private static Dictionary<string, string> scriptList = new Dictionary<string, string>();


    //================================= 以上是属性 ================================================
    [MenuItem("GameX/Builder/Platform/Builder for WebGL")]
    private static void BuildToAssetBundleFromWebGL()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();

        //编译打包AssetBundle资源
        BuildAssetBundle(BuildPlatform.WebGL, BuildTarget.WebGL);
    }

    [MenuItem("GameX/Builder/Platform/Builder for Linux")]
    private static void BuildToAssetBundleFromLinux()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();

        //编译打包AssetBundle资源
        BuildAssetBundle(BuildPlatform.Linux, BuildTarget.StandaloneLinux64);

    }

    [MenuItem("GameX/Builder/Platform/Builder for Windows")]
    private static void BuildToAssetBundleFromWindows()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();

        //编译打包AssetBundle资源
        BuildAssetBundle(BuildPlatform.Windows, BuildTarget.StandaloneWindows);

    }

    [MenuItem("GameX/Builder/Platform/Builder for MacOS")]
    private static void BuildToAssetBundleFromMacOS()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();

        //编译打包AssetBundle资源
        BuildAssetBundle(BuildPlatform.MacOS, BuildTarget.StandaloneOSX);

    }

    [MenuItem("GameX/Builder/Platform/Builder for iOS")]
    private static void BuildToAssetBundleFromIOS()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();

        //编译打包AssetBundle资源
        BuildAssetBundle(BuildPlatform.iOS, BuildTarget.iOS);

    }

    [MenuItem("GameX/Builder/Platform/Builder for Android")]
    private static void BuildToAssetBundleFromAndroid()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();

        //编译打包AssetBundle资源
        BuildAssetBundle(BuildPlatform.Android, BuildTarget.Android);

    }


    [MenuItem("GameX/Builder/Builder Platform All")]
    private static void BuildToAssetBundle()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();

        //编译打包AssetBundle资源
        BuildAssetBundle(BuildPlatform.WebGL, BuildTarget.WebGL);
        BuildAssetBundle(BuildPlatform.Linux, BuildTarget.StandaloneLinux64);
        BuildAssetBundle(BuildPlatform.Windows, BuildTarget.StandaloneWindows);
        BuildAssetBundle(BuildPlatform.MacOS, BuildTarget.StandaloneOSX);
        BuildAssetBundle(BuildPlatform.iOS, BuildTarget.iOS);
        BuildAssetBundle(BuildPlatform.Android, BuildTarget.Android);

    }

    /// <summary>
    /// 重新标记代码脚本Bundles名称
    /// </summary>
    private static void RenameScriptToBundles()
    {
        if (!Directory.Exists(DirectoryType.LuaDevPath))
            return;

        List<Object> list = new List<Object>();
        foreach (string key in scriptList.Keys)
        {
            if (key.Contains("meta"))
                continue;

            TextAsset asset  = Resources.Load<TextAsset>($"{key}.lua");
            list.Add(asset);
        }

        ReNameBundle($"{DirectoryType.LuaBundleName}", list);
    }

    /// <summary>
    /// 创建内部模式代码脚本
    /// </summary>
    private static void CreateResourcesScripts()
    {
        foreach (string key in scriptList.Keys)
        {
            string name = key.Replace("\\", string.Empty);
            string fileName = $"{name}.lua.txt";
            string value = scriptList[name];

            Debug.Log(fileName);

            CreateTextFile(fileName, value);
        }

        Debug.Log("<color=green>Resources 内部模式代码已生成</color>");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }


    /// <summary>
    /// 递归循环遍历目录
    /// </summary>
    /// <param name="path"></param>
    private static void GetDirFiles(string path)
    {
        if(string.IsNullOrEmpty(path)) return;

        string[] files = Directory.GetFiles(path);

        for(int i = 0;i<files.Length;i++)
        {
            if (files[i].Contains("meta"))
                continue;
            int count = files[i].LastIndexOf("\\");
            string str = files[i].Substring(0, count);
            string name = files[i].Replace(str, string.Empty);
            string key = name.Split('.')[0].Replace("\\", string.Empty);


            string value = File.ReadAllText(files[i]);

            if (scriptList.ContainsKey(key))
                continue;
            scriptList.Add(key, value);
        }


        string[] dirs = Directory.GetDirectories(path);

        for (int i = 0; i < dirs.Length; i++)
        {
            GetDirFiles(dirs[i]);
        }
    }

    [MenuItem("GameX/Builder/Clean Bundle Builder")]
    public static void DeleteCache()
    {
        AppData.BuildPlatform = BuildPlatform.None;

        ClearCache($"{DirectoryType.LuaResPath}");
        ClearCache($"{Application.streamingAssetsPath}");

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 清理缓存
    /// </summary>
    private static void ClearCache(string cachePath)
    {
        if (Directory.Exists(cachePath))
        {
            Directory.Delete(cachePath, true);
            Debug.Log($"<color=yellow>{cachePath} 缓存已清理</color>");
        }
        else
        {
            Debug.Log($"{cachePath} 无缓存目录");
        }
    }

    /// <summary>
    /// 复制文件夹及文件
    /// </summary>
    /// <param name="sourceFolder">原文件路径</param>
    /// <param name="destFolder">目标文件路径</param>
    /// <returns></returns>
    public static int CopyFolder(string sourceFolder, string destFolder)
    {
        try
        {
            //如果目标路径不存在,则创建目标路径
            if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                //得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                System.IO.File.Copy(file, dest);//复制文件
            }
            //得到原文件根目录下的所有文件夹
            string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = System.IO.Path.GetFileName(folder);
                string dest = System.IO.Path.Combine(destFolder, name);
                CopyFolder(folder, dest);//构建目标路径,递归复制文件
            }
            return 1;
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 计算md5
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    private static string CreateMD5(byte[] buffer)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] md5Bytes = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sb.Append(md5Bytes[i].ToString("x2"));//X2时，生成字母大写MD5
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// 创建版本信息
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="info"></param>
    private static void CreateTextFile(string fileName, string info)
    {
        if (!Directory.Exists(DirectoryType.LuaResPath))
        {
            Directory.CreateDirectory(DirectoryType.LuaResPath);
        }

        if (File.Exists($"{DirectoryType.LuaResPath}/{fileName}"))
        {
            File.Delete($"{DirectoryType.LuaResPath}/{fileName}");
        }

        File.WriteAllText($"{DirectoryType.LuaResPath}/{fileName}", info);
    }

    /// <summary>
    /// 【Bundle】多个资源打一个包
    /// </summary>
    /// <param name="dir"></param>
    private static void ReNameBundle(string dir, List<Object> list)
    {
        Object[] objs = list.ToArray();

        int totalLength = objs.Length;
        int index = 0;
        float progress = 0f;

        foreach (var v in objs)
        {
            index++;
            progress = (float)index / (float)totalLength;
            EditorUtility.DisplayProgressBar("资源命名", "请稍等..." + index.ToString() + "/" + totalLength.ToString(), progress);

            string path = AssetDatabase.GetAssetPath(v);
            AssetImporter imp = AssetImporter.GetAtPath(path);

            imp.assetBundleName = $"{dir.Trim()}";

            imp.SaveAndReimport();
        }

        AssetDatabase.Refresh();

        UnityEditor.EditorUtility.ClearProgressBar();
    }

    /// <summary>
    /// 编译打包AssetBundle资源
    /// </summary>
    public static void BuildAssetBundle(BuildPlatform buildPlatform, BuildTarget buildTarget)
    {
        string StreamingAssetsPath = Path.Combine(Application.streamingAssetsPath, DirectoryType.ModuleName, buildPlatform.ToString(), "AssetBundles");

        if(!Directory.Exists(StreamingAssetsPath))
            Directory.CreateDirectory (StreamingAssetsPath);

        BuildPipeline.BuildAssetBundles(StreamingAssetsPath, BuildAssetBundleOptions.None, buildTarget);

        Debug.Log($"编译{buildTarget}终端平台运行时,资源AssetBundle打包完成...");

        AssetDatabase.Refresh();
    }
}