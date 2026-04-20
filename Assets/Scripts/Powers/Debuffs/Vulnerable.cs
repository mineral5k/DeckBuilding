using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : Power
{
    public override string IconName => "Vulnerable";
    public override string PowerName => "취약";

    public override string Description => "피해를 50% 더 받습니다.";

    public override Power clone()
    {
        return new Vulnerable();
    }

    

    public override void OnApply(Damagable damagable)
    {
        powerOwner = damagable;
        if (amount >0)
        {
            damagable.vulnuerable = true;
            BattleEvents.OnEnemyTurnEnd += OnTurnEnd;
        }
        else
        {
            ReMoveThis();
        }
    }

    public override void OnAdded(Damagable damagable)
    {
        powerOwner.HPBarUIUpdate();
    }

    public void OnTurnEnd()
    {
        amount--;
        powerOwner.HPBarUIUpdate();
        if (amount <= 0)
        {
            ReMoveThis() ;
        }
    }

    public void ReMoveThis()
    {
        powerOwner.vulnuerable = false;
        powerOwner.powers.Remove(this);
        BattleEvents.OnEnemyTurnEnd -= OnTurnEnd;
        powerOwner.HPBarUIUpdate();
    }
}
