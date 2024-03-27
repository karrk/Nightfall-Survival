using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Weapon_Temp : PoolingObj
{
    Sprite sprite;
    int _power;
    int _moveSpeed;

    int GetDamage()
    {
        return 1;
    }
}