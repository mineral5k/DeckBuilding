using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        int battleid = GameManager.Instance.battleId;
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
}
