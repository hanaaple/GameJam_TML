using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//N초 동안 작동하는 오브젝트들의 상위 추상클래스
public abstract class Common : MonoBehaviour
{
    internal bool isTired;

    public abstract void ReceiveDamage(int Damage);
}