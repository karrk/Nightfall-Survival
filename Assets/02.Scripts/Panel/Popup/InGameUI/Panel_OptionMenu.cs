using VS.Base.Popup;

public class Panel_OptionMenu : Base_AnimationPopup
{
    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent(eEventType.ActToggleOptionMenu_InGame, ShowPopup);
    }

    /// <summary>
    /// [기능] 팝업을 엽니다.
    /// </summary>
    private void ShowPopup()
    {
        base.Logic_Toggle_Base();
    }

    /// <summary>
    /// [기능] 팝업을 닫습니다.
    /// </summary>
    private void HidePopup()
    {
        base.Logic_Close_Base();
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

    /// <summary>
    /// [버튼콜백] 메인 메뉴 씬으로 되돌아갑니다.
    /// </summary>
    public void OnClickMainMenu()
    {
        GameManager.Instance.TryChangeScene(eSceneKind.MainMenu);
        HidePopup();
    }

    /// <summary>
    /// [버튼콜백] 옵션창을 켭니다.
    /// </summary>
    public void OnClickOption()
    {
        GameManager.Instance.Event.CallEvent(eEventType.ActShowOptionPopup);
        HidePopup();
    }

    /// <summary>
    /// [버튼콜백] 도움말 창을 켭니다.
    /// </summary>
    public void OnClickHelp()
    {
        HidePopup();
    }

    /// <summary>
    /// [버튼콜백] 게임을 저장합니다.
    /// </summary>
    public void OnClickSave()
    {
        HidePopup();
    }
    #endregion
}
