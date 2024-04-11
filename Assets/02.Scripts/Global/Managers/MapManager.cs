using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.Base.Manager;

public class MapManager : Base_Manager, IStageParts
{
    public Transform StagePartTransform => transform;

    private MapCreator _creator;
    private Map _currentMap;

    protected override void Logic_Init_Custom()
    {
        _creator = GetComponentInChildren<MapCreator>();
        GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, SendPart);
        GameManager.Instance.Event.RegisterEvent(eEventType.AddStageParts, AddPartsList);
    }

    public Map GetMap(eMapTileKind mapName)
    {
        if (_currentMap != null && _currentMap.MapName == mapName)
            return _currentMap;

        _currentMap = _creator.GetMap(mapName);

        return _currentMap;
    }

    public void SendPart()
    {
        int stageNum = StageManager.Instance.StageNumber;
        eMapTileKind kind = Global_Data.stageTable[stageNum].tileKind;
        StageManager.Instance._stageBuilder.SetMap(GetMap(kind));
    }

    public void AddPartsList()
    {
        StageManager.Instance._stageBuilder.AddPart(this);
    }

    
}



