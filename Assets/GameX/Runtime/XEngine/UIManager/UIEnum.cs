using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CXGame
{
    //UI枚举注意要点
    //枚举名字和制作的UIPanel名字一样
    public enum E_UItype : byte
    {
        eNull = 0,
        InitPanel = 1,
        MainPanel = 2,
        TipPanel = 3,
        StartPanel = 4,
        StartModulePanel = 5,
        LilunPanel = 6,
        SelectMapPanel = 7,
        ChartPanel = 8,
    }
}
