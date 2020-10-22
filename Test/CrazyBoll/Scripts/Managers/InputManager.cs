using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static internal InputManager S;
    internal Vector3 direction;

    Vector2 startPos;
    bool directionChosen = false;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("InputManager.Awake() - создан второй InputManager");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        DirectionFromTouch();
    }

    void DirectionFromTouch()
    {
        if(Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch(touch.phase) {

                case TouchPhase.Began:                    
                    startPos = touch.position;
                    direction = Vector3.up;
                    break;

                case TouchPhase.Moved:
                    //direction = touch.position - startPos;
                    break;

                case TouchPhase.Ended:
                    direction = touch.position - startPos;
                    break;
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            direction = Vector3.up;
            StartCoroutine(GetImpulse());
        }

    }

    IEnumerator GetImpulse()
    {        
        yield return new WaitForSeconds(0.1f);
        direction = Vector3.zero;
    }

}
