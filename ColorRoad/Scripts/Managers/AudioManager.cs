using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerDestroySound()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(audioClips[0]);
    }

    public void EnterInCoins()
    {        
        audioSource.PlayOneShot(audioClips[1]);
    }

    public void PlayGameWin()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
}
