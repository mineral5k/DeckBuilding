using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffenseNextTurn : Power
{
    public override string IconName => "DeffenseNextTurn";

    public override string PowerName => "방어준비";

    public override string Description => "다음 턴 시작시 " + amount + "의 방어도를 얻는다.";

    public override Power clone()
    {
        return new DeffenseNextTurn();
    }

    public override void OnAdded(Damagable damagable)
    {

    }

    public override void OnApply(Damagable damagable)
    {
        powerOwner = damagable;
        BattleEvents.OnTurnStart += Excute;
    }

    public void Excute()
    {
        powerOwner.GainShield(amount);
        powerOwner.powers.Remove(this);
        powerOwner.HPBarUIUpdate();
        BattleEvents.OnTurnStart -= Excute;
    }
}
