using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    public ParticleSystem exhaustRPS;
    public ParticleSystem firePS;
    public ParticleSystem smoke;
    public GameObject rTrack;
    public GameObject lTrack;


    NavMeshAgent agent;
    MainAI mainAI;
    Material rMat;
    Material lMat;

    void Awake()
    {        
        agent = GetComponent<NavMeshAgent>();
        mainAI = gameObject.GetComponent<MainAI>();
        mainAI.fireEvent += FireAnimation;
        rMat = rTrack.GetComponent<MeshRenderer>().material;
        lMat = lTrack.GetComponent<MeshRenderer>().material;
    }

    
    void FixedUpdate()
    {
        MovingAnimation();
    }

    void MovingAnimation()
    {
        if( agent.velocity.x > 0.0005f || agent.velocity.z > 0.0005f) {
            rMat.mainTextureOffset += Vector2.right * 0.1f;
            lMat.mainTextureOffset += Vector2.right * 0.1f;
            exhaustRPS.Play(true);
        } else {
            rMat.mainTextureOffset = Vector2.zero;
            lMat.mainTextureOffset = Vector2.zero;
            exhaustRPS.Stop(true);
        }        
    }

    void FireAnimation(bool fireEvent)
    {
        if(fireEvent) {
            smoke.Play(true);          
            firePS.Play(true);
        } else {
            smoke.Stop(true);
            firePS.Stop(true);
        }
    }

}
