using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 15

public class Shell : MonoBehaviour
{
    [Header("Вручную Shell")]
    public float speed;
    public int playerDamage = 50;

    [Header("Автоматически")]    
    public new Rigidbody2D rigidbody;
    public BorderCheck borderCheck;
    public GameObject explosionPrefab;

    private void Awake()    
    {        
        borderCheck = GetComponent<BorderCheck>();
        rigidbody = GetComponent<Rigidbody2D>();
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
        rigidbody.velocity = speed * transform.up;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(explo, 1f);
    }
}
