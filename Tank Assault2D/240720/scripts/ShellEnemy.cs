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


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            //print("В Вас попали!");
            Destroy(gameObject);            
        }

    }

    public override void ShellMoving()
    {
        rigidbody.velocity = -speed * transform.up;
    }
}
