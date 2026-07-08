using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public event Action OnHPChanged;
    public HPBarUI hpBarUI;
    public BattleManager battleManager;
    public Damagable target = null;
    public Animator animator;
    private GameObject DamagePopUpObject;

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
    public int shield;
    
    protected float percentHP => (float)currentHP / (float)maxHP;
    public float PercentHP
    {
        get { return percentHP; }
    }
    protected bool isDead => currentHP <= 0;
    public List<Power> powers = new List<Power>();

    public int attackUp;          // 공격 피해 증가,감소
    public int deffenseUp;        // 얻는 방어도 증가,감소
    public bool vulnuerable;      // 취약, 받는 데미지 50% 증가
    public bool weakness;         // 약화, 가하는 데미지 25% 감소
    public bool fragile;          // 손상, 얻는 방어도 25% 감소


    protected void Awake()
    {
        GameObject barUI = Resources.Load<GameObject>("Prefabs/UI/HPBarUI");
        DamagePopUpObject = Resources.Load<GameObject>("Prefabs/UI/DamagePopUp");
        GameObject bar = Instantiate(barUI,gameObject.transform);
        hpBarUI = bar.GetComponent<HPBarUI>();
        hpBarUI.Init(this);
        gameObject.AddComponent<BoxHighlite>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Initialize(BattleManager bm)
    {
        battleManager = bm;
        if( this is Monster)       // 몬스터일 경우 플레이어를 타겟으로 삼음.
        {
            Debug.Log(" 몬스터 생성");
            target = bm.Player;
        }
        SetFirstIntention();
    }

    public void HPBarUIUpdate()
    {
        OnHPChanged?.Invoke();
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

    public void AddPower(Power power,int value)
    {
        Power powerOfThis = power;           // power의 이 개체 전용 인스턴스 (각 개체마다 파워가 각각 다른 인스턴스를 가져야 하므로)
        bool isExist = false;
        foreach (Power existedPower in powers)        // 추가하려는 파워가 있는지 검사
        {
            if (existedPower.PowerName == power.PowerName)  //추가하려는 파워가 이미 있는지 검사 (동일한 이름의 파워 존재)
            {
                isExist = true;
                powerOfThis = existedPower;
                break;
            }
        }

        if (!isExist)                              // 추가하려는 파워가 아직 없을 시
        {
            powers.Add(powerOfThis);
            powerOfThis.amount += value;
            powerOfThis.OnApply(this);
        }
        else                                     // 추가하려는 파워가 이미 존재할 시
        {
            powerOfThis.amount += value;
            powerOfThis.OnAdded(this);
        }
        OnHPChanged?.Invoke();

    }

    public void TakeDamage(int damage)
    {
        if (damage <= shield)
        {
            shield -= damage;
            OnHPChanged?.Invoke();
            PopUpDamage("Blocked");
        }

        else if (damage >shield)
        {
            int hpDamage = damage - shield;
            shield = 0;
            currentHP -= hpDamage;
            PopUpDamage(hpDamage.ToString());
            OnHPChanged?.Invoke();
            if (currentHP <= 0)
            {
                animator.SetBool("IsDead", true);
                Die();
            }
            else
            {
                animator.SetTrigger("HurtTrigger");
            }
        }
    }

    public void TakeHPDamage(int damage)
    {
        currentHP -= damage;
        OnHPChanged?.Invoke();
        PopUpDamage(damage.ToString());
        if (currentHP <= 0)
        {
            animator.SetBool("IsDead", true);
            Die();
        }
        else
        {
            animator.SetTrigger("HurtTrigger");
        }
    }

    public void PopUpDamage(string damage)
    {
        Vector3 position = new Vector3 (transform.position.x + 1.5f, transform.position.y+2f, transform.position.z);
        Instantiate(DamagePopUpObject,position,Quaternion.identity).GetComponent<DamagePopUp>().PopUp(damage);
    }

    public void GainShield(int value)
    {
        shield += CalcDeffense(value);
        OnHPChanged?.Invoke();
    }

    public void ResetShield()
    {
        shield = 0;
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
        if (enemy == null) return;
        int finalDamage = CalcDamage(enemy, damage);
        animator.SetTrigger("AttackTrigger");
        enemy.TakeDamage(finalDamage);
    }

    public virtual void Die()
    {
        foreach(Power power in powers )
        {
            
        }
    }

    public virtual void SetFirstIntention()
    {

    }
    
    

    void OnMouseEnter()
    {
        GameManager.Instance.Player.Status.target = this;
        GameManager.Instance.UpdateUI();
        //Debug.Log("타겟팅");
        hpBarUI.ShowAllPowerDescPannels();
    }

    void OnMouseExit()
    {
        GameManager.Instance.Player.Status.target = null ;
        GameManager.Instance.UpdateUI();
        hpBarUI.HideAllPowerDescPannels();

    }
}
