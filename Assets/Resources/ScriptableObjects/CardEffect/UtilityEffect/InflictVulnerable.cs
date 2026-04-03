using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InflictVulnerable", menuName = "Data/Effect/InflictVulnerable")]
public class InflictPower : UtilityEffect
{
    [SerializeField] private int amount;
    public override void Excute(Damagable target, int value)
    {
        Power power = new Vulnerable();
        target.AddPower(power, amount);
    }

   
}
