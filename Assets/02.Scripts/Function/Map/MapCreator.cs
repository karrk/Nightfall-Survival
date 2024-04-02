using System;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    // 동적으로 텍스쳐 변경이 가능한가
    // 두개의 스프라이트 배열을 담아두고 서로 교체하는 방식 
    // [현재 출력되고있는 스프라이트 배열] , [대기중인 스프라이트 배열]
    // => 타일 스프라이트에 각 적용

    readonly string MapDirPath = "Maps/";

    private int _rxc_Count;

    private float tiles_interval;

    private void Awake()
    {
        _rxc_Count = Global_Data.GetRxC_Count();
        Global_Data.SetTextureSize(LoadTexture((eMapTileKind)1).width);
        tiles_interval = GetIntervalDistance();
    }

    private Texture LoadTexture(eMapTileKind mapKind)
    {
        return Resources.Load<Texture>($"{MapDirPath + mapKind.ToString()}");
    }

    public Map GetMap(eMapTileKind map)
    {
        Texture texture = LoadTexture(map);
        Sprite[] splitSprites = DevideTexure(texture);
        Map newMap = new Map(map, CreateTileObj(splitSprites));

        return newMap;
    }

    private Sprite[] DevideTexure(Texture texture)
    {
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

    private GameObject[] CreateTileObj(Sprite[] sprites)
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

    private void SetTileOptions(GameObject tile, int row, int column)
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

    private float GetIntervalDistance()
    {
        return (Global_Data.GetTextureSize() * 0.01f) * 1 / _rxc_Count;
    }

}
