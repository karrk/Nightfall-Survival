using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharacterController : MonoBehaviour
{
    // TODO) 캐릭터 이미지가 아닌 스프라이트로 교체 후 거리 맞추기
    [SerializeField]
    private JoyStick joystick;

    private Rigidbody2D rigidbody2D;

    public float movementSpeed = 1.0f;
    

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(joystick.GetDirection() != Vector2.zero)
        {
            Move(joystick.GetDirection());
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
        }
    }

    public void Move(Vector2 pos)
    {
        Vector2 movement = new Vector2(pos.x, pos.y);
        movement.Normalize();

        rigidbody2D.velocity = movement * movementSpeed;
    }
}
