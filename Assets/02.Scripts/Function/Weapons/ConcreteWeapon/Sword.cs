using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    //protected override eWeaponType weaponType => eWeaponType.Sword;

    //const float OffsetDist = 1f;
    //private Vector3 _offset = Vector3.left * OffsetDist;

    //public override void WeaponSetReady(Character user)
    //{
    //    base.WeaponSetReady(user);

    //    WpFuncs = eWeaponFuncs.Rotatable;
    //}

    //protected override void SetRotate()
    //{
    //    _startPos = this._user.transform.position + _offset;
    //}

    //public override void Fire()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(Rotate());
    //}

    //IEnumerator Rotate()
    //{
    //    while (true)
    //    {
    //        if (_durationTimer <= 0)
    //        {
    //            ReturnObj();
    //            break;
    //        }

    //        this.transform.RotateAround
    //                (_user.transform.position, Vector3.forward, _wpStat.MoveSpeed * Time.deltaTime);

    //        _durationTimer -= Time.deltaTime;

    //        yield return null;
    //    }
    //}
}

