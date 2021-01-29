using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Chibok : Common
{
    public Transform targetPosition;
    
    public void InitializeChibok(MoveManager moveManager, GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.moveManager = moveManager;
    }

    public void Active()
    {
        //타켓 위치로 한칸 이동
        //확률에 따라 산책가도록 변경해야됨
        Move(new Vector2Int((int)targetPosition.position.x, (int)targetPosition.position.y));
    }

    public override void ReceiveDamage(int Damage)
    {
        Debug.Log("치복이 사망");
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