using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : Common
{
    private bool isHorizontalMove;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //움직임 체크하여 움직인다.
        if (!isTired)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //transform.position = Vector3.MoveTowards(, 1f);
            //애니메이션 추가
            Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
        }
    }

    public override void ReceiveDamage()
    {
        
    }

    void Move()
    {
        //이동
    }

    void Attack()
    {
        //공격
    }
}