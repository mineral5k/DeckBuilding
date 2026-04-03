using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Attack, Skill, Power, Debuff }

[CreateAssetMenu(fileName = "NewCardData", menuName = "Data/CardData")]
public class CardData : ScriptableObject
{
    public int cardId;
    public string cardName;
    public int cost;
    public CardType type;
    public Sprite image;
    public bool isTargetting;
    public int damage;
    public int deffense;
    public int utilityAmount;
    public string description;
    public List<CardEffect> effects; 


}

public abstract class CardEffect : ScriptableObject
{
    public abstract void Excute(Damagable? target, int value);
}

public abstract class AttackEffect : CardEffect
{

}

public abstract class DeffenseEffect : CardEffect
{

}

public abstract class UtilityEffect : CardEffect
{

}


