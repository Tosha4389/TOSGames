using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnimations : MonoBehaviour
{        
    public GameObject exhaustR;
    public GameObject exhaustL;

    Tank tank;
    ParticleSystem exhaustRPS;
    ParticleSystem exhaustLPS;

    private void Awake()
    {
        tank = Tank.S;
        exhaustRPS = exhaustR.GetComponent<ParticleSystem>();
        exhaustLPS = exhaustL.GetComponent<ParticleSystem>();

    }

    void FixedUpdate()
    {
        TankAnimation();
    }

    void TankAnimation()
    {
        if(tank.moveX != 0 || tank.moveY != 0) {
            exhaustRPS.Play(true);
            exhaustLPS.Play(true);                   
        } else {
            exhaustRPS.Stop(true);
            exhaustLPS.Stop(true);            
        }

    }

}
