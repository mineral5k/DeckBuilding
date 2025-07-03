using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Damagable
{
    private int maxEnergy = 3;
    private int energy;

    private void Start()
    {
        CardEffectHandler.HandlerInstance.playerStatus = this;
    }

}
