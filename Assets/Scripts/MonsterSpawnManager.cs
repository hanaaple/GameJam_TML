using System.Collections.Generic;
using UnityEngine;
 
public class MonsterSpawnManager: MonoBehaviour
{
    [SerializeField] private GameObject[] MonsterPrefabs;

    [SerializeField] private Transform[] SpawnPosition;
    
    private List<ObjectPool> monsterPool = new List<ObjectPool>();
    private GameManager gameManager;
    private Transform targetPosition;
    private MoveManager moveManager;
    
    //0~3번까지 순서대로 잡몹, 중간몹, 큰몹, 보스몹
    public enum Type { idle, medium, huge, boss }
    
    void Awake()
    {
        ObjectPool mobPool = gameObject.AddComponent<ObjectPool>();
        ObjectPool mediumPool = gameObject.AddComponent<ObjectPool>();
        ObjectPool hugePool = gameObject.AddComponent<ObjectPool>();
        ObjectPool bossPool = gameObject.AddComponent<ObjectPool>();
        
        mobPool.InitializePool(MonsterPrefabs[0], 10);
        mediumPool.InitializePool(MonsterPrefabs[1], 10);
        hugePool.InitializePool(MonsterPrefabs[2], 5);
        bossPool.InitializePool(MonsterPrefabs[3], 1);
        
        monsterPool.Add(mobPool);
        monsterPool.Add(mediumPool);
        monsterPool.Add(hugePool);
        monsterPool.Add(bossPool);
    }

    public void InitializeMonsterSpawnManager(GameManager gameManager, Transform targetPosition, MoveManager moveManager)
    {
        this.gameManager = gameManager;
        this.targetPosition = targetPosition;
        this.moveManager = moveManager;
    }

    // spawnIndex에 monsterIndex번 몬스터를 소환
    void ActiveMonster(int spawnIndex, Type monsterType)
    {
        GameObject monster = monsterPool[(int) monsterType].GetObject();
        //몬스터 초기화 및 동작
        monster.transform.position = SpawnPosition[spawnIndex].position;
        monster.GetComponent<Monster>().InitializeMonster(targetPosition, gameManager, moveManager);
        monster.GetComponent<Monster>().ActiveMonster(monsterType);
    }

    public void SpawnCheck(int turn)
    {
        if (turn == 2)
        {
            ActiveMonster(0, Type.idle);
            ActiveMonster(1, Type.medium);
            //ActiveMonster(2, Type.huge);
            ActiveMonster(3, Type.boss);
        }

        if (turn == 1)
        {
            //SpawnPosition[0], [1], [2] 잡몹 소환
            ActiveMonster(0, Type.idle);
            ActiveMonster(1, Type.idle);
            ActiveMonster(2, Type.idle);
        }

        //3턴마다 소환
        if (turn % 3 == 0)
        {
            //SpawnPosition[0] 잡몹 소환
            ActiveMonster(0, Type.idle);
            if (turn % 6 == 0)
            {
                //SpawnPosition[1] 중간몹 소환
                ActiveMonster(1, Type.medium);
            }
            else
            {
                //SpawnPosition[1] 잡몹 소환
                //ActiveMonster(1, Type.idle);
            }
        }

        if (turn % 10 == 0)
        {
            //SpawnPosition[2] 큰몹 소환
            ActiveMonster(2, Type.huge);
        }
    }

    void SpawnBoss()
    {
        //SpawnPosition[3] 보스 소환
        ActiveMonster(3, Type.boss);
    }
}