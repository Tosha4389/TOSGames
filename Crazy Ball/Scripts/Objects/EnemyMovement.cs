using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    [SerializeField] int force = 100;
    [SerializeField] int heigth = 10;
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction)
    {        
        _rigidbody.AddForce(direction * Time.deltaTime * force, ForceMode.Force);
    }

    public void Jump(Vector3 direction)
    {
        StartCoroutine(DelayJump());
    }

    IEnumerator DelayJump()
    {
        _rigidbody.AddForce(Vector3.up * Time.deltaTime * force * heigth, ForceMode.Impulse);
        yield return new WaitForSeconds(1);

        if(Random.value <= 0.5f)
            _rigidbody.AddForce(Vector3.right * Time.deltaTime * force * heigth, ForceMode.Impulse);
        else _rigidbody.AddForce(Vector3.left * Time.deltaTime * force * heigth, ForceMode.Impulse);

        yield return new WaitForSeconds(1);
        StartCoroutine(DelayJump());
    }
}
