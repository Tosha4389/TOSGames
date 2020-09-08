using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{    
    public GameObject trackAnim;
    public GameObject exhaustR;
    public GameObject exhaustL;

    ParticleSystem exhaustRPS;
    ParticleSystem exhaustLPS;
    NavMeshAgent agent;    

    void Awake()
    {        
        agent = GetComponent<NavMeshAgent>();
        exhaustRPS = exhaustR.GetComponent<ParticleSystem>();
        exhaustLPS = exhaustL.GetComponent<ParticleSystem>();
    }

    
    void FixedUpdate()
    {
        ExhaustAnimation();
    }

    void ExhaustAnimation()
    {
        if(agent.velocity.x > 0.005f || agent.velocity.y > 0.005f) {            
            trackAnim.SetActive(true);
            exhaustRPS.Play(true);
            exhaustLPS.Play(true);
        } else {
            trackAnim.SetActive(false);
            exhaustRPS.Stop(true);
            exhaustLPS.Stop(true);
        }
        
    }
}
