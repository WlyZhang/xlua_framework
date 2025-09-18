using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ArtType = DirectoryType.ArtType;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class SetupSDK
{

    [MenuItem("GameX/SDK/SetupSDK")]
    public static void CreateSdkDirectory()
    {
        List<string> list = GetSdkPaths();

        for (int i = 0; i < list.Count; i++)
        {
            if (string.IsNullOrEmpty(list[i]))
                continue;

            if (!Directory.Exists(list[i]))
            {
                Directory.CreateDirectory(list[i]);
            }
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 获取SDK层级目录列表
    /// </summary>
    /// <returns></returns>
    public static List<string> GetSdkPaths()
    {
        string root = Path.Combine(DirectoryType.RootPath, DirectoryType.DownloadPath);

        string dev = Path.Combine(DirectoryType.RootPath, DirectoryType.DeveloperPath, DirectoryType.Scripts);

        string art = Path.Combine(DirectoryType.RootPath, DirectoryType.DownloadPath, DirectoryType.SourceMatePath);

        string package = Path.Combine(DirectoryType.RootPath, DirectoryType.DownloadPath, DirectoryType.Package);

        // Module Type Path

        string view = Path.Combine(package, ArtType.UGUI.ToString());

        string model = Path.Combine(package, ArtType.Model.ToString());

        string scene = Path.Combine(package, ArtType.Scene.ToString());

        string texture = Path.Combine(package, ArtType.Texture.ToString());

        string sprite = Path.Combine(package, ArtType.Sprite.ToString());

        string audio = Path.Combine(package, ArtType.Audio.ToString());

        string video = Path.Combine(package, ArtType.Video.ToString());

        string config = Path.Combine(package, ArtType.Config.ToString());

        string effect = Path.Combine(package, ArtType.Effect.ToString());

        string material = Path.Combine(package, ArtType.Material.ToString());

        string lua_dev = Path.Combine(DirectoryType.RootPath, DirectoryType.LuaDevPath);

        string lua_res = Path.Combine(package, DirectoryType.LuaResPath);


        //添加到列表
        List<string> list = new List<string>()
        {
            art,
            dev,
            model,
            scene,
            texture,
            sprite,
            audio,
            video,
            config,
            effect,
            material,
            view,
            lua_dev,
            lua_res,
        };

        return list;
    }
}
