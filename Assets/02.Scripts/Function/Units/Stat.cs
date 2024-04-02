<<<<<<< HEAD
=======
<<<<<<<< HEAD:Assets/02.Scripts/Function/MonsterStat.cs
public class MonsterStat
{
    private string _name;
    private float _hp;

    private int _id;
    private int _type;
    private float _damage;
    private float _moveSpeed;
    private float _armor;

    public int ID => _id;

    public void StatCopy(MonsterStat targetData)
    {
        if (targetData == null)
            targetData = new MonsterStat();

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

    public MonsterStat SetID(int id)
    {
        this._id = id;
        return this;
    }

    public MonsterStat SetType(int type)
    {
        this._type = type;
        return this;
    }

    public MonsterStat SetName(string name)
    {
        this._name = name;
        return this;
    }

    public MonsterStat SetHp(float hp)
    {
        this._hp = hp;
        return this;
    }

    public MonsterStat SetArmor(float armor)
    {
        this._armor = armor;
        return this;
    }

    public MonsterStat SetDamage(float damage)
    {
        this._damage = damage;
        return this;
    }

    public MonsterStat SetSpeed(float speed)
    {
        this._moveSpeed = speed;
        return this;
    }

    public void OnDamage(float damage)
    {
        this._hp -= damage;
    }

    #region 스탯 추가부여
    public MonsterStat AddHp(float hp)
    {
        this._hp += hp;
        return this;
    }
    public MonsterStat AddDamage(float damage)
    {
        this._damage += damage;
        return this;
    }
    public MonsterStat AddArmor(float armor)
    {
        this._armor += armor;
        return this;
    }
    #endregion

}
========
>>>>>>> b757374 (# 1.4)
public class Stat
{
    private string _name;
    private float _hp;

    private int _id = -1;
    private int _type;
    private float _damage;
    private float _moveSpeed;
    private float _armor;

    public int ID => _id;
    public float MoveSpeed => _moveSpeed;
    public float Damage => _damage;
    public float HP => _hp;
<<<<<<< HEAD
<<<<<<< HEAD
    public float Armor => _armor;
=======
>>>>>>> b757374 (# 1.4)
=======
    public float Armor => _armor;
>>>>>>> d8e30f5 (#1.5)

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

    public Stat SetStats(Stat stats, Data_Monster mobData)
    {
        stats
            .SetID(mobData.ID)
            .SetHp(mobData.hp)
            .SetName(mobData.name)
            .SetArmor(mobData.armor)
            .SetSpeed(mobData.moveSpeed)
            .SetDamage(mobData.damage)
            .SetType((int)mobData.type);

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

<<<<<<< HEAD
    public float OnDamage(float damage)
    {
        this._hp -= damage;

        return _hp;
=======
    public void OnDamage(float damage)
    {
        this._hp -= damage;
>>>>>>> b757374 (# 1.4)
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
<<<<<<< HEAD
=======
>>>>>>>> b757374 (# 1.4):Assets/02.Scripts/Function/Units/Stat.cs
>>>>>>> b757374 (# 1.4)
