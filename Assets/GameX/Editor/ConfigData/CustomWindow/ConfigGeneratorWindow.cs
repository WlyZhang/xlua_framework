#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.IO;

public class ConfigGeneratorWindow : EditorWindow
{
    private string assetName = "NewConfig"; // Ĭ����Դ����

    [MenuItem("GameX/SDK/����������(ConfigGenerator)")]
    public static void ShowWindow()
    {
        GetWindow<ConfigGeneratorWindow>("��̬����������"); // ��������
    }

    void OnGUI()
    {
        GUILayout.Label("������������", EditorStyles.boldLabel);
        assetName = EditorGUILayout.TextField("��Դ����", assetName);

        if (GUILayout.Button("��������"))
        {
            GenerateConfig(); // ���������߼�
        }
    }

    private void GenerateConfig()
    {
        // ����ScriptableObjectʵ��
        CharacterConfig config = CreateInstance<CharacterConfig>();

        // ͨ�������ȡ�ֶβ�Ӧ������
        FieldInfo[] fields = typeof(CharacterConfig).GetFields();
        foreach (FieldInfo field in fields)
        {
            FieldConfigAttribute attribute =
                field.GetCustomAttribute<FieldConfigAttribute>();

            if (attribute != null)
            {
                // ��ֵ�ֶ�Ӧ�÷�Χ����
                if (field.FieldType == typeof(float) || field.FieldType == typeof(int))
                {
                    float value = Mathf.Clamp(
                        Convert.ToSingle(field.GetValue(config)),
                        attribute.MinValue,
                        attribute.MaxValue
                    );
                    field.SetValue(config, Convert.ChangeType(value, field.FieldType));
                }
            }
        }

        // ����Ϊ.asset�ļ�
        string path = Path.Combine("Assets", DirectoryType.DownloadPath, DirectoryType.Package, DirectoryType.ArtType.Config.ToString(), "Resources");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); // ȷ��Ŀ¼����
        }

        path = Path.Combine(path, $"{assetName}.asset");

        AssetDatabase.CreateAsset(config, path);
        AssetDatabase.SaveAssets();
        Debug.Log($"����������: {path}");
    }
}
#endif