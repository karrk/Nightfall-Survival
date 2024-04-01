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

    public void OnDamaged(float damage)
    {
        _stat.OnDamage(damage);
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


