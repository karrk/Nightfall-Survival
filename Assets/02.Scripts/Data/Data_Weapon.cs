public enum eCollectionType
{
    None = 0,
    Weapon = 1,
    Acc = 2,
    Combine = 3,
}

public enum eWeaponType
{
    None = 0,
    A = 1,
    B = 2,
    C = 3,
    D = 4,
    E = 5,
    F = 6,
    G = 7,
    H = 8,
    I = 9,
    J = 10,
    K = 11,
    L = 12,
    M = 13,
    N = 14,
    O = 15,
    P = 16,
    Q = 17,
    R = 18,
    S = 19,
}

//public enum eWeaponFuncs
//{
//    None,
//    Targeting,
//    Continuous,
//    CollEffect,
//    MobCollision,
//    ScreenCollision,
//    Reflection,
//    NeedDir,
//    CtrlDir,
//    FlexiblePath,
//    Rotatable,
//    Falling,
//    SpecipicStartPos,
    
//    Fire
//}

public enum eWeaponProcess
{
    None,
    CheckFireType,
    FireProcess,
    PostProcess,
    ProcessFinish,
}

public struct Data_Weapon_Stats
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
    /// <summary>
    /// 조합되는 무기 번호 입니다.
    /// </summary>
    public int combineWeaponID;
    /// <summary>
    /// 관통 가능 횟수입니다.
    /// </summary>
    public float passCount;
    /// <summary>
    /// 무기 최대 레벨입니다.
    /// </summary>
    public int maxLevel;
    /// <summary>
    /// 무기 레벨업시 습득하는 추가데미지입니다.
    /// </summary>
    public float[] addtionalDamages;
    /// <summary>
    /// 무기 레벨업시 습득하는 추가이동속도입니다.
    /// </summary>
    public float[] addtionalMoveSpeeds;
    /// <summary>
    /// 무기 레벨업시 습득하는 추가작동시간입니다.
    /// </summary>
    public float[] addtionalDurations;
    /// <summary>
    /// 무기 레벨업시 습득하는 쿨타임감소시간입니다.
    /// </summary>
    public float[] reducionDelays;
    /// <summary>
    /// 무기 레벨업시 습득하는 추가 투사체수입니다.
    /// </summary>
    public float[] addtionalThrowCount;
    /// <summary>
    /// 무기 레벨업시 습득하는 추가 관통수입니다.
    /// </summary>
    public float[] addtionalPassCount;
}

public struct Data_Weapon_Properties
{
    /// <summary>
    /// 무기 번호입니다.
    /// </summary>
    public int ID;
    /// <summary>
    /// 무기의 작동방식이 타겟팅으로 진행되는지 확인합니다.
    /// </summary>
    public bool isTargeting;
    /// <summary>
    /// 무기의 생명주기가 없는지 확인합니다.
    /// </summary>
    public bool isContinuous;
    /// <summary>
    /// 다른 오브젝트에 접촉할경우 이후 처리과정이 필요한지 나타냅니다.
    /// </summary>
    public bool hasPostProcess;
    /// <summary>
    /// 몬스터와의 충돌일경우 후처리를 작동시킵니다.
    /// </summary>
    public bool isCollisionMonster;
    /// <summary>
    /// 스크린 각 모서리와 충돌일경우 후처리를 작동시킵니다.
    /// </summary>
    public bool isCollisionScreen;
    /// <summary>
    /// 후처리를 반사형태로 진행합니다.
    /// </summary>
    public bool hasReflection;
    /// <summary>
    /// 무기의 발사 방향의 필요성을 확인합니다.
    /// </summary>
    public bool isNeedDir;
    /// <summary>
    /// 캐릭터의 움직임으로 투사체의 방향을 조종할수 있는지 확인합니다.
    /// </summary>
    public bool isControllableDir;
    /// <summary>
    /// 투사체 격발후 유동적인 움직임이 있는지 확인합니다.
    /// </summary>
    public bool hasFlexiblePath;
    /// <summary>
    /// 무기의 작동형태가 회전형인지 확인합니다.
    /// </summary>
    public bool isRotate;
    /// <summary>
    /// 무기의 작동형태가 낙하형인지 확인합니다.
    /// </summary>
    public bool isFalling;
    /// <summary>
    /// 특수한 시작지점이 있는지 확인합니다.
    /// </summary>
    public bool hasSpecificStartPos;
}
