using System.Collections.Generic;
using UnityEngine;
 
public class MonsterSpawnManager: MonoBehaviour
{
    //몬스터 프리팹은 여기에!
    
    [SerializeField] private List<GameObject> SpawnPosition = new List<GameObject>();
    
    List<ObjectPool> monsterPool = new List<ObjectPool>();
    private GameManager gameManager;
    
    //0~3번까지 순서대로 잡몹, 중간몹, 큰몹, 보스몹
    private enum Type { idle, medium, huge, boss }
    
    void Awake()
    {
        ObjectPool mobPool = gameObject.AddComponent<ObjectPool>();
        ObjectPool mediumPool = gameObject.AddComponent<ObjectPool>();
        ObjectPool hugePool = gameObject.AddComponent<ObjectPool>();
        ObjectPool bossPool = gameObject.AddComponent<ObjectPool>();
        
        //mobPool.InitializePool(잡몹 프리팹, 10);
        //mediumPool.InitializePool(중간몹 프리팹, 10);
        //hugePool.InitializePool(거대몹 프리팹, 5);
        //bossPool.InitializePool(보스 프리팹, 1);
        
        monsterPool.Add(mobPool);
        monsterPool.Add(mediumPool);
        monsterPool.Add(hugePool);
        monsterPool.Add(bossPool);
    }

    public void InitializeMonsterSpawnManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    // spawnIndex에 monsterIndex번 몬스터를 소환
    void ActiveMonster(int spawnIndex, Type monsterType)
    {
        GameObject monster = monsterPool[(int) monsterType].GetObject();
        //몬스터 초기화 및 동작
        monster.GetComponent<Monster>().ActiveMonster();
    }

    void SpawnCheck(int turn)
    {
        //3턴마다 소환
        if (turn % 3 == 0)
        {
            if (turn % 6 == 0)
            {
                //SpawnPosition[1] 중간몹 소환
                ActiveMonster(1, Type.medium);
            }
            else
            {
                //SpawnPosition[0], [1] 잡몹 소환
                ActiveMonster(0, Type.idle);
                ActiveMonster(1, Type.idle);
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