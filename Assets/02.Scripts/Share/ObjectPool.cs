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

    // 비활성화된 오브젝트 풀
    protected Queue<GameObject> pool = new Queue<GameObject>();
    // 씬 내 활성화중인 오브젝트 요소 목록
    protected List<GameObject> _outObjList = new List<GameObject>();

    protected int _maxSize;
    private Transform tempParent;

    private void Start()
    {
        GetComponentInParent<ObjPoolManager>().AddPool((eEventType)Type, this);
        CreateOnceCount = Global_Data.CreateOnceCount;
        InitPool();
    }

    // 풀 초기화 함수
    protected void InitPool()
    {
        for (int i = 0; i < InitCount; i++)
        {
            pool.Enqueue(CreateObj());
        }

        _maxSize = InitCount;
    }

    // 인스펙터에 등록된 오브젝트 인스턴스 생성
    protected GameObject CreateObj()
    {
        if (_prefab == null) // 테스트중 임시코드
            return null;

        GameObject obj = Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(this.transform);

        return obj;
    }

    // 오브젝트 풀의 오브젝트 반환
    // 만약 지정된 트랜스폼이 없는경우 오브젝트의 부모는 현재 위치 그대로 유지한다.
    // 오브젝트가 없는경우 생성량을 두배로 늘림
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

    // 프로그램의 성능 보완을 위해 작성한 함수로 많은 생성 호출시 해당 생성량을 CreateOnceCount 수로 분할하여 생성
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

    // 풀 반환 함수로, 특정 대상이 없는경우 전체반환처리
    public virtual void ReturnObj()
    {
        for (int i = _outObjList.Count-1 ; i >= 0; i--)
        {
            ReturnObj(_outObjList[i]);
        }
    }

    // 특정 대상을 outedList에서 내부 오브젝트 풀로 반환하는 함수
    public virtual void ReturnObj(GameObject obj)
    {
        this._outObjList.Remove(obj);

        obj.transform.SetParent(this.transform);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
