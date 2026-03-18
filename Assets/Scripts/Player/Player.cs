using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStatus status;
    public PlayerStatus Status
    {
        get { return status; }
    }

    public Card card;

    private void Start()
    {
        DontDestroyOnLoad(this);
        GameManager.Instance.Player = this;
        gameObject.AddComponent<PlayerStatus>();
        status = gameObject.GetComponent<PlayerStatus>();
        //임시 코드 
        card.SetCard(1);

    }
}
