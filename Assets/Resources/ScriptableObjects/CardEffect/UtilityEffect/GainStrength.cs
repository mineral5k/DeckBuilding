using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainStrength", menuName = "Data/Effect/GainStrength")]

public class GainStrength : UtilityEffect
{
    [SerializeField] private int amount;
    public override void Excute(Damagable target, int value)
    {
        Power power = new Strength();
        GameManager.Instance.Player.Status.AddPower(power,amount);
    }
}
