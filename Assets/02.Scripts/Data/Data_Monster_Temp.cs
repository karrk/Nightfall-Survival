using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Monster_Temp : PoolingObj
{
    Sprite _sprite;
    protected int _power;
    protected int _speed;

    protected void SetProperties(int power, int speed)
    {
        this._power = power;
        this._speed = speed;
    }

    void Move()
    {

    }
}
