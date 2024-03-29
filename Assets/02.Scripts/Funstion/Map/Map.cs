using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private string _mapName;
    public string MapName => _mapName;

    GameObject[] _tiles;

    public Map(string mapName, GameObject[] tiles)
    {
        this._mapName = mapName;
        this._tiles = tiles;
    }

    public void ActiveTiles(bool active)
    {
        if (_tiles[0].activeSelf == active)
            return;

        foreach (var tile in _tiles)
        {
            tile.SetActive(active);
        }
    }
}
