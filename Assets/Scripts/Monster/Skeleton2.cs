using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Skeleton2 : Monster
{
    [SerializeField] private int patternProceeded;

    private void Awake()
    {
        maxHP = 44;
        currentHP = 44;
        base.Awake();
    }

    public override void NextPattern()
    {
        switch ((battleManager.TurnCount - 1 + patternProceeded) % 3)   // 1턴에 0번 패턴 시작하기 위해 
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
     깨작이 두마리의 패턴

     0: 6뎀 5방어도
     1: 힘2 얻음
     2: 6 * 2데미지

    한마리는 1번패턴부터 시작

     */

    public void Pattern0()
    {
        Attack(target, 6);
        GainShield(5);
    }

    public void IntentionOfPattern0()
    {
        hpBarUI.CreateIntention1().SetIntentionToAttack(6, 1, this);
        hpBarUI.CreateIntention2().SetIntentionToDeffense();
    }

    public void Pattern1()
    {
        Strength strength = new Strength();
        AddPower(strength, 2);
    }

    public void IntentionOfPattern1()
    {
        hpBarUI.CreateIntention1().SetIntentionToBuff();

    }

    public async void Pattern2()
    {
        Attack(target, 6);
        await Task.Delay(500);
        Attack(target, 6);

    }

    public void IntentionOfPattern2()
    {
        hpBarUI.CreateIntention1().SetIntentionToAttack(6, 2, this);

    }

    public override void SetFirstIntention()
    {
        switch (patternProceeded)
        {
            case 0:
                IntentionOfPattern0();
                break;
            case 1:
                IntentionOfPattern1();
                break;
        }
            
    }

}
