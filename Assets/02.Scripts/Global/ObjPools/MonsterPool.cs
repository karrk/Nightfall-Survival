using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : ObjectPool
{
    protected override int InitCount => 50;

    protected override ePoolingType Type => ePoolingType.Monster;

    public override GameObject GetObj(Transform parent)
    {
        if (pool.Count <= 0)
        {
            _maxSize *= 2;

            StartCoroutine(DevideCreate(_maxSize));
        }

        GameObject obj = pool.Dequeue();

        obj.transform.position 
            = Camera.main.transform.position + Vector3.right * 100;

        obj.transform.SetParent(parent);
        obj.SetActive(true);

        return obj;
    }
}
