using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : Common
{
    private bool isHorizontalMove;
    public Stat playerStat;
    internal bool isTired;
    

    public void InitializePlayer()
    {
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

            Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
            transform.position = Vector2.MoveTowards(transform.position, (Vector2) transform.position + moveVec, 3f);
            //애니메이션 추가
            isTired = true;
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
                Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
                //회전
                Transform pivotTransform = transform.GetChild(0).transform;
                pivotTransform.rotation = Quaternion.Euler(pivotTransform.position.x, pivotTransform.position.y,
                    Quaternion.FromToRotation(Vector3.up, moveVec).eulerAngles.z);
            }else if(Input.GetKeyUp(keyCode))
            {
                //공격 판정이 끝날때까지
                //while (true)
                {
                    
                }
                transform.GetChild(0).gameObject.SetActive(false);
            }
            yield return waitForFixedUpdate;
        }
    }
    
    // 기본 공격 - z
    void Attack()
    {
        Debug.Log("z 누름");
        isTired = true;
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