using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    private List<IStageParts> _parts = new List<IStageParts>();

    Map _map;
    Spawner _spawner;
    List<Monster> _mobs = new List<Monster>();

    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, GetParts);
        GameManager.Instance.Event.RegisterEvent(eEventType.EndGame, Init);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _mobs.Clear();
    }

    public Stage Build()
    {
        return new Stage(_map,_spawner);
    }

    public void AddPart(IStageParts part)
    {
        _parts.Add(part);
    }

    public void SetMob(Monster unit)
    {
        _mobs.Add(unit);
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

    public void SetSpawner(Spawner spawner)
    {
        this._spawner = spawner;
    }

    //public void GetMonsterData
}



