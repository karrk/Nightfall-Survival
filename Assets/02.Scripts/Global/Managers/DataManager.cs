using System;
using System.Text.RegularExpressions;
using UnityEngine;
using VS.Base.Manager;

/// <summary>
/// 데이터 테이블의 종류를 나타냅니다.
/// </summary>
public enum eDataTableType
{
    None = -1,
    GameInfo = 0,
    Stage = 1,
    Monsters = 2,
    Text = 10,
}

/// <summary>
/// [매니저] 여러 파일로부터 데이터를 가공하여 Global_Data에서 사용 될 수 있는 형태로 저장합니다.
/// </summary>
public class DataManager : Base_Manager
{
    /// <summary>
    /// 데이터 테이블의 전반적인 정보를 담고 있습니다. 파싱하는 과정에서 필요한 데이터입니다. 
    /// </summary>
    private Data_DataTableInfo _dataTableInfo = new Data_DataTableInfo();

#if DevelopeMode
    [SerializeField]
    private Logic_WebTableLoader _tableLoader;
#else
    [SerializeField]
    private Logic_CsvConvert _tableLoader;
#endif

    private void Awake()
    {
        transform.GetChild(0).TryGetComponent(out _tableLoader);
        if (_tableLoader != null) _tableLoader.TryLoadData_GameInfo();

        // --- 이벤트 등록 --- //
        GameManager.Instance.Event.RegisterEvent<eDataTableType, string[], int>(eEventType.OnResponseData_Table, OnResponseData);
        // ------------------- //
    }

    /// <summary>
    /// [기능] 모든 데이터 테이블을 가져와 파싱을 시도합니다.
    /// </summary>
    public void LoadData()
    {
        LoadData_StageTable();
        LoadData_MonsterTbale();
    }

    /// <summary>
    /// [기능] 스테이지 테이블을 불러와 Global_Data를 갱신합니다.
    /// </summary>
    public void LoadData_StageTable()
    {
        _tableLoader.TryLoadData_StageTable();
    }

    /// <summary>
    /// [기능] 몬스터 테이블을 불러와 Global_Data를 갱신합니다.
    /// </summary>
    public void LoadData_MonsterTbale()
    {
        _tableLoader.TryLoadData_MonstersTable();
    }

    /// <summary>
    /// [기능] 몬스터 테이블을 불러와 Global_Data를 갱신합니다.
    /// </summary>
    public void LoadData_TextTbale()
    {
        _tableLoader.TryLoadData_TextTable();
    }


    private void OnResponseData(eDataTableType m_type, string[] m_dataArray, int m_errorCode)
    {
        if (m_errorCode < 0)
        {
            // TODO :: 에러메시지 출력 (데이터를 불러오는 과정에서 에러가 발생하였습니다.)
            // m_dataArray[0]에 메시지 타입이 담겨있음.
            return;
        }

        switch (m_type)
        {
            case eDataTableType.GameInfo:
                _tableLoader.LoaderSetting(Convert_GameInfo(m_dataArray));

                LoadData();
                break;
            case eDataTableType.Stage:
                Convert_StageTable(m_dataArray);
                break;
            case eDataTableType.Monsters:
                Convert_MonsterTable(m_dataArray);
                break;
            case eDataTableType.Text:
                break;
            default:
                // TODO :: 에러메시지 출력 (설정되지않은 데이터 타입이 지정되었습니다.)
                break;
        }
    }

    #region 데이터 파싱
    private Data_DataTableInfo Convert_GameInfo(string[] m_dataArray)
    {
        string[] resultData = m_dataArray[0].Split("\t");

        _dataTableInfo.version = resultData[0];
        _dataTableInfo.stageTableCount = resultData[1];
        _dataTableInfo.stageTableURL = resultData[2];
        _dataTableInfo.monsterTableCount = resultData[3];
        _dataTableInfo.monsterTableURL = resultData[4];
        _dataTableInfo.textTableCount = resultData[5];
        _dataTableInfo.textTableURL = resultData[6];

        return _dataTableInfo;
    }


    private void Convert_StageTable(string[] m_dataArray)
    {
        Global_Data.stageTable.Clear();

        for (int i = 0; i < m_dataArray.Length; i++)
        {
            Data_Stage parsingData = new Data_Stage();
            string[] dataSegment = m_dataArray[i].Split("\t");

            parsingData.index = int.Parse(dataSegment[0]);
            parsingData.tileKind = (eMapTileKind)Enum.Parse(typeof(eMapTileKind), dataSegment[1]);

            string[] tempStringData = dataSegment[2].Split(",");
            parsingData.monsters = new int[tempStringData.Length];
            for (int j = 0; j < tempStringData.Length; j++)
            {
                parsingData.monsters[j] = int.Parse(tempStringData[j]);
            }

            tempStringData = dataSegment[3].Split(",");
            parsingData.frequency = new int[tempStringData.Length];
            for (int j = 0; j < tempStringData.Length; j++)
            {
                parsingData.frequency[j] = int.Parse(tempStringData[j]);
            }

            Global_Data.stageTable.Add(parsingData.index, parsingData);
        }
    }

    private void Convert_MonsterTable(string[] m_dataArray)
    {
        Global_Data.mosnterTable.Clear();

        for (int i = 0; i < m_dataArray.Length; i++)
        {
            Data_Monster parsingData = new Data_Monster();
            string[] dataSegment = m_dataArray[i].Split("\t");

            parsingData.ID = int.Parse(dataSegment[0]);
            parsingData.kind = (eMonsterKind)parsingData.ID;
            parsingData.name = dataSegment[1];
            parsingData.type = (eUnitType)Enum.Parse(typeof(eUnitType), dataSegment[2]);
            parsingData.hp = float.Parse(dataSegment[3]);
            parsingData.damage = float.Parse(dataSegment[4]);
            parsingData.moveSpeed = float.Parse(dataSegment[5]);
            parsingData.armor = float.Parse(dataSegment[6]);

            Global_Data.mosnterTable.Add(parsingData.kind, parsingData);
        }
    }

    /// <summary>
    /// 범위 데이터를 사용하여 데이터의 총 크기를 구합니다.
    /// <br> 입력되는 데이터 양식은 A2:B10 같은 형태여야합니다. </br>
    /// </summary>
    private int GetCountData(string m_data)
    {
        int totalCount = 0;

        string[] tempData = m_data.Split(":");

        // 설명 :: A2:B10 데이터 A2부분의 숫자만을 추출하여 totalCount에 담습니다.
        totalCount = int.Parse(Regex.Replace(tempData[0], @"\D", ""));
        // 설명 :: A2:B10 데이터 중 B10 부분의 숫자만을 추출하여 기존 길이값을 빼서 총 길이값을 구합니다.
        totalCount = int.Parse(Regex.Replace(tempData[1], @"\D", "")) - totalCount;

        return totalCount;
    }
    #endregion
}