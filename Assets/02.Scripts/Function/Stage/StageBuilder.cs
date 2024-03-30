using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    private List<IStageParts> _parts = new List<IStageParts>();

    private int _stageNum;

    private Map _map;
    private Spawner _spawner;

    private Dictionary<eUnitType, List<Monster>> _monsterTable =
        new Dictionary<eUnitType, List<Monster>>()
        {
            { eUnitType.Common , new List<Monster>() },
            { eUnitType.Named , new List<Monster>() },
            { eUnitType.Boss , new List<Monster>() },
        };

    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, GetParts);
    }

    private void Start()
    {
        GameManager.Instance.Event.CallEvent(eEventType.AddStageParts);
    }

    public void ResetBuilder(bool hardReset)
    {
        if (hardReset)
            Init();

        foreach (var part in _parts)
        {
            part.ObjTr.position = Vector3.zero;
        }
    }

    private void Init()
    {
        foreach (var e in _monsterTable)
        {
            e.Value.Clear();
        }
    }

    public Stage Build()
    {
        return new Stage(_monsterTable, _map, _spawner);
    }

    public void AddPart(IStageParts part)
    {
        _parts.Add(part);
    }

    private void SetMob(int[] mobs)
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            Monster mob = new GameObject().AddComponent<Monster>();
            Data_Monster data = Global_Data.mosnterTable[(eMonsterKind)mobs[i]];
            mob.SetStats(data);

            mob.name = $"Origin_{data.name}";
            mob.transform.parent = this.transform;
            mob.gameObject.SetActive(false);

            _monsterTable[data.type].Add(mob);
        }
    }

    [ContextMenu("test")]
    private void GetParts()
    {
        Init();
        _stageNum = StageManager.Instance.StageNumber;

        foreach (var partComponent in _parts)
        {
            partComponent.SendPart();
        }

        SetMob(Global_Data.stageTable[_stageNum].monsters);
        //SetMob(Global_Data.stageTable[_stageNum].nameds);
        //SetMob(Global_Data.stageTable[_stageNum].bosses);
    }

    public void SetMap(Map map)
    {
        this._map = map;
    }

    public void SetSpawner(Spawner spawner)
    {
        this._spawner = spawner;
    }

}



