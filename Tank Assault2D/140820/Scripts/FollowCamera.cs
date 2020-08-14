using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 10

public class FollowCamera : MonoBehaviour
{

    [Header("Автоматически:")]    
    public GameObject tank;
    private BorderCheck borderCheck;

    private void Awake()
    {
        borderCheck = tank.GetComponent<BorderCheck>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {

        if(tank) {            
             transform.position = new Vector3(transform.position.x, tank.transform.position.y + borderCheck.screenHeight - borderCheck.size * 2, transform.position.z);            
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);            
        }

    }

}
