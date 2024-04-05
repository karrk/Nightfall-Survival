using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat
{
    private int _id = -1;
    private string _name;
    private float _damage;
    private float _speed;
    private float _delay;
    private float _duration;
    private float _throwCount;
    private eCollectionType _collectionType;

    public int ID => _id;
    public string Name => _name;
    public float MoveSpeed => _speed;
    public float Damage => _damage;
    public float Delay => _delay;
    public float Duration => _duration;
    public float ThrowCount => _throwCount;
    public eCollectionType CollectionType => _collectionType;

    public void SetStats(Data_Weapon wpData)
    {
        this
            .SetID(wpData.ID)
            .SetName(wpData.name)
            .SetSpeed(wpData.speed)
            .SetDamage(wpData.damage)
            .SetDelay(wpData.delay)
            .SetCollectType(wpData.collectType)
            .SetDuration(wpData.duration)
            .SetThrowCount((int)wpData.throwCount);
    }

    public void CopyStats(WeaponStat targetData)
    {
        if (targetData.ID == this._id)
            return;

        targetData
            .SetID(this._id)
            .SetName(this._name)
            .SetSpeed(this._speed)
            .SetDamage(this._damage)
            .SetDelay(this._delay)
            .SetCollectType(this._collectionType)
            .SetDuration(this._duration)
            .SetThrowCount((int)this._throwCount);
    }

    public WeaponStat SetCollectType(eCollectionType type)
    {
        this._collectionType = type;
        return this;
    }

    public WeaponStat SetID(int id)
    {
        this._id = id;
        return this;
    }

    public WeaponStat SetName(string name)
    {
        this._name = name;
        return this;
    }

    public WeaponStat SetDamage(float damage)
    {
        this._damage = damage;
        return this;
    }

    public WeaponStat SetSpeed(float speed)
    {
        this._speed = speed * 0.1f;
        return this;
    }

    public WeaponStat SetDelay(float delay)
    {
        this._delay = delay;
        return this;
    }
    public WeaponStat SetDuration(float duration)
    {
        this._duration = duration;
        return this;
    }

    public WeaponStat SetThrowCount(int count)
    {
        this._throwCount = count;
        return this;
    }

    #region 추가 스탯부여

    public WeaponStat AddDamage(float damage)
    {
        this._damage += damage;
        return this;
    }
    public WeaponStat AddSpeed(float speed)
    {
        this._speed += speed;
        return this;
    }
    public WeaponStat AddDelay(float delay)
    {
        this._delay += delay;
        return this;
    }
    public WeaponStat AddDuration(float duration)
    {
        this._duration += duration;
        return this;
    }
    public WeaponStat AddThrowCount(float count)
    {
        this._throwCount += count;
        return this;
    }
    #endregion
}
