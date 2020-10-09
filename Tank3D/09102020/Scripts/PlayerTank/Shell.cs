using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 40

public class Shell : MonoBehaviour
{
    [Header("Вручную Shell")]
    public float speed;
    public int enemyDamage;
    public GameObject explosionPrefab;

    [Header("Автоматически")]
    [HideInInspector] public new Rigidbody rigidbody;

    public virtual void Awake()    
    {         
        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Start()
    {        
        ShellMoving();
    }

    public virtual void ShellMoving()
    {
        rigidbody.velocity = speed * transform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {         
        Destroy(gameObject);
        GameObject explo = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explo, 2f);
    }
}
