using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    protected abstract int InitCount { get; }
    protected abstract ePoolingType Type { get; }

    protected static int CreateOnceCount = 50;
    

    [SerializeField]
    protected GameObject _prefab;

    protected Queue<GameObject> pool = new Queue<GameObject>();

    protected int _maxSize;

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
        if (_prefab == null) // 테스트중 임시코드
            return null;

        GameObject obj = Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(this.transform);

        return obj;
    }

    public virtual GameObject GetObj(Transform parent)
    {
        if (pool.Count <= 0)
        {
            _maxSize *= 2;

            StartCoroutine(DevideCreate(_maxSize));
        }

        GameObject obj = pool.Dequeue();
        obj.transform.SetParent(parent);
        obj.SetActive(true);

        return obj;
    }

    protected IEnumerator DevideCreate(int requestCount)
    {
        int count = requestCount;

        while (true)
        {
            for (int i = 0; i < CreateOnceCount; i++)
            {
                pool.Enqueue(CreateObj());
            }

            count -= CreateOnceCount;

            if (count <= 0)
                break;

            yield return null;
        }
    }

    public void ReturnObj()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<IPoolingObj>().ReturnObj();
        }
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}