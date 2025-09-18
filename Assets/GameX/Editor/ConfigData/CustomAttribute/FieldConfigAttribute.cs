using System;

[AttributeUsage(AttributeTargets.Field)] // 仅允许应用于字段
public class FieldConfigAttribute : Attribute
{
    public string DisplayName { get; } // 字段在编辑器中的显示名称
    public float MinValue { get; }     // 数值字段的最小值
    public float MaxValue { get; }     // 数值字段的最大值

    public FieldConfigAttribute(string displayName, float min = 0, float max = 100)
    {
        DisplayName = displayName;
        MinValue = min;
        MaxValue = max;
    }
}