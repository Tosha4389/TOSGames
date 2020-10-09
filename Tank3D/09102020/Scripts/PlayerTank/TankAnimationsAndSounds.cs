using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnimationsAndSounds : MonoBehaviour
{        
    public GameObject exhaustR;
    public GameObject exhaustL;
    public GameObject rTrack;
    public GameObject lTrack;
    public AudioClip[] audioClips;

    Tank tank;
    ParticleSystem exhaustRPS;
    ParticleSystem exhaustLPS;
    Material rMat;
    Material lMat;
    AudioSource turretAudio;
    AudioSource chassisAudio;
    AudioSource engineAudio;

    private void Awake()
    {
        tank = Tank.S;
        exhaustRPS = exhaustR.GetComponent<ParticleSystem>();
        exhaustLPS = exhaustL.GetComponent<ParticleSystem>();
        rMat = rTrack.GetComponent<MeshRenderer>().material;
        lMat = lTrack.GetComponent<MeshRenderer>().material;
        turretAudio = tank.turret.GetComponent<AudioSource>();
        chassisAudio = tank.chassis.GetComponent<AudioSource>();
        engineAudio = tank.GetComponent<AudioSource>();
        turretAudio.Stop();
        chassisAudio.Stop();

    }

    private void Start()
    {
        tank.eventMovingAudio.AddListener(PlayAudioMoving);
        tank.eventRotateAudio.AddListener(PlayAudioRotate);
    }

    void FixedUpdate()
    {
        MovingAnimation();        
    }

    void MovingAnimation()
    {
        if(tank.moveX != 0 || tank.moveY != 0) {
            exhaustRPS.Play(true);
            exhaustLPS.Play(true);
            rMat.mainTextureOffset += Vector2.right * 0.1f;
            lMat.mainTextureOffset += Vector2.right * 0.1f;
        } else {
            exhaustRPS.Stop(true);
            exhaustLPS.Stop(true);
            rMat.mainTextureOffset = Vector2.zero;
            lMat.mainTextureOffset = Vector2.zero;
        }
    }

    void PlayAudioMoving(bool play)
    {
        if(play) {
            if(!chassisAudio.isPlaying) {
                chassisAudio.Play();
            }

            if(engineAudio.clip == audioClips[0]) {
                engineAudio.clip = audioClips[1];
                engineAudio.loop = true;
                engineAudio.Play();
            }

        } else { 
            chassisAudio.Stop();

            if( engineAudio.clip == null || engineAudio.clip == audioClips[1]) {
                engineAudio.clip = audioClips[0];
                engineAudio.loop = true;
                engineAudio.Play();
            }
        }
    }

    void PlayAudioRotate(bool play)
    {
        if(play) {
            if(!turretAudio.isPlaying)
                turretAudio.Play();
        } else turretAudio.Stop();
    }

}
