using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//40

public class HPBar : MonoBehaviour
{    
    public Vector3 scaleBar;
    Tank tank;
    public GameObject greenBar;
    public GameObject redBar;

    void Awake()
    {        
        tank = Tank.S;
    }
        
    void FixedUpdate()
    {        
        TrackingBar();
        RotationBar();
    }


    public virtual void TrackingBar()
    {
        transform.position = Tank.S.transform.position + Vector3.down * 1.5f;
    }

    public virtual void RotationBar()
    {
        float rotClampX = Mathf.Clamp(transform.rotation.x, 0f, 0f);
        float rotClampY = Mathf.Clamp(transform.rotation.y, 0f, 0f);
        float rotClampZ = Mathf.Clamp(transform.rotation.z, 0f, 0f);
        transform.rotation = Quaternion.Euler(rotClampX, rotClampY, rotClampZ);
    }

    public virtual void ScaleBar()
    {        
        if(tank.playerHP < 100 && tank.playerHP >=0) {
            greenBar.SetActive(true);
            redBar.SetActive(true);           
        }
        scaleBar = greenBar.transform.localScale;        
        greenBar.transform.localScale = new Vector3(tank.playerHP / 100f, scaleBar.y, scaleBar.z);
        greenBar.transform.localPosition = new Vector3((1f - tank.playerHP / 100f) / -2f, 0f, 0f);
    }

}
