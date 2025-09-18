using Newtonsoft.Json;
using OBS.Model;
using System.IO;
using UnityEngine;

public class HuaweiObsManager
{
    /// <summary>
    /// �������
    /// </summary>
    public static ConfigOBS ConfigOBS;

    /// <summary>
    /// HuaweiOBS �ͻ������
    /// </summary>
    public static ClientOBS ClientOBS;

    /// <summary>
    /// �ϴ����
    /// </summary>
    public static UploadOBS UploadOBS;

    /// <summary>
    /// �������
    /// </summary>
    public static DownloadOBS DownloadOBS;

    /// <summary>
    /// ����Ͱ�������
    /// </summary>
    public static BucketOBS BucketOBS;

    /// <summary>
    /// �����ļ��������
    /// </summary>
    public static FileOBS FileOBS;

    public static void Create()
    {
        //��ʼ�������ļ�
        InitConfig();

        //��ʼ���ͻ���
        InitClient();
    }

    /// <summary>
    /// ��ʼ�������ļ�
    /// </summary>
    public static void InitConfig()
    {
#if UNITY_EDITOR
        string configName = "config";
        string path = $"{Application.streamingAssetsPath}/{configName}.json";

        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            ConfigOBS = JsonConvert.DeserializeObject<ConfigOBS>(json);
        }
#else
        //������Զ�̬��ȡ HuaweiObsConfig ������Ϣ
#endif

    }

    /// <summary>
    /// ��ʼ���ͻ���
    /// </summary>
    public static void InitClient()
    {
        //��ʼ���ͻ���
        ClientOBS = new ClientOBS();
        ClientOBS.Init(ConfigOBS);

        //��ʼ���ϴ����
        UploadOBS = new UploadOBS();

        //��ʼ���������
        DownloadOBS = new DownloadOBS();

        //��ʼ������Ͱ
        BucketOBS = new BucketOBS();

        //��ʼ�������ļ�
        FileOBS = new FileOBS();
    }
}