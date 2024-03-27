using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    protected abstract int InitCount { get; }

    [SerializeField]
    protected List<PoolingObj> _prefabs = new List<PoolingObj>();

    protected Dictionary<int, Queue<PoolingObj>> _dic
            = new Dictionary<int, Queue<PoolingObj>>();

    protected Dictionary<int, int> _sizeTable
            = new Dictionary<int, int>();

    protected void InitPool()
    {
        for (int i = 0; i < _prefabs.Count; i++)
        {
            int objNum = _prefabs[i].PoolingNumber;

            if (_dic.ContainsKey(objNum))
                continue;

            _dic.Add(objNum, new Queue<PoolingObj>());

            for (int j = 0; j < InitCount; j++)
            {
                _dic[objNum].Enqueue(CreateObj(i));
            }

            _sizeTable[objNum] = InitCount;
        }
    }

    #region 기능 논의
    protected void InitPool(bool softClearMode)
    {
        RemovePool(softClearMode);
        InitPool();
    }

    private void RemovePool(bool isSoft)
    {
        if (isSoft)
        {
            _dic.Clear();
            return;
        }

        List<int> keys = new List<int>();

        foreach (var e in _dic)
            keys.Add(e.Key);

        foreach (var e in _dic) // 이거 왠지 에러코드 괜찮은 메서드라면 보완예정
        {
            if (keys.Contains(e.Key))
                continue;

            _dic.Remove(e.Key);
        }

    }
    #endregion


    protected PoolingObj CreateObj(int prefabListNum)
    {
        PoolingObj obj = Instantiate(_prefabs[prefabListNum]);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(this.transform);

        return obj;
    }

    public PoolingObj GetObj(int mNumber)
    {
        if (_dic[mNumber].Count <= 0)
        {
            _sizeTable[mNumber] *= 2;

            for (int i = 0; i < mNumber; i++)
            {
                _dic[mNumber].Enqueue(GetObj(mNumber));
            }
        }

        return _dic[mNumber].Dequeue();
    }

    public void ReturnObj(PoolingObj obj = null)
    {
        if(obj == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ReturnObj (transform.GetChild(i).GetComponent<PoolingObj>());
            }
        }

        obj.gameObject.SetActive(false);

        _dic[obj.PoolingNumber].Enqueue(obj);
    }
}

//public class MonsterPool : ObjectPool
//{
//    protected override int InitCount => 20;


//}

//public class WeaponPool : ObjectPool
//{
//    protected override int InitCount => 10;
//}

