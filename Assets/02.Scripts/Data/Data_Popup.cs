using System;

/// <summary>
/// [데이터] 공통 팝업을 사용할때 필요한 데이터 정보를 구성한 자료형입니다.
/// </summary>
public class Data_Popup
{
    #region 기본 구성
    /// <summary> 팝업의 제목 </summary>
    public string title { get; set; }

    /// <summary> 제목의 텍스트 넘버 </summary>
    public int titleNumber = -1;

    /// <summary> 팝업의 내용 </summary>
    public string content { get; set; }

    /// <summary> 내용의 텍스트 넘버 </summary>
    public int contentNumber = -1;

    /// <summary> 팝업이 클릭된 후 호출될 함수 </summary>
    public Action callback { get; set; }
    #endregion

    /// <summary>
    /// [생성자] 제목, 내용, 콜백 함수를 데이터로 팝업 내용을 구성합니다.
    /// </summary>
    public Data_Popup(string m_title, string m_content, Action m_callback = null)
    {
        title = m_title;
        content = m_content;
        callback = m_callback;
    }

    /// <summary>
    /// [생성자] 제목, 내용, 콜백 함수를 데이터로 팝업 내용을 구성합니다.
    /// </summary>
    public Data_Popup(int m_title, string m_content, Action m_callback = null)
    {
        titleNumber = m_title;
        content = m_content;
        callback = m_callback;
    }

    /// <summary>
    /// [생성자] 제목, 내용, 콜백 함수를 데이터로 팝업 내용을 구성합니다.
    /// </summary>
    public Data_Popup(string m_title, int m_content, Action m_callback = null)
    {
        title = m_title;
        contentNumber = m_content;
        callback = m_callback;
    }

    /// <summary>
    /// [생성자] 제목, 내용, 콜백 함수를 데이터로 팝업 내용을 구성합니다.
    /// </summary>
    public Data_Popup(int m_title, int m_content, Action m_callback = null)
    {
        titleNumber = m_title;
        contentNumber = m_content;
        callback = m_callback;
    }

    /// <summary>
    /// [생성자] 내용, 콜백 함수를 데이터로 팝업 내용을 구성합니다.
    /// </summary>
    public Data_Popup(string m_content, Action m_callback = null)
    {
        title = string.Empty;
        content = m_content;
        callback = m_callback;
    }

    /// <summary>
    /// [생성자] 내용, 콜백 함수를 데이터로 팝업 내용을 구성합니다.
    /// </summary>
    public Data_Popup(int m_content, Action m_callback = null)
    {
        title = string.Empty;
        contentNumber = m_content;
        callback = m_callback;
    }

}
