using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketMainAI : MainAI
{
    Vector3 rocketDest;
    Camera camera;

    public override void Awake()
    {
        base.Awake();
        camera = Camera.main;
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

}
