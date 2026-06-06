using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Power
{
    public override string IconName => "Burn";

    public override string PowerName => "화상";

    public override string Description => "적이 턴 시작 시 " + amount + "의 피해를 입고 수치가 1 감소한다.";

    public override Power clone()
    {
        return new Burn();
    }

    public override void OnApply(Damagable damagable)
    {
        powerOwner = damagable;
        if (amount > 0)
        {
            BattleEvents.OnEnemyTurnStart += OnEnemyTurnStart;
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

    public void OnEnemyTurnStart()
    {
        powerOwner.TakeHPDamage(amount);
        amount--;
        powerOwner.HPBarUIUpdate();
        if (amount <= 0)
        {
            ReMoveThis();
        }
    }

    public void ReMoveThis()
    {
        powerOwner.powers.Remove(this);
        BattleEvents.OnEnemyTurnStart -= OnEnemyTurnStart;
        powerOwner.HPBarUIUpdate();
    }


}
