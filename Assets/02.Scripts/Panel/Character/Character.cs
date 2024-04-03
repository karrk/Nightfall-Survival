using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Character : Base_Unit
{
    [SerializeField]
    private JoyStick joystick;

    private float movementSpeed = 2.0f;
    private float immunityTime = 3.0f;

    private int facingDirection = 1;
    private float delayToIdle = 0.0f;

    private Stat characterStat = new Stat();

    // getter setter
    public override Stat UnitStat { get { return characterStat; } }
    protected override float ImmunityTime { get { return immunityTime; } }

<<<<<<< HEAD
=======

>>>>>>> 430c4b9 (#0.0.9 캐릭터를 Base_Unit 상속 구조로 변경)
    void FixedUpdate()
    {
        // 조이스틱 입력 시 move
        if (joystick.GetDirection() != Vector2.zero)
        {
<<<<<<< HEAD
            Input_Move();
=======
            Move();
>>>>>>> 430c4b9 (#0.0.9 캐릭터를 Base_Unit 상속 구조로 변경)

            // 타이머 리셋
            delayToIdle = 0.05f;
        }
        else
        {
            _rb.velocity = Vector3.zero;

            // Idle 로 전환 시 깜빡임 방지
<<<<<<< HEAD
            //delayToIdle -= Time.deltaTime;
            //if (delayToIdle < 0)
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
=======
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                Idle();
        }
    }

    protected override void Idle()
    {
>>>>>>> 430c4b9 (#0.0.9 캐릭터를 Base_Unit 상속 구조로 변경)
        _anim.SetMoveAnim(false);
    }

    protected override void Move()
    {
        base.Move();
<<<<<<< HEAD
    }

    protected override void OnDamage()
    {
        if (UnitStat.OnDamage(100f) < 0)
        {
            State = eUnitStates.Dead;
        }


        base.OnDamage();
    }

    public void Input_Move()
    {
        State = eUnitStates.Move;

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

=======

        Vector2 pos = joystick.GetDirection();

        float x = pos.x;
        float y = pos.y;

        Vector3 velocity = new Vector3(x, y, 0);
        velocity.Normalize();

        if (velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingDirection = 1;

        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingDirection = -1;
        }

>>>>>>> 430c4b9 (#0.0.9 캐릭터를 Base_Unit 상속 구조로 변경)
        velocity *= movementSpeed;
        _rb.velocity = velocity;
    }
}
