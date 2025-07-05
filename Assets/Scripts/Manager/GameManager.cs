using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnAllCardUIUpdate;

    public static GameManager Instance;
    private Player player;
    public int battleId;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

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

        OnAllCardUIUpdate = null;
    }

    public void UpdateUI()
    {
        OnAllCardUIUpdate?.Invoke();
    }

    public void EnterBattleStage()
    {

    }
}
