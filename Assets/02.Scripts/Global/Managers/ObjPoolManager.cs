using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : MonoBehaviour
{
    Dictionary<ePoolingType, ObjectPool> pools =
        new Dictionary<ePoolingType, ObjectPool>();

    private void Awake()
    {
        //GameManager.Instance.Event.RegisterEvent(eEventType.EndGame, ReturnAllObj);
    }


    public PoolingObj GetObj(ePoolingType poolingType)
    {
        return pools[poolingType].GetObj();
    }

    void ReturnAllObj()
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
