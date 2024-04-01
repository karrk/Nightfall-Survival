using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public static StageManager Instance => _instance;

    [HideInInspector]
    public StageBuilder _stageBuilder;

    private Stage _stage;

    private int _stageNum = -1;
    public int StageNumber => _stageNum;

    private StageLancher _lancher;

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
        _lancher = GetComponent<StageLancher>();
    }

    public void CreateStage(int stageID)
    {
        if (_stage == null || stageID == _stage.ID)
        {
            this._stageNum = stageID;
            _stageBuilder.ResetBuilder(true);
            GameManager.Instance.Event.CallEvent(eEventType.StageReady);
        }
        else
            _stageBuilder.ResetBuilder(false);

        this._stage = _stageBuilder.Build();

        _lancher.SetStageData(Global_Data.stageTable[_stageNum]);
        _lancher.SetStage(_stage);

        GameManager.Instance.Event.CallEvent(eEventType.StageSetupCompleted);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CreateStage(1);
    }
}



