using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test2 : MonoBehaviour
{
    
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.destination = new Vector3(-4f, 9f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
