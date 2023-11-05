using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip hitClip;
    public AudioClip dieClip;
    public AudioClip shootClip;
    public AudioClip swapClip;
    public AudioSource audioSource;
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hitClip);
    }
    public void PlayDie()
    {
        audioSource.PlayOneShot(dieClip);
    }
    public void PlayShoot()
    {
        audioSource.PlayOneShot(shootClip);
    }
    public void PlaySwap()
    {
        audioSource.PlayOneShot(swapClip);
    }
}
