using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour // rename to StageManager
{
    public IEnumerator sceneController;

    public bool isSpawn;
    public Text text;
    internal List<Monster> activeMonsters = new List<Monster>();
    [SerializeField] private float N;

    [SerializeField] private Chibok chibok;
    [SerializeField] private Player player;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;
    [SerializeField] private MoveManager moveManager;

    //오류 방지용도
    internal int StageIndex = 1;
    private float behaviorTime;
    internal int spawnTurn;
    internal bool isBattleMode = true;
    public bool isBossDestroyed { get; internal set; }

    // public void Awake()
    // {
    //     player.InitializePlayer();
    //     monsterSpawnManager.InitializeMonsterSpawnManager(this, player, moveManager); // 분할하기에 문제가 있음..
    //     chibok.InitializeChibok(moveManager, this);
    // }

    // public void Start()
    // {
    //     isBattleMode = true;
    //     StartCoroutine(StageOneRoutine());
    // }

    public void startGame(int StageIndex)
    {
        player.InitializePlayer();
        monsterSpawnManager.InitializeMonsterSpawnManager(this, player, moveManager); // 분할하기에 문제가 있음..
        chibok.InitializeChibok(moveManager, this);

        isBattleMode = true;
        if (StageIndex == 1)
        {
            StartCoroutine(StageOneRoutine());
        }
        else if (StageIndex == 2)
        {
            StartCoroutine(StageTwoRoutine());
        }
    }

    // state: 시작 미연시 -> 튜토리얼 화면 -> stage1 -> 미연시1 -> stage2 -> 미연시2 -> stage3 -> 미연시3
    private void Update()
    {
        if (isBattleMode)
        {
            behaviorTime -= Time.deltaTime;
            if (behaviorTime <= 0) behaviorTime = 0;
            text.text = behaviorTime.ToString();
        }
    }

    IEnumerator StageOneRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(N);
        spawnTurn = 1;

        // 1스테이지   전투시간동안 반복
        while (isBattleMode)
        {
            behaviorTime = N;
            //Debug.Log($"{spawnTurn}번 째 턴");
            player.isTired = false;
            chibok.Active();

            foreach (Monster monster in activeMonsters)
                monster.Active();

            if (isSpawn)
                monsterSpawnManager.SpawnCheck(2);
            isSpawn = false;

            spawnTurn++;
            yield return waitForSeconds;
            //N초 동안 플레이어 자유 행동 가능
        }
        // 게임 종료
        // foreach (Monster monster in gameManager.activeMonsters) // gameManager 쪽에 있어야함
        // {
        //     monster.DestroyMonster();
        // }

        sceneController.MoveNext();
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
        // 게임 종료
        // foreach (Monster monster in gameManager.activeMonsters) // gameManager 쪽에 있어야함
        // {
        //     monster.DestroyMonster();
        // }

        sceneController.MoveNext();
    }
}