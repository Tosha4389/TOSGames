using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 25

public class BorderCheck : MonoBehaviour
{
    //[Header("Вручную:")]
    

    [Header("Автоматически:")]    
    public float screenWidth;
    public float screenHeight;
    public float size;
    public bool exitBorder = false;    

    [HideInInspector]
    public bool exitRight, exitLeft, exitTop, exitBottom;
               

    private void Awake()
    {
                
    }

    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = Camera.main.aspect * screenHeight;

        Vector3 scale = transform.localScale;
        if(scale.x >= scale.y) {
            size = scale.x;
        } else {
            size = scale.y / 2;
        }

    }
        
    void FixedUpdate()
    {
        CheckExit();
    }

    private void CheckExit()
    {
        Vector3 position = transform.position;        

        if(position.x > screenWidth - size) {
            exitRight = true;            
        }
        if(position.x < -screenWidth + size) {
            exitLeft = true;            
        }
        if(position.y > GameManager.S.lenghtScene) {
            exitTop = true;            
        }
        if(position.y < - screenHeight + size) {
            exitBottom = true;            
        }

        if (exitRight || exitLeft || exitBottom || exitTop == true) {
            exitBorder = true;
        }

    }
}
