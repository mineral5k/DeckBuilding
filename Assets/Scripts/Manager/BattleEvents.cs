using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleEvents                             
{
    public static Action OnTurnStart;    
    public static Action OnPlayerTurnEnd;       
    public static Action OnEnemyTurnStart;
    public static Action OnEnemyTurnEnd;
    public static Action OnCardDrawed;
    public static Action OnCardPlayed;
    public static void Reset()
    {
        OnTurnStart = null;
        OnPlayerTurnEnd = null;
        OnEnemyTurnStart = null;
        OnEnemyTurnEnd = null;
        OnCardDrawed = null;
        OnCardPlayed = null;
    }
}


