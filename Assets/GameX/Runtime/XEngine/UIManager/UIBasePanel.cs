using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;


namespace CXGame
{
    public abstract class UIBasePanel : UIDialog
    {
        /// <summary>
        /// 返回Panel UI 类型
        /// </summary>
        public abstract E_UItype UIType { get; }


        [SerializeField][TextArea(3, 50)] string UIStructText;

        StringBuilder _StringBuilder = null;


        private void UIComponentStructString()
        {
            _StringBuilder = new StringBuilder();

            _StringBuilder.Clear();

            _StringBuilder.Append("[System.Serializable]");
            _StringBuilder.Append($"struct {transform.name}UIComponent\n");
            _StringBuilder.Append("{\n");

            foreach (Transform trans in transform)
            {
                string uiFullname = trans.name;
                string[] splistStrs = trans.name.Split('_');

                if (splistStrs.Length > 1)
                {
                    string uiComTypeName = splistStrs[1];

                    _StringBuilder.Append($"    public {uiComTypeName} {uiFullname};\n");
                }
            }

            _StringBuilder.Append("}\n");

            _StringBuilder.Append($"{transform.name}UIComponent _{transform.name}UIComponent;\n");

            UIStructText = _StringBuilder.ToString();

            _StringBuilder.Clear();
            _StringBuilder = null;
        }


        private void UIComponentInitString()
        {
            _StringBuilder = new StringBuilder();

            _StringBuilder.Clear();


            foreach (Transform trans in transform)
            {
                string uiFullname = trans.name;
                string[] splistStrs = trans.name.Split('_');

                if (splistStrs.Length > 1)
                {
                    string uiComTypeName = splistStrs[1];

                    _StringBuilder.Append($"_{transform.name}UIComponent.{uiFullname} = _GetComponent<{uiComTypeName}>(\"{uiFullname}\");\n");
                }
            }

            UIStructText = _StringBuilder.ToString();

            _StringBuilder.Clear();
            _StringBuilder = null;
        }
    }
}
