using UnityEngine;
using System;
using System.Text;

#if UNITY_ANDROID
using UnityEngine.Android; // Android平台权限请求
#endif

public class NFCManager : MonoBehaviour
{
    // 单例模式确保全局唯一访问点
    public static NFCManager Instance { get; private set; }

    // 事件委托：当检测到NFC标签时触发
    public event Action<string> OnTagScanned;

    private void Awake()
    {
        // 单例初始化（避免重复创建）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初始化NFC适配器
    /// </summary>
    public void InitNFC()
    {
#if UNITY_ANDROID
        // 检查Android设备是否支持NFC硬件
        if (!Permission.HasUserAuthorizedPermission("android.permission.NFC"))
        {
            Permission.RequestUserPermission("android.permission.NFC");
            Debug.Log("请求NFC权限");
        }
        // 检测设备硬件支持
        if (AndroidNfcAdapter.GetDefaultAdapter() == null)
        {
            Debug.LogError("设备不支持NFC");
        }
#elif UNITY_IOS
        // iOS需在Player Settings启用"NFC"能力
        Debug.Log("iOS NFC初始化");
#else
        Debug.LogWarning("当前平台不支持NFC功能");
#endif
    }

    /// <summary>
    /// 开始监听NFC事件（前台模式）
    /// </summary>
    public void StartListening()
    {
#if UNITY_ANDROID
        // 创建前台调度Intent
        var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity");
        var intent = new AndroidJavaObject("android.content.Intent", activity, null);
        var pendingIntent = AndroidJavaObject.CallStatic<AndroidJavaObject>(
            "android.app.PendingIntent", "getActivity",
            activity, 0, intent, 0
        );
        // 启用前台调度
        var adapter = AndroidNfcAdapter.GetDefaultAdapter();
        adapter.Call("enableForegroundDispatch", activity, pendingIntent, null, null);
        Debug.Log("Android NFC监听已启动");
#elif UNITY_IOS
        // iOS需使用CoreNFC框架
        Debug.Log("iOS监听需在原生插件实现");
#endif
    }

    /// <summary>
    /// 停止监听NFC事件
    /// </summary>
    public void StopListening()
    {
#if UNITY_ANDROID
        var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity");
        AndroidNfcAdapter.GetDefaultAdapter()
            .Call("disableForegroundDispatch", activity);
        Debug.Log("Android NFC监听已停止");
#endif
    }

    /// <summary>
    /// 处理从原生层传递的NFC数据（Android使用）
    /// </summary>
    /// <param name="nfcData">Base64编码的标签数据</param>
    public void OnNFCTagDetected(string nfcData)
    {
        try
        {
            // 解码Base64数据
            byte[] bytes = Convert.FromBase64String(nfcData);
            string tagId = BitConverter.ToString(bytes).Replace("-", "");
            Debug.Log($"检测到NFC标签: {tagId}");
            // 触发事件通知订阅者
            OnTagScanned?.Invoke(tagId);
        }
        catch (Exception ex)
        {
            Debug.LogError($"NFC数据解析失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 手动触发NFC读取（示例接口）
    /// </summary>
    public void ManualTriggerRead()
    {
#if UNITY_IOS
        // iOS需通过原生插件调用NFC阅读器
        Debug.Log("调用iOS原生NFC读取器");
#endif
    }
}

// === 配套Android原生插件部分（需放置在Assets/Plugins/Android）===
/*
public class AndroidNfcAdapter {
    private static AndroidJavaClass _nfcAdapterClass;
    
    public static AndroidJavaObject GetDefaultAdapter() {
        if (_nfcAdapterClass == null) {
            _nfcAdapterClass = new AndroidJavaClass("android.nfc.NfcAdapter");
        }
        return _nfcAdapterClass.CallStatic<AndroidJavaObject>("getDefaultAdapter");
    }
}
*/



/*
 * 【AndroidManifest.xml 配置示例】
 * 
 * <uses-permission android:name="android.permission.NFC" />
 * <uses-feature android:name="android.hardware.nfc" android:required="true" />
 */