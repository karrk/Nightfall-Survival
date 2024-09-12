using System;


/// <summary>
/// 플레이어 연결정보
/// </summary>
public enum eConnectType
{
    None = 0, // 비연결 상태
    OffLine = 1,
    OnLine_Google = 2,
    LogOut = 99
}

/// <summary>
/// [데이터] 플레이어의 전반적인 정보를 구성합니다. 
/// </summary>
public struct Data_Player
{
    #region 플레이어 기본 정보
    /// <summary> 플레이어를 구분짓는 이름입니다. </summary>
    public string ID;
    /// <summary> 외부에서 계정 정보를 구분할때 사용되는 인덱스입니다.</summary>
    public int index;
    /// <summary> 해당 캐릭터를 만든 날짜를 반영합니다. </summary>
    public DateTime accountTime;
    /// <summary> 가장 마지막에 플레이한 날짜를 반영합니다. </summary>
    public DateTime lastPlayTime;
    /// <summary> 현재 접속된 타입을 나타냅니다. </summary>
    public eConnectType currentConnectType;
    #endregion

    #region 진행상황

    #endregion

    #region 캐릭터 정보

    #endregion

    #region 인벤토리 및 재화

    #endregion

    #region 통계 정보 
    /// <summary> 총 플레이한 시간을 반영합니다. </summary>
    public DateTime totalPlayTime;
    public int gamePoint;
    #endregion
}
