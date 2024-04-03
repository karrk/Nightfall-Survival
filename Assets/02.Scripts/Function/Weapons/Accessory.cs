using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour, ICollection
{
    private string _name;
    private float _alphaValue;

    public void SetStat(Data_Weapon data)
    {
        _name = data.name;
        _alphaValue = data.damage;
    }

    public void Use()
    {
        // 캐릭터스탯에 해당 값 적용
    }
}
