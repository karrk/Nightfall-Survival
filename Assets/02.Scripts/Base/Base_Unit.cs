using System.Collections;
using UnityEngine;

public abstract class Base_Unit : MonoBehaviour
{
    // 유닛의 스탯 정보 프로퍼티
    public abstract BaseStat UnitStat { get; }
    // 무적시간 프로퍼티
    protected abstract float ImmunityTime { get; }

    protected UnitAnimatior _anim;
    protected SpriteRenderer _renderer;
    protected Rigidbody2D _rb;
    protected CapsuleCollider2D _coll;

    protected Base_Unit _attacker;
    protected bool _isImmunte;

    private eUnitStates _state;
    protected eUnitStates UnitState
    {
        get { return _state; }
        set
        {
            _state = value;
            StateAction();
        }
    }

    public virtual void StateAction()
    {
        switch (_state)
        {
            case eUnitStates.None:
                break;
            case eUnitStates.Idle:
                Idle();
                break;
            case eUnitStates.Move:
                Move();
                break;
            case eUnitStates.Dead:
                Dead();
                break;
            case eUnitStates.Attack:
                Attack();
                break;
            case eUnitStates.OnDamage:
                OnDamage();
                break;
            default:
                break;
        }
    }

    protected virtual void Awake()
    {
        _anim = GetComponent<UnitAnimatior>();
        _renderer = GetComponent<SpriteRenderer>();
        _coll = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }


    protected virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        UnitState = eUnitStates.None;
        _isImmunte = false;
        _anim.Init();
    }

    protected virtual void Idle()
    {

    }

    protected virtual void Move()
    {
        _anim.SetMoveAnim(true);
    }

    protected virtual void Attack()
    {
        _anim.PlayAttackAnim();
    }

    protected virtual void Dead()
    {
        _anim.SetDeadAnim(true);
        StopAllCoroutines();
    }

    protected virtual void OnDamage()
    {
        if (_isImmunte)
            return;

        float hp = ApplyDamage(_attacker.UnitStat.Damage);

        if (hp > 0)
        {
            _anim.PlayOnDamagedAnim();
            StartCoroutine(ImmunityRoutines(ImmunityTime));
        }
        else
        {
            UnitState = eUnitStates.Dead;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Base_Unit>(out _attacker);
    }

    // A -> B a 누굴 죽였는지를 판단하는게 아니고
    // B 가 누구한테 죽었는지를 판단합니다.

    public float ApplyDamage(float damage)
    {
        this.UnitStat.OnDamage(damage * (1 / (1 + this.UnitStat.Armor)));
        return this.UnitStat.HP;
    }

    IEnumerator ImmunityRoutines(float time)
    {
        _isImmunte = true;

        while (true)
        {
            yield return new WaitForSeconds(time);

            break;
        }
        _isImmunte = false;
    }

    public void SetStateIdle()
    {
        this.UnitState = eUnitStates.Idle;
    }

    protected void SetSortOrder()
    {
        _renderer.sortingOrder = (int)(this.transform.position.y * -100);
    }
}



