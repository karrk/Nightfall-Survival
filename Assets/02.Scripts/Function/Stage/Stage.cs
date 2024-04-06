using System.Collections.Generic;

public class Stage
{
    private int _id;
    public int ID => _id;

    private Dictionary<eUnitType, List<Monster>> _monsterTable;

    public Spawner Spawner => _spawner;

    private Map _map;
    private Spawner _spawner;

    public Stage(Dictionary<eUnitType, List<Monster>> table, Map map, Spawner spawner)
    {
        this._monsterTable = table;
        _map = map;
        _spawner = spawner;
    }

    public Monster GetOriginMonster(eUnitType type, int tableIndex)
    {
        return _monsterTable[type][tableIndex - 1];
    }

    public int GetMonsterCount(eUnitType type = eUnitType.None)
    {
        int count = 0;

        if (type == eUnitType.None)
        {
            foreach (var e in _monsterTable)
                count += e.Value.Count;

            return count;
        }

        return _monsterTable[type].Count;
    }

    public List<Monster> GetMonsterList(eUnitType type)
    {
        return _monsterTable[type];
    }

}
