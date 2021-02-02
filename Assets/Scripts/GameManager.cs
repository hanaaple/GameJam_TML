using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public IEnumerator sceneController;
    public bool isSpawn; // debug용 bool값
    public Text text;
    internal List<Monster> activeMonsters = new List<Monster>();
    public float N;

    [SerializeField] private Chibok chibok;
    [SerializeField] private Player player;
    [SerializeField] private Transform[] targetPosition;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;

    //오류 방지용도
    private float behaviorTime;
    internal int spawnTurn;
    //bool 자료형은 초기화를 안해줄 경우 자동으로 false가 됩니다.
    internal bool isBattleMode;
    internal bool isWin;
    public bool isBossDestroyed { get; internal set; }

    public void startGame(int stageIndex)
    {
        isBattleMode = true;
        if (stageIndex == 1)
        {
            StartCoroutine(Timer());
            player.InitializePlayer(new Vector3(-8, -4, 0));
            chibok.InitializeChibok(new Vector3(-9, -4, 0), targetPosition[0]);
            monsterSpawnManager.InitializeMonsterSpawnManager();
            StartCoroutine(StageOneRoutine());
        }
        else if (stageIndex == 2)
        {
            StartCoroutine(Timer());
            player.InitializePlayer(new Vector3(-8, -4, 0));
            chibok.InitializeChibok(new Vector3(-9, -4, 0), targetPosition[1]);
            monsterSpawnManager.InitializeMonsterSpawnManager();
            StartCoroutine(StageTwoRoutine());
        }
    }

    IEnumerator Timer()
    {
        while(isBattleMode)
        {
            behaviorTime -= Time.deltaTime;
            if (behaviorTime <= 0) behaviorTime = 0;
            text.text = string.Format("{0:0.#}", behaviorTime);
            yield return null;
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

            //공격과 이동이 함께 이루어지는 경우 이동 중에 공격받는 오류때문 
            Invoke("ActiveMonster", 0.3f);

            if (isSpawn)
            {
                monsterSpawnManager.SpawnCheck(spawnTurn);
                //monsterSpawnManager.SpawnCheck(2);
            }

            isSpawn = false; // spawn one time

            spawnTurn++;
            yield return waitForSeconds;
            //N초 동안 플레이어 자유 행동 가능
        }
        if (isWin)
        {
            sceneController.MoveNext();
        }
    }

    void ActiveMonster()
    {
        foreach (Monster monster in activeMonsters)
            monster.Active();
    }

    IEnumerator StageTwoRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(N);
        spawnTurn = 1;

        // 2스테이지   전투시간동안 반복
        while (isBattleMode)
        {
            behaviorTime = N;
            // Debug.Log("N초마다 말합니다.");
            player.isTired = false;
            chibok.Active();
            
            //공격과 이동이 함께 이루어지는 경우 이동 중에 공격받는 오류때문
            Invoke("ActiveMonster", 0.3f);

            if (!isBossDestroyed)
                monsterSpawnManager.SpawnCheck(spawnTurn);

            spawnTurn++;
            yield return waitForSeconds;
            //N초 동안 플레이어 자유 행동 가능
        }
        if (isWin)
        {
            sceneController.MoveNext();
        }
    }
}