using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    public abstract string IconName { get; }         // 아이콘 불러오기 위함. 각 파워의 영어 이름 그대로 쓸 것
    public abstract string PowerName { get; }        // 아이콘에 마우스를 올리면 나올 이름. 한글로
    public abstract string Description { get; }      // 아이콘에 마우스를 올리면 나올 설명. 한글로
    protected Damagable powerOwner;
    public int amount = 0;
    //public bool isBuff; // true = 버프, false = 디버프

    public abstract Power clone();

    public abstract void OnApply(Damagable damagable);
    public abstract void OnAdded(Damagable damagable);
    
    
}
