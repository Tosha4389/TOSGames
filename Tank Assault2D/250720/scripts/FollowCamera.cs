using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 10

public class FollowCamera : MonoBehaviour
{

    [Header("Автоматически:")]    
    public GameObject tank;    

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(tank) {
            if(tank.transform.position.y >= 10f) {
                transform.position = new Vector3(transform.position.x, tank.transform.position.y, transform.position.z);

            } else {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Debug.LogError("Объект Танк не обнаружен");
        }

    }

}
