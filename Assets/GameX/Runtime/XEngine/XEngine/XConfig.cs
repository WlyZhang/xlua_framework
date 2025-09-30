using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class XConfig
{
    /// <summary>
    /// 判断传输协议时效性
    /// </summary>
    public string Token {  get; set; }

    /// <summary>
    /// 游戏唯一ID
    /// </summary>
    public string AppId {  get; set; }

    /// <summary>
    /// 游戏唯一名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 开发者鉴权（开放平台获取）
    /// </summary>
    public string DevelopLicense { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public XConfig() { }

    /// <summary>
    /// 构造函数（重载）
    /// </summary>
    /// <param name="json"></param>
    public XConfig(string json)
    {
        XConfig config = JsonConvert.DeserializeObject<XConfig>(json);
        Token = config.Token;
        AppId = config.AppId;
        ModuleName = config.ModuleName;
        DevelopLicense = config.DevelopLicense;
    }
}