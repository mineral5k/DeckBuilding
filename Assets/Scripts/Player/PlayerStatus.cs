using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Damagable
{
    public int maxEnergy = 3;
    public int energy;


    private void Awake()
    {
        maxHP = 80;
        currentHP = 80;
        base.Awake();
    }
    private void Start()
    {
    }

}
