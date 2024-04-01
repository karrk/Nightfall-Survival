using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private JoyStick joystick;

    public float movementSpeed = 2.0f;
    
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private float delayToIdle = 0.0f;
    private int facingDirection = 1;
    private bool isDead = false;
    private bool isDeadNoBlood = false;
    private bool isHurt = false;

    // getter setter
    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public bool IsDeadNoBlood { get { return isDeadNoBlood; } set { isDeadNoBlood = value; } }
    public bool IsHurt { get {  return isHurt; } set { isHurt = value; } }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(isHurt == true)
        {
            animator.SetTrigger("Hurt");
            return;
        }
        
        if(isDead == true)
        {
            animator.SetTrigger("Death");
            animator.SetBool("noBlood", isDeadNoBlood);

            return;
        }

        // 조이스틱 입력 시 move
        if(joystick.GetDirection() != Vector2.zero)
        {
            Move(joystick.GetDirection());

            // Reset timer
            delayToIdle = 0.05f;
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;

            // Prevents flickering transitions to idle
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                animator.SetInteger("AnimState", 0);
        }
    }

    public void Move(Vector2 pos)
    {
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

        velocity *= movementSpeed;
        rigidbody2D.velocity = velocity;
    }


}
