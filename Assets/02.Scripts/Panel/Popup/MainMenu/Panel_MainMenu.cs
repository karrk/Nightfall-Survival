using UnityEngine;
using VS.Base.Popup;

public class Panel_MainMenu : Base_AnimationPopup
{


    #region 콜백 함수
    /// <summary>
    /// 게임 시작 버튼이 눌린 경우 호출됩니다. 슬롯 선택 화면으로 전환됩니다. 
    /// </summary>
    public void OnClickGameStart()
    {
        //TODO :: 임시로 작성됨 추후, 슬롯 선택 로직이 들어가야함
        GameManager.Instance.TryChangeScene(eSceneKind.InGame);
    }

    /// <summary>
    /// 현재 보유재화를 바탕으로 기능을 강화할수 있는 창을 엽니다.
    /// </summary>
    public void OnClickUpgrade()
    {

    }

    /// <summary>
    /// 현재 보여중인 업적 및 스킬, 보상등을 열람 할 수 있는 창을 엽니다.
    /// </summary>
    public void OnClickCollection()
    {

    }

    /// <summary>
    /// 잠겨있는 기능들을 개방 할 수 있는 관리창을 엽니다.
    /// </summary>
    public void OnClickPossibility()
    {

    }

    /// <summary>
    /// 옵션 버튼이 클릭된경우 호출됩니다. 옵션창을 엽니다.
    /// </summary>
    public void OnClickOption()
    {

    }

    /// <summary>
    /// 개발 정보 및 게임 설명이 담겨있는 크레딧 화면으로 전환이 됩니다.
    /// </summary>
    public void OnClickCredit()
    {

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
