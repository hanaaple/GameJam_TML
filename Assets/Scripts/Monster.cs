using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Common
{
    protected Transform targetPosition;
    //public bool isRange;
    public Stat monsterStat = new Stat();
    private MonsterSpawnManager.Type monsterType;
    private bool isTired;
    private bool finalAttack;
    private bool finalAttackReady;
    private bool isFinalAttackActive;
    private Animator anim;
    private Vector3 moveVec;
    List<int> hitCounts = new List<int>();
    private float anitime;
    private Transform pivotTransform;
    private RaycastHit2D[] hits;
    private Transform attackRange;
    private SpriteRenderer spriteRenderer;
    private Animator effectAnim;
    private AttackType attackType;
    enum AttackType {Idle, Range, Huge, Final}
    
    public void InitializeMonster(Transform targetPosition, GameManager gameManager, MoveManager moveManager)
    {
        pivotTransform = transform.GetChild(0).transform;
        attackRange = transform.GetChild(0).GetChild(0).transform;
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.targetPosition = targetPosition;
        this.gameManager = gameManager;
        this.moveManager = moveManager;
        effectAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    private void Attack()
    {
        isTired = true;
        attackRange.gameObject.SetActive(false);
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Chibok"));
        hits = Physics2D.BoxCastAll(transform.position + attackRange.localPosition, attackRange.localScale, 0,
            Vector3.up, 0f,
            layerMask);
        RayAttack(hits);
        anim.SetTrigger("Attack");
        anim.SetBool("isAttack", true);
        float effectTime = 0f;
        switch (attackType)
        {
            case AttackType.Idle:
                effectTime = 0.333f;
                effectAnim.SetTrigger("IdleEffect");
                break;
            case AttackType.Range:
                effectTime = 0.5f;
                effectAnim.SetTrigger("RangeEffect");
                break;
            case AttackType.Huge:
                effectTime = 0.5f;
                effectAnim.SetTrigger("HugeEffect");
                break;
            // case AttackType.Final:
            //     effectTime = 0.5f;
            //     effectAnim.SetTrigger("FinalEffect");
            //     break;
        }
        Invoke("EndEffect", effectTime);
    }
    void EndEffect()
    {
        effectAnim.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        effectAnim.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        effectAnim.SetTrigger("EndEffect");
    }
    
    //N초마다 활동
    public void Active()
    {
        isTired = false;
        // Debug.Log("몬스터 활동");
        moveManager.PathFinding(transform,
            new Vector2Int((int) targetPosition.transform.position.x, (int) targetPosition.transform.position.y));

        if (monsterType == MonsterSpawnManager.Type.idle)
        {
            anitime =0.6f;
            if (attackRange.gameObject.activeSelf)
            {
                Attack();
                StartCoroutine(ani(anitime));
            }
            IdleAttack();
        }
        else if (monsterType == MonsterSpawnManager.Type.medium)
        {
            anitime = 0.517f;
            if (attackRange.gameObject.activeSelf)
            {
                Attack();
                StartCoroutine(ani(anitime));
            }

            IdleAttack();
            if(!isTired)
                RangeAttack();
        }
        else if (monsterType == MonsterSpawnManager.Type.huge)
        {
            //anitime = 1f;
            if (attackRange.gameObject.activeSelf)
            {
                Attack();
                StartCoroutine(ani(anitime));
            }
            IdleAttack();
            if(!isTired)
                StrongAttack();
        }
        else if (monsterType == MonsterSpawnManager.Type.boss)
        {
            anitime = 0.35f;
            if (attackRange.gameObject.activeSelf)
            {
                Attack();
                //finalAttack 체크
                if (finalAttackReady)
                {
                    finalAttackReady = false;
                    StartCoroutine(FinalAttackAni(5f));
                }
                else
                {
                    StartCoroutine(ani(anitime));
                }
            }
            if (monsterStat.curHp <= 3 && !finalAttack)
            {
                isFinalAttackActive = true;
                FinalAttack();
                finalAttack = true;
            }
            else
            {
                IdleAttack();
                if(!isTired)
                    RangeAttack();
                if(!isTired)
                    StrongAttack();
            }
        }

        if (!isTired && !isFinalAttackActive)
        {
            if (moveManager.FinalNodeList.Count >= 2)
            {
                moveVec = new Vector3(moveManager.FinalNodeList[1].x - transform.position.x,
                    moveManager.FinalNodeList[1].y - transform.position.y);
                CheckDirection();
                Move();
            }
        }
    }
    IEnumerator FinalAttackAni(float time)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        float curTime = 0;
        while(curTime < time)
        {
            curTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;            
        }
        anim.SetBool("isAttack", false);
        attackRange.gameObject.SetActive(false);
        isFinalAttackActive = false;
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
        attackRange.gameObject.SetActive(false);
    }
    
    private void IdleAttack()
    {
        attackType = AttackType.Idle;
        monsterStat.damage = 1;
        CheckAttack(1f, new Vector3(3f, 1f, 1f));
    }

    private void RangeAttack()
    {
        attackType = AttackType.Range;
        monsterStat.damage = 2;
        CheckAttack(2f, new Vector3(1f, 3f, 1f));
    }
    
    private void StrongAttack()
    {
        attackType = AttackType.Huge;
        monsterStat.damage = 3;
        CheckAttack(1.5f, new Vector3(3f, 2f, 1f));
    }
    
    //실제 공격 판정
    private void RayAttack(RaycastHit2D[] hits)
    {
        Animator anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        //anim.SetBool("isAttack", true);
        //anim.SetTrigger("Attack");
        //공격에 따라 시간 다르게 표시
        //StartCoroutine(ani(0.6f, anim));
        foreach (RaycastHit2D hit in hits)
        {
            //Debug.Log(hit.transform.name);
            //애니메이션 추가 해야됨
            Debug.Log($" 데미지 : {monsterStat.damage}");
            hit.transform.gameObject.GetComponent<Common>().ReceiveDamage(monsterStat.damage);
        }
    }
    void FinalAttack()
    {
        attackType = AttackType.Final;
        monsterStat.damage = 5;
        isTired = true;
        anim.SetTrigger("FinalAttackReady");
        anim.SetBool("isAttack", true);
        anim.SetBool("isHorizontal", false);
        anim.SetInteger("isVertical", -1);
        
        finalAttackReady = true;
        attackRange.localPosition = Vector3.zero;
        attackRange = transform.GetChild(0).GetChild(0).transform;
        attackRange.localScale = new Vector3(7f, 7f, 1f);
        attackRange.gameObject.SetActive(true);
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Chibok"));


        hits = Physics2D.BoxCastAll(transform.position, attackRange.localScale, 0, Vector3.up, 0f,
            layerMask);
    }

    private void CheckDirection()
    {
        if ((int)moveVec.y == 1)
        {
            effectAnim.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            //effectAnim.transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z + 90);
            anim.SetInteger("isVertical", 1);
            anim.SetBool("isHorizontal", false);
            spriteRenderer.flipX = false;
            //뒤
        }else if ((int)moveVec.y == -1)
        {
            //effectAnim.transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z + 180);
            anim.SetBool("isHorizontal", false);
            anim.SetInteger("isVertical", -1);
            spriteRenderer.flipX = false;
            //앞
        }else if ((int)moveVec.x == 1)
        {
            effectAnim.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            anim.SetInteger("isVertical", 0);
            anim.SetBool("isHorizontal", true);
            //오른쪽
            spriteRenderer.flipX = true;
        }else if ((int)moveVec.x == -1)
        {
            anim.SetInteger("isVertical", 0);
            anim.SetBool("isHorizontal", true);
            //왼쪽
            spriteRenderer.flipX = false;
        }
        effectAnim.transform.position = attackRange.transform.position;
        effectAnim.transform.rotation = Quaternion.Euler(pivotTransform.position.x, pivotTransform.position.y,
             Quaternion.FromToRotation(Vector3.up, moveVec).eulerAngles.z + 90f);
    }
    private void CheckAttack(float attackRangePos, Vector3 attackRangeScale)
    {
        attackRange.localScale = attackRangeScale;
        hitCounts.Clear();
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Chibok"));

        //1번 모든 방향으로 체크
        //2번 있다면 그 방향으로 공격
        //상
        attackRange.localPosition = Vector3.up * attackRangePos;
        hits = Physics2D.BoxCastAll(transform.position + attackRange.localPosition, attackRange.localScale, 0, Vector3.up, 0f,
            layerMask);
        hitCounts.Add(hits.Length);

        //하
        attackRange.localPosition = Vector3.down * attackRangePos;
        hits = Physics2D.BoxCastAll(transform.position + attackRange.localPosition, attackRange.localScale, 0, Vector3.down, 0f,
            layerMask);
        hitCounts.Add(hits.Length);

        
        attackRange.localScale = new Vector3(attackRange.localScale.y, attackRange.localScale.x);
        //좌
        attackRange.localPosition = Vector3.left * attackRangePos;
        hits = Physics2D.BoxCastAll(transform.position + attackRange.localPosition, attackRange.localScale, 0, Vector3.left, 0f,
            layerMask);
        hitCounts.Add(hits.Length);
        

        //우
        attackRange.localPosition = Vector3.right * attackRangePos;
        hits = Physics2D.BoxCastAll(transform.position + attackRange.localPosition, attackRange.localScale, 0, Vector3.right, 0f,
            layerMask);
        hitCounts.Add(hits.Length);
        attackRange.localScale = new Vector3(attackRange.localScale.y, attackRange.localScale.x);
        
        if (hitCounts.Max() == 0)
        {
            attackRange.gameObject.SetActive(false);
            return;
        }
        
        switch (hitCounts.IndexOf(hitCounts.Max()))
        {
            case 0:
                attackRange.localPosition = Vector3.up * attackRangePos;
                break;
            case 1:
                attackRange.localPosition = Vector3.down * attackRangePos;
                break;
            case 2:
                attackRange.localScale = new Vector3(attackRange.localScale.y, attackRange.localScale.x);
                attackRange.localPosition = Vector3.left * attackRangePos;
                break;
            case 3:
                attackRange.localScale = new Vector3(attackRange.localScale.y, attackRange.localScale.x);
                attackRange.localPosition = Vector3.right * attackRangePos;
                break;
        }
        
        hits = Physics2D.BoxCastAll(transform.position + attackRange.localPosition, attackRange.localScale, 0, Vector3.up, 0f,
            layerMask);
        
        moveVec = attackRange.localPosition / attackRangePos;
        if (hits != null)
        {
            isTired = true;
            attackRange.gameObject.SetActive(true);
            CheckDirection();
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
        
        if ((int)moveVec.y == 1)
        {
            anim.SetTrigger("HitBack");
            //뒤
        }else if ((int)moveVec.y == -1)
        {
            anim.SetTrigger("HitFront");
            //앞
        }else if ((int)moveVec.x != 0)
        {
            anim.SetTrigger("HitSide");
        }
        anim.SetBool("isHit", true);
        StartCoroutine(HitAni(0.1f));
        
        Debug.Log($"현재 몬스터 체력 : {monsterStat.curHp}");
        if (monsterStat.curHp <= 0)
        {
            Debug.Log("몬스터 사망");
            Invoke("DestroyMonster", 0.1f);
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