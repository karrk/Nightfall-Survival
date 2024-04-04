/// <summary>
/// 맵의 타일 종류를 나타냅니다.
/// </summary>
public enum eMapTileKind
{
    None = 0,
    /// <summary> 평지 </summary>
    Ground = 1,
    /// <summary> 도시 </summary>
    City = 2,
}

public enum eStageEventType
{
    None = 0,
    /// <summary> 스테이지 몬스터 스폰 쿨타임을 감소 </summary>
    ReduceSpawnTime = 1,
    /// <summary> 몬스터를 원형으로 스폰 </summary>
    CircleSpawn = 2,
    /// <summary> 몬스터를 폭발적으로 스폰 </summary>
    BunningSpawn = 3,
}

/// <summary>
/// [데이터] 스테이지에대한 정보를 구성합니다. 
/// </summary>
public struct Data_Stage
{
    #region 파싱 데이터
    /// <summary>
    /// 외부에서 Stage를 구분할때 사용되어지는 인덱스입니다. 
    /// </summary>
    public int index;

    /// <summary>
    /// 해당 스테이지의 지형 종류를 나타냅니다.
    /// </summary>
    public eMapTileKind tileKind;

    /// <summary>
    /// 해당 스테이지에서 등장하는 몬스터의 종류입니다.
    /// <br> Index :: monsters의 몬스터 배열 순서 </br>
    /// </summary>
    public int[] monsters;

    /// <summary>
    /// 해당 스테이지에서 등장하는 네임드 몬스터의 종류입니다.
    /// <br> Index :: monsters의 몬스터 배열 순서 </br>
    /// /// </summary>
    public int[] nameds;

    /// <summary>
    /// 해당 스테이지에서 등장하는 보스 몬스터의 종류입니다.
    /// <br> Index :: monsters의 몬스터 배열 순서 </br>
    /// /// </summary>
    public int[] bosses;

    /// <summary>
    /// 해당 스테이지의 클리어 시간입니다
    /// </summary>
    public int clearTime;

    /// <summary>
    /// 해당 스테이지 클리어 시 주어지는 보상 목록입니다.
    /// </summary>
    public int[] rewards;

    /// <summary>
    /// 해당 스테이지에서 제공될 메인 이벤트 시간 목록입니다.
    /// <br> Index :: 메인이벤트의 시간 배열 순서</br>
    /// </summary>
    public int[] mainEventTimes;

    /// <summary>
    /// 해당 스테이지에서 제공될 메인 보스몬스터 목록입니다.
    /// <br> Index :: 메인이벤트의 보스몬스터 배열 순서</br>
    /// </summary>
    public int[] mainEventBossMobs;

    /// <summary>
    /// 해당 스테이지에서 제공될 서브 이벤트 시간 목록입니다.
    /// <br> Index :: 서브이벤트의 시간 배열 순서</br>
    /// </summary>
    public int[] subEventTimes;

    /// <summary>
    /// 해당 스테이지에서 제공될 네임드몬스터 목록입니다.
    /// <br> Index :: 서브이벤트의 네임드몬스터 배열 순서</br>
    /// </summary>
    public int[] subEventNamedMobs;

    /// <summary>
    /// 해당 스테이지에서 제공될 특정 이벤트 시간 목록입니다.
    /// <br> Index :: 특정이벤트의 시간 배열 순서</br>
    /// </summary>
    public int[] specialEventTimes;

    /// <summary>
    /// 해당 스테이지에서 제공될 특정 이벤트 목록입니다.
    /// <br> Index :: 특정이벤트의 타입 배열 순서</br>
    /// </summary>
    public int[] specialEventTypes;

    /// <summary>
    /// 해당 스테이지에서 현재목록의 시간으로 몬스터의 출현을 결정합니다.
    /// <br> Index :: 몬스터 출현을 바꾸는 시간 배열 순서</br>
    /// </summary>
    public int[] mobChangeTimes;

    /// <summary>
    /// 해당 스테이지에서 어떤 몬스터를 출현시킬지 결정하는 목록입니다.
    /// <br> Index :: 어떤 몬스터들을 스폰할지 결정하는 배열 순서</br>
    /// </summary>
    public int[] mobChanges;

    /// <summary>
    /// 해당 값마다 일반몬스터 출현빈도시간을 줄여줍니다.
    /// </summary>
    public int branchDelay;

    /// <summary>
    /// 해당 스테이지의 시작과 끝의 출현 빈도수를 담는 목록입니다.
    /// <br> Index :: Index[0] 시작시 설정되는 일반 몬스터 출현빈도 
    /// Index[1] 게임이 끝날시 설정 되어야할 일반 몬스터 출현빈도</br>
    /// </summary>
    public int[] startEndSpawnDelay;

    /// <summary>
    /// 해당 스테이지의 BranchDelay의 시간이 지날때마다 줄어들어야할 출현 빈도수 값입니다.
    /// </summary>
    public float spawnDelayInterval;

    ///// <summary>
    ///// 몬스터가 등장하는 빈도수를 나타냅니다. 한번에 등장하는 수량등, 다양한 형태로 변형해서 사용하세요.
    ///// <br> Index :: monsters의 몬스터 배열 순서 </br>
    ///// </summary>
    //public int[] frequency;


    #endregion
}

public struct Data_StageParts
{
    /// <summary>
    /// 스테이지의 구성요소의 목록
    /// </summary>

    public Map map;

    public Spawner spawner;
}
