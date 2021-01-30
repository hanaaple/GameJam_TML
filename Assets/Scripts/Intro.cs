using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public IEnumerator sceneController;

    void Start()
    {
        StartCoroutine(WaitForIt());
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(4.0f);
        sceneController.MoveNext();
        Destroy(this.gameObject);
    }
}
