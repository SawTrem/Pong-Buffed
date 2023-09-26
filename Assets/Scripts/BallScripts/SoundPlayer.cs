using System;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public Action PlayHitSoundAction;

    private void PlayHitSound() {
        audioSource.Play();   
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