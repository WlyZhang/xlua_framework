using System;
using UnityEngine;

[Serializable]
public class ConfigOBS
{
    public string AK { get; set; }
    public string SK { get; set; }
    public string EndPoint { get; set; }
    public string BucketName { get; set; }
    public string Location { get; set; }
    public string StorageClass{ get; set; }
    public string CannedAcl{ get; set; }
}