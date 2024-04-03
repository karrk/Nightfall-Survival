public class Stat
{
    private string _name;
    private float _hp;

    private int _id = -1;
    private int _type; // 당장은 필요없는기능
    private float _damage;
    private float _moveSpeed;
    private float _armor;

    public int ID => _id;
    public float MoveSpeed => _moveSpeed;
    public float Damage => _damage;
    public float HP => _hp;
    public float Armor => _armor;

    public void StatCopy(Stat targetData)
    {
        if (targetData.ID == this._id)
        {
            targetData.SetHp(this._hp);
            return;
        }

        targetData
            .SetID(this._id)
            .SetHp(this._hp)
            .SetName(this._name)
            .SetArmor(this._armor)
            .SetSpeed(this._moveSpeed)
            .SetDamage(this._damage)
            .SetType((int)this._type); 
    }

    public virtual Stat SetStats(Stat stats, Data_Unit data)
    {
        stats
            .SetID(data.ID)
            .SetHp(data.hp)
            .SetName(data.name)
            .SetArmor(data.armor)
            .SetSpeed(data.moveSpeed)
            .SetDamage(data.damage)
            .SetType((int)data.type);

        return stats;
    }

    public Stat SetID(int id)
    {
        this._id = id;
        return this;
    }

    public Stat SetType(int type)
    {
        this._type = type;
        return this;
    }

    public Stat SetName(string name)
    {
        this._name = name;
        return this;
    }

    public Stat SetHp(float hp)
    {
        this._hp = hp;
        return this;
    }

    public Stat SetArmor(float armor)
    {
        this._armor = armor;
        return this;
    }

    public Stat SetDamage(float damage)
    {
        this._damage = damage;
        return this;
    }

    public Stat SetSpeed(float speed)
    {
        this._moveSpeed = speed * 0.1f;
        return this;
    }

    public float OnDamage(float damage)
    {
        this._hp -= damage;

        return _hp;
    }


    #region 스탯 추가부여
    public Stat AddHp(float hp)
    {
        this._hp += hp;
        return this;
    }
    public Stat AddDamage(float damage)
    {
        this._damage += damage;
        return this;
    }
    public Stat AddArmor(float armor)
    {
        this._armor += armor;
        return this;
    }
    public Stat AddSpeed(float speed)
    {
        this._moveSpeed += speed;
        return this;
    }
    #endregion
}

public class CharacterStat : Stat
{
    float _recoverHp;
    float _lucky;
    float _throwCount;
    float _alphaExp;
    float _alphaGold;
    float _delay;
    float _duration;

    public CharacterStat SetRecoverHp(float recoverHp)
    {
        this._recoverHp = recoverHp;
        return this;
    }
    public CharacterStat SetLucky(float lucky)
    {
        this._recoverHp = lucky;
        return this;
    }
    public CharacterStat SetThrowCount(float count)
    {
        this._throwCount = count;
        return this;
    }
    public CharacterStat SetAlphaExp(float alphaPercent)
    {
        this._alphaExp = alphaPercent;
        return this;
    }
    public CharacterStat SetAlphaGold(float alphaPercent)
    {
        this._alphaGold = alphaPercent;
        return this;
    }
    public CharacterStat SetDelay(float delay)
    {
        this._delay = delay;
        return this;
    }
    public CharacterStat SetDuration(float duration)
    {
        this._duration = duration;
        return this;
    }


    #region 추가 스탯부여

    public CharacterStat AddRecoverHp(float recoverHp)
    {
        this._recoverHp += recoverHp;
        return this;
    }
    public CharacterStat AddLucky(float lucky)
    {
        this._recoverHp += lucky;
        return this;
    }
    public CharacterStat AddThrowCount(float count)
    {
        this._throwCount += count;
        return this;
    }
    public CharacterStat AddAlphaExp(float alphaPercent)
    {
        this._alphaExp += alphaPercent;
        return this;
    }
    public CharacterStat AddAlphaGold(float alphaPercent)
    {
        this._alphaGold += alphaPercent;
        return this;
    }
    public CharacterStat AddDelay(float delay)
    {
        this._delay += delay;
        return this;
    }
    public CharacterStat AddDuration(float duration)
    {
        this._duration += duration;
        return this;
    }

    #endregion

    //public override Stat SetStats(Stat stats, Data_Monster characterData)
    //{
    //    stats
    //        .SetID(characterData.ID)
    //        .SetHp(characterData.hp)
    //        .SetName(characterData.name)
    //        .SetArmor(characterData.armor)
    //        .SetSpeed(characterData.moveSpeed)
    //        .SetDamage(characterData.damage)
    //        .SetType((int)characterData.type);

    //    return stats;
    //}
}