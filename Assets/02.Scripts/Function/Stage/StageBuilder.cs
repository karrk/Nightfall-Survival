using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    // 스테이지 빌더의 용도가 아닌 기능만 담김

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
        SetMonsterTable();
        Data_StageParts parts = Global_Data.GetStageParts();

        return new Stage(_monsterTable, parts.map, parts.spawner);
    }

    #region 몬스터 테이블 생성 // 이미있는경우 처리

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
    private void SetMob(eUnitType type, int[] mobs)
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            Monster mob = new GameObject().AddComponent<Monster>();
            Data_Monster data = Global_Data.mosnterTable[(eMonsterKind)mobs[i]];
            mob.UnitStat.SetStats(mob.UnitStat, data);

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



