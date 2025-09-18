using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum LoaderType
{
    /// <summary>
    /// 开发者模式
    /// </summary>
    DevelopLoader,

    /// <summary>
    /// 内部模式
    /// </summary>
    ResourcesLoader,

    /// <summary>
    /// 热更新模式
    /// </summary>
    HotUpdateLoader,
}