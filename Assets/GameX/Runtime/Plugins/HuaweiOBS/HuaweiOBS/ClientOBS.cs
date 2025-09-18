using OBS;
using System;

public class ClientOBS
{
    /// <summary>
    /// OBS�ͻ���ʵ��
    /// </summary>
    private ObsClient Client;

    /// <summary>
    /// OBS����ʵ��
    /// </summary>
    private ConfigOBS Config;

    /// <summary>
    /// ��ʼ��OBSʵ��
    /// </summary>
    /// <param name="obs"></param>
    public void Init(ConfigOBS obs)
    {
        Config = obs;

        // ��ʼ�����ò���
        ObsConfig config = new ObsConfig();
        config.Endpoint = obs.EndPoint;
        // ��֤�õ�ak��skӲ���뵽�����л������Ĵ洢���кܴ�İ�ȫ���գ������������ļ����߻������������Ĵ�ţ�ʹ��ʱ���ܣ�ȷ����ȫ����ʾ����ak��sk�����ڻ���������Ϊ�������б�ʾ��ǰ�����ڱ��ػ��������û�������AccessKeyID��SecretAccessKey��
        // �����Ե�¼���ʹ������̨��ȡ������ԿAK/SK����ȡ��ʽ��μ�https://support.huaweicloud.com/usermanual-ca/ca_01_0003.html
        string accessKey = Environment.GetEnvironmentVariable(obs.AK, EnvironmentVariableTarget.Machine);
        string secretKey = Environment.GetEnvironmentVariable(obs.SK, EnvironmentVariableTarget.Machine);
        // ����ObsClientʵ��
        Client = new ObsClient(accessKey, secretKey, config);
        // ʹ�÷���OBS
        Debug.Log("��ʼ�� HuaweiOBS �ͻ������.");
    }

    /// <summary>
    /// ��ȡOBS�ͻ���
    /// </summary>
    /// <returns></returns>
    public ObsClient GetClientOBS()
    {
        if(Client == null)
        {
            Init(Config);

            return Client;
        }
        else
        {
            return Client;
        }
    }

    public ConfigOBS GetConfigOBS()
    {
        return Config;
    }
}
