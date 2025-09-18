using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ILuaLoader
{
    /// <summary>
    /// 构造函数
    /// </summary>
    void ILoader();

    /// <summary>
    /// 加载Lua代码
    /// </summary>
    /// <param name="type"></param>
    void LoadScript(LoaderType type);







    void Awake(GameObject go);

    void OnEnable(GameObject go);

    void Start(GameObject go);

    void FixedUpdate(GameObject go, float time);

    void Update(GameObject go, float time);

    void LateUpdate(GameObject go, float time);

    void OnDisable(GameObject go);

    void OnDestroy(GameObject go);

    void OnTriggerEnter(GameObject self, Collider other);

    void OnTriggerStay(GameObject self, Collider other);

    void OnTriggerExit(GameObject self, Collider other);

    void OnCollisionEnter(GameObject self, Collision other);

    void OnCollisionStay(GameObject self, Collision other);

    void OnCollisionExit(GameObject self, Collision other);

    void OnTriggerEnter2D(GameObject self, Collider2D other);

    void OnTriggerStay2D(GameObject self, Collider2D other);

    void OnTriggerExit2D(GameObject self, Collider2D other);

    void OnCollisionEnter2D(GameObject self, Collision2D other);

    void OnCollisionStay2D(GameObject self, Collision2D other);

    void OnCollisionExit2D(GameObject self, Collision2D other);

    /// <summary>
    /// 当屏幕 获得(true)/失去(false) 焦点时调用
    /// </summary>
    /// <param name="hasFocus">屏幕是否获得焦点</param>
    void OnApplicationFocus(bool hasFocus);








    /// <summary>
    /// 单击事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerClick(PointerEventData eventData);

    /// <summary>
    /// 双击事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerDoubleClick(PointerEventData eventData);

    /// <summary>
    /// 按下事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerDown(PointerEventData eventData);

    /// <summary>
    /// 抬起事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerUp(PointerEventData eventData);

    /// <summary>
    /// 鼠标移入事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerEnter(PointerEventData eventData);

    /// <summary>
    /// 鼠标移出事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerExit(PointerEventData eventData);

    /// <summary>
    /// 鼠标开始拖拽事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnBeginDrag(PointerEventData eventData);

    /// <summary>
    /// 鼠标结束拖拽事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnEndDrag(PointerEventData eventData);

    /// <summary>
    /// 鼠标拖拽进行中
    /// </summary>
    /// <param name="eventData"></param>
    void OnDrag(PointerEventData eventData);

    /// <summary>
    /// 鼠标放置事件
    /// </summary>
    /// <param name="eventData"></param>
    void OnDrop(PointerEventData eventData);







    /// <summary>
    /// Android / iOS 调用 Unity 接口
    /// </summary>
    /// <param name="data"></param>
    void Callback(string data);

    /// <summary>
    /// 语音数据
    /// </summary>
    /// <param name="data"></param>
    void Speech(string data);

    /// <summary>
    /// 控制器处于开始状态
    /// </summary>
    /// <param name="json"></param>
    void JoyStickTouchBegin(string json);

    /// <summary>
    /// 控制器处于移动状态
    /// </summary>
    /// <param name="json"></param>
    void JoyStickTouchMove(string json);

    /// <summary>
    /// 控制器处于结束状态
    /// </summary>
    void JoyStickTouchEnd();

    /// <summary>
    /// 手指长按
    /// </summary>
    /// <param name="json"></param>
    void FingerLongPress(string json);

    /// <summary>
    /// 手指滑动方向
    /// </summary>
    /// <param name="json"></param>
    void FingerSwipe(string json);

    void FingerTap(string json);
}