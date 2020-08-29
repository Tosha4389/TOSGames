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
    new Camera camera;

    public override void Awake()
    {
        base.Awake();
        camera = Camera.main;
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
                rocketDest.x = Random.Range(-borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
                rocketDest.y = camera.transform.position.y + 0.8f * camera.orthographicSize;
                rocketDest = new Vector3(rocketDest.x, rocketDest.y, transform.position.z);
            }

            agent.destination = rocketDest;

            Vector3 nextPoint = new Vector3(agent.steeringTarget.x, agent.steeringTarget.y, transform.position.z);
            Vector3 direction = nextPoint - transform.position;
            float step = Time.fixedDeltaTime * 1.5f;
            transform.up = Vector3.Lerp(transform.up, -direction, step);

            //rigidbody.AddRelativeForce(Vector3.down * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode2D.Impulse);             
        }
    }

    public override void EnemyAttack()
    {
        if(distanceTank.magnitude <= searchDistance) {
            RaycastHit2D attack = Physics2D.Raycast(rayPos.transform.position, distanceTank, searchDistance);
            if(attack.collider.CompareTag("Player") && Time.time > time + fireRate) {
                StartCoroutine(FireDelay());
                time = Time.time;
            }
        }
    }

    IEnumerator FireDelay()
    {
        float chanseHit = Random.value;
        for(int i = 0; i < 3; i++) {
            shellEnemy = Instantiate(shellEnemyPrefab);
            shellEnemy.transform.rotation = turret.transform.rotation;

            if(chanseHit >= accuracy / 100f) {
                shellEnemy.transform.Rotate(0f, 0f, Random.Range(-scatter, scatter));
                //Debug.Log(chanseHit);
            } else shellEnemy.transform.rotation = turret.rotation;

            Vector3 turretPos = turret.transform.localPosition + Vector3.down * 2f + Vector3.left * 0.3f + Vector3.right * 0.3f * i;
            Vector3 shellPos = turret.transform.TransformPoint(turretPos);
            shellEnemy.transform.position = shellPos;

            Transform turretTransform = turret.transform;
            GameObject fireGO = Instantiate(firePrefab, shellPos, turret.rotation, turretTransform);
            fireGO.transform.Rotate(turret.rotation.x, turret.rotation.y, turret.rotation.z + 180f);            
            Destroy(fireGO.gameObject, 0.15f);

            yield return new WaitForSeconds(0.2f);

        }
        StopCoroutine(FireDelay());
    }
}
