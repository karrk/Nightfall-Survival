using UnityEngine;

public class Panel_IntroScene : MonoBehaviour
{
    #region 기본구성
    [SerializeField] private GameObject b_touchScreen;



    #endregion


    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.OnReturnTotheScreen_Intro, ActiveIntroUI);
        GameManager.Instance.Event.RegisterEvent<eConnectType>(eEventType.ActConnectGame_Intro, TryConnectGame);
    }


    /// <summary>
    /// 인트로의 기본 UI를 활성화 시킵니다. 
    /// </summary>
    private void ActiveIntroUI()
    {
        b_touchScreen.SetActive(true);
    }


    private void TryConnectGame(eConnectType type)
    {
        switch (type)
        {
            case eConnectType.OffLine: // 오프라인 접속
                Global_Data.SuccessConnect(type, "temp");
                GameManager.Instance.TryChangeScene(eSceneKind.MainMenu);
                break;

            default:
                break;
        }
    }


    #region 콜백 함수

    public void OnClickScreen()
    {
        b_touchScreen.SetActive(false);

        GameManager.Instance.Event.CallEvent(eEventType.ActShowLoginPopup_Intro);
    }

    #endregion
}
