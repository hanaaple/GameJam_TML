using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 몬스터의 상위 클래스
public class Monster : Common
{
    private Player player;
    private GameManager gameManager;
    
    public void InitializeMonster(Player player, GameManager gameManager)
    {
        this.player = player;
        this.gameManager = gameManager;
    }

    public void ActiveMonster()
    {
        //초기화
        gameManager.activeCommons.Add(this);
    }

    public override void ReceiveDamage(int Damage)
    {
    }

    void move()
    {
        
    }

    void DestroyMonster()
    {
        gameManager.activeCommons.Remove(this);
    }

    class Stat
    {
        public int Damage;
    }
}