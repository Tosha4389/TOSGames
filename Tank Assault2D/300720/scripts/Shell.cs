﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 15

public class Shell : MonoBehaviour
{
    [Header("Вручную Shell")]
    public float speed;
    public int playerDamage = 50;

    [Header("Автоматически")]    
    public new Rigidbody rigidbody;
    public BorderCheck borderCheck;
    public GameObject exploAnimPrefab;

    private void Awake()    
    {        
        borderCheck = GetComponent<BorderCheck>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ShellMoving();

    }

    void FixedUpdate()
    {
        DestroyShell();
    }

    public void DestroyShell()
    {   
        if(borderCheck.exitBorder == true) {
            Destroy(gameObject);
        }
    }

    public virtual void ShellMoving()
    {
        rigidbody.velocity = speed * transform.up;            
    }

    private void OnTriggerEnter(Collider collision)
    {                   
        switch(collision.gameObject.tag) {
            case "Barrier":
                Destroy(gameObject);
                
                break;



            default:                
                break;
        }

    }

}
