using System;
using System.Collections.Generic;

/// <summary>
/// [종류] text_common에 해당하는 텍스트 종류입니다.
/// </summary>
public enum eTextKind
{
    None = 0,
    // --------- //
    // 팝업 데이터 //
    // --------- //
}


public static class Logic_TextData
{
    /// <summary>
    /// [데이터] 공통으로 사용되어지는 고정 텍스트입니다. 
    /// <br> 해당 텍스트들은 enum으로 관리될수 있습니다. </br>
    /// </summary>
    internal static string[] text_common;

    /// <summary>
    /// [데이터] 일반적으로 사용되어지는 텍스트입니다.
    /// </summary>
    internal static string[] text_basic;

    /// <summary>
    /// [캐시] 모든 텍스트를 담는 리스트입니다.
    /// </summary>
    private static List<Graphic_Text> _textList = new List<Graphic_Text>();


    public static bool IsSettingData() => text_common != null && text_basic != null;

    /// <summary>
    /// [기능] 현재의 언어 설정에 맞쳐서 모든 텍스트를 업데이트합니다.
    /// <br> 언어 설정이 바뀐 경우 호출됩니다.</br>
    /// </summary>
    public static void OnChangeLanguage()
    {
        UpdateAllText();
    }


    /// <summary>
    /// [기능] 텍스트를 매니저에 등록시킵니다.
    /// </summary>
    public static void RegisterText(Graphic_Text m_text)
    {
        _textList.Add(m_text);
    }

    /// <summary>
    /// [기능] 등록된 텍스트를 제거합니다. 
    /// </summary>
    /// <param name="m_text"></param>
    public static void RemoveText(Graphic_Text m_text)
    {
        _textList.Remove(m_text);
    }

    /// <summary>
    /// [기능] 모든 텍스트를 갱신합니다. 
    /// </summary>
    public static void UpdateAllText()
    {
        foreach (var text in _textList)
        {
            text.UpdateText();
        }
    }

    /// <summary>
    /// [설정] 공통 텍스트 리스트를 지정합니다. 
    /// </summary>
    internal static void SetCommonText(string[] m_textList)
    {
        text_common = m_textList;
    }

    /// <summary>
    /// [설정] 일반 텍스트 리스트를 지정합니다. 
    /// </summary>
    internal static void SetBasicText(string[] m_textList)
    {
        text_basic = m_textList;
    }

    /// <summary>
    /// [데이터] 일반적으로 사용되어지는 텍스트를 불러옵니다.
    /// </summary>
    public static string GetText(int m_index)
    {
        try
        {
            return text_basic[m_index];
        }
        catch (System.Exception)
        {
            return "Empty Text";
        }
    }

    /// <summary>
    /// [데이터] 공통적으로 사용되어지는 텍스트를 불러옵니다. 
    /// </summary>
    public static string GetText(eTextKind m_kind)
    {
        try
        {
            return text_common[Convert.ToInt32(m_kind)];
        }
        catch (System.Exception)
        {
            return "Empty Text";
        }
    }

    /// <summary>
    /// [기능] 데이터를 모두 정리합니다.
    /// </summary>
    internal static void ClearData()
    {
        text_common = null;
        text_basic = null;
    }
}
