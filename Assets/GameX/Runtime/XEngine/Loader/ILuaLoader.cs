using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ILuaLoader
{
    /// <summary>
    /// ���캯��
    /// </summary>
    void ILoader();

    /// <summary>
    /// ����Lua����
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
    /// ����Ļ ���(true)/ʧȥ(false) ����ʱ����
    /// </summary>
    /// <param name="hasFocus">��Ļ�Ƿ��ý���</param>
    void OnApplicationFocus(bool hasFocus);








    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerClick(PointerEventData eventData);

    /// <summary>
    /// ˫���¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerDoubleClick(PointerEventData eventData);

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerDown(PointerEventData eventData);

    /// <summary>
    /// ̧���¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerUp(PointerEventData eventData);

    /// <summary>
    /// ��������¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerEnter(PointerEventData eventData);

    /// <summary>
    /// ����Ƴ��¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerExit(PointerEventData eventData);

    /// <summary>
    /// ��꿪ʼ��ק�¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnBeginDrag(PointerEventData eventData);

    /// <summary>
    /// ��������ק�¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnEndDrag(PointerEventData eventData);

    /// <summary>
    /// �����ק������
    /// </summary>
    /// <param name="eventData"></param>
    void OnDrag(PointerEventData eventData);

    /// <summary>
    /// �������¼�
    /// </summary>
    /// <param name="eventData"></param>
    void OnDrop(PointerEventData eventData);







    /// <summary>
    /// Android / iOS ���� Unity �ӿ�
    /// </summary>
    /// <param name="data"></param>
    void Callback(string data);

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="data"></param>
    void Speech(string data);

    /// <summary>
    /// ���������ڿ�ʼ״̬
    /// </summary>
    /// <param name="json"></param>
    void JoyStickTouchBegin(string json);

    /// <summary>
    /// �����������ƶ�״̬
    /// </summary>
    /// <param name="json"></param>
    void JoyStickTouchMove(string json);

    /// <summary>
    /// ���������ڽ���״̬
    /// </summary>
    void JoyStickTouchEnd();

    /// <summary>
    /// ��ָ����
    /// </summary>
    /// <param name="json"></param>
    void FingerLongPress(string json);

    /// <summary>
    /// ��ָ��������
    /// </summary>
    /// <param name="json"></param>
    void FingerSwipe(string json);

    void FingerTap(string json);
}