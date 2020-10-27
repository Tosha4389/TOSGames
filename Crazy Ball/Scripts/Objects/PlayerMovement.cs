using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField] int forceMove = 40;
    [SerializeField] int forceJump;
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction)
    {
        _rigidbody.AddForce(direction * forceMove * Time.deltaTime, ForceMode.Impulse);
    }

    public void Jump(Vector3 direction)
    {
        _rigidbody.AddForce(direction * forceJump * Time.deltaTime, ForceMode.Impulse);
    }
}
