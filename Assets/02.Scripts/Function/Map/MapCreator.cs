using System;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    // 동적으로 텍스쳐 변경이 가능한가

    readonly string MapDirPath = "Maps/";

    int _rxc_Count;
    float tiles_interval;

    bool isInitialized = false;

    public Map GetMap(string mapName)
    {
        if (!isInitialized)
        {
            _rxc_Count = SettingManager.Instance.RxC_Count;
            tiles_interval = GetIntervalDistance();
        }

        Sprite[] splitSprites = DevideTexure(mapName);
        Map newMap = new Map(mapName, CreateTileObj(splitSprites));

        return newMap;
    }

    Sprite[] DevideTexure(string mapName)
    {
        Texture texture = Resources.Load<Texture>($"{MapDirPath + mapName}");

        Sprite[] tempArr = new Sprite[(int)Math.Pow(_rxc_Count, 2)];

        for (int i = 0; i < _rxc_Count; i++)
        {
            for (int j = 0; j < _rxc_Count; j++)
            {
                Rect rect = new Rect(
                    (texture.width / _rxc_Count) * j,
                    (texture.height / _rxc_Count) * (_rxc_Count - 1 - i),
                    texture.width / _rxc_Count, texture.height / _rxc_Count);

                tempArr[(i * _rxc_Count) + j] = Sprite.Create((Texture2D)texture, rect, Vector2.up);
            }
        }

        return tempArr;
    }

    GameObject[] CreateTileObj(Sprite[] sprites)
    {
        GameObject[] tempArr = new GameObject[(int)Math.Pow(_rxc_Count, 2)];

        for (int i = 0; i < _rxc_Count; i++)
        {
            for (int j = 0; j < _rxc_Count; j++)
            {
                GameObject tile = new GameObject();
                tile.transform.SetParent(this.transform);
                SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
                renderer.sortingOrder = -10000;

                Sprite sprite = sprites[(i * _rxc_Count) + j];

                renderer.sprite = sprite;
                SetTileOptions(tile, i, j);

                tempArr[(i * _rxc_Count) + j] = tile;
            }
        }

        return tempArr;
    }

    void SetTileOptions(GameObject tile, int row, int column)
    {
        tile.name = $"tile{(row * _rxc_Count) + column}";

        tile.transform.position +=
            Vector3.right * tiles_interval * column;

        tile.transform.position +=
            Vector3.down * tiles_interval * row;

        if (column >= _rxc_Count / 2)
            tile.transform.position += Vector3.left * (_rxc_Count) * tiles_interval;

        if (row >= _rxc_Count / 2)
            tile.transform.position += Vector3.up * (_rxc_Count) * tiles_interval;

        tile.AddComponent<BoxCollider2D>().isTrigger = true;
        tile.tag = "Map";

        tile.AddComponent<TileMovement>();
        TileMovement.TileSize = GetIntervalDistance();
    }

    float GetIntervalDistance()
    {
        return (SettingManager.Instance.MapTextureSize * 0.01f) * 1 / _rxc_Count;
    }

}
