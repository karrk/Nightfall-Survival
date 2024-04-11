using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.Share;

public class InputManager : MonoBehaviour
{
    #region 매니저 구성
    private static InputManager _instance;

    public static InputManager Instance => _instance;

    private Logic_EventSystem _event;
    /// <summary>
    /// 인풋매니저에서 관리되는 이벤트시스템을 반환합니다. 
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


}
