using VS.Base.Popup;

public class Panel_LoginPopup : Base_AnimationPopup
{
    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent(eEventType.ActShowLoginPopup_Intro, ShowPopup);
    }


    private void ShowPopup()
    {
        base.Logic_Open_Base();
    }


    protected override void Logic_Close_CompleteCallback_Custom()
    {
        // 역할 :: 현재 창이 닫힌 경우 인트로 화면을 활성화 시킵니다.
        GameManager.Instance.Event.CallEvent(eEventType.OnReturnTotheScreen_Intro);
    }

    #region 콜백 함수
    /// <summary>
    /// 오프라인 버튼이 클릭된 경우 호출됩니다.
    /// <br> 오프라인으로 게임 접속을 시도합니다. </br>
    /// </summary>
    public void OnClickOffLine()
    {
        base.Logic_Close_Base();

        GameManager.Instance.Event.CallEvent(eEventType.ActConnectGame_Intro, eConnectType.OffLine);
    }

    #endregion
}
