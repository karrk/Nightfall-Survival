using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IUnit, IStageParts, IPoolingObj
{
    [SerializeField]
    Animation _anim;
    MonsterStat _stat;

    bool _isDead;

    //private void SetProperties()
    //{
    //    _stat = new MonsterStat()
    //        .SetID(1)
    //        .SetHp(10)
    //        .SetArmor(3)
    //        .SetSpeed(5)
    //        .SetDamage(1);
    //}

    Vector3 _basePos = Vector3.zero;

    public void Move()
    {

    }

    IEnumerator ChasePlayer()
    {
        while (true)
        {
            if(_isDead)
                break;

            this.transform.Translate(_basePos * Time.deltaTime * _stat._speed);

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

    public void SendPart()
    {
        //SetProperties();
        StageManager.Instance._stageBuilder.SetMob(Instantiate(this));
    }

    public void AddPartsList()
    {
        StageManager.Instance._stageBuilder.AddPart(this);
    }

    public void Return()
    {
        this.GetComponentInParent<ObjectPool>().ReturnObj(this.gameObject);
    }

    
}

public class MonsterStat
{
    int _id;
    string _name;
    int _type;
    int _hp;
    int _damage;   
    public int _speed;
    int _armor;

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

    public MonsterStat SetHp(int hp)
    {
        this._hp = hp;
        return this;
    }

    public MonsterStat SetArmor(int armor)
    {
        this._armor = armor;
        return this;
    }

    public MonsterStat SetDamage(int damage)
    {
        this._damage = damage;
        return this;
    }

    public MonsterStat SetSpeed(int speed)
    {
        this._speed = speed;
        return this;
    }
}
