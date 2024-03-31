using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public static StageManager Instance => _instance;

    [HideInInspector]
    public StageBuilder _stageBuilder;

    private Stage _stage;

    private int _stageNum;
    public int StageNumber => _stageNum;

    private ObjPoolManager _poolManager;

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
        _poolManager = FindObjectOfType<ObjPoolManager>();
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
    }

    private void Temp()
    {
        GameObject mob = _poolManager.GetObj(ePoolingType.Monster);
        Monster origin = _stage.GetOriginMonster(eUnitType.Common, 0);
        origin.Stat.StatCopy(mob.GetComponent<Monster>().Stat);
        _stage.Spawner.RandomSpawn(mob);
    }
}



