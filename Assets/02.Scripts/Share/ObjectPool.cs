using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    protected abstract int InitCount { get; }
    protected abstract ePoolingType Type { get; }

    protected static int CreateOnceCount;
    
    [SerializeField]
    protected GameObject _prefab;

    protected Queue<GameObject> pool = new Queue<GameObject>();
    protected List<GameObject> _outObjList = new List<GameObject>();

    protected int _maxSize;
    private Transform tempParent;

    private void Start()
    {
        GetComponentInParent<ObjPoolManager>().AddPool((eEventType)Type, this);
        CreateOnceCount = Global_Data.CreateOnceCount;
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
        tempParent = parent == null ? this.transform : parent;

        if (pool.Count <= 0)
        {
            _maxSize *= 2;

            StartCoroutine(DevideCreate(_maxSize));
        }

        GameObject obj = pool.Dequeue();
        obj.transform.SetParent(tempParent);
        obj.SetActive(true);

        this._outObjList.Add(obj);

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
        for (int i = _outObjList.Count-1 ; i >= 0; i--)
        {
            ReturnObj(_outObjList[i]);
        }
    }

    public void ReturnObj(GameObject obj)
    {
        this._outObjList.Remove(obj);

        obj.transform.SetParent(this.transform);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}