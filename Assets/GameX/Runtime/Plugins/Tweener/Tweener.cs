using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 封装缓动工具类
/// </summary>
public class Tweener
{
    public static void DOMove(GameObject go,Vector3 endValue,float time,bool snapping=false)
    {
        go.transform.DOMove(endValue, time, snapping);
    }

    public static void DOMoveX(GameObject go,float endValue,float time, bool snapping = false)
    {
        go.transform.DOMoveX(endValue, time, snapping);
    }

    public static void DOMoveY(GameObject go, float endValue, float time, bool snapping = false)
    {
        go.transform.DOMoveY(endValue, time, snapping);
    }

    public static void DOMoveZ(GameObject go, float endValue, float time, bool snapping = false)
    {
        go.transform.DOMoveZ(endValue, time, snapping);
    }

    public static void DOLocalMove(GameObject go, Vector3 endValue, float time, bool snapping = false)
    {
        go.transform.DOLocalMove(endValue, time, snapping);
    }

    public static void DOLocalMoveX(GameObject go, float endValue, float time, bool snapping = false)
    {
        go.transform.DOLocalMoveX(endValue, time, snapping);
    }

    public static void DOLocalMoveY(GameObject go, float endValue, float time, bool snapping = false)
    {
        go.transform.DOLocalMoveY(endValue, time, snapping);
    }

    public static void DOLocalMoveZ(GameObject go, float endValue, float time, bool snapping = false)
    {
        go.transform.DOLocalMoveZ(endValue, time, snapping);
    }

    public static void DORotate(GameObject go,Vector3 endValue,float time,RotateMode mode = RotateMode.Fast)
    {
        go.transform.DORotate(endValue, time, mode);
    }

    public static void DOLocalRotate(GameObject go, Vector3 endValue, float time, RotateMode mode = RotateMode.Fast)
    {
        go.transform.DOLocalRotate(endValue, time, mode);
    }

    public static void DOScale(GameObject go, Vector3 endValue, float time)
    {
        go.transform.DOScale(endValue, time);
    }

    public static void DOLookAt(GameObject go, Vector3 endValue, float time)
    {
        go.transform.DOLookAt(endValue, time);
    }
}