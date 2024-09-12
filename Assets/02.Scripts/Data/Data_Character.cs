
public enum eCharacterKind
{
    None = 0,
    A = 1,
    B = 2,
    C = 3,
    D = 4,
}

public class Data_Character : Data_Unit
{
    #region 파싱 데이터

    /// <summary>
    /// 캐릭터의 종류를 나타냅니다.
    /// </summary>
    public eCharacterKind kind;
    /// <summary>
    /// 캐릭터가 지닌 기본무기 입니다.
    /// </summary>
    public eWeaponType weapon;
    /// <summary>
    /// 캐릭터의 회복률 입니다.
    /// </summary>
    public float recoverHp;

    /// <summary>
    /// 캐릭터의 행운 입니다.
    /// </summary>
    public float lucky;

    /// <summary>
    /// 캐릭터의 투사체 갯수 입니다.
    /// </summary>
    public float throwCount;

    /// <summary>
    /// 캐릭터의 투사체 속도 입니다.
    /// </summary>
    public float throwSpeed;

    /// <summary>
    /// 캐릭터가 사용하는 투사체의 범위 입니다.
    /// </summary>
    public float attackRange;

    /// <summary>
    /// 캐릭터의 투척 쿨타임 입니다.
    /// </summary>
    public float attackDelay;

    /// <summary>
    /// 캐릭터의 투사체 지속시간입니다.
    /// </summary>
    public float attackDuration;

    /// <summary>
    /// 캐릭터의 추가 획득 경험치 비율입니다.
    /// </summary>
    public float bonusExp;

    /// <summary>
    /// 캐릭터의 추가 획득 골드 비율입니다.
    /// </summary>
    public float bonusGold;

    /// <summary>
    /// 캐릭터의 회피율 입니다.
    /// </summary>
    public float avoidRate;

    #endregion
}
