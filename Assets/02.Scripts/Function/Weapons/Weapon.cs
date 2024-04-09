using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon : MonoBehaviour, ICollection, IPoolingObj
{
    private const string WeaponDirPath = "Weapons/";
    private const float Gravity = -9.81f;

    [SerializeField]
    private eWeaponType _weaponType = eWeaponType.None;

    private CapsuleCollider2D _collider = null;
    private SpriteRenderer _render = null;

    private WeaponData _data = null;
    public WeaponData Data => _data;

    private Base_Unit _contactUnit = null;


    // 격발 시작지점.
    private Vector3 _firePos = Vector3.zero;
    // 방향만 잡고 이동시킨다.
    private Vector3 _dir = Vector3.zero;
    // 도착 지점이 있는경우
    private Vector3 _landPos = Vector3.zero;
    // 일정시간 동안 날아가다 반환
    private float _durationTimer = 0;
    // 관통수가 없으면 반환
    private int _passCount = 0;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _render = GetComponent<SpriteRenderer>();
        _contactUnit = null;
    }

    public void Action(eWeaponType type) // 프로퍼티 설정예정
    {
        if (this._data == null)
            _data = new WeaponData(type);

        if (this._weaponType != type)
        {
            _render.sprite = Resources.Load<Sprite>(WeaponDirPath + type.ToString());
            _data.UpdateData(Global_Data._inventory[type].Data);
        }

        Init();
        Process = eWeaponProcess.CheckFireType;
        Process = eWeaponProcess.FireProcess;
    }

    private eWeaponProcess _process = eWeaponProcess.None;
    private eWeaponProcess Process
    {
        set
        {
            this._process = value;
            switch (_process)
            {
                case eWeaponProcess.None:
                    break;
                case eWeaponProcess.CheckFireType:
                    CheckFireType();
                    break;
                case eWeaponProcess.FireProcess:
                    FireProcess();
                    break;
                case eWeaponProcess.PostProcess:
                    CheckPostProcess();
                    break;
                case eWeaponProcess.ProcessFinish:
                    ProcessFinish();
                    break;
                default:
                    break;
            }
        }
    }

    private void CheckFireType()
    {
        if (Data.IsNeedDir)
            SetDirectionTypes();

        else if(Data.HasSpStartPos)
        {
            if (Data.IsRotate)
                SetRotateTypes();

            else if (Data.IsFalling)
                SetFallingTypes();
        }
    }

    #region CheckFireType 연결 로직

    private void SetDirectionTypes()
    {
        if (Data.IsTargeting)
            _dir = Data.User.GetClosetUnitPos().normalized;

        //else if (Data.IsCtrlDirable)
            //_dir = GetCustomDir(_weaponType);

        else
            _dir = GetRandDir();
    }

    private void SetRotateTypes()
    {
        _firePos += Vector3.left * 2f; 
        _dir = Vector3.forward * (Random.Range(0, 2) * 2 - 1);
    }

    private void SetFallingTypes()
    {
        Vector3 userPos = Data.User.transform.position;
        
        if (Data.IsTargeting)
            _landPos = Data.User.GetClosetUnitPos(true);
        else
        {
            _landPos = userPos;
            _landPos.x += Random.Range(-10f, 10f);
            _landPos.y += Random.Range(-10f, 10f);
        }

        _firePos += new Vector3(Random.Range(-10f, 10f), 10f);
        _dir = Vector3.down;
    }

    //private Vector3 GetCustomDir(eWeaponType type)
    //{
    //    switch (type)
    //    {
    //        case eWeaponType.채찍:
    //            return Vector3.right * Data.User.CharacterDir.x;

    //        case eWeaponType.단검:
    //            return Data.User.CharacterDir;

    //        case eWeaponType.마나의노래:
    //            return Vector3.up;

    //        default:
    //            return Vector3.zero;
    //    }
    //}

    #endregion

    // 유형별 작동 형식
    private void FireProcess()
    {
        this.transform.position = _firePos;

        // 방향형
        if (Data.IsNeedDir)
            StartCoroutine(FireByDir());

        // 회전형
        else if (Data.IsRotate)
            StartCoroutine(FireByRotation());

        // 투하형
        else if (Data.IsFalling)
            StartCoroutine(FireByDrop());

        // 지속형
        else if (Data.IsContinuous)
            StartCoroutine(FireContinuous());

        // 유동형
        else if (Data.HasFelxiblePath)
            FireByFlexiblePath();
    }

    #region 타입별 격발 코루틴

    private IEnumerator FireByDir()
    {
        while (true)
        {
            if (_durationTimer <= 0)
                break;

            this.transform.position += Data.MoveSpeed * _dir * Time.deltaTime;

            _durationTimer -= Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FireByRotation()
    {
        while (true)
        {
            if (_durationTimer <= 0)
                break;

            this.transform.RotateAround
                    (Data.User.transform.position, _dir , Data.MoveSpeed * Time.deltaTime);

            _durationTimer -= Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FireByDrop()
    {
        _collider.enabled = false;

        this.transform.DOKill();
        this.transform.DOMove(_landPos, 1 / Data.MoveSpeed).SetEase(Ease.Linear)
            .OnComplete(()=>
            _collider.enabled = true
            );

        while (true)
        {
            if (_durationTimer <= 0)
                break;

            _durationTimer -= Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FireContinuous()
    {
        while (true)
        {
            yield return null;
        }
    }

    #endregion

    #region 격발 후 유동경로형태

    private void FireByFlexiblePath()
    {
        //if (this._weaponType == 도끼)
        //    StartCoroutine(GravityFall(3f));
        //else if (this._weaponType == 십자가)
        //    ReturnToBack();
    }

    private IEnumerator GravityFall(float mass)
    {
        float gForce = Gravity * mass;
        Vector2 velocity = new Vector2(Random.Range(-5f, 5f), Data.MoveSpeed);

        while (true)
        {
            if (_durationTimer <= 0)
                break;

            velocity += new Vector2(0, gForce) * Time.deltaTime;
            _durationTimer -= Time.deltaTime;

            yield return null;
        }
    }

    private void ReturnToBack()
    {
        this.transform.DOKill();
        this.transform.DOMove
            (Data.MoveSpeed * _dir, Data.Duration*0.3f).SetEase(Ease.OutCubic)
            
            .OnComplete(() =>
            this.transform.DOMove
            (Data.MoveSpeed * (_dir * -1), Data.Duration*0.7f).SetEase(Ease.InCubic));
    }

    #endregion

    // 충돌 후 처리확인
    private void CheckPostProcess()
    {
        if (!Data.HasPostProcess)
            return;

        else if (Data.IsCollisionMob)
            SetNextProcessCollMob();

        //else if (Data.IsCollisionScn)
        //    SetNextProcessCollScn();

        if (Data.HasReflection)
            _dir = GetReflectionDir(_dir); // 기능부족

    }

    #region 후처리 체크로직

    private void SetNextProcessCollMob()
    {
        int count = --_passCount;

        Process = eWeaponProcess.ProcessFinish;
    }

    //private void SetNextProcessCollScn()
    //{

    //}

    #endregion

    #region 작동시간 이후처리

    private void ProcessFinish()
    {
        this.transform.DOKill();
        StopAllCoroutines();

        ReturnObj();
    }

    #endregion

    #region 부수 기능

    private Vector3 GetRandDir()
    {
        return new Vector3(_dir.x, Random.Range(-10f, 10f));
    }

    protected virtual Vector3 GetReflectionDir(Vector3 dir) // 수정필요해보임
    {
        float angleX = Mathf.Abs(dir.x);
        float angleY = Mathf.Abs(dir.y);

        if (angleX > angleY)
            dir.x *= -1;
        else
            dir.y *= -1;

        return dir;
    }

    #endregion

    private void Init()
    {
        this.transform.position = _data.User.transform.position;
        this.transform.rotation = Quaternion.identity;
        _durationTimer = Data.Duration;
        _passCount = Data.PassCount;
        _firePos = Data.User.transform.position;
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.TryGetComponent<Base_Unit>(out _contactUnit);
            
            Process = eWeaponProcess.PostProcess;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Data.IsCollisionScn)
            return;

        if (collision.CompareTag("ScreenBox"))
        {
            Process = eWeaponProcess.PostProcess;
        }
    }

    public void ReturnObj()
    {
        ObjPoolManager.Instance.ReturnObj(ePoolingType.Weapon, this.gameObject);
    }

    #region fff

    //public Character _user = null;
    //protected CharacterStat _userStat = null;
    //protected abstract eWeaponType weaponType { get; }
    //public eCollectionType _collectionType = eCollectionType.None;
    //protected WeaponStat _wpStat = new WeaponStat();
    //protected SpriteRenderer _render;
    //protected Base_Unit _hitUnit;

    //public WeaponStat WpStat => _wpStat;

    //#region 행위 변수값들
    //public Data_Weapon_Funcs _wpInfo;

    //protected Vector3 _targetPos = Vector3.zero;
    //protected Vector3 _startPos = Vector3.zero;
    //protected Vector3 _dir = Vector3.zero;
    //protected Vector3 _incomingDir = Vector3.zero;
    //protected float _durationTimer = 0f;
    //protected bool _hasDir = false;
    //protected int _passCount = int.MaxValue;
    //#endregion

    //private eWeaponFuncs _wpFuncs;
    //public eWeaponFuncs WpFuncs
    //{
    //    get { return _wpFuncs; }
    //    set
    //    {
    //        _wpFuncs = value;
    //        switch (_wpFuncs)
    //        {
    //            case eWeaponFuncs.None:
    //                break;
    //            case eWeaponFuncs.Targeting:
    //                SetTarget();
    //                break;
    //            case eWeaponFuncs.Continuous:
    //                SetActFreq();
    //                break;
    //            case eWeaponFuncs.CollEffect:
    //                SetCollEffect();
    //                break;
    //            case eWeaponFuncs.MobCollision:
    //                SetMobCollAct();
    //                break;
    //            case eWeaponFuncs.ScreenCollision:
    //                SetScreenCollAct();
    //                break;
    //            case eWeaponFuncs.Reflection:
    //                SetReflection();
    //                break;
    //            case eWeaponFuncs.NeedDir:
    //                SetDir();
    //                break;
    //            case eWeaponFuncs.CtrlDir:
    //                SetCtrlDir();
    //                break;
    //            case eWeaponFuncs.FlexiblePath:
    //                SetPath();
    //                break;
    //            case eWeaponFuncs.Rotatable:
    //                SetRotate();
    //                break;
    //            case eWeaponFuncs.Falling:
    //                SetFalling();
    //                break;
    //            case eWeaponFuncs.SpecipicStartPos:
    //                SetStartPos();
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    //protected virtual void Init()
    //{
    //    _collectionType = eCollectionType.None;

    //    Vector3 _targetPos = Vector3.zero;
    //    Vector3 _startPos = Vector3.zero;
    //    Vector3 _dir = Vector3.zero;
    //    Vector3 _incomingDir = Vector3.zero;
    //    float _durationTimer = 0f;
    //    bool _hasDir = false;
    //    int _passCount = int.MaxValue;
    //}

    //public virtual void WeaponSetReady(Character user)
    //{
    //    ApplyUserStat(user);

    //    if (_wpStat.PassCount > 0)
    //        this._passCount = (int)_wpStat.PassCount;

    //    WpFuncs = eWeaponFuncs.Targeting; // 발사 표적설정 , 방향이 같이잡혀야함
    //    WpFuncs = eWeaponFuncs.SpecipicStartPos; // 투사체 발사 시작지점

    //    if (_wpInfo.Continuous)
    //        WpFuncs = eWeaponFuncs.Continuous;
    //    else
    //        this._durationTimer = _wpStat.Duration;
    //}

    //protected virtual void SetTarget()
    //{
    //    // 타겟팅이라면 가까운적을 찾아 대상으로 지정(방향이 같이 잡힘)
    //    if (_wpInfo.Targeting)
    //    {
    //        _targetPos = _user.GetClosetUnitPos();
    //        _dir = Vector3.Normalize(_targetPos);
    //    }

    //    // 논타겟이라면 방향이 필요
    //    else if (_wpInfo.NeedDir)
    //        WpFuncs = eWeaponFuncs.NeedDir;

    //    else
    //    {
    //        _targetPos = Vector3.zero;
    //    }
    //}

    //protected virtual void SetDir()
    //{
    //    this._hasDir = true;

    //    if (_wpInfo.CtrlDir) // 방향 제어가 가능한 타입이라면 별도 설정으로 진입
    //        WpFuncs = eWeaponFuncs.CtrlDir;

    //    else                  // 제어가 없다 = 무작위 투사체
    //        _dir = GetRandDir();
    //}

    //protected virtual void SetCtrlDir()
    //{
    //    _dir = _user.CharacterDir; // 캐릭터의 조종 방향으로 설정
    //}


    ///// <summary>
    ///// 발생지점
    ///// </summary>
    //protected virtual void SetStartPos()
    //{
    //    if (!this._wpInfo.SpecipicStartPos) // 특정된 시작지점이 없다면
    //        _startPos = this._user.transform.position; // 시작지점은 현재위치에서 투척예정

    //    else if (this._wpInfo.Falling) // 떨어지는 타입이라면
    //        WpFuncs = eWeaponFuncs.Falling; // 시작지점을 별도로 설정

    //    // 떨어지는 타입도 아니며 특정 지점이 있다면 _startPos를 설정
    //}

    //protected virtual void SetFalling() // 떨어지는 타입이라면 
    //{                                   // 성수 - 필드로 떨어지는 모습을 보이며 바닥에서 범위생성
    //                                    // 번개 - 필드에 번개를 떨어뜨림 성수와 반대로 지속없음
    //                                    // 킵 고민중
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //protected virtual void SetCollEffect()
    //{
    //    this._passCount--;

    //    if (this._wpStat.PassCount <= 0)
    //        ReturnObj();

    //    if (_wpInfo.MobCollision)
    //        WpFuncs = eWeaponFuncs.MobCollision;
    //    else if (_wpInfo.ScreenCollision)
    //        WpFuncs = eWeaponFuncs.ScreenCollision;

    //    Fire();
    //}

    //protected virtual void SetMobCollAct()
    //{
    //    WpFuncs = eWeaponFuncs.Reflection;
    //}

    //protected virtual void SetScreenCollAct()
    //{
    //    WpFuncs = eWeaponFuncs.Reflection;
    //}

    //protected virtual void SetReflection()
    //{
    //    _dir = GetRandBounceDir(); // 입사각을 확인한 반사각으로 설정
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //protected virtual void SetPath()
    //{
    //    if (_wpInfo.FlexiblePath) // 십자가 일정시간 이후 유저의 뒷방향으로 다시 되돌아감
    //        FlexibleMoveLogic();  // 해당 메서드 오버라이드 필요

    //}

    //protected void FlexibleMoveLogic()
    //{
    //}

    //protected virtual void SetRotate()
    //{
    //}

    //protected virtual void SetActFreq()
    //{

    //}

    //protected void Awake()
    //{
    //    _render = GetComponentInChildren<SpriteRenderer>();
    //    SetSprite();
    //}



    //protected void ApplyUserStat(Character user)
    //{
    //    WeaponStat wpStat = this._wpStat;

    //    wpStat.SetStats(Global_Data.weaponTable[(int)weaponType]);

    //    this._user = user;

    //    _userStat = (CharacterStat)user.UnitStat;

    //    wpStat.AddDamage(_userStat.Damage)
    //        .AddSpeed(_userStat.ThrowSpeed)
    //        .AddDelay(-1 * _userStat.Delay)
    //        .AddDuration(_userStat.Duration)
    //        .AddThrowCount(_userStat.ThrowCount);

    //    wpStat.SetCollectType(Global_Data.weaponTable[(int)weaponType].collectType);
    //}

    //protected virtual void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Monster"))
    //    {
    //        if (this._wpInfo.CollEffect)
    //        {
    //            WpFuncs = eWeaponFuncs.CollEffect;
    //        }

    //        if (this._wpInfo.reflection)
    //        {
    //            _incomingDir = Vector3.Normalize(_user.transform.position + collision.transform.position);
    //        }

    //        collision.TryGetComponent<Base_Unit>(out _hitUnit);
    //    }
    //}

    //protected virtual Vector3 GetRandBounceDir()
    //{
    //    float angleX = Math.Abs(_incomingDir.x);
    //    float angleY = Math.Abs(_incomingDir.y);

    //    if (angleX > angleY)
    //        _dir.x *= -1;
    //    else
    //        _dir.y *= -1;

    //    return _dir;
    //}

    //protected virtual Vector3 GetRandDir()
    //{
    //    float randX = UnityEngine.Random.Range(-10f, 10f);
    //    float randY = UnityEngine.Random.Range(-10f, 10f);

    //    return new Vector3(randX, randY);
    //}

    //public void SetSprite()
    //{
    //    this._render.sprite = Resources.Load<Sprite>($"{WeaponDirPath + weaponType.ToString()}");
    //}

    //public virtual void Fire()
    //{
    //}



    //public Weapon WeaponCopy(GameObject obj)
    //{
    //    return (Weapon)this.MemberwiseClone();
    //}
    #endregion
}



