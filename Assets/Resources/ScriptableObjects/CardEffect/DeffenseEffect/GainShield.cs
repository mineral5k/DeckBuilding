using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainShield", menuName = "Data/Effect/GainShield")]
public class GainShield : DeffenseEffect
{
    public override void Excute(Damagable? target, int value)
    {
        GameManager.Instance.Player.Status.GainShield(value);
    }
}
