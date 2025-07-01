using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    protected int maxHP;
    protected int currentHP;
    protected float percentHP => currentHP / maxHP;
    protected bool isDead => currentHP <= 0;
    protected List<Power> powers = new List<Power>();

    protected int attackUp;          // ���� ���� ����,����
    protected int deffenseUp;        // ��� �� ����,����
    protected bool vulnuerable;      // ���, �޴� ������ 50% ����
    protected bool weakness;         // ��ȭ, ���ϴ� ������ 25% ����



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
