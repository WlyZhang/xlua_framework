using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CXGame
{
    public enum E_ButtonSound
    {
        Normal,
        Back,
        Home,
        Close
    }

    public class ButtonPointer : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        public delegate void ButtonPointerDelegate(PointerEventData eventData);

        public event ButtonPointerDelegate OnButtonPointerClick;

        public event ButtonPointerDelegate OnButtonPointerPress;

        public static bool IsDown;

        public float durationThreshold = 1.0f;
        [SerializeField] private bool interActable = true;
        private bool isPointerDown = false;
        private Graphic[] graphicButton;
        private Color colorNotInterActable = new Color(0.78f, 0.78f, 0.78f, 0.5f);
        public bool InterActable
        {
            get
            {
                return interActable;
            }
            set
            {
                interActable = value;
                if (graphicButton == null)
                {
                    return;
                }
                if (!interActable)
                {
                    foreach (var item in graphicButton)
                    {
                        item.color *= colorNotInterActable;
                    }
                }
                else
                {
                    for (int i = 0; i < graphicButton.Length; i++)
                    {
                        graphicButton[i].color = originColor[i];
                    }
                }
            }
        }


        private float timePressStarted = 0;
        private List<Color> originColor = new List<Color>();
        private PointerEventData _PressPointerEventData = null;

        [SerializeField] E_ButtonSound _E_ButtonSound = E_ButtonSound.Normal;

        System.Text.StringBuilder _StrBuilder = new System.Text.StringBuilder();
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!interActable)
            {
                return;
            }

            OnButtonPointerClick?.Invoke(eventData);
            _StrBuilder.Clear();
            _StrBuilder.Append("sound/");

            if (_E_ButtonSound == E_ButtonSound.Normal)
            {

                //_StrBuilder.Append(UISoundName.Sound_button_common);

            }
            else if (_E_ButtonSound == E_ButtonSound.Home)
            {
                //_StrBuilder.Append(UISoundName.Sound_button_home);
            }
            else if (_E_ButtonSound == E_ButtonSound.Close)
            {
                //_StrBuilder.Append(UISoundName.Sound_button_close);
            }
            else if (_E_ButtonSound == E_ButtonSound.Back)
            {
                //_StrBuilder.Append(UISoundName.Sound_button_return);
            }
            //SH.SoundManager.Instance.PlaySFX(_StrBuilder.ToString());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            timePressStarted = 0;
            Debug.Log("抬起事件");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            _PressPointerEventData = eventData;
            Debug.Log("按下事件");
        }

        void Awake()
        {
            graphicButton = transform.GetComponentsInChildren<Graphic>(true);
            foreach (var item in graphicButton)
            {
                originColor.Add(item.color);
            }
            if (!interActable)
            {
                for (int i = 0; i < graphicButton.Length; i++)
                {
                    graphicButton[i].color *= colorNotInterActable;
                }
            }
        }

        void Update()
        {
            if (isPointerDown)
            {
                timePressStarted += Time.unscaledDeltaTime;

                if (timePressStarted > 1)
                {
                    isPointerDown = false;
                    timePressStarted = 0;

                    OnButtonPointerPress?.Invoke(_PressPointerEventData);

                    Debug.Log("长按事件");
                }
            }
        }
    }
}
