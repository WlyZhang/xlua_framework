public class CharacterConfig : ConfigData
{
    [FieldConfig("生命值", 0, 200)]
    public int health;

    [FieldConfig("移动速度", 1, 10)]
    public float moveSpeed;

    [FieldConfig("攻击力")]
    public int attackPower;
}