using UnityEngine;

public class Panel_InGameUI : MonoBehaviour
{


    #region 콜백 함수

    /// <summary>
    /// [버튼콜백] 옵션 메뉴창을 켭니다.
    /// </summary>
    public void OnClickOptionMenu()
    {
        GameManager.Instance.Event.CallEvent(eEventType.ActToggleOptionMenu_InGame);
    }

    /// <summary>
    /// [버튼콜백] 캐릭터 정보창을 켭니다.
    /// </summary>
    public void OnClickCharInfo()
    {

    }

    /// <summary>
    /// [버튼콜백] 소지품 창을 켭니다.
    /// </summary>
    public void OnClickInventory()
    {

    }

    /// <summary>
    /// [버튼콜백] 스테이지 정보창을 켭니다.
    /// </summary>
    public void OnClickStageInfo()
    {

    }

    /// <summary>
    /// [버튼콜백] 여유.. 임시
    /// </summary>
    public void OnClickTemp()
    {

    }

    #endregion
}
