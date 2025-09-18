using System;

[AttributeUsage(AttributeTargets.Field)] // ������Ӧ�����ֶ�
public class FieldConfigAttribute : Attribute
{
    public string DisplayName { get; } // �ֶ��ڱ༭���е���ʾ����
    public float MinValue { get; }     // ��ֵ�ֶε���Сֵ
    public float MaxValue { get; }     // ��ֵ�ֶε����ֵ

    public FieldConfigAttribute(string displayName, float min = 0, float max = 100)
    {
        DisplayName = displayName;
        MinValue = min;
        MaxValue = max;
    }
}