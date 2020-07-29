using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAI : MonoBehaviour
{
    [Header("Вручную")]    
    public int enemyHP = 100;
    public int playerDamage = 50;
    public float speedMove = 10f;
    public float searchDistanseCoeff = 10f;
    public float speedRotTurret = 1f;
    public float speedRotation = 1f;
    public HPBarEnemy hpBarEnemy;


    [Header("Автоматически")]
    public BorderCheck borderCheck;
    public new Rigidbody rigidbody;   
    public float searchDistance;
    public Transform turret;
    public Vector3 tankPosition;
    public Vector3 distanceTank;
    public bool moveOn = false;


    GameObject lastTrigger = null;
    FireAI fireAI;
    float rotTime;
    float stopTime;
    float waveFrequency = 25f;
    float waveWidth = 2f;
    float xLimit, yLimit;
    Vector3 rotTurret;
    bool enemyEscape = false;
    Vector3 angleManeuver;


    private void Awake()
    {
        borderCheck = GetComponent<BorderCheck>();
        fireAI = GetComponent<FireAI>();
        turret = gameObject.transform.GetChild(0);
        rigidbody = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        stopTime = rotTime = Time.time;
        searchDistance = borderCheck.screenHeight * 2 * searchDistanseCoeff / 10f;
    }

    void FixedUpdate()
    {      
        DistanceTank();
        FindWay();
        EnemyMoving();
        EnemyManeuver();
        EnemyMessage();
        RotateTurret();
        EnemyAttack();
        EnemyDestroy();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject gameObject = rootTransform.gameObject;

        if(lastTrigger == gameObject) {
                return;
            }
        lastTrigger = gameObject;

        if(other.gameObject.CompareTag("ShellPlayer")) {
            enemyHP -= playerDamage;
            hpBarEnemy.ScaleBar();
            Destroy(gameObject);            
        }
    }


    private Vector3 DistanceTank()
    {
        if(Tank.S) {
            tankPosition = Tank.S.transform.position;
        } else {
            Debug.Log("Объект танк не обнаружен!");
            return Vector3.zero;
        }

        distanceTank = tankPosition - transform.position;
        return distanceTank;
    } 


    private void FindWay()
    {        
        //RaycastHit hit;
        //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, searchDistance);        

    }


    private void EnemyMoving()
    {
        if(enemyHP <= 50)
            enemyEscape = true;
        else enemyEscape = false;

        xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        transform.position = new Vector3(xLimit, yLimit, transform.position.z);

        if(moveOn && !enemyEscape) {
           rigidbody.AddRelativeForce(Vector3.down * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode.VelocityChange);             
        } else if(moveOn && enemyEscape) {
            rigidbody.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode.VelocityChange);
        }

    }


    private void EnemyManeuver()
    {       

        int angle;

        if(transform.position.x >= 0)
            angle = Random.Range(10, 30);
        else angle = Random.Range(-30, -10);

        if(distanceTank.sqrMagnitude < 40f && Time.time > stopTime + 0.5f) {            
            angleManeuver = new Vector3(0f, 0f, angle);
            stopTime = Time.time;
        }

        if(distanceTank.sqrMagnitude < 40f) {
            Quaternion deltaRotation = Quaternion.Euler(angleManeuver * Time.deltaTime * speedRotation);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);            
        }

    }


    private void EnemyMessage()
    {

    }


    private void RotateTurret()
    {        
        float age = Time.time - rotTime;
        float theta = Mathf.PI * 2 * age * speedRotTurret / waveFrequency;
        float sin = Mathf.Sin(theta);
        rotTurret.z = borderCheck.screenWidth * waveWidth * sin;
        rotTurret = new Vector3(rotTurret.z, 0f, 0f);

        if(distanceTank.magnitude <= searchDistance) {
            turret.transform.up = (tankPosition - transform.position) * (-1);
        } else {

            turret.transform.up = rotTurret - transform.position * (-1);            
        }
       
            
    }


    private void EnemyAttack()
    {
        if(distanceTank.magnitude <= searchDistance)
            fireAI.fireOn = true;
        else fireAI.fireOn = false;
    }


    public void EnemyDestroy()
    {
        if(enemyHP <= 0)
            Destroy(gameObject);
    }
}
