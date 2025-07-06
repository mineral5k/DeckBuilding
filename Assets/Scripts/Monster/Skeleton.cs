using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{

    private void Awake()
    {
        maxHP = 20;
        currentHP = 20;
        base.Awake();
    }

    public void NextPattern()
    {
        switch (battleManager.TurnCount % 2)
        {
            case 1:
                Attack(GameManager.Instance.Player.Status, 8);
                break;
        }
    }

    
    
}
