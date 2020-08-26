using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 10

public class FollowCamera : MonoBehaviour
{

    [Header("Автоматически:")]    
    public GameObject tank;
    public bool followEnemy = false;

    private BorderCheck borderCheck;

    private void Awake()
    {
        borderCheck = tank.GetComponent<BorderCheck>();
    }

    void FixedUpdate()
    {
        FollowTank();
    }

    public virtual void FollowTank()
    {
        if(!followEnemy && tank ) {            
             Vector3 camPos = new Vector3(transform.position.x, tank.transform.position.y + borderCheck.screenHeight - borderCheck.size * 2, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, camPos, Time.fixedDeltaTime);
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);            
        }
    }

}
