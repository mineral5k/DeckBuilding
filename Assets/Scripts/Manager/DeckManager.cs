using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    public List<GameObject> playerDeck;                  // 게임상 보유하는 덱, 전투중에 변화하지 않음 
    public List<GameObject> preDrawCards;                // 전투시 뽑을 카드뭉치
    public List<GameObject> handCards;                   // 전투시 핸드
    public List<GameObject> usedCards;                   // 전투시 사용한 카드뭉치
    public List<GameObject> exhaustedCards;              // 소멸한 카드뭉치

    [Header("배치 설정")]
    public float cardSpacing = 1.2f;    // 월드 단위 간격 (약 1~1.5 유닛 추천)
    public Vector3 centerPoint = new Vector3(0, -4f, 0); // 화면 하단 중앙 월드 좌표
    public float arcIntensity = 0.2f;   // 부채꼴 곡선 강도
    public float rotationIntensity = 5f; // 회전 강도
    [Space(10f)]
    
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
        preDrawCards[0].SetActive(true);
        handCards.Add(preDrawCards[0]);
        preDrawCards.RemoveAt(0);
        UpdateHand();
    }

    public void UpdateHand()                                 // 핸드의 카드 위치 조정
    {
        int count = handCards.Count;
        if (count == 0) return;

        // 중앙으로부터 왼쪽 시작점 계산
        float totalWidth = (count - 1) * cardSpacing;
        float startX = centerPoint.x - (totalWidth / 2f);

        for (int i = 0; i < count; i++)
        {
            // 1. 기본 위치 계산
            float targetX = startX + (i * cardSpacing);

            // 2. 부채꼴(Arc) Y축 보정
            // 중앙(0)에서 멀어질수록 아래로 내려가게 계산
            float centerOffset = i - (count - 1) / 2f;
            float targetY = centerPoint.y - (Mathf.Abs(centerOffset) * Mathf.Abs(centerOffset) * arcIntensity);

            // 3. Z-Order (앞뒤 순서) 설정
            // 오른쪽 카드가 왼쪽 카드보다 조금 더 앞에 오게 (혹은 반대)
            float targetZ = centerPoint.z - (i * 0.01f);

            Vector3 targetPos = new Vector3(targetX, targetY, targetZ);

            // 4. 회전 계산 (Z축 회전)
            float targetRotZ = centerOffset * -rotationIntensity;
            Quaternion targetRot = Quaternion.Euler(0, 0, targetRotZ);

            // 5. 이동 및 회전 적용 (DOTween)
            handCards[i].transform.DOMove(targetPos, 0.4f).SetEase(Ease.OutCubic);
            handCards[i].transform.DORotateQuaternion(targetRot, 0.4f);

            handCards[i].GetComponent<SortingGroup>().sortingOrder = i;
        }
    
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

    public void GetBasicDeck()
    {
        AddCard(1, playerDeck);
        AddCard(1, playerDeck);
        AddCard(1, playerDeck);
        AddCard(1, playerDeck);
        AddCard(1, playerDeck);

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
