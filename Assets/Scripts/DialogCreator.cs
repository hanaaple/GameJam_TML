using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCreator : MonoBehaviour
{

    [SerializeField] private GameObject DialogMangerPrefab;

    [SerializeField] private GameObject image;
    [SerializeField] private GameObject dialogBox; // for active setting
    [SerializeField] private GameObject endTalkCursor;

    private void Awake()
    {
        DialogManger dm = Instantiate(DialogMangerPrefab).GetComponent<DialogManger>();
        dm.imageGO = image;
        dm.dialogBox = dialogBox;
        dm.endTalkCursor = endTalkCursor;
        dm.dialogContent = new ArriveStoreDialog();
    }

    public void Create(string dialogName)
    {
        DialogManger dm = Instantiate(DialogMangerPrefab).GetComponent<DialogManger>();
        dm.imageGO = image;
        dm.dialogBox = dialogBox;
        dm.endTalkCursor = endTalkCursor;
        switch (dialogName)
        {
            // 치복이 집
            case "주인공이 치복이 집에 도착한 장면":
                dm.dialogContent = new ArriveHomeDialog();
                break;
            case "콜라사러 가자고 설득하는 장면":
                dm.dialogContent = new LetBuyCokeDialog();
                break;

            // 편의점    
            case "편의점에 막 도착한 치복이와 주인공":
                dm.dialogContent = new ArriveStoreDialog();
                break;
            case "코카콜라 고르는 장면":
                dm.dialogContent = new ChoosingCokeDialog();
                break;
            case "계산대":
                dm.dialogContent = new CounterDialog();
                break;

            // 예외
            default:
                Debug.LogWarning("미연시 씬 이름이 잘 못 됐어요!");
                break;
        }
    }
}
