using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject prefab;
    private GameObject folder;
    private List<GameObject> createObjectList = new List<GameObject>();
    private int objectIndex;
    
    
    public void InitializePool(GameObject prefab, int poolSize)
    {
        this.prefab = prefab;
        folder = new GameObject(prefab.name + " Folder");
        CreateObject(poolSize);
    }
    
    // 비어있는 오브젝트 하나를 반환
    public GameObject GetObject()
    {
        GameObject spawnObject = null;

        if (createObjectList[objectIndex].activeSelf == false)
        {
            spawnObject = createObjectList[objectIndex];
        }
        else
        {
            objectIndex = createObjectList.Count;
            CreateObject(5);
            spawnObject = createObjectList[objectIndex];
        }
        objectIndex = (++objectIndex) % createObjectList.Count;
        
        return spawnObject;
    }

    //num만큼 오브젝트 생성
    private void CreateObject(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject createObject = Instantiate(prefab, folder.transform);
            createObject.SetActive(false);
            createObjectList.Add(createObject);
        }
    }
}