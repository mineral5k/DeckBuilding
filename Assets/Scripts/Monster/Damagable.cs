using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public event Action OnHPChanged;
    private HPBarUI hpBarUI;

    protected int maxHP;
    public int MaxHP
    {
        get { return maxHP; }
    }
    protected int currentHP;
    public int CurrentHP
    {
        get { return currentHP; }
    }
    protected float percentHP => currentHP / maxHP;
    public float PercentHP
    {
        get { return percentHP; }
    }
    protected bool isDead => currentHP <= 0;
    protected List<Power> powers = new List<Power>();

    protected int attackUp;          // ���� ���� ����,����
    protected int deffenseUp;        // ��� �� ����,����
    protected bool vulnuerable;      // ���, �޴� ������ 50% ����
    protected bool weakness;         // ��ȭ, ���ϴ� ������ 25% ����



    public void Awake()
    {
        GameObject barUI = Resources.Load<GameObject>("Prefabs/UI/HPBarUI");
        GameObject bar = Instantiate(barUI,gameObject.transform);
        hpBarUI = bar.GetComponent<HPBarUI>();
        hpBarUI.Init(this);
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
        if (currentHP < 0)
        {
            Die();
        }
    }

    public void Attack(Damagable enemy,int damage)
    {
        enemy.TakeDamage(damage);
    }

    public void Die()
    {


    }
}
