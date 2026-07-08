using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    public void PlayCardFlipSound()
    {
        SoundManager.Instance.PlayCardFlipSound();
    }

    public void PlayPlayerHitSound()
    {
        SoundManager.Instance.PlayPlayerHitSound();
    }

    public void PlayPlayerAttackSound()
    {
        SoundManager.Instance.PlayPlayerAttackSound();
    }

    public void PlaySkeletonHitSound()
    {
        SoundManager.Instance.PlaySkeletonHitSound();
    }

    public void PlaySkeletonAttackSound()
    {
        SoundManager.Instance.PlaySkeletonAttackSound();
    }

    public void PlaySkeletonDieSound()
    {
        SoundManager.Instance.PlaySkeletonDieSound();
    }
}
