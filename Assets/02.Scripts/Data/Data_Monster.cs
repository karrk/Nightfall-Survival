/// <summary>
/// 몬스터의 종류를 나타냅니다. 해당 열거형의 인덱스는 몬스터 테이블의 ID에 해당합니다.
/// <br> 몬스터 테이블에서 데이터가 변동된 경우에는 해당 eMosnterKind의 값도 동기화해주는 과정이 필요합니다. </br>
/// </summary>
public enum eMonsterKind
{
    None = 0,
    Bat = 1,
    Bear = 2,
    Bee = 3,
    Boar,
    Crab,
    Duck,
    Flower,
    Frog,
    Pig,
    Rat,
    Scorpion,
    Spider,
    Stump,
    Tortoise,
    Wolf,
}


public class Data_Monster : Data_Unit
{
    #region 파싱 데이터

    /// <summary>
    /// 몬스터의 종류를 나타냅니다.
    /// </summary>
    public eMonsterKind kind;

    /// <summary>
    /// 몬스터를 처치시 주어지는 현상금입니다.
    /// </summary>
    public int bounty;

    #endregion
}
