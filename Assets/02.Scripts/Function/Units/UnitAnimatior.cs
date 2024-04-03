<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimatior : MonoBehaviour
{
<<<<<<< HEAD
    const string DeadParameter = "IsDead";
    const string MoveParameter = "IsMove";
    const string OnDamageParameter = "IsOnDamage";
    const string AttackParameter = "IsAttack";
=======
    const string DeadAnimParameter = "IsDead";
>>>>>>> b757374 (# 1.4)

    private Animator _anim;

    private void Awake()
    {
<<<<<<< HEAD
        _anim = GetComponent<Animator>(); 
=======
        _anim = GetComponent<Animator>();
>>>>>>> b757374 (# 1.4)
    }

    public void Init()
    {
<<<<<<< HEAD
        _anim.SetBool(DeadParameter, false);
        _anim.SetBool(MoveParameter, false);
    }

    public void SetMoveAnim(bool active)
    {
        _anim.SetBool(MoveParameter, active);
    }

    public void SetDeadAnim(bool active)
    {
        _anim.SetBool(DeadParameter, active);
    }

    public void PlayOnDamagedAnim()
    {
        _anim.SetTrigger(OnDamageParameter);
=======
        _anim.SetBool(DeadAnimParameter, false);
    }

    public void PlayIdleAnim()
    {

    }

    public void PlayMoveAnim()
    {

    }

    public void PlayDeadAnim()
    {
        _anim.SetBool(DeadAnimParameter, true);
>>>>>>> b757374 (# 1.4)
    }

    public void PlayAttackAnim()
    {
<<<<<<< HEAD
        _anim.SetTrigger(AttackParameter);
=======

    }

    public void OnDamagedAnim()
    {

>>>>>>> b757374 (# 1.4)
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimatior : MonoBehaviour
{
    const string DeadParameter = "IsDead";
    const string MoveParameter = "IsMove";
    const string OnDamageParameter = "IsOnDamage";
    const string AttackParameter = "IsAttack";

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>(); 
    }

    public void Init()
    {
        _anim.SetBool(DeadParameter, false);
        _anim.SetBool(MoveParameter, false);
    }

    public void SetMoveAnim(bool active)
    {
        _anim.SetBool(MoveParameter, active);
    }

    public void SetDeadAnim(bool active)
    {
        _anim.SetBool(DeadParameter, active);
    }

    public void PlayOnDamagedAnim()
    {
        _anim.SetTrigger(OnDamageParameter);
    }

    public void PlayAttackAnim()
    {
        _anim.SetTrigger(AttackParameter);
    }
}
>>>>>>> d8e30f5 (#1.5)
