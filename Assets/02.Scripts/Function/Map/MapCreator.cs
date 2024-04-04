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

    private Sprite[] _sprites;
    private GameObject[] _tileObjects;

    private void Awake()
    {
        _devideCount = Global_Data._mapTileDevideCount;

        int textureSize = LoadTexture((eMapTileKind)1).width;
        Global_Data._textureSize = textureSize;
        _tiles_interval = GetIntervalDistance(textureSize);

        Init();

        GameManager.Instance.Event.RegisterEvent(eEventType.SetResolution,Init);
    }

    public void Init()
    {
        _halfScreenSize = GetScreenViewScale() / 2;
        _createOffsetVec = GetOffset();

        this.transform.position = GetCreateStartPos();

        _columnCount = CheckDevideCount(_halfScreenSize.x);
        _rowCount = CheckDevideCount(_halfScreenSize.y);

        Global_Data._MapSize = GetMapSize();
    }

    /// <summary>
    /// 제작된 타일맵을 반환합니다.
    /// </summary>
    public Map GetMap(eMapTileKind map)
    {
        Texture texture = LoadTexture(map);

        _sprites = DevideTexure(texture);
        _tileObjects = CreateTileObj(_sprites);
        SetTileOptions(_tileObjects);

        Map newMap = new Map(map, _tileObjects);

        return newMap;
    }

    /// <summary>
    /// 해상도에 맞는 타일 분할수를 결정합니다.
    /// </summary>
    public int CheckDevideCount(float screenHalfSize)
    {
        float limit = (screenHalfSize * 2) + (_tiles_interval * 4) - (_tiles_interval / 2);

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
        Sprite[] tempArr = new Sprite[(int)MathF.Pow(_devideCount, 2)];

        float xInterval = texture.width / _columnCount;
        float yInterval = texture.height / _rowCount;

        for (int i = 0; i < _devideCount; i++)
        {
            for (int j = 0; j < _devideCount; j++)
            {
                Rect rect = new Rect(
                    (texture.width / _devideCount)*j,
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

                Sprite sprite 
                    = sprites[((i * _devideCount) % sprites.Length) + (j % _devideCount)];

                tileRenderer.sprite = sprite;
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
            }
        }

        TileMovement._tileSize = _tiles_interval;
        TileMovement.SetMoveDistance(_columnCount * _tiles_interval, _rowCount * _tiles_interval);
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
    private float GetIntervalDistance(int size)
    {
        return (size * 0.01f) * 1 / _devideCount;
    }

    public Vector3 GetMapSize()
    {
        return new Vector3
            ((_tiles_interval * _columnCount), (_tiles_interval * _rowCount));
    }










}
