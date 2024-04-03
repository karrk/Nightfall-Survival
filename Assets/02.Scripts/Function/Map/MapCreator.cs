using System;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    // 동적으로 텍스쳐 변경이 가능한가
    // 두개의 스프라이트 배열을 담아두고 서로 교체하는 방식 
    // [현재 출력되고있는 스프라이트 배열] , [대기중인 스프라이트 배열]
    // => 타일 스프라이트에 각 적용

    readonly string MapDirPath = "Maps/";

    private Vector3 _halfScreenSize;
    private Vector3 _createOffsetVec;

    private int _devideCount;
    private float _tiles_interval;

    private int _rowCount;
    private int _columnCount;

    private void Awake()
    {
        _devideCount = Global_Data.GetMapDevideCount();
        Global_Data.SetTextureSize(LoadTexture((eMapTileKind)1).width);
        _tiles_interval = GetIntervalDistance();
    }

    public void Init()
    {
        _halfScreenSize = GetScreenViewScale() / 2;
        _createOffsetVec = GetOffset();

        _columnCount = CheckDevideCount(_halfScreenSize.x);
        _rowCount = CheckDevideCount(_halfScreenSize.y);

        // 해상도 1920*1080 rc = 4 일때 88개 타일생성
        //Debug.Log($"{_halfScreenSize.x} {_halfScreenSize.y}"); 
        //Debug.Log($"{_columnCount} {_rowCount}");

        this.transform.position = GetCreateStartPos();
    }

    public int CheckDevideCount(float screenHalfSize)
    {
        float limit = (screenHalfSize * 2) + (_tiles_interval * 4);

        int count = 4;

        while (true)
        {
            if (_tiles_interval * count >= limit)
                return count;

            count++;
        }
    }

    /// <summary>
    /// 전달받은 텍스쳐파일을 분할하여 배열로 저장합니다.
    /// </summary>
    private Sprite[] DevideTexure(Texture texture)
    {
        Sprite[] tempArr = new Sprite[(int)MathF.Pow(_devideCount,2)];

        for (int i = 0; i < _devideCount; i++)
        {
            for (int j = 0; j < _devideCount; j++)
            {
                Rect rect = new Rect(
                    (texture.width / _columnCount) * j,
                    (texture.height / _devideCount) * (_devideCount - 1 - i),
                    texture.width / _devideCount, texture.height / _devideCount);

                tempArr[(i * _devideCount) + j] = Sprite.Create((Texture2D)texture, rect, Vector2.up);
            }
        }

        return tempArr;
    }

    /// <summary>
    /// 분할된 스프라이트를 오브젝트화 시키고, 생성된 배열에 담아줍니다.
    /// </summary>
    private GameObject[] CreateTileObj(Sprite[] sprites)
    {
        GameObject[] tempArr = new GameObject[_rowCount * _columnCount];

        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _columnCount; j++)
            {
                GameObject tile = new GameObject();
                tile.transform.SetParent(this.transform);
                SpriteRenderer tileRenderer = tile.AddComponent<SpriteRenderer>();
                tileRenderer.sortingOrder = -10000;

                //Sprite sprite = sprites[(i * _rxc_Count) + j];
                Sprite sprite 
                    = sprites[((i * _devideCount) % sprites.Length) + (j % _devideCount)];

                tileRenderer.sprite = sprite;
                //SetTileOptions(tile, i, j);

                //tempArr[(i * _rxc_Count) + j] = tile;
                tempArr[(i * _columnCount)+j] = tile;
            }
        }

        return tempArr;
    }

    /// <summary>
    /// 타일오브젝트의 좌표설정, 옵션설정
    /// </summary>
    private void SetTileOptions(GameObject[] tiles)
    {
        GameObject tile;
        
        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _columnCount; j++)
            {
                tile = tiles[(i * _columnCount) + j];
                tile.name = $"tile{(i * _columnCount) + j}";

                float posX = _tiles_interval * j;
                float posY = -1 * _tiles_interval * i;

                tile.transform.position = this.transform.position + new Vector3(posX,posY);

                tile.AddComponent<BoxCollider2D>().isTrigger = true;
                tile.tag = "Map";

                tile.AddComponent<TileMovement>();
                TileMovement.TileSize = _tiles_interval;
            }
        }

        //tile.name = $"tile{(row * _rxc_Count) + column}";

        //tile.transform.position +=
        //    Vector3.right * tiles_interval * column;

        //tile.transform.position +=
        //    Vector3.down * tiles_interval * row;

        //if (column >= _rxc_Count / 2)
        //    tile.transform.position += Vector3.left * (_rxc_Count) * tiles_interval;

        //if (row >= _rxc_Count / 2)
        //    tile.transform.position += Vector3.up * (_rxc_Count) * tiles_interval;

        //tile.AddComponent<BoxCollider2D>().isTrigger = true;
        //tile.tag = "Map";

        //tile.AddComponent<TileMovement>();
        //TileMovement.TileSize = GetIntervalDistance();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Init();
            Sprite[] sprites = DevideTexure(LoadTexture((eMapTileKind)1));
            GameObject[] tiles = CreateTileObj(sprites);
            //SetTileOptions(tiles);
        }
            
    }

    private Vector3 GetCreateStartPos()
    {
        return new Vector3(-1 * _halfScreenSize.x - _createOffsetVec.x,
            _halfScreenSize.y + _createOffsetVec.y);
    }

    Vector3 GetScreenViewScale()
    {
        return new Vector3(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2);
    }

    private Vector3 GetOffset()
    {
        return new Vector3(_tiles_interval * 2 , _tiles_interval * 2);
    }

    /// <summary>
    /// 리소스에 저장된 텍스쳐파일을 불러옵니다.
    /// </summary>
    private Texture LoadTexture(eMapTileKind mapKind)
    {
        return Resources.Load<Texture>($"{MapDirPath + mapKind.ToString()}");
    }

    /// <summary>
    /// 텍스쳐사이즈와 분할수를 확인해 타일오브젝트들의 간격을 지정합니다.
    /// </summary>
    private float GetIntervalDistance()
    {
        return (Global_Data.GetTextureSize() * 0.01f) * 1 / _devideCount;
    }

    ///// <summary>
    ///// 제작된 타일맵을 반환합니다.
    ///// </summary>
    //public Map GetMap(eMapTileKind map)
    //{
    //    Texture texture = LoadTexture(map);
    //    Sprite[] splitSprites = DevideTexure(texture);
    //    Map newMap = new Map(map, CreateTileObj(splitSprites));

    //    return newMap;
    //}





    



}
