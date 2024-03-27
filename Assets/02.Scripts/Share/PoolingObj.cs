using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingObj : MonoBehaviour
{
    public int PoolingNumber { get { return _poolingNumber; } }

    protected int _poolingNumber;

    protected static int FindNumberToCSV(string name)
    {
        //csv -> 넘버를 반환
        
        return 0;
    }
}

public abstract class Monster : PoolingObj
{
    protected string _type;
    protected int _power;
    protected int _speed;

    private void Start() // 순서 애매한데
    {
        this._poolingNumber = FindNumberToCSV(_type);
    }

    protected void SetProperties(string type, int power, int speed)
    {
        this._type = type;
        this._power = power;
        this._speed = speed;
    }
}

public class Orc : Monster
{
    private void Start()
    {
        SetProperties("orc", 5, 3);
    }
}

public class Fox : Monster
{
    private void Start()
    {
        SetProperties("fox", 2, 2);
    }
}

public abstract class Weapon : PoolingObj
{

}
