using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal List<Monster> activeMonsters = new List<Monster>();
    [SerializeField] private float N;

    [SerializeField] private Chibok chibok;
    [SerializeField] private Player player;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;
    [SerializeField] private MoveManager moveManager;
    
    //오류 방지용도
    internal int StageIndex = 1;
    internal int spawnTurn;
    internal bool isBattleMode = true; 
    public bool isBossDestroyed { get; internal set; }

    public void Awake()
    {
        player.InitializePlayer();
        monsterSpawnManager.InitializeMonsterSpawnManager(this, player, moveManager);
        chibok.InitializeChibok(moveManager, this);
    }

    public void Start()
    {
        isBattleMode = true;
        StartCoroutine(StageOneRoutine());
    }

    IEnumerator StageOneRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(N);
        spawnTurn = 1;
        
        // 1스테이지   전투시간동안 반복
        while (isBattleMode)
        {
            Debug.Log($"{spawnTurn}번 째 턴");
            player.isTired = false;
            chibok.Active();
            
            foreach (Monster monster in activeMonsters)
                monster.Active();
            
            monsterSpawnManager.SpawnCheck(spawnTurn);
            
            spawnTurn++;
            yield return waitForSeconds;
            //N초 동안 플레이어 자유 행동 가능
        }
    }


    IEnumerator StageTwoRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(N);
        spawnTurn = 1;
        
        // 2스테이지   전투시간동안 반복
        while (isBattleMode)
        {
            Debug.Log("N초마다 말합니다.");
            player.isTired = false;
            chibok.Active();
            
            foreach (Monster monster in activeMonsters)
                monster.Active();
            
            if (!isBossDestroyed)
                monsterSpawnManager.SpawnCheck(spawnTurn);
            
            spawnTurn++;
            yield return waitForSeconds;
            //N초 동안 플레이어 자유 행동 가능
        }
    }
}