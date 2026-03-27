using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public event Action OnHPChanged;
    private HPBarUI hpBarUI;
    protected BattleManager battleManager;
    public Damagable target = null;


    protected int maxHP ;
    public int MaxHP
    {
        get { return maxHP; }
    }
    protected int currentHP ;
    public int CurrentHP
    {
        get { return currentHP; }
    }
    protected int shield;
    public int Shield
    {
        get { return shield; }
    }
    protected float percentHP => (float)currentHP / (float)maxHP;
    public float PercentHP
    {
        get { return percentHP; }
    }
    protected bool isDead => currentHP <= 0;
    protected List<Power> powers = new List<Power>();

    protected int attackUp;          // 공격 피해 증가,감소
    protected int deffenseUp;        // 얻는 방어도 증가,감소
    protected bool vulnuerable;      // 취약, 받는 데미지 50% 증가
    protected bool weakness;         // 약화, 가하는 데미지 25% 감소
    protected bool fragile;          // 손상, 얻는 방어도 25% 감소


    protected void Awake()
    {
        GameObject barUI = Resources.Load<GameObject>("Prefabs/UI/HPBarUI");
        GameObject bar = Instantiate(barUI,gameObject.transform);
        hpBarUI = bar.GetComponent<HPBarUI>();
        hpBarUI.Init(this);
        gameObject.AddComponent<BoxHighlite>();
    }

    public void Initialize(BattleManager bm)
    {
        battleManager = bm;
    }

    public int CalcDamage(Damagable Target,int number)
    {
        int damage = 0;
        damage = number + attackUp;
        damage = (int)(damage * (weakness ? 0.75 : 1) * (Target.vulnuerable ? 1.5 : 1));
        return damage;
    }

    public int CalcDamage(int number)
    {
        int damage = 0;
        damage = number + attackUp;
        damage = (int)(damage * (weakness ? 0.75 : 1));
        return damage;
    }

    public int CalcDeffense(int number)
    {
        int deffense = 0;
        deffense = number + deffenseUp;
        deffense = (int)(deffense * (fragile ? 0.75 : 1));

        return deffense;
    }

    public void AddPower(Power power)
    {
        powers.Add(power);
        power.OnApply(this);
    }

    public void RemovePower(Power power)
    {
        power.OnRemove(this);
        powers.Remove(power);
    }

    public void StartTurn()
    {
        foreach (var power in powers)
        {
            power.OnTurnStart(this);
        }
    }

    public void EndTurn()
    {
        foreach (var power in powers)
        {
            power.OnTurnEnd(this);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        OnHPChanged?.Invoke();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void GainShield(int value)
    {
        shield += CalcDeffense(value);
        OnHPChanged?.Invoke();
    }

    public void Healed(int damage)
    {
        currentHP += damage;
        if (currentHP >= maxHP) currentHP = maxHP;
        OnHPChanged?.Invoke();
    }

    public void Attack(Damagable enemy,int damage)
    {
        enemy.TakeDamage(damage);
    }

    public void Die()
    {


    }

    void OnMouseEnter()
    {
        GameManager.Instance.Player.Status.target = this;
        GameManager.Instance.UpdateUI();
        Debug.Log("타겟팅");
    }

    void OnMouseExit()
    {
        GameManager.Instance.Player.Status.target = null ;
        GameManager.Instance.UpdateUI();

    }
}
