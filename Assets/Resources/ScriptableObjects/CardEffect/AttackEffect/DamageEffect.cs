using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Data/Effect/AttackData")]
public class DamageEffect : AttackEffect
{
    public override void Excute(Damagable target, int value)
    {
        target.TakeDamage(value);
    }
}
