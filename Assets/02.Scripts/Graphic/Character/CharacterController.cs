using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharacterController : MonoBehaviour
{
    // TODO)
    // 캐릭터 이미지가 아닌 스프라이트로 교체 후 거리 맞추기
    [SerializeField]
    private JoyStick joystick;
    //private JoyStick joyStick;

    ////////////////////////////////
    public float movementSpeed = 3.0f;
    
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
        float x = pos.x;
        float y = pos.y;

        Vector3 velocity = new Vector3(x, y, 0);
        velocity *= movementSpeed;
        rigidbody2D.velocity = velocity;
    }
}
