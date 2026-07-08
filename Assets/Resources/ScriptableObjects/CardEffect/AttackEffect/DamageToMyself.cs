using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageToMyself", menuName = "Data/Effect/DamageToMyself")]
public class DamageToMyself : AttackEffect
{
    public override void Excute(Damagable target, int value)
    {
        GameManager.Instance.Player.Status.TakeHPDamage(value);
    }

}
