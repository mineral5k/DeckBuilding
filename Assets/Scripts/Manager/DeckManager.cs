using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    public List<GameObject> playerDeck;                  // 게임상 보유하는 덱, 전투중에 변화하지 않음 
    public List<GameObject> preDrawCards;                // 전투시 뽑을 카드뭉치
    public List<GameObject> handCards;                   // 전투시 핸드
    public List<GameObject> usedCards;                   // 전투시 사용한 카드뭉치
    public List<GameObject> exhaustedCards;              // 소멸한 카드뭉치

    [SerializeField]
    private GameObject cardTemplete;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AddCard(int id, List<GameObject> deck)              // id : 카드 아이디 deck : 어디에 추가하는지 
    {
        GameObject newCard = Instantiate(cardTemplete);
        newCard.GetComponent<Card>().SetCard(id);
        deck.Add(newCard);
        newCard.SetActive(false);
    }

    public void BattleDeckSetting()                               // 전투 시작시 세팅
    {
        preDrawCards = playerDeck;
        preDrawCards.Shuffle();
        
    }

    public void ShuffleDeck()                                 //카드 섞기                        
    {
        preDrawCards = usedCards;
        preDrawCards.Shuffle();
    }

    public void DrawCard()   
    {
        if (preDrawCards.Count == 0) ShuffleDeck();           // 뽑을 카드가 없을 때 다시 섞기
        handCards.Add(preDrawCards[0]);
        preDrawCards.RemoveAt(0);
    }

    public void MoveCard(List<GameObject> from, List<GameObject> to, GameObject card)
    {
        to.Add(card);
        from.Remove(card);
    }
      
    public void DeckReset()                                  //전투 종료 시 전투관련 카드 리스트 초기화
    {
        preDrawCards.Clear();
        handCards.Clear();
        usedCards.Clear();
        exhaustedCards.Clear();
    }





}


public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
