using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;
    private PlayerStatus playerStatus;
    private int turnCount;
    public int TurnCount
    {
        get { return turnCount; }
    }


    public void Init(int battleid)
    {
        playerStatus = GameManager.Instance.Player.Status;
        playerStatus.Initialize(this);
        switch (battleid)
        {
            case 1:
                GameObject skeleton = Instantiate(skeletonPrefab,new Vector3(0,0,0), Quaternion.identity);
                Monster skeletonStatus = skeleton.GetComponent<Monster>();
                skeletonStatus.Initialize(this);
                break;
        }
    }

    public void DevSetting()
    {
        //playerStatus.energy = playerStatus.maxEnergy;
        DeckManager.Instance.preDrawCards = DeckManager.Instance.playerDeck;   // 임시 나중에 반드시 삭제
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        //playerStatus.StartTurn();
    }

    public void TurnStart()
    {
        BattleEvents.OnTurnStart.Invoke();
        playerStatus.energy = playerStatus.maxEnergy;
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
    }

    public void EnemyTurnEnd()
    {
        BattleEvents.OnEnemyTurnEnd.Invoke();
        DeckManager.Instance.DiscardAllHand();

    }
}
