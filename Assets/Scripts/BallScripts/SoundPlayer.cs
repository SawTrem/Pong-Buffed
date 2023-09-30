using System;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    public Action PlaySoundAction;

    private void PlaySound() 
    {
        audioSource.PlayOneShot(audioClip);
    }
    private void OnEnable()
    {
        PlaySoundAction += PlaySound;
    }
    private void OnDisable()
    {
        PlaySoundAction -= PlaySound;
    }
}