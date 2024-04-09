using UnityEngine;
using VS.Base.Manager;

public class StageManager : Base_Manager
{
    private static StageBuilder _stageBuilder;

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

    public static void SetCharacter(eCharacterKind kind)
    {
        //Global_Data._character = _stageBuilder.GetCharacter(kind);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //GameManager.Instance.Event.CallEvent(eEventType.StageReady);
            //GameManager.Instance.Event.CallEvent(eEventType.StageSetupCompleted);
            //GameManager.Instance.Event.CallEvent(eEventType.OnGameComplete);
        }
    }

}