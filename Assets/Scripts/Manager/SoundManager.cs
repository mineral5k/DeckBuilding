using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("BGM")]
    public AK.Wwise.Event BGM;

    [Header("Card Sound")]
    public AK.Wwise.Event CardFlipSound;
    public AK.Wwise.Event CardSelectSound;

    [Header("Player Sound")]
    public AK.Wwise.Event playerHitSound;
    public AK.Wwise.Event playerAttackSound;

    [Header("Enemy Sound")]
    public AK.Wwise.Event skeletonHitSound;
    public AK.Wwise.Event skeletonAttackSound;
    public AK.Wwise.Event skeletonDieSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        BGM.Post(gameObject);
    }

    public void PlayCardFlipSound()
    {
        CardFlipSound.Post(gameObject);
    }

    public void PlayCardSelectSound()
    {
        CardSelectSound.Post(gameObject);
    }

    public void PlayPlayerHitSound()
    {
        playerHitSound.Post(gameObject);
    }

    public void PlayPlayerAttackSound()
    {
        playerAttackSound.Post(gameObject);
    }

    public void PlaySkeletonHitSound()
    {
        skeletonHitSound.Post(gameObject);
    }

    public void PlaySkeletonAttackSound()
    {
        skeletonAttackSound.Post(gameObject);
    }

    public void PlaySkeletonDieSound()
    {
        skeletonDieSound.Post(gameObject);
    }
   
}
