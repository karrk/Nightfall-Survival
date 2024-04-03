
public enum eCharacterKind
{
    None = 0,

}

public class Data_Character : Data_Unit
{
    #region 파싱 데이터

    /// <summary>
    /// 몬스터의 종류를 나타냅니다.
    /// </summary>
    public eCharacterKind kind;

    #endregion
}
