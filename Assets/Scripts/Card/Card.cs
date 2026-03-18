using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

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
        costText.text = cardData.cost.ToString();
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

        descriptionText.text = cardData.description;                                                // 적과 아군 상태에 따라 숫자를 실시간으로 반영
        descriptionText.text = descriptionText.text.Replace("Damage", finalDamage.ToString());
        descriptionText.text = descriptionText.text.Replace("Deffense", finalDeffense.ToString());
    }

    public void UpdateUI()
    {
        OnThisCardUIUpdate?.Invoke();
        costText.text = cardData.cost.ToString();

        descriptionText.text = cardData.description;                                                // 적과 아군 상태에 따라 숫자를 실시간으로 반영
        descriptionText.text = descriptionText.text.Replace("Damage", finalDamage.ToString());
        descriptionText.text = descriptionText.text.Replace("Deffense",finalDeffense.ToString());

    }

    public void UseCard()
    {
        foreach (var effect in cardData.effects)
        {
            int value = 0;
            if (effect is AttackEffect) value = finalDamage;
            else if (effect is DeffenseEffect) value = finalDeffense;
            effect.Excute(player.target, value);
        }
    }









    void OnMouseOver()
    {
        // 마우스를 올리면 오브젝트를 크게 만듦
        transform.localScale = new Vector3(1.2f, 1.2f, 1f);
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
    }

    void OnMouseExit()
    {
        // 마우스가 나가면 원래 크기로 복구
        transform.localScale = Vector3.one;
    }
}
