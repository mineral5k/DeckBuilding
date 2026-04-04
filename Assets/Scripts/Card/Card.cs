using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Card : MonoBehaviour
{
    public event Action OnThisCardUIUpdate;

    private CardData cardData;
    private SpriteRenderer spriteRenderer;
    public TextMeshPro costText;
    public TextMeshPro descriptionText;
    public TextMeshPro nameText;
    public TextMeshPro typeText;
    public SpriteRenderer cardImage;
    public BezierArrow drawLine;
    private bool isSelected = false;
    private PlayerStatus player => GameManager.Instance.Player.Status;
    private Damagable target => player.target;


    private int finalDamage => (player.target == null) ? player.CalcDamage(cardData.damage) : player.CalcDamage(player.target, cardData.damage);
    private int finalDeffense => player.CalcDeffense(cardData.deffense);

    private Color damageColor;   // 버프로 데미지나 방어도가 높아지면 초록색, 낮아지면 빨간색으로 숫자 표시
    private Color deffenseColor;



    private void Awake()
    {
        OnThisCardUIUpdate = null;
        //Player player = GameManager.Instance.Player;
        //SetCard(1);
    }

    private void Start()
    {
        GameManager.Instance.OnAllCardUIUpdate += UpdateUI;
    }

    public void SetCard( int  cardId )
    {
        cardData = Resources.Load<CardData>($"ScriptableObjects/CardData/{cardId}");
        OnThisCardUIUpdate?.Invoke();
        cardImage.sprite = cardData.image;
        nameText.text = cardData.cardName;

        switch (cardData.type)
        {
            case CardType.Attack:
                typeText.text = "공격";
                break;
            case CardType.Skill:
                typeText.text = "스킬";
                break;
            case CardType.Power:
                typeText.text = "파워";
                break;
            case CardType.Debuff:
                typeText.text = "상태이상";
                break;
        }
        UpdateUI();
        
    }

    public void UpdateUI()
    {
        OnThisCardUIUpdate?.Invoke();
        costText.text = cardData.cost.ToString();

        descriptionText.text = cardData.description;                                                // 적과 아군 상태에 따라 숫자를 실시간으로 반영

        if (finalDamage > cardData.damage)
        {
            descriptionText.text = descriptionText.text.Replace("Damage", $"<color=green>{finalDamage}</color>");
        }
        else if (finalDamage < cardData.damage)
        {
            descriptionText.text = descriptionText.text.Replace("Damage", $"<color=red>{finalDamage}</color>");
        }
        else
        {
            descriptionText.text = descriptionText.text.Replace("Damage", $"{finalDamage}");

        }

        if (finalDeffense > cardData.deffense)
        {
            descriptionText.text = descriptionText.text.Replace("Deffense", $"<color=green>{finalDeffense}</color>");
        }
        else if (finalDeffense < cardData.deffense)
        {
            descriptionText.text = descriptionText.text.Replace("Deffense", $"<color=red>{finalDeffense}</color>");
        }
        else
        {
            descriptionText.text = descriptionText.text.Replace("Deffense", $"{finalDeffense}");
        }

    }

    public void DecideColor()
    {
        if (finalDamage > cardData.damage) damageColor = Color.green;
        else if (finalDamage < cardData.damage) damageColor = Color.red;
        if (finalDeffense > cardData.deffense) deffenseColor = Color.green;
        else if (finalDeffense < cardData.deffense) deffenseColor = Color.red;


    }

    public void UseCard()
    {
        foreach (var effect in cardData.effects)
        {
            int value = 0;
            if (effect is AttackEffect) value = cardData.damage;
            else if (effect is DeffenseEffect) value = cardData.deffense;
            else if (effect is UtilityEffect) value = cardData.utilityAmount;
            effect.Excute(player.target, value);
        }
    }









    void OnMouseOver()
    {
        // 마우스를 올리면 오브젝트를 크게 만듦
        transform.DOScale(1.2f, 0.1f);
        GetComponent<SortingGroup>().sortingOrder = 100;
    }

    private void OnMouseDown()
    {
        drawLine.isDraw = true;
    }

    private void OnMouseUp()
    {
        drawLine.isDraw = false;
        if (GameManager.Instance.Player.Status.target!=null)
        {
            UseCard();
        }
        transform.DOScale(1.0f, 0.1f);
        DeckManager.Instance.UpdateHand();
    }

    void OnMouseExit()
    {
        if (Input.GetMouseButton(0)) return;         //선택해서 움직이는 중이면 유지
        // 마우스가 나가면 원래 크기로 복구
        transform.DOScale(1.0f, 0.1f);
        DeckManager.Instance.UpdateHand();
    }
}
