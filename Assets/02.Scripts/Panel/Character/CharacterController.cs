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
    private Animator m_animator;

    private float m_delayToIdle = 0.0f;
    private int m_facingDirection = 1;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 조이스틱 입력 시 move
        if(joystick.GetDirection() != Vector2.zero)
        {
            Move(joystick.GetDirection());

            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;

            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
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
            m_facingDirection = 1;

        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        velocity *= movementSpeed;
        rigidbody2D.velocity = velocity;
    }
}
