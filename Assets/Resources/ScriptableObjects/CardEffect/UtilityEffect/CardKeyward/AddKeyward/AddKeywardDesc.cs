using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeywardName", menuName = "Data/Effect/AddKeywardDesc")]
public class AddKeywardDesc : CardKeywardEffect
{
    [SerializeField] private string keywardTitle; 
    public override void Excute(Damagable target, int value)
    {
        if (KeywardText.keywardDict.ContainsKey(keywardTitle))
        {
            cardOfEffect.KeywardDescs.Add(KeywardText.keywardDict[keywardTitle]);
        }

        if (System.Enum.TryParse(keywardTitle, out CardKeyward keywardEnum))
        {
            cardOfEffect.keywards.Add(keywardEnum);
        }
        else
        {
        }

    }

    public override void SetCard(Card card)
    {
        cardOfEffect = card;
    }

   
}
