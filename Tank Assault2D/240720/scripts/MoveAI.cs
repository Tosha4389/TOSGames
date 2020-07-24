using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 90

public class MoveAI : MonoBehaviour
{
    [Header("Вручную")]
    public float offsetY = 2f;
    public float radius = 1f;
    public float speed = 0.1f;
    public float timeStop = 1f;


    [Header("Автоматически")]
    public Vector3 pointFinish;
    public new Rigidbody rigidbody;
    public BorderCheck borderCheck;
    public Vector3 distancePoint;
    public Vector3 distanceTarget;

    private float dTime;
    private Vector2 pointX;
    private Vector2 pointY;
    private float move;
    private IEnumerator coroutine;



    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        dTime = Time.fixedDeltaTime;
        borderCheck = GetComponent<BorderCheck>();

    }

    void Start()
    {
        pointFinish = new Vector3(0f, 0f, rigidbody.position.z);
        coroutine = Stop(timeStop);
        StartCoroutine(coroutine);
    }

    void FixedUpdate()
    {
        Move();
        //print("До цели " + distanceTarget.sqrMagnitude);
    }

    public void Move()
    {
        pointY.x = rigidbody.position.y - 3f;
        pointY.y = rigidbody.position.y - 1f;

        pointX.x = rigidbody.position.x - 3f;
        pointX.y = rigidbody.position.x - 1f;

        Stop(1f);


        distancePoint = pointFinish - rigidbody.position;
        distanceTarget = Tank.S.transform.position - rigidbody.position;



        if(distanceTarget.sqrMagnitude >= 30f) {
            move = -speed;
            rigidbody.AddForce(pointFinish.normalized * dTime * move, mode: ForceMode.VelocityChange);
            rigidbody.transform.up = pointFinish;
        }


        if(distanceTarget.sqrMagnitude < 30f) {
            move = 0f;
            rigidbody.MovePosition(rigidbody.position - pointFinish * dTime * move);
            rigidbody.transform.TransformDirection(transform.up);
        }



    }

    private IEnumerator Stop(float waitTime)
    {
        while(true) {
            yield return new WaitForSeconds(waitTime);
            pointFinish.x = Random.Range(pointX.x, pointX.y);
            pointFinish.y = Random.Range(pointY.y, pointY.x);
            pointFinish = new Vector3(pointFinish.x, pointFinish.y, rigidbody.position.z);
        }
    }

}
