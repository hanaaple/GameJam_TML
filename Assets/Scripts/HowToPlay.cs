using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public IEnumerator sceneController;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            sceneController.MoveNext();
            Destroy(this.gameObject);
        }
    }
}
