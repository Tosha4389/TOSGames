using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 7

public class FollowCamera : MonoBehaviour
{

    [Header("Автоматически:")]    
    public GameObject tank;    

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
        if(tank.transform.position.y >= 10f) {
            transform.position = new Vector3(transform.position.x, tank.transform.position.y, transform.position.z);

        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }


    }

}
