using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_DamageText_Temp : PoolingObj
{
    System.Text.StringBuilder _sb;

    void Converter(int damage)
    {
        _sb.Clear();
        _sb.Append(damage);
    }
}
