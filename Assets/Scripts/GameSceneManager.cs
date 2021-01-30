using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject introPrefab;
    [SerializeField] private GameObject howToPlayPrefab;
    [SerializeField] private DialogCreator dialogCreator;
    [SerializeField] private GameManager gameManager;


    private IEnumerator sceneController;
    private void Awake()
    {
        sceneController = gotoNextScene();
        dialogCreator.sceneController = sceneController;
        gameManager.sceneController = sceneController;

        sceneController.MoveNext();
    }

    IEnumerator gotoNextScene()
    {
        // intro page
        GameObject intro = Instantiate(introPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        intro.transform.localPosition = Vector3.zero;
        intro.GetComponent<Intro>().sceneController = sceneController;
        yield return null;

        // story arrive home
        dialogCreator.Create("주인공이 치복이 집에 도착한 장면");
        yield return null;

        // story let by coke
        dialogCreator.Create("콜라사러 가자고 설득하는 장면");
        yield return null;

        // tutorial page
        GameObject howToPlay = Instantiate(howToPlayPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        howToPlay.transform.localPosition = Vector3.zero;
        howToPlay.GetComponent<HowToPlay>().sceneController = sceneController;
        yield return null;

        // stage 1
        gameManager.startGame(1);
        yield return null;

        // story arrive convenience store 
        dialogCreator.Create("편의점에 막 도착한 치복이와 주인공");
        yield return null;

        // stage 2
        gameManager.startGame(2);
        yield return null;

        // story choosing coke
        dialogCreator.Create("코카콜라 고르는 장면");
        yield return null;

        // stage 3
        // gameManager.startGame(3);
        // yield return null;

        // story counter
        dialogCreator.Create("계산대");
    }

}
