using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using VS.Base.Manager;
using VS.Share;


public enum eSceneKind
{
    None = -1,
    Load = 0,
    Intro,
    MainMenu,
    InGame,
}

public class GameManager : MonoBehaviour
{
    #region 매니저 구성
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    /// <summary>
    /// 게임매니저에서 관리되는 이벤트시스템을 반환합니다. 
    /// </summary>
    [HideInInspector]
    public Logic_EventSystem Event = new Logic_EventSystem();

    private eSceneKind _currentScene = eSceneKind.None;
    /// <summary>
    /// 현재 씬의 종류를 나타냅니다. 
    /// </summary>
    public eSceneKind currentScene => _currentScene;

    /// <summary>
    /// 매니저들을 게임매니저가 관리하기위해서 리스트 형태로 캐시합니다.
    /// </summary>
    private List<Base_Manager> managerList;

    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            GameInit();
        }
        else
            Destroy(this.gameObject);
    }


    /// <summary>
    /// [초기화] 최초 1회 실행되며 게임 진행에 필요한 데이터를 설정합니다. 
    /// </summary>
    private void GameInit()
    {
        CheckCurrentScene();

        StartCoroutine(ManagerInitProcedure());
    }

    #region 매니저 관리

    /// <summary>
    /// 매니저 등록 및 초기화 절차를 진행합니다. 
    /// </summary>
    private IEnumerator ManagerInitProcedure()
    {
        // 한 프레임 쉼. 매니저 등록 절차에대한 우선순위를 미루기위함.
        yield return null;

        Managers_Register();

        Managers_Init();
    }

    /// <summary>
    /// TODO :: 해당 기능처럼 직접찾는것보다는, 매니저입장에서 직접등록하는 구조가 더 바람직함..
    /// <br> 짧은 개발 기간을 고려하여 임시로 조치함. </br>
    /// </summary>
    private void Managers_Register() => managerList = GetComponentsInChildren<Base_Manager>().ToList();

    /// <summary>
    /// 매니저들을 초기화시킵니다.
    /// </summary>
    private void Managers_Init()
    {
        foreach (var manager in managerList)
        {
            manager.Logic_Init();
        }
    }
    #endregion

    #region 씬 관리

    /// <summary>
    /// [기능] 씬 인덱스를 통해서 씬 전환을 시도합니다.
    /// </summary>
    /// <param name="add"> 새로운 씬을 갱신하지않고 씬을 중첩하여 생성할지를 지정합니다. </param>
    public void TryChangeScene(int index, bool add = false)
    {
        TryChangeScene((eSceneKind)index, add);
    }

    /// <summary>
    /// [기능] 현재 무슨 씬인지를 확인합니다.
    /// </summary>
    private eSceneKind CheckCurrentScene() => _currentScene = GetSceneKind(SceneManager.GetActiveScene().name);

    /// <summary>
    /// [기능] 씬 타입을 통해서 씬 전환을 시도합니다.
    /// </summary>
    /// <param name="kind"></param>
    public void TryChangeScene(eSceneKind kind, bool add = false)
    {
        TryChangeScene(GetSceneName(kind), add);
    }

    /// <summary>
    /// [기능] 씬 이름을 통해서 씬 전환을 시도합니다. 
    /// </summary>
    /// <param name="add"> 새로운 씬을 갱신하지않고 씬을 중첩하여 생성할지를 지정합니다. </param>
    public void TryChangeScene(string name, bool add = false)
    {
        _currentScene = GetSceneKind(name);

        SceneManager.LoadSceneAsync(name, (add) ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }

    /// <summary>
    /// [변환] 씬 이름을 통해서 씬타입을 반환합니다.
    /// </summary>
    public eSceneKind GetSceneKind(string name)
    {
        switch (name)
        {
            case "Load":
                return eSceneKind.None;
            case "Intro":
                return eSceneKind.Intro;
            case "MainMenu":
                return eSceneKind.MainMenu;
            case "InGame":
                return eSceneKind.InGame;
            default:
                return eSceneKind.None;
        }
    }

    /// <summary>
    /// [변환] 씬 타입을 통해서 씬의 이름을 반환합니다.
    /// </summary>
    public string GetSceneName(eSceneKind kind)
    {
        switch (kind)
        {
            case eSceneKind.Load:
                return "Load";
            case eSceneKind.Intro:
                return "Intro";
            case eSceneKind.MainMenu:
                return "MainMenu";
            case eSceneKind.InGame:
                return "InGame";
            default:
                return string.Empty;
        }
    }


    #endregion
}
