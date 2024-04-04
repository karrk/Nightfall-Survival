using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour, ICollection
{
    private WeaponStat _wpStat;

    private float _rotateSpeed = 40.0f;

    private Image _wpImage;

    // getter setter
    public WeaponStat WpStat { get { return _wpStat; } set { _wpStat = value; } }
    public float RotateSpeedn { get { return _rotateSpeed; } set { _rotateSpeed = value; } }
    public Image WpImage { get { return _wpImage; } set { _wpImage = value; } }


    public void Start()
    {
        _wpImage = GetComponent<Image>();
    }

    public void FixedUpdate()
    {
        RotateAroundCharacter();
    }

    public void SetStat(Data_Weapon data)
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {

    }

    private void ActiveWeapon()
    {

    }

    public void RotateAroundCharacter()
    {
        transform.RotateAround(Vector3.zero, Vector3.back, Time.fixedDeltaTime * _rotateSpeed);
    }

    public void OnTriggerEnter(Collider other)
    {
        // TODO) 몬스터와의 충돌 감지
        if(other.tag.Equals("Monster"))
        {

        }
    }
}

