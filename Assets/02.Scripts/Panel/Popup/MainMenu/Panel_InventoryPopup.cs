using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.Base.Popup;
using static UnityEngine.InputSystem.InputSettings;

public class Panel_InventoryPopup : Base_AnimationPopup
{
    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent(eEventType.ActShowInventoryPopup_MainMenu, ShowPopup);
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

    protected override void Logic_Close_CompleteCallback_Custom()
    {

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
    /// [버튼 콜백] 아이템 슬롯을 클릭 시 자세히 팝업창을 엽니다.
    /// <br> 해당 아이템이 착용했는지의 여부에 따라 적절한 버튼을 활성화합니다. </br>
    /// </summary>
    public void OnClickItemSlot(bool isSetting)
    {
        GameManager.Instance.Event.CallEvent<bool>(eEventType.ActShowInventoryDetailPopup_MainMenu, isSetting);
    }

    #endregion
}
