using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    InputManager inputManager;
    GameManager gameManager;
    IMovement move;
    IDestroyGO destroyGO;    
    public bool isFlight = false;

    private void Awake()
    {        
        move = GetComponent<IMovement>();
        destroyGO = GetComponent<IDestroyGO>();
    }

    private void Start()
    {
        inputManager = InputManager.S;
        gameManager = GameManager.S;        
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        move.Movement(inputManager.directionMove);
    }

    void Jump()
    {
        if(!isFlight)
            move.Jump(inputManager.directionJump);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Score")) {
            gameManager.IncreaseScore();
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Border"))
            isFlight = false;

        if(collision.transform.CompareTag("Enemy")) {            
            destroyGO.DestroyObjects();
            gameManager.GameOver();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!collision.transform.CompareTag("Border"))
            isFlight = false;
        else isFlight = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isFlight = true;
    }
}
