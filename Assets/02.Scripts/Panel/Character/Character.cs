using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;

    // TODO) 임시 이므로 수정해야함
    int characterType;
    int weaponType = 0;

    int maxHP = 0;
    int remainHP = 0;
    int attackPower = 0;
    int defendPower = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();    
    }

    public int CalculateDamage(int damageAmount)
    {
        if(damageAmount <= 0)
        {
            return 0;
        }

        return damageAmount * (1 / (1 + defendPower));
    }
}
