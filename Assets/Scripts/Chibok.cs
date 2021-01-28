using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Chibok : Common
{
    private MoveManager moveManager;

    public void InitializeChibok(MoveManager moveManager)
    {
        this.moveManager = moveManager;
    }


    public void Move(){
        //체크 후
        moveManager.PathFinding(new Vector2Int(1, 2));
        //이동
        moveManager.Move(moveManager.FinalNodeList[0]);
    }

    public override void ReceiveDamage(int Damage)
    {
        //게임 종료   
    }
}