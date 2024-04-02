using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public interface IUnitState
//{
//    public void OnEnter(Base_Unit unit);
//    public void OnExit(Base_Unit unit);
//    public void Update(Base_Unit unit);
//}

//public class IdleState : IUnitState
//{
//    Base_Unit _unit;

//    public void OnEnter(Base_Unit unit)
//    {
//        _unit = unit;
//        unit.Anim.SetIdleAnim(true);
//    }

//    public void OnExit(Base_Unit unit)
//    {
//        unit.Anim.SetIdleAnim(false);
//    }

//    public void Update(Base_Unit unit)
//    {

//    }

//    IEnumerator IdleUpdate(Transform targetTr)
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(0.2f);

//            if (targetTr != null)
//                break;
//        }

//    }

//}