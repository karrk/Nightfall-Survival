using System.Collections;
using UnityEngine;

public class Monster : Base_Unit, IPoolingObj
{
    readonly static float NearDistance = 0.1f;

    public override Stat UnitStat => _stat;
    protected override float ImmunityTime => 0.3f;

    private Stat _stat = new Stat();

    private bool _isRight;
    private bool _tempIsRight;

    private static Base_Unit _chaseTarget;

    public override void Init()
    {
        base.Init();
        SetStateIdle();
    }

    public static Base_Unit FindTarget()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        
        if (target == null)
            return null;

        Base_Unit unit = target.GetComponent<Base_Unit>();

            return unit;
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

    IEnumerator Chase()
    {
        while (true)
        {
            this.transform.position = Vector3.MoveTowards
                (this.transform.position, _chaseTarget.transform.position, 
                _stat.MoveSpeed * Time.deltaTime);

            SetForward(_chaseTarget.transform.position.x);
            SetSortOrder();

            if (Vector3.Distance(transform.position, _chaseTarget.transform.position) <= NearDistance)
            {
                UnitState = eUnitStates.Attack;
                break;
            }

            yield return null;
        }
    }

    IEnumerator ReadyChase()
    {
        while (true)
        {
            if (_chaseTarget != null)
            {
                UnitState = eUnitStates.Move;
                break;
            }

            _chaseTarget = FindTarget();

            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion

    #region 스프라이트 Flip X 설정

    private void SetForward(float posX)
    {
        _tempIsRight = this.transform.position.x < posX ? true : false;

        if (_isRight == _tempIsRight)
        {
            _isRight = !_tempIsRight;
            _renderer.flipX = _isRight;
        }
    }

    #endregion

    public void ReturnObj()
    {
        this.GetComponentInParent<ObjectPool>().ReturnObj(this.gameObject);
    }

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

}