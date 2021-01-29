using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터 공격범위 - 방식 변경할 예정
public class AttackRange : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Chibok"))
        {
            transform.parent.parent.GetComponent<Monster>().Attack(other.gameObject.GetComponent<Common>());
        }
        else
        {
            transform.parent.parent.GetComponent<Monster>().Move(new Vector2Int((int)other.transform.position.x, (int)other.transform.position.y));
        }
        //실행 후 범위 비활성화
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}