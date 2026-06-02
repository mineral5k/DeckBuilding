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


    private void Start()
    {
        DontDestroyOnLoad(this);
        GameManager.Instance.Player = this;
        gameObject.AddComponent<PlayerStatus>();
        status = gameObject.GetComponent<PlayerStatus>();

    }
}
