using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InflictPower", menuName = "Data/Effect/InflictPower")]
public class InflictPower : UtilityEffect
{
    [SerializeField] private int amount;
    [SerializeField] private string powerName;
    public override void Excute(Damagable target, int value)
    {
        Power power = KeywardText.powerDict[powerName]();
        target.AddPower(power, amount);
    }

   
}
