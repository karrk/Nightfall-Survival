using UnityEngine;
using VS.Base.Popup;

public class Panel_MainMenu : Base_AnimationPopup
{

    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent(eEventType.ActShowMainMenuPanel_MainMenu, ShowPanel);
    }


    /// <summary>
    /// [기능] 패널을 노출시킵니다.
    /// </summary>
    private void ShowPanel()
    {
        Logic_Open_Base();
    }

    /// <summary>
    /// [기능] 패널을 숨깁니다.
    /// </summary>
    private void HidePanel()
    {
        Logic_Close_Base();
    }


    #region 콜백 함수
    /// <summary>
    /// 게임 시작 버튼이 눌린 경우 호출됩니다. 슬롯 선택 화면으로 전환됩니다. 
    /// </summary>
    public void OnClickGameStart()
    {
        HidePanel();
        GameManager.Instance.Event.CallEvent(eEventType.ActShowSelectSlotPopup_MainMenu);
    }

    /// <summary>
    /// 현재 보유재화를 바탕으로 기능을 강화할수 있는 창을 엽니다.
    /// </summary>
    public void OnClickUpgrade()
    {
        HidePanel();
        GameManager.Instance.Event.CallEvent(eEventType.ActShowUpgradePopup_MainMenu);
    }

    /// <summary>
    /// 현재 보여중인 업적 및 스킬, 보상등을 열람 할 수 있는 창을 엽니다.
    /// </summary>
    public void OnClickCollection()
    {
        HidePanel();
        GameManager.Instance.Event.CallEvent(eEventType.ActShowCollectionPopup_MainMenu);
    }

    /// <summary>
    /// 잠겨있는 기능들을 개방 할 수 있는 관리창을 엽니다.
    /// </summary>
    public void OnClickPossibility()
    {
        HidePanel();
        GameManager.Instance.Event.CallEvent(eEventType.ActShowPossibilityPopup_MainMenu);
    }

    /// <summary>
    /// 옵션 버튼이 클릭된경우 호출됩니다. 옵션창을 엽니다.
    /// </summary>
    public void OnClickOption()
    {
        HidePanel();
        GameManager.Instance.Event.CallEvent(eEventType.ActShowOptionPopup);
    }

    /// <summary>
    /// 개발 정보 및 게임 설명이 담겨있는 크레딧 화면으로 전환이 됩니다.
    /// </summary>
    public void OnClickCredit()
    {
        HidePanel();
        GameManager.Instance.Event.CallEvent(eEventType.ActShowCredit_MainMenu);
    }

    /// <summary>
    /// 게임을 종료시킵니다.
    /// </summary>
    public void OnClickGameQuit()
    {
        Application.Quit();
    }

    #endregion
}
