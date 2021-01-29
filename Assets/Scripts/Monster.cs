using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 몬스터의 상위 클래스
public class Monster : Common
{
    protected Player player;
    //public bool isRange;
    Stat monsterStat = new Stat();
    private MonsterSpawnManager.Type monsterType;
    private bool isTired;
    
    public void InitializeMonster(Player player, GameManager gameManager, MoveManager moveManager)
    {
        this.player = player;
        this.gameManager = gameManager;
        this.moveManager = moveManager;
    }
    
    //N초마다 활동
    public void Active()
    {
        isTired = false;
        // Debug.Log("몬스터 활동");
        

        if (monsterType == MonsterSpawnManager.Type.idle)
            CheckAttack();
        else if (monsterType == MonsterSpawnManager.Type.medium)
        {
            RangeAttack();
            if(!isTired)
                CheckAttack();
        }
        else if (monsterType == MonsterSpawnManager.Type.huge)
        {
            StrongAttack();
            if(!isTired)
                CheckAttack();
        }
        else if (monsterType == MonsterSpawnManager.Type.boss)
        {
        }
        
        if(!isTired)
            Move(new Vector2Int((int)player.transform.position.x, (int)player.transform.position.y));
    }

    private void RayAttack(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            //Debug.Log(hit.transform.name);
            //애니메이션 추가 해야됨
            hit.transform.gameObject.GetComponent<Common>().ReceiveDamage(monsterStat.damage);
        }
        isTired = true;
    }

    void CheckAttack()
    {
        //Debug.Log("몬스터 기본 공격");

        #region 공격 범위 보여주는 용도

        Transform pivotTransform = transform.GetChild(0).transform;
        Transform attackRange = transform.GetChild(0).GetChild(0).transform;
        attackRange.localScale = new Vector3(3f, 1f, 1f);

        #endregion

        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Chibok"));

        //1번 모든 방향으로 체크
        //2번 있다면 그 방향으로 공격
        RaycastHit2D[] hits;
        //상
        attackRange.localPosition = Vector3.up;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.up, attackRange.localScale, 0, Vector3.up, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            //Debug.Log("위");
            RayAttack(hits);
            return;
        }

        //하
        attackRange.localPosition = Vector3.down;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.down, attackRange.localScale, 0, Vector3.down, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            //Debug.Log("아래");
            RayAttack(hits);
            return;
        }

        attackRange.localScale = new Vector3(attackRange.localScale.y, attackRange.localScale.x);
        //좌
        attackRange.localPosition = Vector3.left;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.left, attackRange.localScale, 0, Vector3.left, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            // Debug.Log("왼쪽");
            RayAttack(hits);
            return;
        }

        //우
        attackRange.localPosition = Vector3.right;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.right, attackRange.localScale, 0, Vector3.right, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            // Debug.Log("오른쪽");
            RayAttack(hits);
            return;
        }
        pivotTransform.gameObject.SetActive(false);
        
        // 왜 있는거지
        // moveManager.PathFinding(transform,
        //     new Vector2Int((int) player.transform.position.x, (int) player.transform.position.y));
        //
        // if (moveManager.FinalNodeList.Count >= 2)
        // {
        //     Vector3 moveVec = new Vector3(moveManager.FinalNodeList[1].x - transform.position.x,
        //         moveManager.FinalNodeList[1].y - transform.position.y);
        // }
    }

    void RangeAttack()
    {
        //Debug.Log("중간 몬스터 공격");
        
        Transform pivotTransform = transform.GetChild(0).transform;
        Transform attackRange = transform.GetChild(0).GetChild(0).transform;
        attackRange.localScale = new Vector3(1f, 3f, 1f);


        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Chibok"));

        //1번 모든 방향으로 체크
        //2번 있다면 그 방향으로 공격
        RaycastHit2D[] hits;
        //상
        attackRange.localPosition = Vector3.up * 2f;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.up, attackRange.localScale, 0, Vector3.up, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            //Debug.Log("위");
            RayAttack(hits);
            return;
        }

        //하
        attackRange.localPosition = Vector3.down;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.down, attackRange.localScale, 0, Vector3.down, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            //Debug.Log("아래");
            RayAttack(hits);
            return;
        }

        attackRange.localScale = new Vector3(attackRange.localScale.y, attackRange.localScale.x);
        //좌
        attackRange.localPosition = Vector3.left;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.left, attackRange.localScale, 0, Vector3.left, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            // Debug.Log("왼쪽");
            RayAttack(hits);
            return;
        }

        //우
        attackRange.localPosition = Vector3.right;
        hits = Physics2D.BoxCastAll(transform.position + Vector3.right, attackRange.localScale, 0, Vector3.right, 0f,
            layerMask);
        if (hits.Length != 0)
        {
            pivotTransform.gameObject.SetActive(true);
            // Debug.Log("오른쪽");
            RayAttack(hits);
            return;
        }
        pivotTransform.gameObject.SetActive(false);
    }
    
    void StrongAttack()
    {
        Debug.Log("큰 몬스터 공격");
        
        #region 공격 범위 보여주는 용도
        transform.GetChild(0).gameObject.SetActive(true);
        Transform attackRange = transform.GetChild(0).GetChild(0).transform;
        attackRange.localPosition = Vector3.up * 1.5f;
        attackRange.localScale = new Vector3(3f, 2f, 1f);
        #endregion
        
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Chibok"));
        Vector2 size = attackRange.localScale;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(attackRange.localPosition, size, 0f, Vector2.zero, 0f, layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            hit.transform.gameObject.GetComponent<Common>().ReceiveDamage(monsterStat.damage);            
        }
    }
    

    
    
    
    
    
    
    
    
    //스폰 - 몬스터 활성화
    public void ActiveMonster(MonsterSpawnManager.Type monsterType)
    {
        this.monsterType = monsterType;
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