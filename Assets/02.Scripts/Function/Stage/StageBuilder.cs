<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public Data_Stage _stageData;

    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, GetParts);
    }

    public void ResetBuilder(bool hardReset)
    {
        if (hardReset)
            Init();

        foreach (var part in _parts)
        {
            part.StagePartTransform.position = Vector3.zero;
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

    private void SetMob(eUnitType type,int[] mobs)
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            Monster mob = new GameObject().AddComponent<Monster>();
            Data_Monster data = Global_Data.mosnterTable[(eMonsterKind)mobs[i]];
            mob.UnitStat.SetStats(mob.UnitStat,data);

            if(type == eUnitType.Named)
            {
<<<<<<< HEAD
                mob.UnitStat.AddHp(data.namedHp)
                            .AddDamage(data.namedDamage)
                            .AddArmor(data.namedArmor);
=======
                mob.Stat.AddHp(data.namedHp)
                    .AddDamage(data.namedDamage)
                    .AddArmor(data.namedArmor);
>>>>>>> 6b9376b (#1.3)
            }

            else if (type == eUnitType.Boss)
            {
<<<<<<< HEAD
                mob.UnitStat.AddHp(data.bossHp)
                            .AddDamage(data.bossDamage)
                            .AddArmor(data.bossArmor);
=======
                mob.Stat.AddHp(data.bossHp)
                    .AddDamage(data.bossDamage)
                    .AddArmor(data.bossArmor);
>>>>>>> 6b9376b (#1.3)
            }

            mob.name = $"Origin_{type}_{data.name}";
            mob.transform.parent = this.transform;
            mob.gameObject.SetActive(false);

            _monsterTable[type].Add(mob);
        }
    }

    private void GetParts()
    {
        Init();
        _stageNum = StageManager.Instance.StageNumber;
        _stageData = Global_Data.stageTable[_stageNum];

        GetMonsterTable();

        foreach (var partComponent in _parts)
        {
            partComponent.SendPart();
        }
    }

    private void GetMonsterTable()
    {
        SetMob(eUnitType.Common,_stageData.monsters);
        SetMob(eUnitType.Named,_stageData.nameds);
        SetMob(eUnitType.Boss,_stageData.bosses);
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



=======
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public Data_Stage _stageData;

    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, GetParts);
    }

    public void ResetBuilder(bool hardReset)
    {
        if (hardReset)
            Init();

        foreach (var part in _parts)
        {
            part.StagePartTransform.position = Vector3.zero;
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

    private void SetMob(eUnitType type,int[] mobs)
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            Monster mob = new GameObject().AddComponent<Monster>();
            Data_Monster data = Global_Data.mosnterTable[(eMonsterKind)mobs[i]];
            mob.UnitStat.SetStats(mob.UnitStat,data);

            if(type == eUnitType.Named)
            {
                mob.UnitStat.AddHp(data.namedHp)
                            .AddDamage(data.namedDamage)
                            .AddArmor(data.namedArmor);
            }

            else if (type == eUnitType.Boss)
            {
                mob.UnitStat.AddHp(data.bossHp)
                            .AddDamage(data.bossDamage)
                            .AddArmor(data.bossArmor);
            }

            mob.name = $"Origin_{type}_{data.name}";
            mob.transform.parent = this.transform;
            mob.gameObject.SetActive(false);

            _monsterTable[type].Add(mob);
        }
    }

    private void GetParts()
    {
        Init();
        _stageNum = StageManager.Instance.StageNumber;
        _stageData = Global_Data.stageTable[_stageNum];

        GetMonsterTable();

        foreach (var partComponent in _parts)
        {
            partComponent.SendPart();
        }
    }

    private void GetMonsterTable()
    {
        SetMob(eUnitType.Common,_stageData.monsters);
        SetMob(eUnitType.Named,_stageData.nameds);
        SetMob(eUnitType.Boss,_stageData.bosses);
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



>>>>>>> b757374 (# 1.4)
