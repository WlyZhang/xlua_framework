using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum LoaderType
{
    /// <summary>
    /// ������ģʽ
    /// </summary>
    DevelopLoader,

    /// <summary>
    /// �ڲ�ģʽ
    /// </summary>
    ResourcesLoader,

    /// <summary>
    /// �ȸ���ģʽ
    /// </summary>
    HotUpdateLoader,
}