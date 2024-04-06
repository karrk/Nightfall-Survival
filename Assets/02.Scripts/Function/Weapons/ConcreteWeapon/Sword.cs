using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : ContinuousWp
{
    protected override eWeaponType weaponType => eWeaponType.Sword;

    Vector3 _initPos = Vector3.zero;
    Vector3 _offsetPos = Vector3.left * 1.5f;

    public override void Use()
    {
        Init();
        this.gameObject.SetActive(true);
        StartCoroutine(Rotate());
    }

    protected override void Init()
    {
        this.transform.position = _user.transform.position + _initPos + _offsetPos;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator Rotate()
    {
        float useTimer = 0f;

        while (true)
        {
            if(useTimer >= _wpStat.Duration)
            {
                this.gameObject.SetActive(false);
                break;
            }

            this.transform.RotateAround
                    (_user.transform.position, Vector3.forward, _wpStat.MoveSpeed * Time.deltaTime);

            useTimer += Time.deltaTime;

            yield return null;
        }
    }

}

