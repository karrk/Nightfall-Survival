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
    /// 몬스터가 등장하는 빈도수를 나타냅니다. 한번에 등장하는 수량등, 다양한 형태로 변형해서 사용하세요.
    /// <br> Index :: monsters의 몬스터 배열 순서 </br>
    /// </summary>
    public int[] frequency;
    #endregion
}
