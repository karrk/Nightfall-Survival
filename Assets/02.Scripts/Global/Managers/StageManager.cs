using UnityEngine;
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
        GameManager.Instance.Event.RegisterEvent(eEventType.OnGameComplete, LanchGame);
    }

    public void CreateStage(int stageNum) // 스테이지 구성호출
    {
        if (_stage == null || stageNum == _stage.ID)
        {
            this._stageNum = stageNum;
            _stageBuilder.ResetBuilder(true);
            GameManager.Instance.Event.CallEvent(eEventType.StageReady);
        }
        else
            _stageBuilder.ResetBuilder(false);

        this._stage = _stageBuilder.Build();

        _lancher.SetStageData(Global_Data.stageTable[_stageNum]);
        _lancher.SetStage(_stage);
    }

    public void LanchGame()
    {
        _lancher.StageStart();
    }

}