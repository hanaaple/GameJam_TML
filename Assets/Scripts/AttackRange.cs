using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.parent.GetComponent<Monster>().Attack();
        }
        else
        {
            transform.parent.parent.GetComponent<Monster>().Move();
        }
        //실행 후 범위 비활성화
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}