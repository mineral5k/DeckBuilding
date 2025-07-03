using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectHandler : MonoBehaviour
{

    public static CardEffectHandler HandlerInstance;

    public Damagable playerStatus;
    public Damagable selectedEnemy;

    private void Awake()
    {
        if (HandlerInstance == null)
        {
            HandlerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (HandlerInstance != null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Play1()
    {
        playerStatus.Attack(selectedEnemy,8);
    }
        
        


}
