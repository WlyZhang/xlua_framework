using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// ��װ Debug ����ģ��
/// </summary>
public class Debug
{
    public static void Log(params object[] args)
    {
#if UNITY_EDITOR
        for (int i = 0; i < args.Length; i++)
        {
            UnityEngine.Debug.Log($"<color=gray>GameCX:</color> {args[i]}");
        }
#endif
    }

    public static void LogWarning(params object[] args)
    {
#if UNITY_EDITOR
        for (int i = 0; i < args.Length; i++)
        {
            UnityEngine.Debug.Log($"<color=gray>GameCX:</color> <color=yellow>{args[i]}</color>");
        }
#endif
    }

    public static void LogError(params object[] args)
    {
#if UNITY_EDITOR
        for (int i = 0; i < args.Length; i++)
        {
            UnityEngine.Debug.Log($"<color=gray>GameCX:</color> <color=red>{args[i]}</color>");
        }
#endif
    }

    public static void LogException(params object[] args)
    {
#if UNITY_EDITOR
        for (int i = 0; i < args.Length; i++)
        {
            UnityEngine.Debug.Log($"<color=gray>GameCX:</color> <color=blue>{args[i]}</color>");
        }
#endif
    }
}