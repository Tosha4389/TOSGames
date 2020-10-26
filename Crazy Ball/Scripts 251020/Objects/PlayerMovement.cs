using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField] int force = 40;
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction)
    {
        _rigidbody.AddForce(direction * force * Time.deltaTime, ForceMode.Impulse);
    }

    public void Jump(Vector3 direction)
    {
        _rigidbody.AddForce(direction * force * Time.deltaTime, ForceMode.Impulse);
    }
}
