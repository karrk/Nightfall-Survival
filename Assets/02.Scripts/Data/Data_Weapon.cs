public enum eCollectionType
{
    None = 0,
    Weapon = 1,
    Acc = 2,
}

public enum eWeaponType
{
    None = 0,
    Sward = 1,
    Wand = 2,
    Knife = 3,
    Clover = 4,
}

public struct Data_Weapon
{
    /// <summary>
    /// 아이템의 고유 인식 번호입니다.
    /// </summary>
    public int ID;
    /// <summary>
    /// 무기의 이름입니다.
    /// </summary>
    public string name;
    /// <summary>
    /// 무기의 피해량입니다.
    /// </summary>
    public float damage;
    /// <summary>
    /// 무기의 속도입니다.
    /// </summary>
    public float speed;
    /// <summary>
    /// 무기의 딜레이입니다.
    /// </summary>
    public float delay;

    public eCollectionType collectType;

    public float duration;

    public float throwCount;

}
