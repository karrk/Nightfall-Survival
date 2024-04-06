using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.Base.Manager;

public class ObjPoolManager : Base_Manager
{
    private static ObjPoolManager _instance;
    public static ObjPoolManager Instance => _instance;

    private Dictionary<ePoolingType, ObjectPool> _pools =
        new Dictionary<ePoolingType, ObjectPool>();

    private void Awake()
    {
        _instance = this;
    }

    protected override void Logic_Init_Custom()
    {
        //GameManager.Instance.Event.RegisterEvent(eEventType.CharacterDead, ReturnAllObj);
        //수정해야됨
    }

    public GameObject GetObj(ePoolingType poolingType,Transform parent)
    {
        return _pools[poolingType].GetObj(parent);
    }

    private void ReturnAllObj()
    {
        foreach (var e in _pools)
        {
            e.Value.ReturnObj();
        }
    }

    public void AddPool(eEventType poolType,ObjectPool pool)
    {
        this._pools.Add((ePoolingType)poolType, pool);
    }

    public ObjectPool GetPool(ePoolingType type)
    {
        return _pools[type];
    }
}
