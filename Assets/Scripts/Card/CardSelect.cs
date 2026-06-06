using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    [SerializeField] List<CardSelectCard> cardSelectCards;
    private int chance;

    public void SelectCard(int count)
    {
        chance = count;
        Debug.Log("셀렉트 카드");
        foreach (CardSelectCard card in cardSelectCards)
        {
            card.Init(this);
        }
        SuggestCard();
    }

    public void SuggestCard()
    {
        Debug.Log("서제스트 카드");
        System.Random random = new System.Random();
        var uniqueNumbers = Enumerable.Range(1, 4)
                                      .OrderBy(x => random.Next())
                                      .Take(3)
                                      .ToList();

        for (int i = 0; i<3; i++)
        {
            cardSelectCards[i].SetCard(uniqueNumbers[i]);
        }
    }

    public void ClickACard(int cardId)
    {
        Debug.Log("클릭 카드");
        DeckManager.Instance.AddCard(cardId,DeckManager.Instance.playerDeck);
        chance--;
        if (chance > 0)
        {
            SuggestCard();
        }
        else if ( chance == 0)
        {
            BattleManager bm = FindObjectOfType<BattleManager>();
            bm.SetEnemies();
            bm.Init();
            Destroy(gameObject);
        }
    }
}
