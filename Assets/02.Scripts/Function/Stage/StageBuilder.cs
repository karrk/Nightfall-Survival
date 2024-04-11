using System.Collections.Generic;
using System;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    [SerializeField]
    private Character _characterPrefab;

    [SerializeField]
    private GameObject _joyStick;

    private Dictionary<eUnitType, List<Monster>> _monsterTable =
        new Dictionary<eUnitType, List<Monster>>()
        {
            { eUnitType.Common , new List<Monster>() },
            { eUnitType.Named , new List<Monster>() },
            { eUnitType.Boss , new List<Monster>() },
        };

    /// <summary>
    /// 스테이지 ID 에 맞는 스테이지를 구성후 해당 스테이지를 반환합니다.
    /// </summary>
    public Stage Build()
    {
        if (! Global_Data.IsSamePreviousStage)
            SetMonsterTable();
        else
            InitTable();

        Data_StageParts parts = Global_Data.GetStageParts();

        return new Stage(_monsterTable, parts.map, parts.spawner);
    }

    public void InitTable()
    {
        for (int i = 1; i < 4; i++)
        {
            _monsterTable[(eUnitType)i].Clear();
        }
    }

    #region 몬스터 테이블 생성

    private void SetMonsterTable()
    {
        Data_Stage _stageData = Global_Data.stageTable[Global_Data._stageNum];

        SetMob(eUnitType.Common, _stageData.monsters);
        SetMob(eUnitType.Named, _stageData.nameds);
        SetMob(eUnitType.Boss, _stageData.bosses);
    }

    /// <summary>
    /// 등급별 원본 몬스터 테이블 생성
    /// </summary>
    private void SetMob(eUnitType type, long[] mobs)
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            Monster mob = new GameObject().AddComponent<Monster>();
            Data_Monster data = Global_Data.mosnterTable[(eMonsterKind)mobs[i]];
            mob.UnitStat.SetStats(data);

            if (type == eUnitType.Named)
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

    #endregion

}



