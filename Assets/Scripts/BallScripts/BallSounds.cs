using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallSounds : MonoBehaviour
{
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioSource audioSource;

    public void PlayHitSound()
    { 
        audioSource.PlayOneShot(hitClip);
    }
    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(hurtClip);
    }
}
