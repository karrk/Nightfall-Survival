using UnityEngine;
using VS.Base.Manager;

public class StageManager : Base_Manager
{
    [HideInInspector]
    public StageBuilder _stageBuilder;

    private int _stageNum = -1;
    public int StageNumber => _stageNum;

    private StageLancher _lancher;

    protected override void Logic_Init_Custom()
    {
        _stageBuilder = GetComponent<StageBuilder>();
        _lancher = GetComponent<StageLancher>();
        GameManager.Instance.Event.RegisterEvent(eEventType.OnGameComplete, LanchGame);
        GameManager.Instance.Event.RegisterEvent(eEventType.StageSetupCompleted, CreateStage);
    }

    public void CreateStage()
    {
        Global_Data.SetStage(_stageBuilder.Build());

        _lancher.SetStageData();
        _lancher.SetStage();
    }

    public void LanchGame()
    {
        _lancher.StageStart();
    }

}