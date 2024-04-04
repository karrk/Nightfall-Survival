using UnityEngine;

public class Character : Base_Unit
{
    [SerializeField]
    private JoyStick joystick;

    [SerializeField]
    private Weapons weapons;

    private float movementSpeed = 2.0f;
    private float immunityTime = 3.0f;

    private int facingDirection = 1;

    private Stat characterStat = new Stat();

    // getter setter
    public override Stat UnitStat { get { return characterStat; } }
    protected override float ImmunityTime { get { return immunityTime; } }


    protected override void Start()
    {
        base.Start();

        // TODO) 임시 코드
        weapons.SettingWeapons(3, new WeaponStat()); 
    }

    void FixedUpdate()
    {
        // 조이스틱 입력 시 move
        if (joystick.GetDirection() != Vector2.zero)
        {
            Input_Move();
        }
        else
        {
            _rb.velocity = Vector3.zero;
            State = eUnitStates.Idle;
        }
    }

    protected override void Dead()
    {
        base.Dead();
        // TODO :: GameManager_Instance.Event.CallEvent(eEventType. Char_Dead);
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
        if (UnitStat.OnDamage(100f) < 0)
        {
            UnitState = eUnitStates.Dead;
        }


        base.OnDamage();
    }


    public void Input_Move()
    {
        UnitState = eUnitStates.Move;

        Vector2 pos = joystick.GetDirection();

        float x = pos.x;
        float y = pos.y;

        Vector3 velocity = new Vector3(x, y, 0);
        velocity.Normalize();

        if (velocity.x > 0)
        {
            _renderer.flipX = false;
            facingDirection = 1;
        }
        else
        {
            _renderer.flipX = true;
            facingDirection = -1;
        }
    }
}
