using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    Map _map;
    Spawner _spawner;

    public Stage(Map map, Spawner spawner)
    {
        _map = map;
        _spawner = spawner;
    }
}
