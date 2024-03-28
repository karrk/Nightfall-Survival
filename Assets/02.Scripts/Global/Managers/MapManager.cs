using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;

    public static MapManager Instance => _instance;

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

    [SerializeField]
    MapCreator _creator;

    Map _currentMap;

    public Map GetMap(string mapName)
    {
        if (_currentMap != null && _currentMap.MapName == mapName)
            return _currentMap;

        return _creator.GetMap(mapName);
    }
}


