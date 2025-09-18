using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class XConfig
{
    /// <summary>
    /// �жϴ���Э��ʱЧ��
    /// </summary>
    public string Token {  get; set; }

    /// <summary>
    /// ��ϷΨһID
    /// </summary>
    public string AppId {  get; set; }

    /// <summary>
    /// ��ϷΨһ����
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// �����߼�Ȩ������ƽ̨��ȡ��
    /// </summary>
    public string DevelopLicense { get; set; }

    public XConfig(string json)
    {
        XConfig config = JsonConvert.DeserializeObject<XConfig>(json);
        Token = config.Token;
        AppId = config.AppId;
        ModuleName = config.ModuleName;
        DevelopLicense = config.DevelopLicense;
    }
}