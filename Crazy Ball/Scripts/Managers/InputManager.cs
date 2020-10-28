using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static internal InputManager S;
    internal Vector3 directionMove;
    internal Vector3 directionJump;

    Vector3 startPosMouse;
    Vector3 endPosMouse;

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
        yield return new WaitForSeconds(0.05f);
        directionMove = Vector3.zero;
    }

    IEnumerator GetVectorJumpZero()
    {
        yield return new WaitForSeconds(0.05f);
        directionJump = Vector3.zero;
    }
}
