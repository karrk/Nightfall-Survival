using VS.Base.Popup;

public class Panel_PossibilityPopup : Base_AnimationPopup
{
    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent(eEventType.ActShowPossibilityPopup_MainMenu, ShowPopup);
    }

    /// <summary>
    /// [기능] 팝업을 엽니다.
    /// </summary>
    private void ShowPopup()
    {
        base.Logic_Open_Base();
    }

    /// <summary>
    /// [기능] 팝업을 닫습니다.
    /// </summary>
    private void HidePopup()
    {
        base.Logic_Close_Base();
    }

    protected override void Logic_Close_CompleteCallback_Custom()
    {
        GameManager.Instance.Event.CallEvent(eEventType.ActShowMainMenuPanel_MainMenu);
    }


    #region 콜백 함수

    /// <summary>
    /// [버튼콜백] 팝업창을 닫습니다. 
    /// <br> 닫기 버튼이 클릭된 경우 호출됩니다.</br>
    /// </summary>
    public void OnClickClose()
    {
        HidePopup();
    }
    #endregion
}
