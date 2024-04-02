<<<<<<< HEAD
using UnityEngine;

public interface IUnit
{
<<<<<<< HEAD
    //public Transform UnitTr { get; }
    //public bool IsDead { get; }
    //public bool IsOnDamaged { get; }

    //public float ImmunityTime { get; }
    //public Stat UnitStat { get; }
    ////public void ChangeState(IMonsterState state);

    //public void Idle();
    //public void Move();
    //public void Dead();
    //public void Attack(IUnit unit);
    //public void OnDamaged(float damage);
=======
    public void Move();
    public void Dead();
    public void OnDamaged(float damage);
>>>>>>> 6b9376b (#1.3)
}
=======
using UnityEngine;

public interface IUnit
{
    public Transform UnitTr { get; }
    public bool IsDead { get; }
    public bool IsOnDamaged { get; }

    public float ImmunityTime { get; }
    public Stat UnitStat { get; }
    //public void ChangeState(IMonsterState state);

    public void Idle();
    public void Move();
    public void Dead();
    public void Attack(IUnit unit);
    public void OnDamaged(float damage);
}
>>>>>>> b757374 (# 1.4)
