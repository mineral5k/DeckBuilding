using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawCard", menuName = "Data/Effect/DrawCard")]
public class DrawCard : UtilityEffect
{
    [SerializeField] private int amount;
    public override void Excute(Damagable target, int value)
    {
        DeckManager.Instance.DrawCard(amount);
    }
}
