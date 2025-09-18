#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(CharacterConfig))]
public class CharacterConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 获取所有字段
        FieldInfo[] fields = target.GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
            FieldConfigAttribute attribute =
                field.GetCustomAttribute<FieldConfigAttribute>();

            if (attribute == null) continue;

            // 根据字段类型渲染不同控件
            if (field.FieldType == typeof(float))
            {
                float value = EditorGUILayout.Slider(
                    attribute.DisplayName,
                    (float)field.GetValue(target),
                    attribute.MinValue,
                    attribute.MaxValue
                );
                field.SetValue(target, value);
            }
            else if (field.FieldType == typeof(int))
            {
                int value = EditorGUILayout.IntSlider(
                    attribute.DisplayName,
                    (int)field.GetValue(target),
                    (int)attribute.MinValue,
                    (int)attribute.MaxValue
                );
                field.SetValue(target, value);
            }
            else // 默认文本字段
            {
                EditorGUILayout.LabelField(attribute.DisplayName);
                EditorGUILayout.PropertyField(
                    serializedObject.FindProperty(field.Name)
                );
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif