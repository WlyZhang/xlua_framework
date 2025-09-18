#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.IO;

public class ConfigGeneratorWindow : EditorWindow
{
    private string assetName = "NewConfig"; // 默认资源名称

    [MenuItem("GameX/SDK/配置生成器(ConfigGenerator)")]
    public static void ShowWindow()
    {
        GetWindow<ConfigGeneratorWindow>("动态配置生成器"); // 创建窗口
    }

    void OnGUI()
    {
        GUILayout.Label("配置生成设置", EditorStyles.boldLabel);
        assetName = EditorGUILayout.TextField("资源名称", assetName);

        if (GUILayout.Button("生成配置"))
        {
            GenerateConfig(); // 触发生成逻辑
        }
    }

    private void GenerateConfig()
    {
        // 创建ScriptableObject实例
        CharacterConfig config = CreateInstance<CharacterConfig>();

        // 通过反射获取字段并应用特性
        FieldInfo[] fields = typeof(CharacterConfig).GetFields();
        foreach (FieldInfo field in fields)
        {
            FieldConfigAttribute attribute =
                field.GetCustomAttribute<FieldConfigAttribute>();

            if (attribute != null)
            {
                // 数值字段应用范围限制
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

        // 保存为.asset文件
        string path = Path.Combine("Assets", DirectoryType.DownloadPath, DirectoryType.Package, DirectoryType.ArtType.Config.ToString(), "Resources");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); // 确保目录存在
        }

        path = Path.Combine(path, $"{assetName}.asset");

        AssetDatabase.CreateAsset(config, path);
        AssetDatabase.SaveAssets();
        Debug.Log($"配置已生成: {path}");
    }
}
#endif