using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAI : MonoBehaviour
{
    [Header("Вручную")]    
    public int enemyHP = 100;
    public int playerDamage = 50;
    public HPBarEnemy hpBarEnemy;

    [Header("Автоматически")]
    BorderCheck borderCheck;
    public bool playerFound = false;    
    public float searchDistance;
    public Transform turret;
    public Vector3 tankPosition;
    public Vector3 distanceTank;

    private GameObject lastTrigger = null;
    private FireAI fireAI;


    private void Awake()
    {
        borderCheck = GetComponent<BorderCheck>();
        fireAI = GetComponent<FireAI>();
        turret = gameObject.transform.GetChild(0);
    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        DistanceTank();
        SearchPlayer();               
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


    private void SearchPlayer()
    {
        searchDistance = borderCheck.screenHeight * 2 - borderCheck.size * 5;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, searchDistance)) {
            if(hit.collider.CompareTag("Player")) {                
            }
        }
        EnemyAttack();
        RotateTurret();
    }


    private void Patrol()
    {
                
        
    }


    private void EnemyMoving()
    {

        
    }


    private void EnemyManeuver()
    {

    }


    private void EnemyMessage()
    {

    }


    private void RotateTurret()
    {
        if(distanceTank.magnitude <= searchDistance)
            turret.transform.up = (tankPosition - transform.position) * (-1);
        else turret.transform.up = transform.position;
    }


    private void EnemyAttack()
    {
        if(distanceTank.magnitude <= searchDistance)
            fireAI.fireOn = true;
        else fireAI.fireOn = false;
    }


    private void EnemyEscape()
    {

    }


    public void EnemyDestroy()
    {
        if(enemyHP <= 0)
            Destroy(gameObject);
    }
}
