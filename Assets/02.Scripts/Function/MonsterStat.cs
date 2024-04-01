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
