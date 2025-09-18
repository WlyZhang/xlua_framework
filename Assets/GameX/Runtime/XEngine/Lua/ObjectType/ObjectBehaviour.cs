using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 这个类用于IObjectLoader接口和实际挂载在游戏体脚本之间接桥
/// </summary>
public class ObjectBehaviour : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public static IObjectLoader loader;

    private void Awake()
    {
        XObject xObject = new XObject();
        loader = xObject;
        loader.Awake();
    }

    public void OnEnable()
    {
        loader.OnEnable(gameObject);
    }

    public void Start()
    {
        loader.Start(gameObject);
    }

    public void OnDisable()
    {
        loader.OnDisable(gameObject);
    }

    public void OnDestroy()
    {
        loader.OnDestroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        loader.OnTriggerEnter(gameObject, other);
    }

    public void OnTriggerStay(Collider other)
    {
        loader.OnTriggerStay(gameObject, other);
    }

    public void OnTriggerExit(Collider other)
    {
        loader.OnTriggerExit(gameObject, other);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        loader.OnTriggerEnter2D(gameObject, collision);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        loader.OnTriggerStay2D(gameObject, collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        loader.OnTriggerExit2D(gameObject, collision);
    }

    public void OnCollisionEnter(Collision collision)
    {
        loader.OnCollisionEnter(gameObject,collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        loader.OnCollisionStay(gameObject,collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        loader.OnCollisionExit(gameObject, collision);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        loader.OnCollisionEnter2D(gameObject, collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        loader.OnCollisionStay2D(gameObject, collision);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        loader.OnCollisionExit2D(gameObject, collision);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        loader.OnPointerClick(gameObject, eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        loader.OnPointerDown(gameObject, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        loader.OnPointerUp(gameObject, eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        loader.OnPointerEnter(gameObject, eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        loader.OnPointerExit(gameObject, eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        loader.OnBeginDrag(gameObject, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        loader.OnDrag(gameObject, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        loader.OnEndDrag(gameObject, eventData);
    }
}
