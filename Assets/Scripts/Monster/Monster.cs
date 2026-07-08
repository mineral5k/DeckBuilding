using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Damagable
{
    public List<Sprite> monsterIntentionImages;
    
    public virtual void NextPattern()       // 매 턴 각 몬스터들이 실행하며, 몬스터마다 클래스에 행동 패턴 구현 예정
    {

    }

    public override void Die()
    {
        hpBarUI.gameObject.SetActive(false);
        if (this is Monster)
        {
            battleManager.MonsterDie(this);
        }
    }



}
