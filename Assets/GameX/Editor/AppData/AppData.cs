using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildPlatform = DirectoryType.BuildPlatform;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AppData
{
    /// <summary>
    /// 发布平台
    /// </summary>
    public static BuildPlatform BuildPlatform { private get; set; }

    /// <summary>
    /// 发布平台属性
    /// </summary>
    public static BuildTarget Platform
    {
        get
        {
            BuildTarget plat = BuildTarget.NoTarget;
            if (BuildPlatform == BuildPlatform.Windows)
                plat = BuildTarget.StandaloneWindows;
            else if (BuildPlatform == BuildPlatform.Linux)
                plat = BuildTarget.StandaloneLinux64;
            else if (BuildPlatform == BuildPlatform.MacOS)
                plat = BuildTarget.StandaloneOSX;
            else if (BuildPlatform == BuildPlatform.iOS)
                plat = BuildTarget.iOS;
            else if (BuildPlatform == BuildPlatform.Android)
                plat = BuildTarget.Android;
            else if (BuildPlatform == BuildPlatform.WebGL)
                plat = BuildTarget.WebGL;
            else
            {
                plat = BuildTarget.NoTarget;
            }

            return plat;
        }
        set
        {
            Platform = value;
        }
    }
}