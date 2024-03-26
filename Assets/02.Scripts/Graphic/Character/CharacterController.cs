using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharacterController : MonoBehaviour
{
    // TODO) 임시 이므로 수정해야함
    // 조이스틱 직접 구현한 것으로 교체
    // 캐릭터 이미지가 아닌 스프라이트로 교체 후 거리 맞추기
    [SerializeField]
    private VirtualJoystick joystick;
    //private JoyStick joyStick;

    public float movementSpeed = 3.0f;
    ////////////////////////////////
    

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 조이스틱 입력 시 move
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
        // TODO) 수정 예정
        /*
        float dirX = pos.x;
        float dirY = pos.y;

        Vector3 moveX = transform.right * dirX;
        Vector3 moveY = transform.up* dirY;

        Vector3 velocity = (moveX + moveY).normalized * movementSpeed;

        rigidbody2D.MovePosition(transform.position + velocity * Time.deltaTime);
        */
        float x = pos.x;
        float y = pos.y;

        Vector3 velocity = new Vector3(x, y, 0);
        velocity *= movementSpeed;
        rigidbody2D.velocity = velocity;

    }
}
