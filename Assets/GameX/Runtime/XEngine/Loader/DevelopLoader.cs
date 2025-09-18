
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 开发者模式
/// </summary>
public class DevelopLoader : ILuaLoader
{
    #region Loader API

    public void ILoader()
    {
        Debug.Log("<color=yellow>进入开发者模式</color>");

    }

    public async void LoadScript(LoaderType type)
    {
        string data = await DevelopModule();

        Lua.GetEnv().DoString(data);

    }


    private async UniTask<string> DevelopModule()
    {
        string path = DirectoryType.LuaProjectPath;

        await GetScriptAll(path);


        string data = string.Empty;

        if (Lua.LuaList.ContainsKey(GameManager.Instance.mainScript))
        {
            data = Lua.LuaList[GameManager.Instance.mainScript];
        }
        else
        {
            data = string.Empty;
        }

        return data;
    }


    /// <summary>
    /// 获取文件夹全部脚本文件
    /// </summary>
    /// <param name="dirPath"></param>
    private async UniTask GetScriptAll(string dirPath)
    {
        string[] dirList = Directory.GetDirectories(dirPath);

        if (dirList.Length > 0)
        {
            //遍历子文件夹
            for (int i = 0; i < dirList.Length; i++)
            {
                //获取下一层文件夹
                await GetScriptAll(dirList[i]);
            }

            //添加子文件夹文件
            AddScript(dirPath);
        }
        else
        {
            //添加本文件夹文件
            AddScript(dirPath);
        }
    }

    /// <summary>
    /// 添加文件夹脚本文件
    /// </summary>
    /// <param name="dirPath"></param>
    private void AddScript(string dirPath)
    {
        string[] dirlist = Directory.GetFiles(dirPath);
        if (dirlist.Length > 0)
        {
            for (int i = 0; i < dirlist.Length; i++)
            {
                if (dirlist[i].Contains(".meta"))
                    continue;

                string[] strArr = dirlist[i].Replace("\\", "/").Split('/');

                string key = strArr[strArr.Length - 1].Split('.')[0];
                string value = File.ReadAllText(dirlist[i].Replace("\\", "/"));

                Lua.Set<string, string>(key, value);

                if (Lua.LuaList.ContainsKey(key))
                    continue;

                Lua.LuaList.Add(key, value);
            }
        }
    }

    #endregion


    #region Unity API

    public void Awake(GameObject go)
    {
        
    }

    public void FixedUpdate(GameObject go, float time)
    {
        
    }

    public void LateUpdate(GameObject go, float time)
    {
        
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnCollisionEnter(GameObject self, Collision other)
    {
        
    }

    public void OnCollisionEnter2D(GameObject self, Collision2D other)
    {
        
    }

    public void OnCollisionExit(GameObject self, Collision other)
    {
        
    }

    public void OnCollisionExit2D(GameObject self, Collision2D other)
    {
        
    }

    public void OnCollisionStay(GameObject self, Collision other)
    {
        
    }

    public void OnCollisionStay2D(GameObject self, Collision2D other)
    {
        
    }

    public void OnDestroy(GameObject go)
    {
        
    }

    public void OnDisable(GameObject go)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnEnable(GameObject go)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDoubleClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnTriggerEnter(GameObject self, Collider other)
    {
        
    }

    public void OnTriggerEnter2D(GameObject self, Collider2D other)
    {
        
    }

    public void OnTriggerExit(GameObject self, Collider other)
    {
        
    }

    public void OnTriggerExit2D(GameObject self, Collider2D other)
    {
        
    }

    public void OnTriggerStay(GameObject self, Collider other)
    {
        
    }

    public void OnTriggerStay2D(GameObject self, Collider2D other)
    {
        
    }

    public void Start(GameObject go)
    {
        
    }

    public void Update(GameObject go, float time)
    {
        
    }

    #endregion


    #region Custom API

    public void Callback(string data)
    {
        
    }

    public void Speech(string data)
    {
        
    }

    public void JoyStickTouchBegin(string json)
    {
        
    }

    public void JoyStickTouchMove(string json)
    {
        
    }

    public void JoyStickTouchEnd()
    {
        
    }

    public void FingerLongPress(string json)
    {
        
    }

    public void FingerSwipe(string json)
    {
        
    }

    public void FingerTap(string json)
    {
        
    }

#endregion

}
