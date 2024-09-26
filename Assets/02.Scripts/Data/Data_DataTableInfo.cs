public class Data_DataTableInfo
{
    #region 파싱 데이터
    /// <summary>
    /// 데이터 테이블의 버전 정보를 나타냅니다.
    /// </summary>
    public string version;

    /// <summary>
    /// 몬스터 테이블의 총 갯수입니다.
    /// </summary>
    public string monsterTableCount;

    public string monsterTableURL;

    /// <summary>
    /// 스테이지 테이블의 총 갯수입니다.
    /// </summary>
    public string stageTableCount;

    public string stageTableURL;

    /// <summary>
    /// 무기 테이블의 총 갯수입니다.
    /// </summary>
    public string weaponTableCount;

    public string weaponTableURL;

    /// <summary>
    /// 공통 텍스트 테이블의 총 갯수입니다. 
    /// </summary>
    public int[] commonTextTableCount;

    public string commonTextTableURL;


    /// <summary>
    /// 기본 텍스트 테이블의 총 갯수입니다. 
    /// </summary>
    public int[] basicTextTableCount;

    public string basicTextTableURL;
    #endregion
}
