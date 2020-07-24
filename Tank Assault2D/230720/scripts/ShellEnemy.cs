using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 10

public class ShellEnemy : Shell
{


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        borderCheck = GetComponent<BorderCheck>();

    }



    private void OnTriggerEnter(Collider other)
    {

    }

    public override void ShellMoving()
    {
        rigidbody.velocity = -speed * transform.up;
    }
}
