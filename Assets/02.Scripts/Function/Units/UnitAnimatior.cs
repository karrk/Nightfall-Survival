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