using UnityEngine;
using VS.Share;


public enum eSceneKind
{
    None = 0,
}

public class GameManager : MonoBehaviour
{
    #region 매니저 구성
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private Logic_EventSystem _event;
    /// <summary>
    /// 게임매니저에서 관리되는 이벤트시스템을 반환합니다. 
    /// </summary>
    public Logic_EventSystem Event
    {
        get
        {
            if (_event == null)
            {
                _event = new Logic_EventSystem();
            }
            return _event;
        }

    }
    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

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
    /// [기능] 씬 이름을 통해서 씬 전환을 시도합니다. 
    /// </summary>
    /// <param name="add"> 새로운 씬을 갱신하지않고 씬을 중첩하여 생성할지를 지정합니다. </param>
    public void TryChangeScene(string name, bool add = false)
    {
        TryChangeScene(GetSceneKind(name), add);
    }

    /// <summary>
    /// [변환] 씬 이름을 통해서 씬타입을 반환합니다.
    /// </summary>
    public eSceneKind GetSceneKind(string name)
    {
        return eSceneKind.None;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="kind"></param>
    public void TryChangeScene(eSceneKind kind, bool add = false)
    {

    }
    #endregion
}
