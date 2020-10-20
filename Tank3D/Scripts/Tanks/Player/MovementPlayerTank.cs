using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerTank : MonoBehaviour, IMovement
{
    [SerializeField] float speedMove;
    [SerializeField] float speedRotate;
    [SerializeField] ParticleSystem exhaustPS;
    [SerializeField] GameObject rTrack;
    [SerializeField] GameObject lTrack;
    [SerializeField] AudioClip[] audioClips;

    Rigidbody rigidbody;
    Material rMat;
    Material lMat;
    Transform chassis;
    AudioSource chassisAudio;
    AudioSource engineAudio;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rMat = rTrack.GetComponent<MeshRenderer>().material;
        lMat = lTrack.GetComponent<MeshRenderer>().material;
        chassis = gameObject.transform.GetChild(1);
        chassisAudio = chassis.GetComponentInChildren<AudioSource>();
        engineAudio = GetComponent<AudioSource>();
        chassisAudio.Stop();
    }

    public void Movement(float moveX, float moveY)
    {
        rigidbody.AddForce(transform.forward * moveY * speedMove, mode: ForceMode.Impulse);
        rigidbody.AddTorque(transform.up * moveX * speedRotate, mode: ForceMode.Impulse);

        if(moveX != 0 || moveY != 0) {
            exhaustPS.Play(true);
            PlayMovementsEffects();
            PlaySoundMovement();
        } else {
            exhaustPS.Stop(true);
            StopSoundMovement();
        }
    }

    void PlayMovementsEffects()
    {
        rMat.mainTextureOffset += Vector2.right * 0.1f;
        lMat.mainTextureOffset += Vector2.right * 0.1f;
    }

    void PlaySoundMovement()
    {
        if(!chassisAudio.isPlaying) {
            chassisAudio.Play();
        }

        if(engineAudio.clip == audioClips[0]) {
            engineAudio.clip = audioClips[1];
            engineAudio.loop = true;
            engineAudio.Play();
        }
    }

    void StopSoundMovement()
    {
       chassisAudio.Stop();

       if(engineAudio.clip == null || engineAudio.clip == audioClips[1]) {
           engineAudio.clip = audioClips[0];
           engineAudio.loop = true;
           engineAudio.Play();
       }
    }

}
