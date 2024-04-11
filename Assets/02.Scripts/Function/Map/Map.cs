using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private eMapTileKind _mapKind;
    public eMapTileKind MapName => _mapKind;

    private GameObject[] _tiles;

    public Map(eMapTileKind mapName, GameObject[] tiles)
    {
        this._mapKind = mapName;
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
