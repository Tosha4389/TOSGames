using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    [SerializeField] int force = 100;
    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction)
    {        
        rigidbody.AddForce(direction * Time.deltaTime * force, ForceMode.Force);
    }
}
