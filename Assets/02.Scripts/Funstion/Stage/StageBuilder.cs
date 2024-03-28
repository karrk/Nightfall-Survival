using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    private List<IStageParts> _parts = new List<IStageParts>();

    Map _map;

    //private void Awake()
    //{
    //    GameManager.Instance.Event.RegisterEvent(eEventType.StageReady,GetParts);
    //}

    public Stage Build()
    {
        return new Stage(_map);
    }

    public void AddPart(IStageParts part)
    {
        _parts.Add(part);
    }

    private void GetParts()
    {
        foreach (var partComponent in _parts)
        {
            partComponent.SendPart();
        }
    }

    public void SetMap(Map map)
    {
        this._map = map;
    }
}



