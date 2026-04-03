using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    public abstract string PowerName { get; }
    public abstract string Description { get; }
    protected Damagable powerOwner;
    public int amount = 0;
    //public bool isBuff; // true = 버프, false = 디버프

    public abstract Power clone();

    public abstract void OnApply(Damagable damagable);
    public abstract void OnAdded(Damagable damagable);
    
    
}
