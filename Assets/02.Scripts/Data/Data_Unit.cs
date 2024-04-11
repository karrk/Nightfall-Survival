
/// <summary>
/// 유닛의 일반적인 타입을 지정합니다.
/// </summary>
public enum eUnitType
{
    None = 0,
    Common = 1,
    Named = 2,
    Boss = 3,
}

/// <summary>
/// 유닛의 상태를 지정합니다.
/// </summary>
public enum eUnitStates
{
    None,
    Idle,
    Run,
    Death,
    Attack,
    OnDamage,
}

public class Data_Unit
{
    #region 파싱 데이터
    /// <summary>
    /// 유닛을 구분하는 ID(index) 입니다.
    /// </summary>
    public int ID;
    /// <summary>
    /// 유닛의 이름입니다.
    /// </summary>
    public string name;
    /// <summary>
    /// 유닛의 타입을 구분합니다.
    /// </summary>
    public eUnitType type;
    /// <summary>
    /// 유닛의 체력입니다.
    /// </summary>
    public float hp;
    /// <summary>
    /// 유닛의 피해량입니다.
    /// </summary>
    public float damage;
    /// <summary>
    /// 유닛의 이동속도입니다.
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 유닛의 방어력입니다.
    /// </summary>
    public float armor;


    /// <summary>
    /// 네임드 몬스터로 전환시 부여되는 추가 스탯들 입니다.
    /// </summary>
    public float namedHp;

    public float namedDamage;

    public float namedArmor;


    /// <summary>
    /// 보스 몬스터로 전환시 부여되는 추가 스탯들 입니다.
    /// </summary>
    public float bossHp;

    public float bossDamage;

    public float bossArmor;
    #endregion
}
