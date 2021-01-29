using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 몬스터의 상위 클래스
public class Monster : Common
{
    protected Player player;
    //public bool isRange;
    Stat monsterStat = new Stat();
    
    public void InitializeMonster(Player player, GameManager gameManager, MoveManager moveManager)
    {
        this.player = player;
        this.gameManager = gameManager;
        this.moveManager = moveManager;
    }
    
    //N초마다 활동
    public void Active()
    {
        Debug.Log("몬스터 활동");
        //transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        //AttackRange 스크립트에서 collider 체크 후  공격 혹은 이동 실행
        //Raycast로 얻어오고 있다면 그것을 체크해서 공격 혹은 이동
        
        //if(Raycast2d)
        //Attack();
        //Move(new Vector2Int((int)player.gameObject.transform.position.x, (int)player.transform.position.y));
        Physics2D.BoxCastAll(transform.position, size, 0f, Vector2.zero, 0f, layermask)
    }
    
    //스폰 - 몬스터 활성화
    public void ActiveMonster()
    {
        monsterStat.curHp = monsterStat.maxHp;
        gameManager.activeMonsters.Add(this);
        gameObject.SetActive(true);
    }

    public override void ReceiveDamage(int Damage)
    {
        monsterStat.curHp -= Damage;
        Debug.Log($"현재 몬스터 체력 : {monsterStat.curHp}");
        if (monsterStat.curHp <= 0)
        {
            Debug.Log("몬스터 사망");
            DestroyMonster();
        }
    }

    public void Attack(Common common)
    {
    }

    public void DestroyMonster()
    {
        gameManager.activeMonsters.Remove(this);
        gameObject.SetActive(false);
    }
    [System.Serializable]
    public class Stat
    {
        public int curHp;
        public int maxHp;
        public int damage;
    }
}