using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : Power
{
    public override string IconName => "Weakness";

    public override string PowerName => "약화";

    public override string Description => "공격의 피해량이 25% 감소한다.";

    public override Power clone()
    {
        return new Weakness();
    }

    public override void OnApply(Damagable damagable)
    {
        powerOwner = damagable;
        if (amount > 0)
        {
            damagable.weakness = true;
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
            ReMoveThis();
        }
    }

    public override void ReMoveThis()
    {
        powerOwner.weakness = false;
        powerOwner.powers.Remove(this);
        BattleEvents.OnEnemyTurnEnd -= OnTurnEnd;
        powerOwner.HPBarUIUpdate();
    }
}
