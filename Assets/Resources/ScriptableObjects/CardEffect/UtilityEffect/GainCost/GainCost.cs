using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainCost", menuName = "Data/Effect/GainCost")]
public class GainCost : UtilityEffect
{
    [SerializeField] private int amount;
    public override void Excute(Damagable target, int value)
    {
        GameManager.Instance.Player.Status.Energy += amount;
    }
}
