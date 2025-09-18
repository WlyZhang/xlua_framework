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
    /// �ű��б�
    /// </summary>
    private static Dictionary<string, string> scriptList = new Dictionary<string, string>();


    //================================= ���������� ================================================
    [MenuItem("GameX/Builder/Platform/Builder for WebGL")]
    private static void BuildToAssetBundleFromWebGL()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //�ݹ��ȡ�����ļ�
        GetDirFiles(scriptPath);

        //�����ڲ�ģʽ����ű�
        CreateResourcesScripts();

        //���±�Ǵ���ű�Bundles����
        RenameScriptToBundles();

        //������AssetBundle��Դ
        BuildAssetBundle(BuildPlatform.WebGL, BuildTarget.WebGL);
    }

    [MenuItem("GameX/Builder/Platform/Builder for Linux")]
    private static void BuildToAssetBundleFromLinux()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //�ݹ��ȡ�����ļ�
        GetDirFiles(scriptPath);

        //�����ڲ�ģʽ����ű�
        CreateResourcesScripts();

        //���±�Ǵ���ű�Bundles����
        RenameScriptToBundles();

        //������AssetBundle��Դ
        BuildAssetBundle(BuildPlatform.Linux, BuildTarget.StandaloneLinux64);

    }

    [MenuItem("GameX/Builder/Platform/Builder for Windows")]
    private static void BuildToAssetBundleFromWindows()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //�ݹ��ȡ�����ļ�
        GetDirFiles(scriptPath);

        //�����ڲ�ģʽ����ű�
        CreateResourcesScripts();

        //���±�Ǵ���ű�Bundles����
        RenameScriptToBundles();

        //������AssetBundle��Դ
        BuildAssetBundle(BuildPlatform.Windows, BuildTarget.StandaloneWindows);

    }

    [MenuItem("GameX/Builder/Platform/Builder for MacOS")]
    private static void BuildToAssetBundleFromMacOS()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //�ݹ��ȡ�����ļ�
        GetDirFiles(scriptPath);

        //�����ڲ�ģʽ����ű�
        CreateResourcesScripts();

        //���±�Ǵ���ű�Bundles����
        RenameScriptToBundles();

        //������AssetBundle��Դ
        BuildAssetBundle(BuildPlatform.MacOS, BuildTarget.StandaloneOSX);

    }

    [MenuItem("GameX/Builder/Platform/Builder for iOS")]
    private static void BuildToAssetBundleFromIOS()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //�ݹ��ȡ�����ļ�
        GetDirFiles(scriptPath);

        //�����ڲ�ģʽ����ű�
        CreateResourcesScripts();

        //���±�Ǵ���ű�Bundles����
        RenameScriptToBundles();

        //������AssetBundle��Դ
        BuildAssetBundle(BuildPlatform.iOS, BuildTarget.iOS);

    }

    [MenuItem("GameX/Builder/Platform/Builder for Android")]
    private static void BuildToAssetBundleFromAndroid()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //�ݹ��ȡ�����ļ�
        GetDirFiles(scriptPath);

        //�����ڲ�ģʽ����ű�
        CreateResourcesScripts();

        //���±�Ǵ���ű�Bundles����
        RenameScriptToBundles();

        //������AssetBundle��Դ
        BuildAssetBundle(BuildPlatform.Android, BuildTarget.Android);

    }


    [MenuItem("GameX/Builder/Builder Platform All")]
    private static void BuildToAssetBundle()
    {
        string scriptPath = $"{DirectoryType.LuaDevPath}";

        //�ݹ��ȡ�����ļ�
        GetDirFiles(scriptPath);

        //�����ڲ�ģʽ����ű�
        CreateResourcesScripts();

        //���±�Ǵ���ű�Bundles����
        RenameScriptToBundles();

        //������AssetBundle��Դ
        BuildAssetBundle(BuildPlatform.WebGL, BuildTarget.WebGL);
        BuildAssetBundle(BuildPlatform.Linux, BuildTarget.StandaloneLinux64);
        BuildAssetBundle(BuildPlatform.Windows, BuildTarget.StandaloneWindows);
        BuildAssetBundle(BuildPlatform.MacOS, BuildTarget.StandaloneOSX);
        BuildAssetBundle(BuildPlatform.iOS, BuildTarget.iOS);
        BuildAssetBundle(BuildPlatform.Android, BuildTarget.Android);

    }

    /// <summary>
    /// ���±�Ǵ���ű�Bundles����
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
    /// �����ڲ�ģʽ����ű�
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

        Debug.Log("<color=green>Resources �ڲ�ģʽ����������</color>");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }


    /// <summary>
    /// �ݹ�ѭ������Ŀ¼
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
    /// ������
    /// </summary>
    private static void ClearCache(string cachePath)
    {
        if (Directory.Exists(cachePath))
        {
            Directory.Delete(cachePath, true);
            Debug.Log($"<color=yellow>{cachePath} ����������</color>");
        }
        else
        {
            Debug.Log($"{cachePath} �޻���Ŀ¼");
        }
    }

    /// <summary>
    /// �����ļ��м��ļ�
    /// </summary>
    /// <param name="sourceFolder">ԭ�ļ�·��</param>
    /// <param name="destFolder">Ŀ���ļ�·��</param>
    /// <returns></returns>
    public static int CopyFolder(string sourceFolder, string destFolder)
    {
        try
        {
            //���Ŀ��·��������,�򴴽�Ŀ��·��
            if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                //�õ�ԭ�ļ���Ŀ¼�µ������ļ�
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                System.IO.File.Copy(file, dest);//�����ļ�
            }
            //�õ�ԭ�ļ���Ŀ¼�µ������ļ���
            string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = System.IO.Path.GetFileName(folder);
                string dest = System.IO.Path.Combine(destFolder, name);
                CopyFolder(folder, dest);//����Ŀ��·��,�ݹ鸴���ļ�
            }
            return 1;
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// ����md5
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
                sb.Append(md5Bytes[i].ToString("x2"));//X2ʱ��������ĸ��дMD5
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// �����汾��Ϣ
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
    /// ��Bundle�������Դ��һ����
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
            EditorUtility.DisplayProgressBar("��Դ����", "���Ե�..." + index.ToString() + "/" + totalLength.ToString(), progress);

            string path = AssetDatabase.GetAssetPath(v);
            AssetImporter imp = AssetImporter.GetAtPath(path);

            imp.assetBundleName = $"{dir.Trim()}";

            imp.SaveAndReimport();
        }

        AssetDatabase.Refresh();

        UnityEditor.EditorUtility.ClearProgressBar();
    }

    /// <summary>
    /// ������AssetBundle��Դ
    /// </summary>
    public static void BuildAssetBundle(BuildPlatform buildPlatform, BuildTarget buildTarget)
    {
        string StreamingAssetsPath = Path.Combine(Application.streamingAssetsPath, DirectoryType.ModuleName, buildPlatform.ToString(), "AssetBundles");

        if(!Directory.Exists(StreamingAssetsPath))
            Directory.CreateDirectory (StreamingAssetsPath);

        BuildPipeline.BuildAssetBundles(StreamingAssetsPath, BuildAssetBundleOptions.None, buildTarget);

        Debug.Log($"����{buildTarget}�ն�ƽ̨����ʱ,��ԴAssetBundle������...");

        AssetDatabase.Refresh();
    }
}