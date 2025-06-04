using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    public string powerName;
    public string description;
    public int amount;
    public bool isBuff; // true = 버프, false = 디버프

    public abstract void OnApply(Damagable damagable);
    public abstract void OnTurnStart(Damagable damagable);
    public abstract void OnTurnEnd(Damagable damagable);
    public abstract void OnRemove(Damagable damagable);
}
