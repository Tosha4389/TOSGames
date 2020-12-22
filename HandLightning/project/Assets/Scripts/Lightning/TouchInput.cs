using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    LightningScript lightning;
    Camera cam;
    Vector3 start;

    private void Start()
    {
        lightning = GetComponent<LightningScript>();
        cam = Camera.main;
    }

    private void Update()
    {
        switch(Application.platform) {
            case RuntimePlatform.Android:
                TouchInputInit();
                break;
            case RuntimePlatform.WindowsEditor:
                MouseInput();
                break;
        }
    }

    Vector3 WorldPosition(Vector2 screen)
    {
        Vector3 touch = cam.ScreenToWorldPoint(screen);
        Vector3 position = new Vector3(touch.x, touch.y, 0f);        
        return position;
    }

    void TouchInputInit()
    {
        switch(Input.touchCount) {
            case 0:
                if(lightning.SphereLightning.activeInHierarchy ) {
                    lightning.ActiveSphereLightning(false);
                }

                if(lightning.LeftLightning.activeInHierarchy && lightning.RightLightning.activeInHierarchy) {
                    lightning.ActiveLineLightning(false);
                }
                break;

            case 1:
                if(lightning.LeftLightning.activeInHierarchy && lightning.RightLightning.activeInHierarchy) {
                    lightning.ActiveLineLightning(false);
                }

                if(!lightning.SphereLightning.activeInHierarchy && !lightning.LeftLightning.activeInHierarchy && !lightning.RightLightning.activeInHierarchy) {
                    lightning.ActiveSphereLightning(true);
                    lightning.MoveLightning(lightning.gameObject, WorldPosition(Input.GetTouch(0).position), true);
                }                
                break;

            case 2:
                if(lightning.SphereLightning.activeInHierarchy) {
                    lightning.ActiveSphereLightning(false);
                }

                if(!lightning.LeftLightning.activeInHierarchy && !lightning.RightLightning.activeInHierarchy) {
                    lightning.MoveLightning(lightning.gameObject, WorldPosition(Input.GetTouch(0).position), true);
                    lightning.ActiveLineLightning(true);
                }                
                break;
        }

        if(lightning.SphereLightning.activeInHierarchy)
            lightning.MoveLightning(lightning.gameObject, WorldPosition(Input.GetTouch(0).position), false);
        else if(!lightning.LeftLightning.activeInHierarchy && !lightning.RightLightning.activeInHierarchy)
            lightning.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if(lightning.LeftLightning.activeInHierarchy && lightning.RightLightning.activeInHierarchy) {
            lightning.RotateLightning(WorldPosition(Input.GetTouch(0).position), WorldPosition(Input.GetTouch(1).position));            
            lightning.MoveLightning(lightning.RightLightning, WorldPosition(Input.GetTouch(0).position), false);
            lightning.MoveLightning(lightning.LeftLightning, WorldPosition(Input.GetTouch(1).position), false);
            lightning.MoveLightning(lightning.ColliderLightning, WorldPosition((Input.GetTouch(0).position + Input.GetTouch(1).position) / 2f), false);
            lightning.ScaleLightning();
        }
    }

    void MouseInput()
    {
        if(Input.GetMouseButtonDown(0)) {
            if(!lightning.SphereLightning.activeInHierarchy && !lightning.LeftLightning.activeInHierarchy && !lightning.RightLightning.activeInHierarchy) {
                lightning.ActiveSphereLightning(true);
                start = Input.mousePosition;
            }
        }

        if(Input.GetMouseButtonUp(0)) {
            if(lightning.SphereLightning.activeInHierarchy) {
                lightning.MoveLightning(lightning.gameObject, WorldPosition(start), true);
                lightning.ActiveSphereLightning(false);
            }
        }

        if(Input.GetMouseButtonDown(1)) {
            if(lightning.SphereLightning.activeInHierarchy) {                
                lightning.ActiveSphereLightning(false);
            }

            if(!lightning.LeftLightning.activeInHierarchy && !lightning.RightLightning.activeInHierarchy) {                
                lightning.ActiveLineLightning(true);                
            }
        }

        if(Input.GetMouseButtonUp(1)) {
            if(lightning.LeftLightning.activeInHierarchy && lightning.RightLightning.activeInHierarchy) {
                lightning.ActiveLineLightning(false);
            }
        }

        if(lightning.SphereLightning.activeInHierarchy) {
            lightning.MoveLightning(lightning.gameObject, WorldPosition(Input.mousePosition), false);
        } else if(!lightning.LeftLightning.activeInHierarchy && !lightning.RightLightning.activeInHierarchy)
            lightning.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if(lightning.LeftLightning.activeInHierarchy && lightning.RightLightning.activeInHierarchy) {
            lightning.RotateLightning(WorldPosition(start), WorldPosition(Input.mousePosition));
            lightning.MoveLightning(lightning.gameObject, WorldPosition(start), true);
            lightning.MoveLightning(lightning.LeftLightning, WorldPosition(Input.mousePosition), false);
            lightning.MoveLightning(lightning.RightLightning, WorldPosition(start), false);
            lightning.MoveLightning(lightning.ColliderLightning, WorldPosition((start + Input.mousePosition) / 2f), false);
            lightning.ScaleLightning();
        }
    }
}
