using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{

    private void Awake()
    {
        maxHP = 20;
        currentHP = 20;
        base.Awake();
    }

    
}
