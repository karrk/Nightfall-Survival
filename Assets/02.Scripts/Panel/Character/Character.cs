using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Base_Unit
{
    private const float SubRoutineDelay = 0.1f;

    [SerializeField]
    private JoyStick _joystick;

    public override BaseStat UnitStat { get { return _unitStat; } }
    private CharacterStat _unitStat = new CharacterStat();

    private eCharacterKind _characterKind = eCharacterKind.None;

    protected override float ImmunityTime { get { return 0.5f; } }

    //int[무기의 현재 쿨타임,무기의 기본 쿨타임]
    private Dictionary<eWeaponType, float[]> _wpCoolTimes =
        new Dictionary<eWeaponType, float[]>();

    Data_Character _characterData;

    private bool _isMoving;
    private bool _isDead;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.Event.RegisterEvent(eEventType.StageSetupCompleted, Init);
        GameManager.Instance.Event.RegisterEvent(eEventType.OnGameComplete, ActiveCharacter);
    }

    public override void Init()
    {
        Global_Data._character = this;

        _rb.isKinematic = false;

        _anim.Init();
        this._characterKind = Global_Data._selectedCharacter;
        
        _characterData = Global_Data.characterTable[(int)_characterKind];
        UnitStat.SetStats(_characterData);

        _isDead = false;
        _isMoving = false;

        // 인벤토리, 장비가 제작되면 반영, 수정
        Global_Data._inventory.Clear();
        _wpCoolTimes.Clear();

        AddWeapon(_characterData.weapon);
    }

    // 캐릭터 무기적용 임시함수
    public void AddWeapon(eWeaponType type)
    {
        if (!_wpCoolTimes.ContainsKey(type))
            _wpCoolTimes.Add(type, new float[2]);

        GameObject obj = ObjPoolManager.Instance.GetObj(ePoolingType.Weapon,this.transform);
        string componentName = type.ToString();
        Weapon wp = (Weapon)obj.AddComponent(Type.GetType(componentName));
        obj.SetActive(false);

        wp.ApplyUserStat(this);

        Global_Data._inventory.Add(_characterData.weapon, wp);
        _wpCoolTimes[type][0] = 0;
        _wpCoolTimes[type][1] = wp.WpStat.Delay;
    }

    public void ActiveCharacter()
    {
        StartCoroutine(MainCoroutine());
    }

    #region 상태머신 Override

    protected override void Idle()
    {
        _anim.SetMoveAnim(false);
    }

    protected override void Dead()
    // 애니메이션을 재생하고 이벤트를 호출할까
    // 애니메이션 재생과 이벤트를 동시에 호출할까 (현재)
    {
        base.Dead();
        _isDead = true;
        _rb.isKinematic = true;

        Global_Data._character = null;
        StopAllCoroutines();

        GameManager.Instance.Event.CallEvent(eEventType.CharacterDead);
    }

    #endregion

    #region 유저 입력처리

    private void InputLogic()
    {
        if (_joystick.GetDirection() != Vector2.zero)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                UnitState = eUnitStates.Move;
            }
            Input_Move();
        }
        else
        {
            if (_isMoving)
            {
                _isMoving = false;
                UnitState = eUnitStates.Idle;
            }

            _rb.velocity = Vector3.zero;
        }
    }

    private Vector3 _tempVec;
    private float _moveRate;
    private Vector3 _dir;

    private void Input_Move()
    {
        _tempVec = _joystick.GetDirection();

        _moveRate = Vector2.Distance(_joystick.transform.position,_tempVec) * 0.01f;


        _moveRate = Math.Clamp(_moveRate, 0.2f, 1);
        _dir = Vector3.Normalize(_tempVec);

        this.transform.position += _dir * UnitStat.MoveSpeed * _moveRate * Time.deltaTime;
    }

    #endregion

    #region 코루틴 함수

    IEnumerator MainCoroutine()
    {
        float innerTimer = 0;

        while (true)
        {
            if (_isDead)
                break;

            InputLogic();

            if (innerTimer >= SubRoutineDelay)
            {
                SubCoroutine(SubRoutineDelay);

                innerTimer = 0;
            }
            else
                innerTimer += Time.deltaTime;


            yield return null;
        }
    }

    public void SubCoroutine(float freq)
    {
        WeaponsCheckLogic(freq);

        if (_isMoving)
        {
            base.SetSortOrder();
            SetFlipX();
        }
    }

    private void WeaponsCheckLogic(float frequency)
    {
        foreach (var e in _wpCoolTimes)
        {
            eWeaponType weapon = e.Key;

            if (_wpCoolTimes[weapon][0] <= 0)
            {
                _wpCoolTimes[weapon][0] = _wpCoolTimes[weapon][1];
                Global_Data._inventory[weapon].Use();
            }
            else
            {
                _wpCoolTimes[weapon][0] -= frequency;
            }
        }
    }

    private void SetFlipX()
    {
        if (_dir.x > 0)
            _renderer.flipX = false;
        else
            _renderer.flipX = true;
    }

    #endregion

}
