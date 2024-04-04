using UnityEngine;
using VS.Base.Manager;

public class MapManager : Base_Manager
{
    private MapCreator _creator;

    protected override void Logic_Init_Custom()
    {
        _creator = GetComponentInChildren<MapCreator>();
        GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, SendPart);
    }

    public void SendPart()
    {
        int stageNum = Global_Data._stageNum;
        eMapTileKind kind = Global_Data.stageTable[stageNum].tileKind;
        Map map = _creator.GetMap(kind);

        Global_Data.SetMap(map);
    }
}