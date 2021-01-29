﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : Common
{
    private bool isHorizontalMove;
    public Stat playerStat;
    internal bool isTired;
    private bool isRange;
    private Vector3 moveVec;  //처음 값 설정해야될듯?
    private Animator anim;

    public void InitializePlayer()
    {
        anim = GetComponent<Animator>();
        playerStat = new Stat();
    }
    void Update()
    {
        //움직임 체크하여 움직인다.
        if (!isTired)
        {
            // 공격 시 방향 조절은 코루틴으로
            //코루틴에서 키를 누른 상태에서 움직일 경우 그에 따라 방향 결정
            //키를 땔 경우 공격
            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("공격이다!");
                if (Input.GetKey(KeyCode.Z))
                    Attack();
                else if (Input.GetKey(KeyCode.X))
                    StrongAttack();
                else if (Input.GetKey(KeyCode.C))
                    RangeAttack();
            }
            else
            {
                if(!Input.GetButton("Fire1"))
                    Move();
            }
        }
    }

    public override void ReceiveDamage(int Damage)
    {
        playerStat.curHp -= Damage;
        if(playerStat.curHp <= 0)
        {
            Debug.Log("플레이어 맞음");
            // 사망
        }
    }

    void Move()
    {
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (hDown || vDown)
        {
            //Debug.Log("방향키 눌러써!");

            //동시 입력 방지
            if (hDown)
                isHorizontalMove = true;
            else if (vDown)
                isHorizontalMove = false;

            moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
            //transform.position = Vector2.MoveTowards(transform.position,  transform.position + moveVec, 3f);
            //애니메이션 추가
            StartCoroutine(asd(transform, transform.position + moveVec));
            isTired = true;
        }
    }
    IEnumerator asd(Transform trans, Vector3 target)
    {
        float count = 0;
        Vector3 wasPos = trans.position;
        while (true)
        {
            count += Time.deltaTime * 5f;
            trans.position = Vector3.Lerp(wasPos, target, count);
            if (count >= 1)
            {
                trans.position = target;
                break;
            }
            
            yield return null;
        }
        
    }
    
    IEnumerator ShowAttackRange(KeyCode keyCode)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        while (Input.GetKey(keyCode))
        {
            //피벗이 현재 캐릭터 방향으로 되며 방향키를 누를 시 방향 변경
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            bool hDown = Input.GetButtonDown("Horizontal");
            bool vDown = Input.GetButtonDown("Vertical");
            if (hDown || vDown)
            {
                //Debug.Log("누른상태로 움직였다");
                if (hDown)
                    isHorizontalMove = true;
                else if (vDown)
                    isHorizontalMove = false;
                moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
                //회전
                Transform pivotTransform = transform.GetChild(0).transform;
                pivotTransform.rotation = Quaternion.Euler(pivotTransform.position.x, pivotTransform.position.y,
                    Quaternion.FromToRotation(Vector3.up, moveVec).eulerAngles.z);
            }
            yield return waitForFixedUpdate;
        }
        if(Input.GetKeyUp(keyCode))
        {
            int layerMask = (1 << LayerMask.NameToLayer("Monster"));

            Transform attackRange = transform.GetChild(0).GetChild(0).transform;
            Transform pivotTransform = transform.GetChild(0).transform;  // Z축 방향 전환    시계방향으로 0 -90 -180 90
            //쏘기
            if (moveVec.y == 0)
            {
                attackRange.localScale = new Vector3(attackRange.localScale.y, attackRange.localScale.x);
            }
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position+ moveVec, attackRange.localScale,pivotTransform.rotation.z, moveVec, 0f, layerMask);
            anim.SetBool("isAttack", true);
            if(moveVec.y == -1){
                //앞옆뒤에 따라 초 다르게 설정
                anim.SetTrigger("isAttackFront");
                StartCoroutine(ani(0.417f));
                
            }else if (moveVec.y == 1)
            {
             //뒤
                
            }else if (moveVec.x == 1 || moveVec.x == -1)
            {
                anim.SetTrigger("isAttackSide");
                StartCoroutine(ani(0.333f));
            }
            foreach (RaycastHit2D hit in hits)
            {
                hit.transform.GetComponent<Common>().ReceiveDamage(playerStat.damage);
            }
                
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator ani(float time)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        float curTime = 0;
        while(curTime < time)
        {
            curTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;            
        }

        anim.SetBool("isAttack", false);
    }
    
    
    // 기본 공격 - z
    void Attack()
    {
        Debug.Log("z 누름");
        isTired = true;
        isRange = true;
        transform.GetChild(0).gameObject.SetActive(true);
        Transform attackRange = transform.GetChild(0).GetChild(0).transform;
        attackRange.localPosition = Vector3.up;
        attackRange.localScale = new Vector3(3f, 1f, 1f);
        StartCoroutine(ShowAttackRange(KeyCode.Z));
    }


    // 좀 쎈 공격 - x
    void StrongAttack()
    {
        Debug.Log("x 누름");
        isTired = true;
        transform.GetChild(0).gameObject.SetActive(true);
        Transform attackRange = transform.GetChild(0).GetChild(0).transform;
        attackRange.localPosition = new Vector3(0f, 1.5f);
        attackRange.localScale = new Vector3(3f, 2f, 1f);
        StartCoroutine(ShowAttackRange(KeyCode.X));
    }
    
    // 원거리 공격 - c
    void RangeAttack()
    {
        Debug.Log("c 누름");
        isTired = true;
        transform.GetChild(0).gameObject.SetActive(true);
        Transform attackRange = transform.GetChild(0).GetChild(0).transform;
        attackRange.localPosition = Vector3.up * 3f;
        attackRange.localScale = new Vector3(1f, 5f, 1f);
        StartCoroutine(ShowAttackRange(KeyCode.C));
    }
    [System.Serializable]
    public class Stat
    {
        public int curHp;
        public int maxHp;
        public int damage;
        
    }
}