using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    protected abstract int InitCount { get; }
    protected abstract ePoolingType Type { get; }

    protected static int CreateLimitCount = 50;

    [SerializeField]
    protected GameObject _prefab;

    Queue<GameObject> pool = new Queue<GameObject>();

    int _maxSize;

    private void Start()
    {
        ObjPoolManager.Instance.AddPool((eEventType)Type, this);
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

    protected GameObject CreateObj()
    {
        GameObject obj = Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(this.transform);

        return obj;
    }

    public GameObject GetObj()
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

    public void ReturnObj()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<IPoolingObj>().Return();
        }
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}