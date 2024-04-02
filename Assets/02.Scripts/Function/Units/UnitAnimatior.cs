using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimatior : MonoBehaviour
{
    const string DeadAnimParameter = "IsDead";

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Init()
    {
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
    }

    public void PlayAttackAnim()
    {

    }

    public void OnDamagedAnim()
    {

    }
}
