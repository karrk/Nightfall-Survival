using System.Collections;
using UnityEngine;
using VS.Base.Popup;

public class Panel_NoButtonPopup : Base_AnimationPopup
{
    /// <summary>
    /// 팝업의 제목에 해당하는 텍스트 입니다.
    /// </summary>
    #region 팝업 기본 구성
    [Header("공용 팝업 구성")]
    [SerializeField] private Graphic_Text _t_title;

    /// <summary>
    /// 팝업의 내용에 해당하는 텍스트입니다.
    /// </summary>
    [SerializeField] private Graphic_Text _t_content;

    [Range(2f, 10f)]
    [Tooltip("팝업이 자동으로 닫히기까지 걸리는 시간을 지정합니다.")]
    [SerializeField] private float _closeDelayTime = 3f;

    /// <summary>
    /// 자동으로 닫아주는 코루틴 함수입니다.
    /// </summary>
    private Coroutine AutoClosePopup;
    #endregion


    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent<Data_Popup>(eEventType.SetNoButtonPopup, ShowPopup);
        GameManager.Instance.Event.RegisterEvent<Data_Popup, float>(eEventType.SetNoButtonPopup_SetDelay, ShowPopup);
    }


    /// <summary>
    /// [기능] 팝업의 내용을 변경합니다.
    /// </summary>
    private void SetPopup(Data_Popup m_data, float timer)
    {
        _cb_complete = m_data.callback;
        _closeDelayTime = timer;

        if (m_data.titleNumber != -1)
        { // 조건 :: 텍스트 넘버가 설정된 경우
            _t_title.SetText(m_data.titleNumber);
        }
        else
        { // 조건 :: 직접 텍스트가 입력된 경우
            _t_title.SetText(m_data.title);
        }

        if (m_data.titleNumber != -1)
        { // 조건 :: 텍스트 넘버가 설정된 경우
            _t_content.SetText(m_data.contentNumber);
        }
        else
        { // 조건 :: 직접 텍스트가 입력된 경우
            _t_content.SetText(m_data.content);
        }
    }

    /// <summary>
    /// [기능] 팝업창을 엽니다.
    /// </summary>
    private void ShowPopup(Data_Popup m_data)
    {
        ShowPopup(m_data, _closeDelayTime);
    }

    /// <summary>
    /// [기능] 지연시간을 지정하여 팝업창을 엽니다.
    /// </summary>
    private void ShowPopup(Data_Popup m_data, float m_timer)
    {
        ClearPopup();

        SetPopup(m_data, m_timer);

        base.Logic_Open_Base();

        AutoClosePopup = StartCoroutine(DelayClosePopup());
    }

    /// <summary>
    /// 공통 팝업이 이미 열려있는 경우 기존 팝업을 닫습니다.
    /// </summary>
    private void ClearPopup()
    {
        if (AutoClosePopup != null)
        {
            StopCoroutine(AutoClosePopup);
            AutoClosePopup = null;
            ClosePopup();
        }
    }

    /// <summary>
    /// [기능] 일정 시간이 도달한후 팝업이 닫히도록 합니다.
    /// </summary>
    private IEnumerator DelayClosePopup()
    {
        yield return new WaitForSeconds(_closeDelayTime);

        ClosePopup();
    }


    /// <summary>
    /// [기능] 팝업창을 닫습니다.
    /// </summary>
    private void ClosePopup()
    {
        base.Logic_Close_Base(_cb_complete);
    }
}
