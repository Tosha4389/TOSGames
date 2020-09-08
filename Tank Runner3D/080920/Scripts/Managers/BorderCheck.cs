using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 30

public class BorderCheck : MonoBehaviour
{
    //[Header("Вручную:")]
    

    [Header("Автоматически:")]    
    public float screenWidth;
    public float screenHeight;
    public float size;
    public bool exitBorder = false;
    public new Camera camera;

    FollowCamera followCamera;    
    bool exitRight, exitLeft, exitTop, exitBottom;
               

    private void Awake()
    {
        camera = Camera.main;
        followCamera = camera.GetComponent<FollowCamera>();
        screenHeight = camera.orthographicSize;
        screenWidth = camera.aspect * screenHeight;
    }

    void Start()
    {
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

        if(transform.position.x > screenWidth - size) {
            exitRight = true;            
        } else exitRight = false;

        if(transform.position.x < -screenWidth + size) {
            exitLeft = true;            
        } else exitLeft = false;

        if(transform.position.y > UIManager.S.lenghtScene) {
            exitTop = true;            
        } else exitTop = false;

        if( transform.position.y < Tank.S.transform.position.y && gameObject.CompareTag("Enemy")) {
            exitBottom = true;
            //followCamera.followEnemy = true;
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, Tank.S.transform.position.y + 5f, camera.transform.position.z), Time.fixedDeltaTime * 2f);
        } else {
            exitBottom = false;
            //followCamera.followEnemy = false;
        }

        if (exitRight || exitLeft || exitBottom || exitTop == true) {
            exitBorder = true;
        } else exitBorder = false;

    }
}
