using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Damagable
{
    private int maxEnergy;
    private int energy;

    private void Awake()
    {
        maxEnergy = 3;
    }


}
