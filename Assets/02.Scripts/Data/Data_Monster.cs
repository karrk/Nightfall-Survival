/// <summary>
/// 몬스터의 종류를 나타냅니다. 해당 열거형의 인덱스는 몬스터 테이블의 ID에 해당합니다.
/// <br> 몬스터 테이블에서 데이터가 변동된 경우에는 해당 eMosnterKind의 값도 동기화해주는 과정이 필요합니다. </br>
/// </summary>
public enum eMonsterKind
{
    None = 0,
    Orc = 1,
    Fox = 2,
    Tiger = 3,
}


public class Data_Monster : Data_Unit
{
    /// <summary>
    /// 몬스터의 종류를 나타냅니다.
    /// </summary>
    public eMonsterKind kind;
}
