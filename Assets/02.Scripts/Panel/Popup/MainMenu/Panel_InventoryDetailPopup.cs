using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.Base.Popup;
using static UnityEngine.InputSystem.InputSettings;

public class Panel_InventoryDetailPopup : Base_AnimationPopup
{
    [Header("Buttons")]
    [SerializeField]
    private GameObject setBtn;

    [SerializeField]
    private GameObject unsetBtn;


    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent<bool>(eEventType.ActShowInventoryDetailPopup_MainMenu, ShowPopup);
    }

    /// <summary>
    /// [기능] 팝업을 엽니다.
    /// </summary>
    private void ShowPopup(bool isSetting)
    {
        base.Logic_Toggle_Base();

        setBtn.SetActive(isSetting);
        unsetBtn.SetActive(!isSetting);

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
    /// [버튼 콜백] 자세히 버튼을 클릭 시의 이벤트 입니다.
    /// </summary>
    public void OnClickDetailBtn()
    {
        // TODO) 기능 작성
    }

    /// <summary>
    /// [버튼 콜백] 착용 / 해제 버튼을 클릭 시의 이벤트 입니다.
    /// </summary>
    public void OnClickSetUnSetBtn(bool isSetting)
    {
        

        if (isSetting == true)
        {
            // TODO) 기능 작성

        }
        else
        {
            // TODO) 기능 작성
        }
    }

    #endregion
}
