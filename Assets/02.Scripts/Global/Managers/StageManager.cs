using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;

    public static StageManager Instance => _instance;

    [HideInInspector]
    public StageBuilder _stageBuilder;

    private int _stageNum;

    public int StageNumber => _stageNum;

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

    private void Start()
    {
        _stageBuilder = GetComponent<StageBuilder>();
        GameManager.Instance.Event.CallEvent(eEventType.StageReady);
    }

    public void SetStageNumber(int num)
    {
        this._stageNum = num;
    }
}


