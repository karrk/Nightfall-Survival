using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return _monsterTable[type][tableIndex];
    }

}
