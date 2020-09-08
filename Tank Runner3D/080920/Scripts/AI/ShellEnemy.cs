using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5

public class ShellEnemy : Shell
{


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        borderCheck = GetComponent<BorderCheck>();
    }

    public override void ShellMoving()
    {
        rigidbody.velocity = speed * transform.forward;
    }
}
