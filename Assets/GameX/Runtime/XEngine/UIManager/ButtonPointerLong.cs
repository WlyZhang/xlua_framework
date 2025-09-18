using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CXGame
{
    public class ButtonPointerLong : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        public delegate void ButtonPointerDelegate(PointerEventData eventData);

        public event ButtonPointerDelegate OnButtonPointerOnLongPress;

        private PointerEventData _PressPointerEventData = null;

        private bool IsDown;
        private float Delay = 0.3f;//延迟相当于按下持续时间
        private float LastDownTime;

        public static bool IsGuideUp;


        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonPointerOnLongPress?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("------------------------按钮被按下-------------------------");
            IsDown = true;
            LastDownTime = Time.time;
            _PressPointerEventData = eventData;

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("-------------------------按钮抬起--------------------------");
            IsDown = false;
            IsGuideUp = true;
        }

        private void OnDisable()
        {
            _PressPointerEventData = null;
            IsDown = false;
        }



        private void Update()
        {
            //判断是否属于长按
            if (IsDown)
            {
                if (Time.time - LastDownTime >= Delay)
                {
                    Debug.Log("长按");
                    LastDownTime = Time.time;
                    OnButtonPointerOnLongPress?.Invoke(_PressPointerEventData);
                }
            }
        }

    }

}
