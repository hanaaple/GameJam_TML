using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonster : Monster
{
    public override void Attack()
    {
        
    }

    public override void Move()
    { 
        
    }

    // IEnumerator ShowAttackRange()
    // {
    //     WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    //     while (!isTired)
    //     {
    //         //피벗이 현재 캐릭터 방향으로 되며 방향키를 누를 시 방향 변경
    //         float h = Input.GetAxisRaw("Horizontal");
    //         float v = Input.GetAxisRaw("Vertical");
    //         bool hDown = Input.GetButtonDown("Horizontal");
    //         bool vDown = Input.GetButtonDown("Vertical");
    //         if (hDown || vDown)
    //         {
    //             //Debug.Log("누른상태로 움직였다");
    //             if (hDown)
    //                 isHorizontalMove = true;
    //             else if (vDown)
    //                 isHorizontalMove = false;
    //             Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
    //             //회전
    //             Transform pivotTransform = transform.GetChild(0).transform;
    //             pivotTransform.rotation = Quaternion.Euler(pivotTransform.position.x, pivotTransform.position.y,
    //                 Quaternion.FromToRotation(Vector3.up, moveVec).eulerAngles.z);
    //         }
    //         if (!Input.GetKey(keyCode)) yield break;
    //         yield return waitForFixedUpdate;
    //     }
    // }
    //
    //
    // void Attack()
    // {
    //     Transform attackRange = transform.GetChild(0).GetChild(0).transform;
    //     attackRange.position = Vector3.up;
    //     attackRange.localScale = new Vector3(3f, 1f, 1f);
    //     StartCoroutine(ShowAttackRange());
    //     //공격
    //     isTired = true;
    // }
}