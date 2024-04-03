using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using VS.Base.Manager;

public class StageManager : Base_Manager
{
    private static StageManager _instance;
    public static StageManager Instance => _instance;

    [HideInInspector]
    public StageBuilder _stageBuilder;

    private Stage _stage;

    private int _stageNum = -1;
    public int StageNumber => _stageNum;

    private StageLancher _lancher;

    protected override void Logic_Init_Custom() // start
    {
        _instance = this;
        _stageBuilder = GetComponent<StageBuilder>();
        _lancher = GetComponent<StageLancher>();
        GameManager.Instance.Event.CallEvent(eEventType.AddStageParts);
    }

    public void CreateStage(int stageID) // 스테이지 구성호출
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

        GameManager.Instance.Event.CallEvent(eEventType.StageSetupCompleted); // 스테이지 호출이벤트
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //        CreateStage(1);
    //}

    
}



