using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.IO;

#if ENABLE_DEBUG
using UnityEditor;
#endif

namespace CXGame
{

    /// <summary>
    /// 处理所有界面
    /// </summary>
    public class UIManager : BaseManager
    {

        public static UIManager Instance { set; get; }

        public override bool IsLoad { get; set; } = false;


        public E_UItype LastUI => m_LastUI;

        public GameObject UIRoot => m_PanelRoot;

        public bool isChatPanelOn = false;

        public bool isinformation;//是否为个人信息页面
        /// <summary>
        /// 储存UI字典
        /// </summary>
        Dictionary<E_UItype, UIDialog> m_UIPanel_Dictionary = null;
        public Dictionary<E_UItype, UIDialog> PanelDic => m_UIPanel_Dictionary;

        /// <summary>
        /// 最后打开的界面记录
        /// </summary>
        E_UItype m_LastUI = E_UItype.eNull;

        /// <summary>
        /// 倒数第二打开的界面记录
        /// </summary>
        public E_UItype m_LastTwoUI = E_UItype.eNull;

        /// <summary>
        /// UI根目录
        /// </summary>
        GameObject m_PanelRoot;

        Stack<E_UItype> m_OpenState = new Stack<E_UItype>(5);


        public static void Create()
        {
            Instance = new UIManager();
        }

        public override async UniTask<bool> Init()
        {
            if(GameObject.FindObjectOfType<Canvas>() == null)
            {
                Debug.LogError($"当前场景无法获取到<Canvas>画布组件.");

                return false;
            }

            m_UIPanel_Dictionary = new Dictionary<E_UItype, UIDialog>();

            m_PanelRoot = GameObject.FindObjectOfType<Canvas>().gameObject;

            IsLoad = true;

            return IsLoad;
        }

        public void PushOpenUI(E_UItype ui)
        {
            if (!m_OpenState.Contains(ui))
                m_OpenState.Push(ui);
        }


        public void ClearPushPanel()
        {
            if (m_OpenState.Count > 0)
                m_OpenState.Clear();
        }

        public async UniTask<T> GetUIPanel<T>(E_UItype panel) where T : UIBasePanel
        {

            if (m_UIPanel_Dictionary.ContainsKey(panel))
            {
                return m_UIPanel_Dictionary[panel] as T;
            }
            else
            {
                UIDialog load_dialog = await LoadUIPanel(panel);

                m_UIPanel_Dictionary.Add(panel, load_dialog);

                return load_dialog as T;
            }
        }



        public void HideAllUI(bool isHideLobbywaitRoom = true)
        {
            foreach (var v in m_UIPanel_Dictionary)
            {
                v.Value.Hide();
            }
            m_LastTwoUI = m_LastUI;

            m_LastUI = E_UItype.eNull;

            //if (isHideLobbywaitRoom)
            //{
            //    m_LastUI = E_UItype.eNull;
            //}
            //else
            //{
            //    m_LastUI = E_UItype.LobbyWaitRoom;
            //}
        }



        /// <summary>
        /// 切换各个UIPanel 
        /// </summary>
        /// <param name="uiType"></param>
        public async void ShowUIPanel(E_UItype uiType, bool closeLastUI = true)
        {
            if (!m_UIPanel_Dictionary.ContainsKey(uiType))
            {
                UIDialog load_dialog = await LoadUIPanel(uiType);

                if (load_dialog != null)
                {
                    m_UIPanel_Dictionary.Add(uiType, load_dialog);
                }
                else
                {
                    Debug.Log("loading... error" + uiType);
                }
            }

            if (closeLastUI)
            {
                HideAllUI();
            }

            if (m_UIPanel_Dictionary.ContainsKey(uiType))
            {
                UIDialog dialog = m_UIPanel_Dictionary[uiType];

                if (dialog.UIComponentInit == false)
                {
                    dialog.InitUIComponent();
                }

                dialog.Show();
            }
        }

        public void HideUIPanel(E_UItype uiType)
        {
            if (uiType != E_UItype.eNull)
            {
                if (m_UIPanel_Dictionary.ContainsKey(uiType))
                {
                    m_UIPanel_Dictionary[uiType].Hide();
                }
            }
        }


        /// <summary>
        /// 添加 载入一个UI Panel
        /// </summary>
        /// <param name="uiType"></param>
        public async UniTask<UIDialog> LoadUIPanel(E_UItype uiType)
        {
            if (uiType == E_UItype.eNull)
                return null;

            if (uiType == m_LastUI)
                return null;

            GameObject p = await LoadPanel(uiType.ToString());

            GameObject uiPanel = GameObject.Instantiate(p);
            //uiPanel.SetActive(false);
            uiPanel.transform.SetParent(this.m_PanelRoot.transform);
            RectTransform rect = uiPanel.GetComponent<RectTransform>();
            rect.anchoredPosition3D = Vector3.zero;
            rect.pivot = Vector2.one * 0.5f;
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            uiPanel.transform.localScale = Vector3.one;
            uiPanel.transform.localPosition = Vector3.zero;
            UIDialog dialog = uiPanel.GetComponent<UIDialog>();

            return dialog;
        }


        /// <summary>
        /// 移除一个UI Panel
        /// </summary>
        /// <param name="uiType"></param>
        public void RemoveUIPanel(E_UItype uiType)
        {
            if (m_UIPanel_Dictionary.ContainsKey(uiType))
            {
                UIDialog dialog = m_UIPanel_Dictionary[uiType];
                m_UIPanel_Dictionary.Remove(uiType);
                GameObject.Destroy(dialog.gameObject);
            }
        }


        private async UniTask<GameObject> LoadPanel(string panelName)
        {
            string fullPath = Path.Combine(DirectoryType.StreamingAssetsPath, DirectoryType.ArtType.UGUI.ToString(), panelName);

            GameObject panel = await AssetLoadManager.Instance.LoadPanelAsync(fullPath, panelName);

            return panel;
        }

        public override void Update(float time)
        {

        }

        public override void Destroy()
        {
            if (m_UIPanel_Dictionary != null)
            {
                m_UIPanel_Dictionary.Clear();
                m_UIPanel_Dictionary = null;
            }
        }
    }
}