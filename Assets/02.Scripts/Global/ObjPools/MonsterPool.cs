using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : ObjectPool
{
    private int count = 1;

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
        obj.name = count.ToString();
        count++;

        _outObjList.Add(obj);

        return obj;
    }
}
