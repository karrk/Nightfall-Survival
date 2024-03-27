using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : MonoBehaviour
{
    Dictionary<ePoolingType, ObjectPool> _dic =
        new Dictionary<ePoolingType, ObjectPool>();

    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.EndGame, ReturnAllObj);
    }


    public PoolingObj GetObj(ePoolingType poolingType, int poolingNumber)
    {
        return _dic[poolingType].GetObj(poolingNumber);
    }

    void ReturnAllObj()
    {
        foreach (var e in _dic)
        {
            e.Value.ReturnObj();
        }
    }
}

//public static class PoolTypeCheck
//{
//    public static PoolingObj GetObj(ePoolingType poolType, eMonster detailType)
//    {
//        return null;
//    }

//    public static PoolingObj GetObj(ePoolingType poolType, eWeapon detailType)
//    {
//        return null;
//    }

//    public static PoolingObj GetObj(ePoolingType poolType, eEffect detailType)
//    {
//        return null;
//    }

//    public static PoolingObj GetObj(ePoolingType poolType, eSound detailType)
//    {
//        return null;
//    }

//    public static PoolingObj GetObj(ePoolingType poolType, eText detailType)
//    {
//        return null;
//    }

//}