using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour , IStageParts
{
    private static MapManager _instance;

    public static MapManager Instance => _instance;

    public Transform StagePartTransform => transform;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, SendPart);
        GameManager.Instance.Event.RegisterEvent(eEventType.AddStageParts, AddPartsList);
    }

    [SerializeField]
    private MapCreator _creator;

    private Map _currentMap;

    public Map GetMap(string mapName)
    {
        if (_currentMap != null && _currentMap.MapName == mapName)
            return _currentMap;

        _currentMap = _creator.GetMap(mapName);

        return _currentMap;
    }

    public void SendPart()
    {
        StageManager.Instance._stageBuilder.SetMap(GetMap("Test"));

        //방식 고려중..
        //string needMap = CSV.GetInfo(StageManager.Instance.StageNumber).MapName;
        //StageManager.Instance._stageBuilder.SetMap(GetMap(needMap));
    }

    public void AddPartsList()
    {
        StageManager.Instance._stageBuilder.AddPart(this);
    }
}



