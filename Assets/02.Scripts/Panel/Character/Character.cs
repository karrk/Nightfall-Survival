using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Base_Unit
{
    [SerializeField]
    private JoyStick _joystick;

    [SerializeField]
    private Weapon testWeapon;

    private float immunityTime = 0.5f;

    bool _isMoving = false;

    private CharacterStat characterStat = null;
    private eCharacterKind characterKind = eCharacterKind.None;

    // getter setter
    public override BaseStat UnitStat { get { return characterStat; } }
    protected override float ImmunityTime { get { return immunityTime; } }

    private Dictionary<eWeaponType, Weapon> _inventory
        = new Dictionary<eWeaponType, Weapon>();

    private Dictionary<eWeaponType, float> _coolTimes
        = new Dictionary<eWeaponType, float>();

    private List<int> _weaponsNums = new List<int>();

    public override void Init()
    {
        base.Init();

        _isDead = false;

        Data_Character data = Global_Data.characterTable[(int)characterKind];

        GameObject obj = ObjPoolManager.Instance.GetObj(ePoolingType.Weapon, this.transform);
        Weapon wp = obj.AddComponent<RotateWeapon>();

        wp.ApplyUserStat(this);

        _inventory.Add(data.weapon, wp);
        _coolTimes.Add(data.weapon, wp.WpStat.Delay);
        _weaponsNums.Add((int)wp.WpStat.ID);

        obj.SetActive(false);
        StartCoroutine(TimerStart(0.1f));
    }

    IEnumerator TimerStart(float frequency)
    {
        while (true)
        {
            for (int i = 0; i < _weaponsNums.Count; i++)
            {
                _coolTimes[(eWeaponType)_weaponsNums[i]] -= frequency;
                
                if (_coolTimes[(eWeaponType)_weaponsNums[i]] <= 0)
                {
                    _inventory[(eWeaponType)_weaponsNums[i]].Use();
                    _coolTimes[(eWeaponType)_weaponsNums[i]] =
                         _inventory[(eWeaponType)_weaponsNums[i]].WpStat.Delay;
                }
            }

            yield return new WaitForSeconds(frequency);
        }
    }


    public void SetKind(eCharacterKind kind)
    {
        characterKind = kind;
    }
    
    public void SetJoyStick(JoyStick joyStick)
    {
        this._joystick = joyStick;
    }

    void FixedUpdate()
    {
        if (_isDead)
            return;

        // 조이스틱 입력 시 move
        if (_joystick.GetDirection() != Vector2.zero)
        {
            if(!_isMoving)
            {
                _isMoving = true;
                UnitState = eUnitStates.Move;
            }

            Input_Move();
        }
        else
        {
            if(_isMoving)
            {
                _isMoving = false;
                UnitState = eUnitStates.Idle;
            }

            _rb.velocity = Vector3.zero;
        }
    }

    #region 상태
    protected override void Dead()
    {
        base.Dead();
        GameManager.Instance.Event.CallEvent(eEventType. CharacterDead);
    }

    protected override void Idle()
    {
        _anim.SetMoveAnim(false);
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void OnDamage()
    {
        base.OnDamage();
    }

    bool _isDead;
    #endregion

    public void SetStat(eCharacterKind kind)
    {
        CharacterStat stat = new CharacterStat();
        stat.SetStats(stat, Global_Data.characterTable[(int)kind]);
        this.characterStat = stat;
    }

    public void Input_Move()
    {
        Vector2 pos = _joystick.GetDirection();

        float x = pos.x;
        float y = pos.y;

        Vector3 velocity = new Vector3(x, y, 0);
        velocity.Normalize();

        if (velocity.x > 0)
        {
            _renderer.flipX = false;
            //facingDirection = 1;
        }
        else
        {
            _renderer.flipX = true;
            //facingDirection = -1;
        }

        this.transform.position += velocity * UnitStat.MoveSpeed * Time.fixedDeltaTime;
    }
}
