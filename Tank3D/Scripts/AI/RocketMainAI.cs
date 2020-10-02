using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 85

public class RocketMainAI : MainAI
{
    [Header("Ракетница: Вручную")]
    public float reloadRate = 10f;

    Vector3 rocketDest;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void EnemyMoving()
    {
        if(distanceTank.magnitude <= searchDistance || playerFound) {
            if(enemyEscape || enemyManeuver) {
                return;
            }
            if(transform.position.x == rocketDest.x || rocketDest.x == 0) {


            }

            agent.SetDestination(tankPosition);

            Vector3 dest = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z);
            Vector3 direction = dest - transform.position;
            float step = Time.fixedDeltaTime * 1.5f;
            transform.forward = Vector3.Lerp(transform.forward, direction, step);            
        }
    }

    public override void RotateTurret()
    {
        if(distanceTank.magnitude >= searchDistance) {
            turret.forward = transform.forward;
        } else {
            Vector3 target = new Vector3(tankPosition.x, turret.position.y, tankPosition.z);
            Vector3 direction = target - turret.position;
            turret.forward = direction;

            Vector3 startPos = turret.transform.position + Vector3.forward * 3f;
            Vector3 tankPos = tankPosition;
            float distance = Vector3.Distance(startPos, tankPos);
            float angle = Mathf.Asin(distance * 9.8f / (30f * 30f)) * Mathf.Rad2Deg / 2f;
            turret.localRotation = Quaternion.Euler(-angle, turret.localRotation.eulerAngles.y, turret.localRotation.eulerAngles.z);
        }
    }

    public override IEnumerator EnemyAttack()
    {
        if(distanceTank.magnitude <= searchDistance) {
            RaycastHit hit;
            if(Physics.Raycast(rayPos.transform.position, distanceTank, out hit, searchDistance)) {
                Debug.DrawRay(rayPos.transform.position, distanceTank, Color.red, searchDistance);
                if(hit.collider.CompareTag("Player")) {
                    StartCoroutine(RocketVolley());
                }
            }
        }
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(EnemyAttack());
    }

    IEnumerator RocketVolley()
    {
        float chanseHit = Random.value;
        for(int i = 0; i < 4; i++) {

            Vector3 turretPos = turret.position + turret.forward * 3f + Vector3.down * 0.3f + Vector3.left * 0.45f + Vector3.right * 0.3f * i;
            GameObject shellEnemy = Instantiate(shellEnemyPrefab, turretPos, turret.transform.rotation);

            //if(chanseHit >= accuracy / 100f) {
            //    shellEnemy.transform.Rotate(0f, Random.Range(-scatter, scatter), 0f);
            //} else shellEnemy.transform.rotation = turret.rotation;          

            yield return new WaitForSeconds(0.2f);
        }
        StopCoroutine(RocketVolley());
    }
}
