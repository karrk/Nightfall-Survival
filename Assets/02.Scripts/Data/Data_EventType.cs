
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

    #region 사용 주의 
    // 해당 이벤트 들은 특정 씬에서만 사용되어지는 이벤트 타입입니다.
    #region Intro
    /// <summary> 인트로의 빈화면으로 돌아갈 경우 호출됩니다. </summary>
    OnReturnTotheScreen_Intro,
    /// <summary> 로그인 팝업창을 켭니다. </summary>
    ActShowLoginPopup_Intro,
    /// <summary> 게임 접속을 시도합니다. </summary>
    ActConnectGame_Intro,
    #endregion

    #region MainMenu

    #endregion
    #endregion


    #region 입력
    /// <summary> 스페이스바가 눌렸을때 호출되는 이벤트 입니다. </summary>
    OnSpacebarPress,
    /// <summary> ESC 버튼이 눌렸을때 호출되는 이벤트 입니다. </summary>
    OnESCPress,
    #endregion

    #region 시스템 기능
    /// <summary> 언어 설정 변경을 시도합니다. </summary>
    SetLanguage,
    #endregion

    #region 네트워크 통신
    /// <summary> 테이블 데이터에 관한 네트워크 응답이 온경우에 반환됩니다.</summary>
    OnResponseData_Table,
    #endregion

    #region 팝업관련
    #region 공통팝업
    /// <summary> 버튼이 없는 형태의 공통 팝업을 설정합니다. 지연시간을 지정할 수 있습니다. </summary>
    SetNoButtonPopup_SetDelay,
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
    AddStageParts,
}
