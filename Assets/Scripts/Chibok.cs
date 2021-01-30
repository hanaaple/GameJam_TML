using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Chibok : Common
{
    public Transform targetPosition;
    private Animator anim;
    private Vector3 moveVec;
    public GameObject End;
    
    public void InitializeChibok(MoveManager moveManager, GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.moveManager = moveManager;
        anim = gameObject.GetComponent<Animator>();
    }

    public void Active()
    {
        //타켓 위치로 한칸 이동
        //확률에 따라 산책가도록 변경해야됨
        moveManager.PathFinding(transform, new Vector2Int((int)targetPosition.position.x, (int)targetPosition.position.y));
        if (moveManager.FinalNodeList.Count >= 2)
        {
            moveVec = new Vector3(moveManager.FinalNodeList[1].x - transform.position.x,
                moveManager.FinalNodeList[1].y - transform.position.y);
            if ((int) moveVec.y == 1)
            {
                anim.SetInteger("isVertical", 1);
                anim.SetBool("isHorizontal", false);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                //뒤
            }
            else if ((int) moveVec.y == -1)
            {
                anim.SetBool("isHorizontal", false);
                anim.SetInteger("isVertical", -1);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                //앞
            }
            else if ((int) moveVec.x == 1)
            {
                anim.SetInteger("isVertical", 0);
                anim.SetBool("isHorizontal", true);
                //오른쪽
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if ((int) moveVec.x == -1)
            {
                anim.SetInteger("isVertical", 0);
                anim.SetBool("isHorizontal", true);
                //왼쪽
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            Move();
        }
    }
    
    IEnumerator HitAni(float time)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        float curTime = 0;
        while(curTime < time)
        {
            curTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;            
        }
        anim.SetBool("isHit", false);
    }
    
    public override void ReceiveDamage(int Damage)
    {
        //gameManager.isBattleMode = false;
        //Animator anim = gameObject.GetComponent<Animator>();
        //방향에 따라
        //anim.SetTrigger("");
        anim.SetBool("isHit", true);
        if ((int) moveVec.y == 1)
        {
            anim.SetTrigger("HitBack");
            //뒤
        }
        else if ((int) moveVec.y == -1)
        {
            anim.SetTrigger("HitFront");
            //앞
        }
        else if ((int) moveVec.x != 0)
        {
            anim.SetTrigger("HitSide");
        }
        
        StartCoroutine(HitAni(0.1f));
        Invoke("ChibokDead", 0.1f);
    }

    void ChibokDead()
    {
        Debug.Log("치복이 사망");
        End.SetActive(true);
        gameManager.isBattleMode = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Convenient Store"))
        {
            //gameManager.isBattleMode = false;
            // foreach (Monster monster in gameManager.activeMonsters)
            // {
            //     monster.DestroyMonster();
            // }
            //미연시 시작
        }
    }
}