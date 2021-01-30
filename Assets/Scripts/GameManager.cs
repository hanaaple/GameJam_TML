using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public IEnumerator sceneController;
    public bool isSpawn;
    public Text text;
    internal List<Monster> activeMonsters = new List<Monster>();
    public float N;

    [SerializeField] private Chibok chibok;
    [SerializeField] private Player player;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;

    //오류 방지용도
    private float behaviorTime;
    internal int spawnTurn;
    internal bool isBattleMode = false;
    internal bool isWin = false;
    public bool isBossDestroyed { get; internal set; }

    public void startGame(int stageIndex)
    {
        isBattleMode = true;
        if (stageIndex == 1)
        {
            player.InitializePlayer(new Vector3(-8, -4, 0));
            chibok.InitializeChibok(new Vector3(-9, -4, 0), targetPosition);
            monsterSpawnManager.InitializeMonsterSpawnManager();
            StartCoroutine(StageOneRoutine());
        }
        else if (stageIndex == 2)
        {
            player.InitializePlayer(new Vector3(-8, -4, 0));
            chibok.InitializeChibok(new Vector3(-9, -4, 0), targetPosition);
            monsterSpawnManager.InitializeMonsterSpawnManager();
            StartCoroutine(StageTwoRoutine());
        }
    }

    private void Update()
    {
        behaviorTime -= Time.deltaTime;
        if (behaviorTime <= 0) behaviorTime = 0;
        text.text = behaviorTime.ToString();
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

            Invoke("asd", 0.3f);

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

    void asd()
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
            // Debug.Log("N초마다 말합니다.");
            player.isTired = false;
            chibok.Active();

            Invoke("asd", 0.3f);

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