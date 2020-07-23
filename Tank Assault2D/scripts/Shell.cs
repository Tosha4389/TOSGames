using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 12

public class Shell : MonoBehaviour
{
    [Header("Вручную Shell")]
    public float speed;

    [Header("Автоматически")]    
    public Rigidbody rigidbody;
    public BorderCheck borderCheck;
    

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
        GetComponent<Rigidbody>().velocity = speed * transform.up;            
    }

    private void OnTriggerEnter(Collider other)
    {



    }

}
