using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private EnergyText EnergyText;
    public PlayerStatus Player => GameManager.Instance.Player.Status;
    
    

    private int turnCount = 0;
    public int TurnCount
    {
        get { return turnCount; }
    }
    private int battleId => GameManager.Instance.battleId;

    public List<Monster> enemies = new List<Monster> { };
    public List<Transform> enemyLocations1;   // 적 1체일 때 몬스터 위치
    public List<Transform> enemyLocations2;   // 적 2체일 때 몬스터 위치
    public List<Transform> enemyLocations3;   // 적 3체일 때 몬스터 위치

    private List<Transform> actualEnemyLocations;  // 실제 생성시 사용할 리스트




    public void SetEnemies()
    {
        string[] monsterNames;
        switch (battleId)                                              // 게임 매니저에서 설정된 배틀 아이디를 참조하여 적을 결정
        {
            case 1:
                monsterNames = new string[] { "Skeleton"};
                actualEnemyLocations = enemyLocations1;
                break;

            case 2:
                monsterNames = new string[] { "Skeleton2_1", "Skeleton2_2" };
                actualEnemyLocations = enemyLocations2;
                break;

            default:
                Debug.LogWarning("정의되지 않은 battleId입니다!" + battleId);
                monsterNames = new string[] { "Skeleton" };        // 임시 코드 
                actualEnemyLocations = enemyLocations1;
                break;
            
        }
        
        for(int i = 0; i < monsterNames.Length; i++)
        {
            GameObject monsterPrefab = Resources.Load<GameObject>("Prefabs/Monsters/" + monsterNames[i]);
            Vector3 monsterPosition = Camera.main.ScreenToWorldPoint(actualEnemyLocations[i].position);
            monsterPosition.z = 0;
            GameObject monster = Instantiate(monsterPrefab, monsterPosition,Quaternion.identity);
            Monster monsterstatus = monster.GetComponent<Monster>();
            monsterstatus.Initialize(this);
            enemies.Add(monsterstatus);
        }

    }

    public void Init()
    {
        turnCount = 0;
        //playerStatus = GameManager.Instance.Player.Status;
        Player.Initialize(this);
        Player.OnEnergyChanged += EnergyText.UpdateEnergyUI;
        DeckManager.Instance.BattleDeckSetting();
        TurnStart();
        
    }

    public void UpdateAllEnemiesIntention()
    {
        if (enemies.Count == 0) return;
        foreach (var enemy in enemies)
        {
            enemy.hpBarUI.UpdateIntentionIcons();
        }
    }

    public void DevSetting()
    {
        //playerStatus.energy = playerStatus.maxEnergy;
        //DeckManager.Instance.preDrawCards = DeckManager.Instance.playerDeck;   // 임시 나중에 반드시 삭제
        //DeckManager.Instance.DrawCard();
        //DeckManager.Instance.DrawCard();
        //DeckManager.Instance.DrawCard();
        //DeckManager.Instance.DrawCard();
        //DeckManager.Instance.DrawCard();
        //playerStatus.StartTurn();
    }

    public void TurnStart()
    {
        turnCount++;
        Player.ResetShield();
        BattleEvents.OnTurnStart?.Invoke();
        //playerStatus.energy = playerStatus.maxEnergy;
        DeckManager.Instance.DrawCard(5);
        Player.Energy = Player.MaxEnergy;
        
    }

    public void PlayerTurnEnd()
    {
        BattleEvents.OnPlayerTurnEnd?.Invoke();
        DeckManager.Instance.DiscardAllHand();

    }

    public async void EnemyTurnStart()
    {
        BattleEvents.OnEnemyTurnStart?.Invoke();
        foreach (var enemy in enemies )
        {
            enemy.ResetShield();
            await Task.Delay(1000);
            enemy.NextPattern();
            await Task.Delay(600);

        }
        EnemyTurnEnd();
        TurnStart();
    }

    public void EnemyTurnEnd()
    {
        BattleEvents.OnEnemyTurnEnd?.Invoke();

    }

    public void ProceedTurn()         // 턴 종료 버튼을 눌렀을 시 실행 
    {
        PlayerTurnEnd();
        EnemyTurnStart();
        //EnemyTurnEnd(); 
        //TurnStart();
    }

    public async void MonsterDie(Monster monster)
    {
        enemies.Remove(monster);
        await Task.Delay(1400);
        Destroy(monster.gameObject);
        if (enemies.Count ==0 )
        {
            WinStage();
        }
    }

    public void WinStage()
    {
        ResetBattleManager();
        Player.Resetstatus();
        GameManager.Instance.battleId++;
        GameManager.Instance.SelectCard();
    }

    public void ResetBattleManager()
    {
        AnimManager.Instance.BattleEnd();
        foreach (var card in DeckManager.Instance.handCards)
        {
            card.SetActive(false);
        }
        DeckManager.Instance.handCards.Clear();
        DeckManager.Instance.usedCards.Clear();
        DeckManager.Instance.exhaustedCards.Clear();
        DeckManager.Instance.UpdateHand();
        turnCount = 0;
        Player.powers.Clear();
        BattleEvents.Reset();
    }
}
