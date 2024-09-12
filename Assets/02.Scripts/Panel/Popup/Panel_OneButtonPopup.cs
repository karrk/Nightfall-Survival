using UnityEngine;
using VS.Base.Popup;

public class Panel_OneButtonPopup : Base_AnimationPopup
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
    #endregion


    protected override void Logic_Init_Base()
    {
        base.Logic_Init_Base();

        GameManager.Instance.Event.RegisterEvent<Data_Popup>(eEventType.SetOneButtonPopup, ShowPopup);
    }

    /// <summary>
    /// [기능] 팝업의 내용을 변경합니다.
    /// </summary>
    private void SetPopup(Data_Popup m_data)
    {
        _cb_complete = m_data.callback;

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
        ClearPopup();

        SetPopup(m_data);

        base.Logic_Open_Base();
    }

    /// <summary>
    /// 공통 팝업이 이미 열려있는 경우 기존 팝업을 닫습니다.
    /// </summary>
    private void ClearPopup()
    {
        if (CheckIsActivePopup())
        {
            ClosePopup();
        }
    }

    /// <summary>
    /// [기능] 팝업창을 닫습니다.
    /// </summary>
    private void ClosePopup()
    {
        OnClickOkay();
    }


    #region 콜백함수
    /// <summary>
    /// [버튼콜백] 확인 버튼이 눌린 경우 호출됩니다.
    /// </summary>
    public void OnClickOkay()
    {
        base.Logic_Close_Base(_cb_complete);
    }
    #endregion
}
