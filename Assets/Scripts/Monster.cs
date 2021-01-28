using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 몬스터의 상위 클래스
public abstract class Monster : Common
{
    private Player player;
    private GameManager gameManager;
    public bool isRange;
    public Stat monsterStat = new Stat();
    
    public void InitializeMonster(Player player, GameManager gameManager)
    {
        this.player = player;
        this.gameManager = gameManager;
    }
    
    //N초마다 활동
    public override void Active()
    {
        transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        //콜라이더 체크 후 공격 혹은 이동 실행
    }
    
    //스폰 - 몬스터 활성화
    public void ActiveMonster()
    {
        //초기화
        gameManager.activeCommons.Add(this);
    }

    public override void ReceiveDamage(int Damage)
    {
        monsterStat.hp -= Damage;
        if(monsterStat.hp <= 0)
            DestroyMonster();
    }

    public abstract void Attack();
    public abstract void Move();

    void DestroyMonster()
    {
        gameManager.activeCommons.Remove(this);
        gameObject.SetActive(false);
    }

    public class Stat
    {
        public int hp;
        public int damage;
    }
}