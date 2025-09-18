


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 内部模式
/// </summary>
public class ResourcesLoader : ILuaLoader
{
    #region Loader API

    public void ILoader()
    {
        Debug.Log("<color=yellow>进入内部模式</color>");
    }

    public void LoadScript(LoaderType type)
    {
        string data = ResourcesModule();

        Lua.GetEnv().DoString(data);

    }

    private string ResourcesModule()
    {
        TextAsset[] asset = Resources.LoadAll<TextAsset>("");

        for (int i = 0; i < asset.Length; i++)
        {
            string key = asset[i].name.Split('.')[0];

            string value = asset[i].text;

            Lua.GetGlobal().Set<string, string>(key, value);

            Lua.LuaList.Add(key, value);
        }

        if (Lua.LuaList.ContainsKey(GameManager.Instance.mainScript))
        {
            return Lua.LuaList[GameManager.Instance.mainScript];
        }

        return string.Empty;
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