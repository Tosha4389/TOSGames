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
        _rigidbody.AddForce(direction * Time.fixedDeltaTime * force, ForceMode.Force);
    }

    public void Jump(Vector3 direction)
    {
        StartCoroutine(DelayJump());
    }

    IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(0.5f);
        _rigidbody.AddForce(Vector3.up * Time.fixedDeltaTime * heigth, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        _rigidbody.AddForce(Vector3.zero * Time.fixedDeltaTime * heigth, ForceMode.Impulse);

        if(Random.value <= 0.5f)
            _rigidbody.AddForce(Vector3.right * Time.fixedDeltaTime * force, ForceMode.Impulse);
        else _rigidbody.AddForce(Vector3.left * Time.fixedDeltaTime * force, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DelayJump());
    }
}
