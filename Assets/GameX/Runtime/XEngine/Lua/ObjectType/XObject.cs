using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class XObject : IObjectLoader
{
    private Action<GameObject> luaStart = null;
    private Action<GameObject> luaOnEnable = null;
    private Action<GameObject> luaOnDisable = null;
    private Action<GameObject> luaOnDestroy = null;

    private Action<GameObject, Collider> luaOnTriggerEnter = null;
    private Action<GameObject, Collider> luaOnTriggerStay = null;
    private Action<GameObject, Collider> luaOnTriggerExit = null;

    private Action<GameObject, Collider2D> luaOnTriggerEnter2D = null;
    private Action<GameObject, Collider2D> luaOnTriggerStay2D = null;
    private Action<GameObject, Collider2D> luaOnTriggerExit2D = null;
    
    private Action<GameObject, Collision> luaOnCollisionEnter = null;
    private Action<GameObject, Collision> luaOnCollisionStay = null;
    private Action<GameObject, Collision> luaOnCollisionExit = null;
    
    private Action<GameObject, Collision2D> luaOnCollisionEnter2D = null;
    private Action<GameObject, Collision2D> luaOnCollisionStay2D = null;
    private Action<GameObject, Collision2D> luaOnCollisionExit2D = null;

    private Action<GameObject, PointerEventData> luaOnPointerClick = null;
    private Action<GameObject, PointerEventData> luaOnPointerDown = null;
    private Action<GameObject, PointerEventData> luaOnPointerUp = null;
    private Action<GameObject, PointerEventData> luaOnPointerEnter = null;
    private Action<GameObject, PointerEventData> luaOnPointerExit = null;
    private Action<GameObject, PointerEventData> luaOnBeginDrag = null;
    private Action<GameObject, PointerEventData> luaOnDrag = null;
    private Action<GameObject, PointerEventData> luaOnEndDrag = null;


    public void Awake()
    {


        luaStart = Lua.GetGlobal().Get<Action<GameObject>>("Start");
        luaOnEnable = Lua.GetGlobal().Get<Action<GameObject>>("OnEnable");
        luaOnDisable = Lua.GetGlobal().Get<Action<GameObject>>("OnDisable");
        luaOnDestroy = Lua.GetGlobal().Get<Action<GameObject>>("OnDestroy");

        luaOnTriggerEnter = Lua.GetGlobal().Get<Action<GameObject, Collider>>("OnTriggerEnter");
        luaOnTriggerStay = Lua.GetGlobal().Get<Action<GameObject, Collider>>("OnTriggerStay");
        luaOnTriggerExit = Lua.GetGlobal().Get<Action<GameObject, Collider>>("OnTriggerExit");

        luaOnTriggerEnter2D = Lua.GetGlobal().Get<Action<GameObject, Collider2D>>("OnTriggerEnter2D");
        luaOnTriggerStay2D = Lua.GetGlobal().Get<Action<GameObject, Collider2D>>("OnTriggerStay2D");
        luaOnTriggerExit2D = Lua.GetGlobal().Get<Action<GameObject, Collider2D>>("OnTriggerExit2D");

        luaOnCollisionEnter = Lua.GetGlobal().Get<Action<GameObject, Collision>>("OnCollisionEnter");
        luaOnCollisionStay = Lua.GetGlobal().Get<Action<GameObject, Collision>>("OnCollisionStay");
        luaOnCollisionExit = Lua.GetGlobal().Get<Action<GameObject, Collision>>("OnCollisionExit");

        luaOnCollisionEnter2D = Lua.GetGlobal().Get<Action<GameObject, Collision2D>>("OnCollisionEnter2D");
        luaOnCollisionStay2D = Lua.GetGlobal().Get<Action<GameObject, Collision2D>>("OnCollisionStay2D");
        luaOnCollisionExit2D = Lua.GetGlobal().Get<Action<GameObject, Collision2D>>("OnCollisionExit2D");

        luaOnPointerClick = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnClick");
        luaOnPointerDown = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnDown");
        luaOnPointerUp = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnUp");
        luaOnPointerEnter = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnEnter");
        luaOnPointerExit = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnExit");
        luaOnBeginDrag = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnBeginDrag");
        luaOnDrag = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnDrag");
        luaOnEndDrag = Lua.GetGlobal().Get<Action<GameObject, PointerEventData>>("OnEndDrag");
    }

    public void OnEnable(GameObject go)
    {
        if (luaOnEnable != null)
        {
            luaOnEnable(go);
        }
    }

    public void Start(GameObject go)
    {
        if (luaStart != null)
        {
            luaStart(go);
        }
    }

    public void OnDisable(GameObject go)
    {
        if (luaOnDisable != null)
        {
            luaOnDisable(go);
        }
    }

    public void OnDestroy(GameObject go)
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy(go);
        }
    }

    public void OnTriggerEnter(GameObject self, Collider collition)
    {
        if (luaOnTriggerEnter != null)
        {
            luaOnTriggerEnter(self, collition);
        }
    }

    public void OnTriggerStay(GameObject self, Collider collition)
    {
        if (luaOnTriggerStay != null)
        {
            luaOnTriggerStay(self, collition);
        }
    }

    public void OnTriggerExit(GameObject self, Collider collition)
    {
        if (luaOnTriggerExit != null)
        {
            luaOnTriggerExit(self, collition);
        }
    }

    public void OnTriggerEnter2D(GameObject self, Collider2D collition)
    {
        if (luaOnTriggerEnter2D != null)
        {
            luaOnTriggerEnter2D(self, collition);
        }
    }

    public void OnTriggerStay2D(GameObject self, Collider2D collition)
    {
        if (luaOnTriggerStay2D != null)
        {
            luaOnTriggerStay2D(self, collition);
        }
    }

    public void OnTriggerExit2D(GameObject self, Collider2D collition)
    {
        if (luaOnTriggerExit2D != null)
        {
            luaOnTriggerExit2D(self, collition);
        }
    }

    public void OnCollisionEnter(GameObject self, Collision collition)
    {
        if (luaOnCollisionEnter != null)
        {
            luaOnCollisionEnter(self, collition);
        }
    }

    public void OnCollisionStay(GameObject self, Collision collition)
    {
        if (luaOnCollisionStay != null)
        {
            luaOnCollisionStay(self, collition);
        }
    }

    public void OnCollisionExit(GameObject self, Collision collition)
    {
        if (luaOnCollisionExit != null)
        {
            luaOnCollisionExit(self, collition);
        }
    }

    public void OnCollisionEnter2D(GameObject self, Collision2D collition)
    {
        if (luaOnCollisionEnter2D != null)
        {
            luaOnCollisionEnter2D(self, collition);
        }
    }

    public void OnCollisionStay2D(GameObject self, Collision2D collition)
    {
        if (luaOnCollisionStay2D != null)
        {
            luaOnCollisionStay2D(self, collition);
        }
    }

    public void OnCollisionExit2D(GameObject self, Collision2D collition)
    {
        if (luaOnCollisionExit2D != null)
        {
            luaOnCollisionExit2D(self, collition);
        }
    }

    public void OnPointerClick(GameObject self, PointerEventData eventData)
    {
        if (luaOnPointerClick != null)
        {
            luaOnPointerClick(self, eventData);
        }
    }

    public void OnPointerDown(GameObject self, PointerEventData eventData)
    {
        if (luaOnPointerDown != null)
        {
            luaOnPointerDown(self, eventData);
        }
    }

    public void OnPointerUp(GameObject self, PointerEventData eventData)
    {
        if (luaOnPointerUp != null)
        {
            luaOnPointerUp(self, eventData);
        }
    }

    public void OnPointerEnter(GameObject self, PointerEventData eventData)
    {
        if (luaOnPointerEnter != null)
        {
            luaOnPointerEnter(self, eventData);
        }
    }

    public void OnPointerExit(GameObject self, PointerEventData eventData)
    {
        if (luaOnPointerExit != null)
        {
            luaOnPointerExit(self, eventData);
        }
    }

    public void OnBeginDrag(GameObject self, PointerEventData eventData)
    {
        if (luaOnBeginDrag != null)
        {
            luaOnBeginDrag(self, eventData);
        }
    }

    public void OnDrag(GameObject self, PointerEventData eventData)
    {
        if (luaOnDrag != null)
        {
            luaOnDrag(self, eventData);
        }
    }

    public void OnEndDrag(GameObject self, PointerEventData eventData)
    {
        if (luaOnEndDrag != null)
        {
            luaOnEndDrag(self, eventData);
        }
    }
}