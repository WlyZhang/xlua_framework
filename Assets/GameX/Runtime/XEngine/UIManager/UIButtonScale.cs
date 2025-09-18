using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CXGame
{

    [DisallowMultipleComponent]
    public class UIButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Selectable SelfButton;
        private Vector3 ScaleFrom;
        [SerializeField] private Vector3 ScaleTo = new Vector3(1.1f, 1.1f, 1.1f);
        private float ScaleTime = 0.2f;

        private void Awake()
        {
            ScaleFrom = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(ScaleTo.x * ScaleFrom.x, ScaleTo.y * ScaleFrom.y, ScaleFrom.z * ScaleTo.z), 0.1f);

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(ScaleFrom, ScaleTime);

        }
    }

}
