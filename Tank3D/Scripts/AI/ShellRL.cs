using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5

public class ShellRL : Shell
{
    Tank tank;
    Vector3 eulerAngleVelocity;

    public override void Awake()
    {
        base.Awake();
        tank = Tank.S;
    }

    public override void Start()
    {
        base.Start();
        Vector3 startPos = transform.position + Vector3.forward * 3f;
        Vector3 distance = startPos - tank.transform.position;
        eulerAngleVelocity = new Vector3(transform.eulerAngles.x / distance.magnitude, 0, 0);

        //StartCoroutine(VelocityMetrics());
    }

    private void Update()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime * 3f);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
    }

    public override void ShellMoving()
    {        
        rigidbody.AddForce(transform.forward * speed / 107f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            tank.HitPlayerTank(enemyDamage);
            Destroy(gameObject);          
        }
    }

    IEnumerator VelocityMetrics()
    {
        Vector3 point1 = transform.position;
        yield return new WaitForSeconds(1);
        Vector3 point2 = transform.position;
        Debug.Log(Vector3.Distance(point1, point2));
        StopCoroutine(VelocityMetrics());
    }
}
