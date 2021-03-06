﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//N초 동안 작동하는 오브젝트들의 상위 추상클래스였던 것
public abstract class Common : MonoBehaviour
{
    protected GameManager gameManager;
    protected MoveManager moveManager;

    public abstract void ReceiveDamage(int Damage);
    public void Move()
    { 
        //체크 후
        //moveManager.PathFinding(transform, targetPosition);
        //이동
        if (moveManager.FinalNodeList.Count == 1) return;
        moveManager.Move(transform, moveManager.FinalNodeList[1]);
    }
}