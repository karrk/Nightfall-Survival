using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    protected abstract int InitCount { get; }
    protected abstract ePoolingType Type { get; }

    protected static int CreateLimitCount = 50;

    [SerializeField]
    protected PoolingObj _prefab;

    Queue<PoolingObj> pool = new Queue<PoolingObj>();

    int _maxSize;

    private void Start()
    {
        FindObjectOfType<ObjPoolManager>().AddPool((eEventType)Type, this);
        InitPool();
    }

    protected void InitPool()
    {
        for (int i = 0; i < InitCount; i++)
        {
            pool.Enqueue(CreateObj());
        }

        _maxSize = InitCount;
    }

    protected PoolingObj CreateObj()
    {
        PoolingObj obj = Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(this.transform);

        return obj;
    }

    public PoolingObj GetObj()
    {
        if (pool.Count <= 0)
        {
            _maxSize *= 2;

            StartCoroutine(DevideCreate(_maxSize));
        }

        return pool.Dequeue();
    }

    IEnumerator DevideCreate(int requestCount)
    {
        int count = requestCount;

        while (true)
        {
            for (int i = 0; i < CreateLimitCount; i++)
            {
                pool.Enqueue(CreateObj());
            }

            count -= CreateLimitCount;

            if (count <= 0)
                break;

            yield return null;
        }
    }

    public void ReturnObj(PoolingObj obj = null)
    {
        if (obj == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ReturnObj(transform.GetChild(i).GetComponent<PoolingObj>());
            }
        }

        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}