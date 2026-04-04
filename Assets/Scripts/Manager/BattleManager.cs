using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;
    private PlayerStatus playerStatus;
    private int turnCount = 1;
    public int TurnCount
    {
        get { return turnCount; }
    }

    public List<Monster> enemies;


    public void Init(int battleid)
    {
        turnCount = 1;
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
        turnCount++;
        BattleEvents.OnTurnStart?.Invoke();
        //playerStatus.energy = playerStatus.maxEnergy;
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
        DeckManager.Instance.DrawCard();
    }

    public void PlayerTurnEnd()
    {
        BattleEvents.OnPlayerTurnEnd?.Invoke();
        DeckManager.Instance.DiscardAllHand();

    }

    public void EnemyTurnStart()
    {
        BattleEvents.OnEnemyTurnStart?.Invoke();
        foreach (var enemy in enemies )
        {
            enemy.NextPattern();
        }
    }

    public void EnemyTurnEnd()
    {
        BattleEvents.OnEnemyTurnEnd?.Invoke();

    }

    public void ProceedTurn()         // 턴 종료 버튼을 눌렀을 시 실행 
    {
        PlayerTurnEnd();
        EnemyTurnStart();
        EnemyTurnEnd(); 
        TurnStart();
    }
}
