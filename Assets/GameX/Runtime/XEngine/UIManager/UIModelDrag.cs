using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CXGame
{

    public class UIModelDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject Target = null;

        [Header("模型转动参数")]

        float speed = 0.1f;

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Target != null)
            {
                Target.transform.eulerAngles += new Vector3(0, -eventData.delta.x * speed, 0);
            }

        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }

        public void SetModelTarget(GameObject target)
        {
            Target = target;

            Target.transform.eulerAngles = Vector3.zero + new Vector3(0, 180, 0);
        }
        /// <summary>
        /// //排行榜专用
        /// </summary>
        /// <param name="target"></param>
        public void SetModelTarget_Ranking(GameObject target)
        {
            Target = target;

            Target.transform.eulerAngles = Vector3.zero + new Vector3(0, 0, 0);
        }

        // Start is called before the first frame update
        void Start()
        {

        }
    }

}
