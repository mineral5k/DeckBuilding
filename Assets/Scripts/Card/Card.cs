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


    private int finalDamage => cardData.damage;
    private int finalDeffense => cardData.deffense; 




    private void Awake()
    {
        OnThisCardUIUpdate = null;
        //Player player = GameManager.Instance.Player;
        SetCard(1);
    }

    private void Start()
    {
        GameManager.Instance.OnAllCardUIUpdate += UpdateUI;
    }

    public void SetCard( int  cardId )
    {
        cardData = Resources.Load<CardData>($"ScriptableObjects/CardData/{cardId}");
        UpdateUI();
    }

    public void UpdateUI()
    {
        OnThisCardUIUpdate?.Invoke();
        cardImage.sprite = cardData.image;
        costText.text = cardData.cost.ToString();
        nameText.text = cardData.cardName;

        switch ( cardData.type )
        {
            case CardType.Base:
                typeText.text = "�⺻";
                break;
            case CardType.Rare:
                typeText.text = "���";
                break;
            case CardType.Epic:
                typeText.text = "����";
                break;
            case CardType.Legend:
                typeText.text = "����";
                break;
            case CardType.Debuff:
                typeText.text = "�����̻�";
                break;
        }

        descriptionText.text = cardData.description;
        descriptionText.text = descriptionText.text.Replace("Damage", finalDamage.ToString());
        descriptionText.text = descriptionText.text.Replace("Deffense",finalDeffense.ToString());

    }
}
