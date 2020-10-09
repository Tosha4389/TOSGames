using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 85

public class RocketMainAI : MainAI
{
    [Header("Ракетница: Вручную")]
    public float speedRotate = 4f;
    public float reloadRate = 10f;
    public int rocketsOnVolley = 4;

    Vector3 delta;
    Vector3 dest;
    Vector3 targetDirection;
    float angleRot;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(RLMoving());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void EnemyMoving() { 
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speedRotate * Time.deltaTime, 0.0f);
        rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
    }

    public override void RotateTurret()
    {
        if(distanceTank.magnitude >= searchDistance) {
            turret.forward = transform.forward;
        } else {
            Vector3 target = new Vector3(tankPosition.x, turret.position.y, tankPosition.z);
            Vector3 direction = target - turret.position;
            turret.forward = direction;

            //Vector3 startPos = turret.transform.position + Vector3.forward * 3f;
            //Vector3 tankPos = tankPosition;
            float distance = Vector3.Distance(turret.transform.position, tankPosition);
            //Debug.Log(distance);
            float angle = Mathf.Asin(distance * Mathf.Abs(Physics.gravity.y) / (25f * 25f)) * Mathf.Rad2Deg / 2f;
            turret.localRotation = Quaternion.Euler(angle - 90, turret.localRotation.eulerAngles.y, turret.localRotation.eulerAngles.z);
        }
    }

    public override IEnumerator EnemyAttack()
    {
        if(distanceTank.magnitude <= searchDistance) {
            StartCoroutine(RocketVolley());
            //RaycastHit hit;
            //if(Physics.Raycast(rayPos.transform.position, distanceTank, out hit, searchDistance)) {
            //    //Debug.DrawRay(rayPos.transform.position, distanceTank, Color.red, searchDistance);
            //    if(hit.collider.CompareTag("Player")) {
            //        StartCoroutine(RocketVolley());
            //    }
            //}
        }
        yield return new WaitForSeconds(reloadRate);
        StartCoroutine(EnemyAttack());
    }

    IEnumerator RocketVolley()
    {
        yield return new WaitForSeconds(0.5f);
        float chanseHit = Random.value;
        for(int i = 0; i < rocketsOnVolley; i++) {

            Vector3 turretPos = turret.position + turret.forward * 3f + turret.right * 0.3f * i;
            GameObject shellEnemy = Instantiate(shellEnemyPrefab, turretPos, turret.transform.rotation);

            //if(chanseHit >= accuracy / 100f) {
            //    shellEnemy.transform.Rotate(0f, Random.Range(-scatter, scatter), 0f);
            //} else shellEnemy.transform.rotation = turret.rotation;          

            yield return new WaitForSeconds(fireRate);
        }
        StopCoroutine(RocketVolley());
    }

    IEnumerator RLMoving()
    {
        if(distanceTank.magnitude <= searchDistance || playerFound) {
            Vector3 point = new Vector3(tank.transform.position.x + distanceTank.magnitude / 2f, transform.position.y, tank.transform.position.z + distanceTank.magnitude / 2f);            
            agent.SetDestination(point);
            dest = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z);
        } else {            
            agent.SetDestination(new Vector3(transform.position.x + Random.Range(-10f, 10f), transform.position.y, transform.position.z + Random.Range(-10f, 10f)));
            dest = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
        }
        targetDirection = dest - transform.position;
        yield return new WaitForSeconds(2f);
        StartCoroutine(RLMoving());
    }
}
