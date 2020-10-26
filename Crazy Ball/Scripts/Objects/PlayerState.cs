using UnityEngine;

public class PlayerState : MonoBehaviour
{
    InputManager inputManager;
    GameManager gameManager;
    IMovement move;
    IDestroyGO destroyGO;    
    bool isGrounded = true;    

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

        //if(isGrounded)
        //    move.Movement(inputManager.directionMove);
        //else move.Movement(Vector3.zero);
    }

    void Jump()
    {
        move.Jump(inputManager.directionJump);

        //if(isGrounded)
        //    move.Jump(inputManager.directionJump);
        //else move.Jump(Vector3.zero);
    }

    private void OnCollisionEnter(Collision collision)
    {  
        if(collision.transform.CompareTag("Enemy")) {            
            destroyGO.DestroyObjects();
            gameManager.GameOver();
        }

        if(collision.transform.CompareTag("Map"))
            isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Score")) {
            gameManager.IncreaseScore();
            other.gameObject.SetActive(false);
        }

    }

    private void OnCollisionExit(Collision collision)
    {        
        isGrounded = false;
    }

}
