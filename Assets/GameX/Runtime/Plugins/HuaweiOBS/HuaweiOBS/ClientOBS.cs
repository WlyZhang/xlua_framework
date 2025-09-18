using OBS;
using System;

public class ClientOBS
{
    /// <summary>
    /// OBS客户端实例
    /// </summary>
    private ObsClient Client;

    /// <summary>
    /// OBS配置实例
    /// </summary>
    private ConfigOBS Config;

    /// <summary>
    /// 初始化OBS实例
    /// </summary>
    /// <param name="obs"></param>
    public void Init(ConfigOBS obs)
    {
        Config = obs;

        // 初始化配置参数
        ObsConfig config = new ObsConfig();
        config.Endpoint = obs.EndPoint;
        // 认证用的ak和sk硬编码到代码中或者明文存储都有很大的安全风险，建议在配置文件或者环境变量中密文存放，使用时解密，确保安全；本示例以ak和sk保存在环境变量中为例，运行本示例前请先在本地环境中设置环境变量AccessKeyID和SecretAccessKey。
        // 您可以登录访问管理控制台获取访问密钥AK/SK，获取方式请参见https://support.huaweicloud.com/usermanual-ca/ca_01_0003.html
        string accessKey = Environment.GetEnvironmentVariable(obs.AK, EnvironmentVariableTarget.Machine);
        string secretKey = Environment.GetEnvironmentVariable(obs.SK, EnvironmentVariableTarget.Machine);
        // 创建ObsClient实例
        Client = new ObsClient(accessKey, secretKey, config);
        // 使用访问OBS
        Debug.Log("初始化 HuaweiOBS 客户端完成.");
    }

    /// <summary>
    /// 获取OBS客户端
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
