using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSounds : MonoBehaviour
{
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    
    public void Playbeep()
    {
        if(!audio.isPlaying)
            audio.Play();
    }
}
