using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntentionIcon : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private Sprite Attack;
    [SerializeField] private Sprite Buff;
    [SerializeField] private Sprite Debuff;
    [SerializeField] private Sprite Deffense;
    [SerializeField] private Sprite GiveCard;
    [SerializeField] private TextMeshProUGUI damageIntention;
    private Damagable monster;
    private int damage;
    private int count;

    public void SetIntentionToAttack(int InsertedDamage, int InsertedCount, Damagable damagable)
    {
        Init(damagable);
        damage = InsertedDamage;
        count = InsertedCount;

        Icon.sprite = Attack;
        damageIntention.text = "sample"; // ui 업데이트를 위해 아무 텍스트 삽입
        UIUpdate();
    }

    public void Init(Damagable damagable)
    {
        monster = damagable;
        damageIntention.text = "";
    }

    public void SetIntentionToBuff()
    {
        Icon.sprite = Buff;
        damageIntention.text = "";

    }

    public void SetIntentionToDebuff()
    {
        Icon.sprite = Debuff;
        damageIntention.text = "";

    }

    public void SetIntentionToDeffense()
    {
        Icon.sprite = Deffense;
        damageIntention.text = "";

    }

    public void SetIntentionToGiveCard()
    {
        Icon.sprite= GiveCard;
        damageIntention.text = "";

    }

    public void UIUpdate()
    {
        if (damageIntention.text == "") return;
        damageIntention.text = monster.CalcDamage(monster.target, damage).ToString() + "x" + count.ToString();
        if (count == 1)
        {
            damageIntention.text = monster.CalcDamage(monster.target, damage).ToString();
        }

    }

}
