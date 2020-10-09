using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5

public class ShellRL : Shell
{
    [Header("ShellRL")]
    public float radius = 3f;

    Tank tank;
    float time;
    float timeStart;
    Vector3 targetDirection;

    public override void Awake()
    {
        base.Awake();
        tank = Tank.S;
    }

    public override void Start()
    {
        base.Start();        
        Vector3 distance = transform.position - tank.transform.position;
        time = Mathf.Abs(2 * speed * Mathf.Sin(90 - transform.eulerAngles.x) / Physics.gravity.y);
        timeStart = Time.time;
    }

    private void FixedUpdate()
    {
        if(Time.time > timeStart + 1.5f && Time.time < timeStart + 3.5f) {
            targetDirection = tank.transform.position - transform.position;
            float singleStep = Time.deltaTime * 2.5f;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
        }           
        
    }

    public override void ShellMoving()
    {        
        StartCoroutine(StartVelocity());
        StartCoroutine(VelocityMetrics());
    }

    IEnumerator StartVelocity()
    {
        rigidbody.velocity = transform.forward * speed * Time.fixedDeltaTime * 50f;
        yield return new WaitForSeconds(1f);
        StopCoroutine(StartVelocity());
    }

    IEnumerator VelocityMetrics()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 point1 = transform.position;
        yield return new WaitForSeconds(0.5f);
        Vector3 point2 = transform.position;
        //Debug.Log(Vector3.Distance(point1, point2) * 2f);
        StopCoroutine(VelocityMetrics());
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] hitColliders = new Collider[5];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, hitColliders);
        for(int i = 0; i < numColliders; i++) {
            if(hitColliders[i].gameObject.CompareTag("Player")) {
                tank.HitPlayerTank(enemyDamage);
                Destroy(gameObject);
            }
        }     
    }

}
