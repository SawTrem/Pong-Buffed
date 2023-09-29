using System;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource soundHit;
    public Action PlayHitSoundAction;

    private void PlayHitSound() {
        soundHit.Play();   
    }

    private void OnEnable()
    {
        PlayHitSoundAction += PlayHitSound;
    }
    private void OnDisable()
    {
        PlayHitSoundAction -= PlayHitSound;
    }
}