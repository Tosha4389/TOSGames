using UnityEngine;

public class EnemyState : MonoBehaviour
{

    IMovement move;
    IDestroyGO destroyGO;

    private void Awake()
    {
        move = GetComponent<IMovement>();
        destroyGO = GetComponent<IDestroyGO>();
    }

    private void Start()
    {        
        move.Jump(Vector3.zero);
    }

    private void FixedUpdate()
    {
        move.Movement(Vector3.back);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Destroy"))
            destroyGO.DestroyObjects();
    }

}
