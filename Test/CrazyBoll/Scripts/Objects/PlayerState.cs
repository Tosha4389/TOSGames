using UnityEngine;

public class PlayerState : MonoBehaviour
{
    InputManager inputManager;
    IMovement move;
    IDestroyGO destroyGO;

    private void Awake()
    {
        move = GetComponent<IMovement>();
        destroyGO = GetComponent<IDestroyGO>();
    }

    private void Start()
    {
        inputManager = InputManager.S;
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        move.Movement(inputManager.direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemy")) {
            destroyGO.DestroyObjects();
        }
    }
}
