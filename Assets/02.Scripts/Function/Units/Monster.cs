using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Base_Unit, IPoolingObj
{
    public override Stat UnitStat => _stat;
    protected override float ImmunityTime => 0.3f;

    private Stat _stat = new Stat();

    private bool _isRight;
    private bool _tempIsRight;

    [SerializeField]
    private static Base_Unit _chaseTarget;

    public override void Init()
    {
        base.Init();

        SetStateIdle();

        if (_chaseTarget == null) // 임시
            _chaseTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Base_Unit>();

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

    public void ReturnObj()
    {
        this.GetComponentInParent<ObjectPool>().ReturnObj(this.gameObject);
    }

    //#region IUnit 인터페이스 메서드 구현
    //public void Idle()
    //{
    //    _animator.PlayIdleAnim();
    //    _moveCo = StartCoroutine(ReadyChase());
    //}

    //public void Move()
    //{
    //    _animator.PlayMoveAnim();
    //    _moveCo = StartCoroutine(Chase());
    //}

    //public void Dead()
    //{
    //    _animator.PlayDeadAnim();
    //    _isDead = true;
    //    StopCoroutine(_moveCo);
    //    StopCoroutine(_damageCo);

    //    Return();
    //}

    //public void Attack(IUnit unit)
    //{
    //    if (!unit.IsOnDamaged)
    //        unit.OnDamaged(this._stat.Damage);
    //}

    //public void OnDamaged(float damage)
    //{
    //    _onDamaged = true;
    //    this._stat.OnDamage(damage);
    //    _damageCo = StartCoroutine(DecreaseImmunityTime(ImmunityTime));

    //    if (this._stat.HP <= 0)
    //        Dead();
    //}
    //#endregion

    //#region 구체적인 행동 로직
    //IEnumerator Chase()
    //{
    //    while (true)
    //    {
    //        this.transform.position =
    //            Vector3.Lerp(this.transform.position, _target.UnitTr.position,
    //            _stat.MoveSpeed * Time.deltaTime);

    //        SetForward(_target.UnitTr.position.x);
    //        SetSortOrder();

    //        if (Vector3.Distance(transform.position, _target.UnitTr.position)
    //            <= nearDistance )
    //        {
    //            Attack(_target);
    //            break;
    //        }

    //        if(_isDead)
    //        {
    //            Dead();
    //            break;
    //        }

    //        yield return null;
    //    }
    //}

    //IEnumerator DecreaseImmunityTime(float time)
    //{
    //    float tempTime = time;

    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.1f);

    //        if (tempTime <= 0)
    //            break;

    //        tempTime -= 0.1f;
    //    }
    //}

    //IEnumerator ReadyChase()
    //{
    //    while (true)
    //    {
    //        if (_target != null)
    //        {
    //            Move();
    //            break;
    //        }

    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
    //#endregion

    //#region 스프라이트 조정옵션
    //private void SetForward(float posX)
    //{
    //    _tempIsRight = this.transform.position.x < posX ? true : false;

    //    if (_isRight == _tempIsRight)
    //    {
    //        _isRight = !_tempIsRight;
    //        _renderer.flipX = _isRight;
    //    }
    //}

    //private void SetSortOrder()
    //{
    //    _renderer.sortingOrder = (int)(this.transform.position.y * -100);
    //}
    //#endregion

    //private void SetForward(float posX)
    //{
    //    _tempIsRight = this.transform.position.x < posX ? true : false;

    //    if (_isRight == _tempIsRight)
    //    {
    //        _isRight = !_tempIsRight;
    //        _renderer.flipX = _isRight;
    //    }
    //}

    //
    //
    //
    //

    //public void Return()
    //{
    //    this.GetComponentInParent<ObjectPool>().ReturnObj(this.gameObject);
    //}



    //public Transform UnitTr => this.transform;
    //public static IUnit _target;

    //private SpriteRenderer _renderer;
    //private UnitAnimatior _animator;

    //public float ImmunityTime => 0.3f;
    //private float nearDistance = 0.5f;

    //public Stat UnitStat => _stat;
    //private Stat _stat = new Stat();

    //public bool IsDead => _isDead;
    //private bool _isDead;

    //public bool IsOnDamaged => _onDamaged;
    //private bool _onDamaged;

    //private Coroutine _moveCo;
    //private Coroutine _damageCo;

    

}


