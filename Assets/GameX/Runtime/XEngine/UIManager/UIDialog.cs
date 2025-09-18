using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CXGame
{

    public abstract class UIDialog : MonoBehaviour
    {

        [SerializeField] public bool UIComponentInit = false;


        public virtual void Awake()
        {
            InitUIComponent();
        }


        public virtual void Start()
        {
            RegisterUIEvent();

        }

        public virtual void OnDestroy()
        {
            UnRegisterUIEvent();
            CloseAnimation();
        }

        public virtual async void Show()
        {
            while (UIComponentInit == false)
            {
                await UniTask.Yield();
            }

            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }


        public bool IsShow => gameObject.activeSelf;

        /// <summary>
        /// 初始化UI组件
        /// </summary>
        public abstract void InitUIComponent();

        /// <summary>
        /// 注册UI事件
        /// </summary>
        protected abstract void RegisterUIEvent();

        /// <summary>
        /// 销毁注册事件
        /// </summary>
        protected abstract void UnRegisterUIEvent();

        /// <summary>
        /// 界面打开时的动画
        /// </summary>
        protected virtual void ShowAnimation() { }

        /// <summary>
        /// 关闭界面时的动画
        /// </summary>
        protected virtual void CloseAnimation() { }

        protected T _GetComponent<T>(string nodePath)
        {
            return transform.Find(nodePath).GetComponent<T>();
        }
    }
}
