using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    static internal InputManager S;
    internal Vector3 directionMove;
    internal Vector3 directionJump;

    Vector2 startPos;
    Vector3 startPosMouse;
    Vector3 endPosMouse;

    UIManager uiManager;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("InputManager.Awake() - создан второй InputManager");
            Destroy(gameObject);
        }        
    }

    void Start()
    {
        uiManager = UIManager.S;
    }

    private void Update()
    {
        DirectionFromTouch();        
    }

    void DirectionFromTouch()
    {
        //if(Input.touchCount > 0) {
        //    if(Input.touches[0].phase == TouchPhase.Began) {
        //        directionJump = Vector3.up;
        //        StartCoroutine(GetVectorJumpZero());
        //        startPosMouse = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
        //    }
        //}

        //if(Input.touchCount > 0) {

        //    if(Input.touches[0].phase == TouchPhase.Began) {
        //        endPosMouse = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
        //        directionMove = Vector3.Normalize(endPosMouse - startPosMouse);
        //        StartCoroutine(GetVectorMoveZero());
        //    }
        //}        

        if(Input.GetMouseButtonDown(0)) {
            directionJump = Vector3.up;
            StartCoroutine(GetVectorJumpZero());
            startPosMouse = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
        }

        if(Input.GetMouseButtonUp(0)) {

            endPosMouse = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
            directionMove = Vector3.Normalize(endPosMouse - startPosMouse);
            StartCoroutine(GetVectorMoveZero());
        }
    }

    IEnumerator GetVectorMoveZero()
    {        
        yield return new WaitForSeconds(0.1f);
        directionMove = Vector3.zero;
    }

    IEnumerator GetVectorJumpZero()
    {
        yield return new WaitForSeconds(0.1f);
        directionJump = Vector3.zero;
    }
}
