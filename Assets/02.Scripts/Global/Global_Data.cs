using System.Collections.Generic;
using UnityEngine;

public class Global_Data : MonoBehaviour
{
    #region 동적 데이터
    private static Data_Player _player;

    private static Data_GameOption _option = new Data_GameOption();

    public static int _prevStageNum = 0;
    public static int _stageNum = 1;

    private static Stage _stage = null;

    public static int _mobLimitCouns = 50;

    #region 맵 관련 변수
    public static int _textureSize;

    public static int _mapTileDevideCount = 4;

    public static Vector3 _MapSize = Vector3.zero;

    private static Data_StageParts _stageParts;
    #endregion

    /// <summary>
    /// [기능] 언어 설정을 변경합니다.
    /// </summary>
    public static void SetLanguage(eLanguageKind m_kind)
    {
        _option.language = m_kind;

        GameManager.Instance.Event.CallEvent(eEventType.SetLanguage);
    }
    /// <summary>
    /// [기능] 해상도 설정을 변경합니다.
    /// </summary>
    public static void SetResolution()
    {
        _option.resolution = new Vector2(Screen.width, Screen.height);

        GameManager.Instance.Event.CallEvent(eEventType.SetResolution);
    }

    /// <summary>
    /// [기능] 연결에 성공된 경우 거기에 알맞는 데이터를 변경합니다.
    /// </summary>
    public static void SuccessConnect(eConnectType m_type, string m_playerID)
    {
        _player.currentConnectType = m_type;
        _player.ID = m_playerID;
    }

    /// <summary>
    /// [기능] 현재 언어의 캐릭터 타입을 반환합니다. 스프레드 데이터 구분에서만 사용됩니다.
    /// </summary>
    public static string GetLanguageChar()
    {
        switch (_option.language)
        {
            case eLanguageKind.KR:
                return "B";
            case eLanguageKind.EN:
                return "C";
            case eLanguageKind.JP:
                return "D";
            case eLanguageKind.ZH_CN:
                goto case eLanguageKind.KR;
            //return "E";
            case eLanguageKind.ZH_TW:
                goto case eLanguageKind.KR;
            //return "F";
            default:
                goto case eLanguageKind.KR;
        }
    }

    #region 스테이지 구성요소 함수

    public static Data_StageParts GetStageParts()
    {
        return _stageParts;
    }

    public static Stage GetStage()
    {
        return _stage;
    }

    public static void SetMap(Map map)
    {
        _stageParts.map = map;
    }

    public static void SetSpawner(Spawner spawner)
    {
        _stageParts.spawner = spawner;
    }

    public static void SetStage(Stage stage)
    {
        _stage = stage;
    }

    #endregion


    #endregion


    #region 정적 데이터
    private static Dictionary<eMonsterKind, Data_Monster> _mosnterTable = new Dictionary<eMonsterKind, Data_Monster>();

    public static Dictionary<eMonsterKind, Data_Monster> mosnterTable => _mosnterTable;


    private static Dictionary<int, Data_Stage> _stageTable = new Dictionary<int, Data_Stage>();

    public static Dictionary<int, Data_Stage> stageTable => _stageTable;


    private static Dictionary<int, Data_Weapon> _weaponTable = new Dictionary<int, Data_Weapon>();

    public static Dictionary<int, Data_Weapon> weaponTable => _weaponTable;


    private static Dictionary<int, Data_Character> _characterTable = new Dictionary<int, Data_Character>();

    public static Dictionary<int, Data_Character> characterTable => _characterTable;
    #endregion 
}
