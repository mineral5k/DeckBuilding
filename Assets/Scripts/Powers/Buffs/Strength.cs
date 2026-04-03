using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : Power
{
    public override string PowerName => "힘";

    public override string Description => $"공격시 피해를를 {amount}만큼 증가시킵니다.";
    
    
    public bool isBuff = true; // true = 버프, false = 디버프

    public override Power clone()
    {
        return new Strength();
    }
    public override void OnApply(Damagable damagable)
    {
        powerOwner = damagable;
        powerOwner.attackUp = amount;
        if (amount == 0)                              //힘이 0이면 버프창에서 제거  ... 힘은 음수일때도 영향이 있기에 0 일때만 제거
        {
            powerOwner.powers.Remove(this);
        }
    }

    public override void OnAdded(Damagable damagable)
    {
        OnApply(damagable);
    }
}
