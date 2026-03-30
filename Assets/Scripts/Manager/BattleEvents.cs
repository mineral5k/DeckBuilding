using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleEvents                             
{
    public static Action OnTurnStart;    
    public static Action OnTurnEnd;        
    public static Action OnCardDrawed;
    public static Action OnCardPlayed;
    public static void Reset()
    {
        OnTurnStart = null;
        OnTurnEnd = null;
        OnCardDrawed = null;
        OnCardPlayed = null;
    }
}


