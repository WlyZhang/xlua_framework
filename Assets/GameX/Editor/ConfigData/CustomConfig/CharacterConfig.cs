public class CharacterConfig : ConfigData
{
    [FieldConfig("����ֵ", 0, 200)]
    public int health;

    [FieldConfig("�ƶ��ٶ�", 1, 10)]
    public float moveSpeed;

    [FieldConfig("������")]
    public int attackPower;
}