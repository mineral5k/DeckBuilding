using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Damagable
{
    private int maxEnergy = 3;
    public int MaxEnergy
    {
        get => maxEnergy;
        set
        {
            maxEnergy = value;
            OnEnergyChanged?.Invoke();
        }
    }
    private int energy = 3;
    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
            OnEnergyChanged?.Invoke();
        }
    }
    public event Action OnEnergyChanged;


    private void Awake()
    {
        maxHP = 80;
        currentHP = 80;
        base.Awake();
    }
    private void Start()
    {
    }

    public override void Die()
    {
        
    }

}
