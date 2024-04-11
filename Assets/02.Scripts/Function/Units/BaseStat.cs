public class BaseStat
{
    private int _id;
    private int _type;
    private string _name;
    private float _hp;
    private float _damage;
    private float _moveSpeed;
    private float _armor;

    public int ID => _id;
    public int Type => _type;
    public string Name => _name;
    public float HP => _hp;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float Armor => _armor;

    public void StatCopy(BaseStat targetData)
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

    public BaseStat SetStats(Data_Unit data)
    {
        this
            .SetID(data.ID)
            .SetHp(data.hp)
            .SetName(data.name)
            .SetArmor(data.armor)
            .SetSpeed(data.moveSpeed)
            .SetDamage(data.damage)
            .SetType((int)data.type);

        return this;
    }

    public float OnDamage(float damage)
    {
        this._hp -= damage;

        return _hp;
    }

    #region 기본 스탯 설정

    public BaseStat SetID(int id)
    {
        this._id = id;
        return this;
    }

    public BaseStat SetType(int type)
    {
        this._type = type;
        return this;
    }

    public BaseStat SetName(string name)
    {
        this._name = name;
        return this;
    }

    public BaseStat SetHp(float hp)
    {
        this._hp = hp;
        return this;
    }

    public BaseStat SetArmor(float armor)
    {
        this._armor = armor;
        return this;
    }

    public BaseStat SetDamage(float damage)
    {
        this._damage = damage;
        return this;
    }

    public BaseStat SetSpeed(float speed)
    {
        this._moveSpeed = speed * 0.1f;
        return this;
    }
    #endregion

    #region 스탯 추가부여
    public BaseStat AddHp(float hp)
    {
        this._hp += hp;
        return this;
    }
    public BaseStat AddDamage(float damage)
    {
        this._damage += damage;
        return this;
    }
    public BaseStat AddArmor(float armor)
    {
        this._armor += armor;
        return this;
    }
    public BaseStat AddSpeed(float speed)
    {
        this._moveSpeed += speed;
        return this;
    }
    #endregion
}

