using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainPower", menuName = "Data/Effect/GainPower")]

public class GainPower : UtilityEffect
{
    [SerializeField] private int amount;
    [SerializeField] private string powerName;
    public override void Excute(Damagable target, int value)
    {
        Power power = KeywardText.powerDict[powerName]();

        GameManager.Instance.Player.Status.AddPower(power, amount);
    }
}
