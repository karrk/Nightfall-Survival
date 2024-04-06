public enum eCollectionType
{
    None = 0,
    Weapon = 1,
    Acc = 2,
}

public enum eWeaponType
{
    None = 0,
    Sword = 1,
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
    /// <summary>
    /// 장비의 종류를 나타냅니다.
    /// </summary>
    public eCollectionType collectType;
    /// <summary>
    /// 무기사용시 지속시간입니다.
    /// 투사체의 경우 해당값의 영향을 받지 않습니다.
    /// </summary>
    public float duration;
    /// <summary>
    /// 투척되는 무기의 수 입니다.
    /// </summary>
    public float throwCount;

}
