using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IUnit, IPoolingObj
{
    [SerializeField]
    Animation _anim;
    MonsterStat _stat;
    public MonsterStat Stat => _stat;

    bool _isDead;

    public void Move()
    {

    }

    IEnumerator ChasePlayer()
    {
        while (true)
        {
            if(_isDead)
                break;

            //this.transform.Translate(_basePos * Time.deltaTime * _stat._speed);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnDamaged()
    {
        //공격받을때
    }

    public void Dead()
    {
        //죽을때 로직
    }

    public void SetAnim()
    {
        _anim = GetComponent<Animation>();
    }

    public void Return()
    {
        this.GetComponentInParent<ObjectPool>().ReturnObj(this.gameObject);
    }

    public void SetStats(Data_Monster mobData)
    {
        _stat = new MonsterStat()
            .SetID(mobData.ID)
            .SetHp(mobData.hp)
            .SetName(mobData.name)
            .SetArmor(mobData.armor)
            .SetSpeed(mobData.moveSpeed)
            .SetDamage(mobData.damage)
            .SetType((int)mobData.type);
    }

}

public class MonsterStat
{
    private string _name;
    private int _id;
    private int _type;
    private float _hp;
    private float _damage;
    private float _moveSpeed;
    private float _armor;

    public int ID => _id;

    public void StatCopy(MonsterStat targetData)
    {
        if (targetData.ID != this._id)
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


}
