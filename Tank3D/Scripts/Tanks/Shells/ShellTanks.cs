using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTanks : MonoBehaviour, IShell
{
    [Header("Вручную Shell")]
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] GameObject explosionPrefab;

    Rigidbody rigidbody;

    public void ShellMovement()
    {
        rigidbody.velocity = speed * transform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if(damagable != null) {
            damagable.DecreaseValue(damage);
        }
        Destroy(gameObject);
        GameObject explo = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explo, 2f);
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ShellMovement();
    }
}
