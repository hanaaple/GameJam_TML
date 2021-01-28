using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal List<Common> activeCommons = new List<Common>();
    [SerializeField] private float N;

    [SerializeField] private Chibok chibok;
    [SerializeField] private Player player;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;
    [SerializeField] private MoveManager moveManager;
    
    private float behaviorTime;
    private int StageIndex = 1;
    private int spawnTurn;
    private bool isBattleMode = true;


    public void Awake()
    {
        monsterSpawnManager.InitializeMonsterSpawnManager(this);
        chibok.InitializeChibok(moveManager);
    }

    public void Start()
    {
        
        StartCoroutine(StageOneRoutine());
    }

    IEnumerator StageOneRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(N);
        spawnTurn = 1;
        
        // 1스테이지   전투시간동안 반복
        while (isBattleMode && StageIndex == 1)
        {
            //N초마다
            player.isTired = false;
            foreach (Common common in activeCommons)
            {
                common.isTired = false;
                common.Active();
            }
            //chibok.이동
            //
            
            
            //턴 진행중
            
            
            
            spawnTurn++;
            yield return waitForSeconds;
        }
    }


    IEnumerator StageTwoRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(N);
        spawnTurn = 1;
        
        // 2스테이지   전투시간동안 반복
        while (isBattleMode && StageIndex == 2)
        {
            foreach (Common common in activeCommons)
                common.isTired = false;
            
            //턴 진행중

            spawnTurn++;
            yield return waitForSeconds;
        }
    }
}