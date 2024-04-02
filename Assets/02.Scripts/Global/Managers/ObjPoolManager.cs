using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.Base.Manager;

public class ObjPoolManager : Base_Manager
{
    private static ObjPoolManager _instance;
    public static ObjPoolManager Instance => _instance;

    Dictionary<ePoolingType, ObjectPool> pools =
        new Dictionary<ePoolingType, ObjectPool>();

    private void Awake()
    {
        _instance = this;
    }

    protected override void Logic_Init_Custom()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.EndGame, ReturnAllObj);
    }

    public GameObject GetObj(ePoolingType poolingType)
    {
        return pools[poolingType].GetObj();
    }

    private void ReturnAllObj()
    {
        foreach (var e in pools)
        {
            e.Value.ReturnObj();
        }
    }

    public void AddPool(eEventType poolType,ObjectPool pool)
    {
        this.pools.Add((ePoolingType)poolType, pool);
    }

    
}
