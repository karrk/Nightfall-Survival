using UnityEngine;

public enum eLanguageKind
{
    /// <summary> [디폴트] 한국어 </summary>
    KR,
    /// <summary> 영어 </summary>
    EN,
    /// <summary> 일본어 </summary>
    JP,
    /// <summary> 중국어(간체) </summary>
    ZH_CN,
    /// <summary> 중국어(번체) </summary>
    ZH_TW,
}


/// <summary>
/// 사운드, 게임 부수적 기능, 해상도 등등 게임의 설정 값을 구성하고 있는 데이터입니다.
/// </summary>
public class Data_GameOption
{
    /// <summary>
    /// 현재 설정된 언어를 나타냅니다.
    /// </summary>
    public eLanguageKind language = eLanguageKind.KR;

    public Vector2 resolution = Vector2.zero;
}
