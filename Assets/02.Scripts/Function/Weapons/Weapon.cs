using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, ICollection
{
    readonly static string WeaponDirPath = "Weapons/";

    public Base_Unit _user = null;
    protected CharacterStat _userStat = null;
    protected abstract eWeaponType weaponType { get; }
    public eCollectionType _collectionType = eCollectionType.None;
    protected WeaponStat _wpStat;
    protected SpriteRenderer _render;
    protected Base_Unit _target;

    public WeaponStat WpStat => _wpStat;

    protected void Awake()
    {
        _render = GetComponentInChildren<SpriteRenderer>();
        SetSprite();
    }

    protected virtual void Init()
    {

    }

    public void ApplyUserStat(Character user)
    {
        WeaponStat stats = new WeaponStat();
        stats.SetStats(Global_Data.weaponTable[(int)weaponType]);

        this._user = user;

        _userStat = (CharacterStat)user.UnitStat;

        stats.AddDamage(_userStat.Damage)
            .AddSpeed(_userStat.ThrowSpeed)
            .AddDelay(-1 * _userStat.Delay)
            .AddDuration(_userStat.Duration)
            .AddThrowCount(_userStat.ThrowCount);

        stats.SetCollectType(Global_Data.weaponTable[(int)weaponType].collectType);

        _wpStat = stats;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            collision.TryGetComponent<Base_Unit>(out _target);
        }
    }

    public void SetSprite()
    {
        this._render.sprite = Resources.Load<Sprite>($"{WeaponDirPath + weaponType.ToString()}");
    }

    public abstract void Use();
}

public abstract class ContinueousWp : Weapon
{

}


public abstract class ThrowingWp : Weapon
{

}

