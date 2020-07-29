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
    public Vector3 tankPosition;

    private float dTime;
    private Vector2 pointX, pointY;
    private float move;
    private IEnumerator coroutine;
    private float xLimit, yLimit;



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
        if(Tank.S)
            tankPosition = Tank.S.transform.position;
        else return;

    }

    public void Move()
    {
        pointX.x = rigidbody.position.x - 3f;
        pointX.y = rigidbody.position.x - 1f;

        pointY.x = rigidbody.position.y - 3f;
        pointY.y = rigidbody.position.y - 1f;

        Stop(1f);

        distancePoint = pointFinish - rigidbody.position;
        distanceTarget = tankPosition - rigidbody.position;

        xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        transform.position = new Vector3(xLimit, yLimit, transform.position.z);

        if(distanceTarget.sqrMagnitude < 30f && rigidbody.velocity.y == 0f) {            
            
        }

        if(distanceTarget.sqrMagnitude >= 30f) {
            move = -speed;
            rigidbody.AddForce(pointFinish.normalized * dTime * move, mode: ForceMode.VelocityChange);
            rigidbody.transform.up = pointFinish;
        }






    }

    private IEnumerator Stop(float waitTime)
    {
        while(true) {
            
            pointFinish.x = Random.Range(pointX.x, pointX.y);
            pointFinish.y = Random.Range(pointY.y, pointY.x);
            pointFinish = new Vector3(pointFinish.x, pointFinish.y, 0f);
            yield return new WaitForSeconds(waitTime);
        }
    }

}
