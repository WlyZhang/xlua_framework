using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using ArtType = DirectoryType.ArtType;
using BuildPlatform = DirectoryType.BuildPlatform;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AssetLoadManager
{
    public static AssetLoadManager Instance;

    public static string CachePath;

    public static void Create()
    {
        Instance = new AssetLoadManager();
        CachePath = DirectoryType.CacheAssetsPath;
    }

    /// <summary>
    /// 加载UIPanel面板
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<GameObject> LoadPanelAsync(string path, string panelName, bool canUpdate = false)
    {
        GameObject results = null;

        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            results = await LoadObjectAsync(path, panelName, ArtType.UGUI, ".prefab", canUpdate) as GameObject;
        }
        else if (XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, panelName, ArtType.UGUI, canUpdate);
            results = ab.LoadAsset<GameObject>(panelName);
        }

        return results;
    }


    /// <summary>
    /// 加载场景资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="loadSceneMode"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask LoadSceneAsync(string path, string assetName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, bool canUpdate = false)
    {
        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            SceneManager.LoadScene(assetName, loadSceneMode);
        }
        else if (XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Scene, canUpdate);
            SceneManager.LoadScene(assetName, loadSceneMode);
        }
    }


    /// <summary>
    /// 加载模型资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<GameObject> LoadAssetAsync(string path, string assetName, bool canUpdate = false)
    {
        GameObject results = null;

        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            results = await LoadObjectAsync(path, assetName, ArtType.Model, ".prefab", canUpdate) as GameObject;
        }
        else if (XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Model, canUpdate);
            results = ab.LoadAsset<GameObject>(assetName);
        }

        return results;
    }


    /// <summary>
    /// 加载纹理资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<Texture> LoadTextureAsync(string path, string assetName, bool canUpdate = false)
    {
        Texture results = null;

        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            results = await LoadObjectAsync(path, assetName, ArtType.Texture, ".png", canUpdate) as Texture;
        }
        else if (XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Texture, canUpdate);
            results = ab.LoadAsset<Texture>(assetName);
        }

        return results;
    }


    /// <summary>
    /// 加载精灵资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<Sprite> LoadSpriteAsync(string path, string assetName, bool canUpdate = false)
    {
        Sprite results = null;

        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            results = await LoadObjectAsync(path, assetName, ArtType.Sprite, ".sprite", canUpdate) as Sprite;
        }
        else if (XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Sprite, canUpdate);
            results = ab.LoadAsset<Sprite>(assetName);
        }

        return results;
    }


    /// <summary>
    /// 加载音频资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<AudioClip> LoadAudioAsync(string path, string assetName, bool canUpdate = false)
    {
        AudioClip results = null;

        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            results = await LoadObjectAsync(path, assetName, ArtType.Video, ".wav", canUpdate) as AudioClip;
        }
        else if (XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Audio, canUpdate);
            results = ab.LoadAsset<AudioClip>(assetName);
        }

        return results;
    }

    
    /// <summary>
    /// 加载视频资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<VideoClip> LoadVideoAsync(string path, string assetName, bool canUpdate = false)
    {
        VideoClip results = null;

        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            results = await LoadObjectAsync(path, assetName, ArtType.Video, ".mp4", canUpdate) as VideoClip;
        }
        else if (XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Video, canUpdate);
            results = ab.LoadAsset<VideoClip>(assetName);
        }

        return results;
    }


    /// <summary>
    /// 加载文本资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<TextAsset> LoadTextAssetAsync(string path, string assetName, bool canUpdate = false)
    {
        TextAsset results = null;

        if (XEngine.Instance.BuildType == BuildType.Debug)
        {
            results = await LoadObjectAsync(path, assetName, ArtType.Text, ".txt", canUpdate) as TextAsset;
        }
        else if(XEngine.Instance.BuildType == BuildType.Release)
        {
            AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Text, canUpdate);
            results = ab.LoadAsset<TextAsset>(assetName);
        }

        return results;
    }


    /// <summary>
    /// Debug模式下加载资源包
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="artType"></param>
    /// <param name="suffix"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<Object> LoadObjectAsync(string path, string assetName, ArtType artType, string suffix, bool canUpdate = false)
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(assetName))
        {
            Debug.LogError("AssetLoadManager LoadBundleAsync path or assetName is null");
            return null;
        }

        if (!XEngine.Instance.IsLoad)
        {
            await XEngine.Instance.Init();
        }

        string fullPath = Path.Combine(DirectoryType.EditorAssetsPath, artType.ToString(), $"{assetName}{suffix}");

        Object obj = AssetDatabase.LoadAssetAtPath<Object>(fullPath);

        if (obj == null)
        {
            Debug.LogError($"Failed to load asset at path: {path}, assetName: {assetName}, artType: {artType}, suffix: {suffix}");
            return null;
        }

        return obj;
#endif
    }


    /// <summary>
    /// Release模式下加载资源包
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    /// <param name="artType"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<AssetBundle> LoadAssetBundleAsync(string path, string assetName, ArtType artType, bool canUpdate = false)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(assetName))
        {
            Debug.LogError("AssetLoadManager LoadBundleAsync path or assetName is null");
            return null;
        }

        if (!XEngine.Instance.IsLoad)
        {
            await XEngine.Instance.Init();
        }

        AssetBundle assetBundle = null;

        if (canUpdate)
        {
            byte[] bytes = await LoadByteAssetAsync(path);
            string savePath = Path.Combine(CachePath, artType.ToString(), assetName);
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            CreateFile(savePath, assetName, bytes);

            assetBundle = AssetBundle.LoadFromMemory(bytes);
        }
        else
        {
            assetBundle = AssetBundle.LoadFromFile(path);
        }

        if (assetBundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle from {path}");
            return null;
        }

        return assetBundle;
    }


    /// <summary>
    /// 加载AssetBundle资源包
    /// </summary>
    /// <param name="pkg"></param>
    /// <param name="assetName"></param>
    /// <param name="canUpdate"></param>
    /// <returns></returns>
    public async UniTask<AssetBundle> LoadBundleAsync(string pkg, string assetName, bool canUpdate = false)
    {
        string path = Path.Combine(DirectoryType.StreamingAssetsPath, pkg);

        AssetBundle ab = await LoadAssetBundleAsync(path, assetName, ArtType.Model, canUpdate);

        if (ab == null)
        {
            Debug.LogError($"Failed to load AssetBundle from {path} for asset {assetName}");
            return null;
        }

        return ab;
    }


    /// <summary>
    /// 根据路径加载二进制文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public async UniTask<byte[]> LoadByteAssetAsync(string path)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        await www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load AssetBundle from {path}: {www.error}");
            return null;
        }

        byte[] bytes = www.downloadHandler.data;

        return bytes;
    }


    /// <summary>
    /// 根据二进制字节流创建文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="info"></param>
    public void CreateFile(string path, string name, byte[] info)
    {
#if !UNITY_WEBPLAYER

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (File.Exists($"{path}/{name}"))
        {
            File.Delete($"{path}/{name}");
        }

        File.WriteAllBytes($"{path}/{name}", info);
#endif
    }


    /// <summary>
    /// 根据文本内容创建文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="info"></param>
    public void CreateTextFile(string path, string name, string info)
    {
#if !UNITY_WEBPLAYER

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (File.Exists($"{path}/{name}"))
        {
            File.Delete($"{path}/{name}");
        }

        File.WriteAllText($"{path}/{name}", info);
#endif
    }


    /// <summary>
    /// 拷贝文件夹到指定目录
    /// </summary>
    /// <param name="sourceFolder"></param>
    /// <param name="destFolder"></param>
    /// <returns></returns>
    public static int CopyFolder(string sourceFolder, string destFolder)
    {
        try
        {
            if (!System.IO.Directory.Exists(destFolder))
            {
                System.IO.Directory.CreateDirectory(destFolder);
            }

            string[] files = System.IO.Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = System.IO.Path.GetFileName(file);
                string dest = System.IO.Path.Combine(destFolder, name);
                System.IO.File.Copy(file, dest);
            }

            string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = System.IO.Path.GetFileName(folder);
                string dest = System.IO.Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
            return 1;
        }
        catch
        {
            return -1;
        }
    }


    /// <summary>
    /// 卸载所有资源
    /// </summary>
    /// <param name="isUnloadAll"></param>
    public static void Unload(bool isUnloadAll)
    {
        AssetBundle.UnloadAllAssetBundles(isUnloadAll);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }


    /// <summary>
    /// 清理缓存文件夹
    /// </summary>
    public static void DeleteCacheFolder()
    {
        string path = Application.persistentDataPath;

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }
}
