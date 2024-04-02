using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class Monster : Base_Unit, IPoolingObj
{
    readonly static float NearDistance = 0.1f;

    public override Stat UnitStat => _stat;
    protected override float ImmunityTime => 0.3f;

=======
public class Monster : MonoBehaviour, IUnit, IPoolingObj
{
    public Transform UnitTr => this.transform;
    public static IUnit _target;

    private SpriteRenderer _renderer;
    private UnitAnimatior _animator;

    public float ImmunityTime => 0.3f;
    private float nearDistance = 0.5f;

    public Stat UnitStat => _stat;
>>>>>>> b757374 (# 1.4)
    private Stat _stat = new Stat();

    private bool _isRight;
    private bool _tempIsRight;

<<<<<<< HEAD
    private static Base_Unit _chaseTarget;

    public override void Init()
    {
        base.Init();
        SetStateIdle();
    }

    public static Base_Unit FindTarget()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Base_Unit>();
    }

    protected override void Idle()
    {
        StartCoroutine(ReadyChase());
    }

    protected override void Move()
    {
        StopAllCoroutines();
        base.Move();
        StartCoroutine(Chase());
    }

    #region 구체적인 동작 로직

=======
    public bool IsDead => _isDead;
    private bool _isDead;

    public bool IsOnDamaged => _onDamaged;
    private bool _onDamaged;

    private Coroutine _moveCo;
    private Coroutine _damageCo;

    public void Init()
    {
        if (_target == null) // 임시
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<IUnit>();

        _animator = GetComponent<UnitAnimatior>();
        _renderer = GetComponent<SpriteRenderer>();
        _isDead = false;
        _onDamaged = false;
        Idle();
    }

    public void Return()
    {
        this.GetComponentInParent<ObjectPool>().ReturnObj(this.gameObject);
    }

    #region IUnit 인터페이스 메서드 구현
    public void Idle()
    {
        _animator.PlayIdleAnim();
        _moveCo = StartCoroutine(ReadyChase());
    }

    public void Move()
    {
        _animator.PlayMoveAnim();
        _moveCo = StartCoroutine(Chase());
    }

    public void Dead()
    {
        _animator.PlayDeadAnim();
        _isDead = true;
        StopCoroutine(_moveCo);
        StopCoroutine(_damageCo);

        Return();
    }

    public void Attack(IUnit unit)
    {
        if (!unit.IsOnDamaged)
            unit.OnDamaged(this._stat.Damage);
    }

    public void OnDamaged(float damage)
    {
        _onDamaged = true;
        this._stat.OnDamage(damage);
        _damageCo = StartCoroutine(DecreaseImmunityTime(ImmunityTime));

        if (this._stat.HP <= 0)
            Dead();
    }
    #endregion

    #region 구체적인 행동 로직
>>>>>>> b757374 (# 1.4)
    IEnumerator Chase()
    {
        while (true)
        {
            this.transform.position =
<<<<<<< HEAD
                Vector3.Lerp(this.transform.position, _chaseTarget.transform.position,
                _stat.MoveSpeed * Time.deltaTime);

            SetForward(_chaseTarget.transform.position.x);
            SetSortOrder();

            if (Vector3.Distance(transform.position, _chaseTarget.transform.position)
                <= NearDistance)
            {
                State = eUnitStates.Attack;
=======
                Vector3.Lerp(this.transform.position, _target.UnitTr.position,
                _stat.MoveSpeed * Time.deltaTime);

            SetForward(_target.UnitTr.position.x);
            SetSortOrder();

            if (Vector3.Distance(transform.position, _target.UnitTr.position)
                <= nearDistance )
            {
                Attack(_target);
                break;
            }

            if(_isDead)
            {
                Dead();
>>>>>>> b757374 (# 1.4)
                break;
            }

            yield return null;
        }
    }

<<<<<<< HEAD
=======
    IEnumerator DecreaseImmunityTime(float time)
    {
        float tempTime = time;
        
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (tempTime <= 0)
                break;

            tempTime -= 0.1f;
        }
    }

>>>>>>> b757374 (# 1.4)
    IEnumerator ReadyChase()
    {
        while (true)
        {
<<<<<<< HEAD
            if (_chaseTarget != null)
            {
                State = eUnitStates.Move;
                break;
            }

            _chaseTarget = FindTarget();

            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion

    #region 스프라이트 Flip X 설정

=======
            if (_target != null)
            {
                Move();
                break;
            }
               
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion

    #region 스프라이트 조정옵션
>>>>>>> b757374 (# 1.4)
    private void SetForward(float posX)
    {
        _tempIsRight = this.transform.position.x < posX ? true : false;

        if (_isRight == _tempIsRight)
        {
            _isRight = !_tempIsRight;
            _renderer.flipX = _isRight;
        }
    }

<<<<<<< HEAD
    #endregion

    public void ReturnObj()
    {
        this.GetComponentInParent<ObjectPool>().ReturnObj(this.gameObject);
    }

    //protected override void Attack()
    //{

    //}

    //protected override void Dead()
    //{

    //    //if(_attacker == 플레이어라면)
    //    //    //보상이나
    //    //    //

    //    ////누구의 공격인지 확인하는 데이터
    //    //_isDead = true;
    //    //Anim.PlayDeadAnim();
    //    //Return();
    //    //// 템떨구기
    //    //// 대상
    //}

    //protected override void Idle()
    //{

    //}

    //protected override void Move()
    //{

    //}

    //protected override void OnDamage()
    //{

    //}

}
=======
    private void SetSortOrder()
    {
        _renderer.sortingOrder = (int)(this.transform.position.y * -100);
    }
    #endregion
}


>>>>>>> b757374 (# 1.4)
