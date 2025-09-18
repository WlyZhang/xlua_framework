using UnityEngine;
using System;
using System.Text;

#if UNITY_ANDROID
using UnityEngine.Android; // Androidƽ̨Ȩ������
#endif

public class NFCManager : MonoBehaviour
{
    // ����ģʽȷ��ȫ��Ψһ���ʵ�
    public static NFCManager Instance { get; private set; }

    // �¼�ί�У�����⵽NFC��ǩʱ����
    public event Action<string> OnTagScanned;

    private void Awake()
    {
        // ������ʼ���������ظ�������
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �糡������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ��ʼ��NFC������
    /// </summary>
    public void InitNFC()
    {
#if UNITY_ANDROID
        // ���Android�豸�Ƿ�֧��NFCӲ��
        if (!Permission.HasUserAuthorizedPermission("android.permission.NFC"))
        {
            Permission.RequestUserPermission("android.permission.NFC");
            Debug.Log("����NFCȨ��");
        }
        // ����豸Ӳ��֧��
        if (AndroidNfcAdapter.GetDefaultAdapter() == null)
        {
            Debug.LogError("�豸��֧��NFC");
        }
#elif UNITY_IOS
        // iOS����Player Settings����"NFC"����
        Debug.Log("iOS NFC��ʼ��");
#else
        Debug.LogWarning("��ǰƽ̨��֧��NFC����");
#endif
    }

    /// <summary>
    /// ��ʼ����NFC�¼���ǰ̨ģʽ��
    /// </summary>
    public void StartListening()
    {
#if UNITY_ANDROID
        // ����ǰ̨����Intent
        var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity");
        var intent = new AndroidJavaObject("android.content.Intent", activity, null);
        var pendingIntent = AndroidJavaObject.CallStatic<AndroidJavaObject>(
            "android.app.PendingIntent", "getActivity",
            activity, 0, intent, 0
        );
        // ����ǰ̨����
        var adapter = AndroidNfcAdapter.GetDefaultAdapter();
        adapter.Call("enableForegroundDispatch", activity, pendingIntent, null, null);
        Debug.Log("Android NFC����������");
#elif UNITY_IOS
        // iOS��ʹ��CoreNFC���
        Debug.Log("iOS��������ԭ�����ʵ��");
#endif
    }

    /// <summary>
    /// ֹͣ����NFC�¼�
    /// </summary>
    public void StopListening()
    {
#if UNITY_ANDROID
        var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity");
        AndroidNfcAdapter.GetDefaultAdapter()
            .Call("disableForegroundDispatch", activity);
        Debug.Log("Android NFC������ֹͣ");
#endif
    }

    /// <summary>
    /// �����ԭ���㴫�ݵ�NFC���ݣ�Androidʹ�ã�
    /// </summary>
    /// <param name="nfcData">Base64����ı�ǩ����</param>
    public void OnNFCTagDetected(string nfcData)
    {
        try
        {
            // ����Base64����
            byte[] bytes = Convert.FromBase64String(nfcData);
            string tagId = BitConverter.ToString(bytes).Replace("-", "");
            Debug.Log($"��⵽NFC��ǩ: {tagId}");
            // �����¼�֪ͨ������
            OnTagScanned?.Invoke(tagId);
        }
        catch (Exception ex)
        {
            Debug.LogError($"NFC���ݽ���ʧ��: {ex.Message}");
        }
    }

    /// <summary>
    /// �ֶ�����NFC��ȡ��ʾ���ӿڣ�
    /// </summary>
    public void ManualTriggerRead()
    {
#if UNITY_IOS
        // iOS��ͨ��ԭ���������NFC�Ķ���
        Debug.Log("����iOSԭ��NFC��ȡ��");
#endif
    }
}

// === ����Androidԭ��������֣��������Assets/Plugins/Android��===
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
 * ��AndroidManifest.xml ����ʾ����
 * 
 * <uses-permission android:name="android.permission.NFC" />
 * <uses-feature android:name="android.hardware.nfc" android:required="true" />
 */