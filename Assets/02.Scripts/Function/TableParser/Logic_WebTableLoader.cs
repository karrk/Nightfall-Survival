using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class Logic_WebTableLoader : MonoBehaviour
{
    /// <summary>
    /// [데이터] 게임 데이터 테이블이 존재하는 구글 스프레드시트 기반 주소입니다.
    /// </summary>
    [Header("GoogleSpreadSheet")]
    [SerializeField] private string URL_googleCommon = "https://docs.google.com/spreadsheets/d/1DSGWwW260IHCuE55DuXsItIYeyT1CuHvXqpmLK33VOA/export?format=tsv&gid=";

    [SerializeField]
    private string gameInfoSheetsURL = "1922427578";

    private Data_DataTableInfo settingData;

    public void LoaderSetting(Data_DataTableInfo m_data)
    {
        settingData = m_data;
    }


    /// <summary>
    /// 언어 설정이 포함된 공통 텍스트 주소를 반환합니다.
    /// </summary>
    private string GetGooleCommonURL(eDataTableType m_type)
    {
        switch (m_type)
        {
            case eDataTableType.GameInfo:
                return URL_googleCommon + gameInfoSheetsURL + "&range=" + "A2:G2";
            case eDataTableType.Stage:
                return URL_googleCommon + settingData.stageTableURL + "&range=" + settingData.stageTableCount;
            case eDataTableType.Monsters:
                return URL_googleCommon + settingData.monsterTableURL + "&range=" + settingData.monsterTableCount;
            case eDataTableType.Text:
                return URL_googleCommon + settingData.textTableURL + "&range=" + settingData.textTableCount;
            default:
                return null;
        }
    }

    public void TryLoadData_GameInfo()
    {
        StartCoroutine(LoadData(eDataTableType.GameInfo));
    }

    public void TryLoadData_StageTable()
    {
        StartCoroutine(LoadData(eDataTableType.Stage));
    }

    public void TryLoadData_MonstersTable()
    {
        StartCoroutine(LoadData(eDataTableType.Monsters));
    }

    public void TryLoadData_TextTable()
    {
        StartCoroutine(LoadData(eDataTableType.Text));
    }


    /// <summary>
    /// [기능] 구글 스프레드 시트에서 데이터 정보를 가져옵니다.
    /// </summary>
    private IEnumerator LoadData(eDataTableType m_kind)
    {
        // returnData :: 스프레드 시트로부터 반환받은 데이터입니다. // 
        string returnData = string.Empty;

        #region 공통 텍스트 설정
        if (!string.IsNullOrEmpty(URL_googleCommon))
        {
            using (UnityWebRequest www = UnityWebRequest.Get(GetGooleCommonURL(m_kind)))
            {
                yield return www.SendWebRequest();
                returnData = www.downloadHandler.text;
                int errorCode = 0;
                string[] dataArray = null;
                switch (www.result)
                {
                    case UnityWebRequest.Result.Success:
                        // -- 데이터 가공 -- //
                        returnData = returnData.Replace("\r\n", "\n");
                        // --------------- // 

                        dataArray = returnData.Split("\n");
                        break;
                    default:
                        dataArray = new string[] { www.result.ToString() };
                        errorCode = -1;
                        break;
                }

                GameManager.Instance.Event.CallEvent(eEventType.OnResponseData_Table, m_kind, dataArray, errorCode);
            }
        }
        #endregion
    }
}