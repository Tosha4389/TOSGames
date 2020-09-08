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
    public new Rigidbody rigidbody;
    public BorderCheck borderCheck;

    private void Awake()    
    {        
        borderCheck = GetComponent<BorderCheck>();
        rigidbody = GetComponent<Rigidbody>();        
    }

    void Start()
    {
        ShellMoving();

    }

    void FixedUpdate()
    {
        DestroyShell();
    }

    public void DestroyShell()
    {   
        if(borderCheck.exitBorder == true) {
            Destroy(gameObject);
        }
    }

    public virtual void ShellMoving()
    {
        rigidbody.velocity = speed * transform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(explo, 0.9f);
    }
}
