using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRocket : Shell
{
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        borderCheck = GetComponent<BorderCheck>();
    }

    public override void ShellMoving()
    {
        rigidbody.velocity = -speed * transform.up;
    }
}
