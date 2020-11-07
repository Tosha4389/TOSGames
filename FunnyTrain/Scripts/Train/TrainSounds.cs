using UnityEngine;

public class TrainSounds : MonoBehaviour
{
    AudioSource sounds;

    void Awake()
    {
        sounds = GetComponent<AudioSource>();
    }
    
    public void Playbeep()
    {
        if(!sounds.isPlaying)
            sounds.Play();
    }
}
