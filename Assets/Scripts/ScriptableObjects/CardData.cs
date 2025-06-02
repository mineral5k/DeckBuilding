using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Base, Rare, Epic, Legend, Debuff }

[CreateAssetMenu(fileName = "NewCardData", menuName = "Data/CardData")]
public class CardData : ScriptableObject
{
    public int cardId;
    public string cardName;
    public int cost;
    public CardType type;
    public Sprite image;
    public int damage;
    public int deffense;
    public string description;


}
