using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Monster : Base_Unit, IPoolingObj
{
    private const float IdleDelay = 0.1f;
    private readonly static WaitForSeconds IdleTime = new WaitForSeconds(IdleDelay);
    private const float MoveDelay = 0.03f;
    private readonly static WaitForSeconds MoveTime = new WaitForSeconds(MoveDelay);
    private readonly static float NearDistance = 0.7f;

    private UnitAnimatior _anim = null;

    protected override float ImmunityTime => 0.3f;
    public override BaseStat UnitStat => _stat;
    private BaseStat _stat = new BaseStat();

    private Weapon _contactWeapon;
    private static Base_Unit _chaseTarget;

    private CapsuleCollider2D _triggerCollider;
    private CapsuleCollider2D _collidableCollider;

    private bool _isRight;
    private bool _tempIsRight;

    private Coroutine _stepCoroutine;

    public void SetMobType(eUnitType type)
    {
        if (type == eUnitType.Common)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            return;
        }

        this.gameObject.transform.localScale += new Vector3(1, 1);
    }

    protected override void Awake()
    {
        base.Awake();
        _anim = GetComponent<UnitAnimatior>();
    }

    public override void Init()
    {
        if (_collidableCollider == null)
        {
            _triggerCollider = GetComponent<CapsuleCollider2D>();
            _collidableCollider = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
        }

        _anim.Init();
        _anim.SetSpriteAsset(this._stat.Name);
        _anim.SetStateAnim(eUnitStates.Idle);
        SetStateIdle();

        _triggerCollider.enabled = true;
        _collidableCollider.enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (collision.TryGetComponent<Weapon>(out _contactWeapon))
            {
                _attacker = _contactWeapon.Data.User;
                this.UnitState = eUnitStates.OnDamage;
            }
        }
    }

    #region 상태머신 Override

    protected override void Idle()
    {
        if (_stepCoroutine != null)
            StopCoroutine(_stepCoroutine);

        
        _stepCoroutine = StartCoroutine(ReadyChase());
    }

    protected override void Move()
    {
        base.Move();
        _anim.SetMoveAnim(true);
        _stepCoroutine = StartCoroutine(Chase());
    }
    protected override void Attack()
    {
        _anim.PlayAttackAnim();
        base.Attack();
        _chaseTarget._attacker = this;
        _chaseTarget.UnitState = eUnitStates.OnDamage;

        if (_chaseTarget.UnitState == eUnitStates.Death)
            { _chaseTarget = null; }

        this.UnitState = eUnitStates.Idle;
    }
    protected override void Dead()
    {
        _anim.SetDeadAnim(true);
        _triggerCollider.enabled = false;
        _collidableCollider.enabled = false;
        StopAllCoroutines();
        base.Dead();
        StartCoroutine(WaitDeadAnim());
    }

    protected override void OnDamage()
    {
        if (_isImmunte)
            return;

        float hp = ApplyDamage(_contactWeapon.Data.GetDamageWithUnit());

        if (hp > 0)
        {
            //_anim.PlayOnDamagedAnim();
            StartCoroutine(ImmunityRoutines(ImmunityTime));
            UnitState = eUnitStates.Idle;
        }
        else
        {
            UnitState = eUnitStates.Death;
        }
    }

    #endregion

    #region 구체적인 동작 로직

    IEnumerator ReadyChase()
    {
        while (true)
        {
            yield return IdleTime;

            if (_chaseTarget != null)
            {
                UnitState = eUnitStates.Run;
                break;
            }

            _chaseTarget = Global_Data._character;
        }
    }

    IEnumerator Chase()
    {
        Vector3 targetPos;

        while (true)
        {
            targetPos = _chaseTarget == null ?  
                this.transform.position : _chaseTarget.transform.position;

            if (_chaseTarget == null)
            {
                UnitState = eUnitStates.Idle;
                break;
            }

            this.transform.position = Vector3.MoveTowards
                (this.transform.position, targetPos,
                _stat.MoveSpeed * MoveDelay);

            SetForward(targetPos.x);
            //SetSortOrder();

            if (Vector3.Distance(transform.position, targetPos) <= NearDistance)
            {
                UnitState = eUnitStates.Attack;
                break;
            }

            yield return MoveTime;
        }
    }

    IEnumerator WaitDeadAnim()
    {
        float duration = _anim.CurrentAnimLength;

        while (true)
        {
            yield return new WaitForSeconds(duration);
            break;
        }

        ReturnObj();
    }

    #endregion

    #region 스프라이트 Flip X 설정

    private void SetForward(float posX)
    {
        _tempIsRight = this.transform.position.x < posX ? true : false;

        if (_isRight == _tempIsRight)
        {
            _isRight = !_tempIsRight;
            _anim.Render.flipX = _isRight;
        }
    }

    #endregion

    public void ReturnObj()
    {
        eMonsterKind kind = (eMonsterKind)_stat.ID;
        StageLancher._mobCounts[kind]--;
        ObjPoolManager.Instance.ReturnObj(ePoolingType.Monster, this.gameObject);
    }

}