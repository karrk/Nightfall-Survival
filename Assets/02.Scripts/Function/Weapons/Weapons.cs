using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    private List<Weapon> _weaponList = new List<Weapon>();


    public void Init()
    {
        ClearWeapons();
    }

    // 동일한 무기들을 장착함
    public void SettingWeapons(int weaponCount, WeaponStat wpStat)
    {
        // TODO) WeaponPool 로부터 Weapon 생성 받아서 가져와야함

        Vector3 initPos = new Vector3(0, 130, 0);
        float angle = 360.0f / weaponCount;

        for(int i = 0; i < weaponCount; i++)
        {
            Weapon newWeapon = Instantiate(weapon, transform);
            newWeapon.transform.RotateAround(Vector3.zero, Vector3.back, angle * i);

            newWeapon.WpStat = wpStat;

            _weaponList.Add(newWeapon);
        }
    }

    public void ClearWeapons()
    {
        // TODO) WeaponPool 과 연동해서 자원 반납

        foreach(Weapon wp in _weaponList)
        {
            Destroy(wp);
        }

        _weaponList.Clear();
    }
}
