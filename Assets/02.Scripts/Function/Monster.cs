using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IUnit, IStageParts, IPoolingObj
{
    [SerializeField]
    Animation _anim;
    MonsterStat _stat;

    bool _isDead;

    public void SetMonster(eMonsterKind kind)
    {
        Data_Monster mobData = Global_Data.mosnterTable[kind];

        _stat = new MonsterStat()
            .SetID(mobData.ID)
            .SetHp(mobData.hp)
            .SetArmor(mobData.armor)
            .SetSpeed(mobData.moveSpeed)
            .SetDamage(mobData.moveSpeed);
    }

    Vector3 _basePos = Vector3.zero;

    private void Start()
    {
        //Debug.Log(Global_Data.mosnterTable[eMonsterKind.Orc].)
    }

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

    public void SendPart()
    {
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
    float _hp;
    float _damage;   
    float _moveSpeed;
    float _armor;

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
