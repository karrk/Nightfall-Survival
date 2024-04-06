using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponTable
{
    private static Dictionary<eWeaponType, Weapon> _table
        = new Dictionary<eWeaponType, Weapon>();

    public static void Init()
    {
        for (int i = 1; i <= Global_Data.weaponTable.Count; i++)
        {
            //_table.Add((eWeaponType)i, ICollection);
        }
    }

    public static Weapon GetWeapon(eWeaponType type)
    {
        return _table[type];
    }
}
