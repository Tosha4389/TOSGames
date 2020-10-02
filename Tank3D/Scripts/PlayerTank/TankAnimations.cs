using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnimations : MonoBehaviour
{        
    public GameObject exhaustR;
    public GameObject exhaustL;
    public GameObject rTrack;
    public GameObject lTrack;

    Tank tank;
    ParticleSystem exhaustRPS;
    ParticleSystem exhaustLPS;
    Material rMat;
    Material lMat;

    private void Awake()
    {
        tank = Tank.S;
        exhaustRPS = exhaustR.GetComponent<ParticleSystem>();
        exhaustLPS = exhaustL.GetComponent<ParticleSystem>();
        rMat = rTrack.GetComponent<MeshRenderer>().material;
        lMat = lTrack.GetComponent<MeshRenderer>().material;

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

}
