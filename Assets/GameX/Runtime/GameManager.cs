using CXGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using XLua;


public enum BuildType : byte
{
    /// <summary>
    /// 开发模式
    /// </summary>
    Debug,

    /// <summary>
    /// 发布模式
    /// </summary>
    Release,
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string mainScript = "app";

    public string suffix = ".lua";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    private void InitComponent()
    {
        
    }
}