
/// <summary>
/// [종류] 이벤트 타입의 종류를 나타냅니다.
/// <br> 추후 이벤트 타입을 추가로 사용하실경우 해당 열거형을 추가하여 사용하시면 됩니다. </br>
/// <br> 기존 이벤트 시스템이 해당 eEventType 으로만 작동하겠금 폐쇄적으로 설계되어있기때문에  </br>
/// <br> 해당 타입 이외의 열거형으로는 작동하지 않습니다.</br>
/// </summary>
public enum eEventType
{
    Test,

    #region 게임 진행

    #endregion

    #region 입력
    /// <summary> 스페이스바가 눌렸을때 호출되는 이벤트 입니다. </summary>
    OnSpacebarPress,
    /// <summary> ESC 버튼이 눌렸을때 호출되는 이벤트 입니다. </summary>
    OnESCPress,
    #endregion


    #region 팝업관련
    #region 공통팝업
    /// <summary> 버튼이 없는 형태의 공통 팝업을 설정합니다. </summary>
    SetNoButtonPopup,
    /// <summary> 확인 버튼만 있는 형태의 공통 팝업을 설정합니다. </summary>
    SetOneButtonPopup,
    /// <summary> 확인과 취소 버튼이 있는 공통 팝업을 설정합니다.</summary>
    SetTwoButtonPopup,
    #endregion

    #region 팝업조작
    /// <summary> 모든 열려있는 팝업창을 닫습니다.</summary>
    ActAllClosePopup,

    #endregion
    #endregion
    StartGame,
    EndGame,
    StageSetupCompleted,
    StageReady,
}
