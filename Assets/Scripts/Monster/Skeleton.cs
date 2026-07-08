using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{

    private void Awake()
    {
        maxHP = 48;
        currentHP = 48;
        base.Awake();
    }

    public override void NextPattern()
    {
        switch ( (battleManager.TurnCount-1) % 3 )   // 1턴에 0번 패턴 시작하기 위해 
        {
            case 0:
                Pattern0();
                IntentionOfPattern1();
                break;
            case 1:
                Pattern1();
                IntentionOfPattern2();
                break;
            case 2:
                Pattern2();
                IntentionOfPattern0();
                break;
        }
    }
    /*
     스켈레톤의 패턴

     0: 12 x 1 공격
     1:  6 x 1 공격 , 5방어도 얻음
     2:  힘 2 얻음. 플레이어에게 취약 1 부여

     */

    public void Pattern0()
    {
        Attack(target, 12);
    }

    public void IntentionOfPattern0()
    {
        hpBarUI.CreateIntention1().SetIntentionToAttack(12, 1, this);
    }

    public void Pattern1()
    {
        Attack(target, 6);
        GainShield(5);

    }

    public void IntentionOfPattern1()
    {
        hpBarUI.CreateIntention1().SetIntentionToAttack(6,1,this);
        hpBarUI.CreateIntention2().SetIntentionToDeffense();
    }

    public void Pattern2()
    {
        Strength strength = new Strength();
        Vulnerable vulnerable = new Vulnerable();
        AddPower(strength, 2);
        target.AddPower(vulnerable, 2);

    }

    public void IntentionOfPattern2()
    {
        hpBarUI.CreateIntention1().SetIntentionToBuff();
        hpBarUI.CreateIntention2().SetIntentionToDebuff();
    }

    public override void SetFirstIntention()
    {
        IntentionOfPattern0();
    }

}
