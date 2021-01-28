using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : Common
{
    private bool isHorizontalMove;

    private void Awake()
    {
    }

    void Update()
    {
        //움직임 체크하여 움직인다.
        //if (!isTired)
        {
            // 공격 시 방향 조절은 코루틴으로
            //코루틴에서 키를 누른 상태에서 움직일 경우 그에 따라 방향 결정
            //키를 땔 경우 공격
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("공격이다!");
                switch(Input.inputString)
                {
                    case "z":
                    case "Z":
                        break;
                    case "x":
                    case "X":
                        break;
                    case "c":
                    case "C":
                        break;
                }

                isTired = true;
                
            }
            else
            {
                bool hDown = Input.GetButtonDown("Horizontal");
                bool vDown = Input.GetButtonDown("Vertical");
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                if (hDown || vDown)
                {
                    Debug.Log(" 방향키 눌러써!");
                    
                    //동시 입력 방지
                    if (hDown)
                        isHorizontalMove = true;
                    else if (vDown)
                        isHorizontalMove = false;

                    Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
                    transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + moveVec, 3f);
                    //애니메이션 추가
                    isTired = true;
                }
            }
        }
    }

    public override void ReceiveDamage()
    {
        
    }

    void Move()
    {
        //이동
    }

    // 기본 공격 - z
    void Attack()
    {
        //공격
    }

    // 좀 쎈 공격 - x
    void StrongAttack()
    {
        
    }
    
    // 원거리 공격 - c
    void RangeAttack()
    {
        
    }

    class Stat
    {
        public int hp;
        public int damage;
        
    }
}