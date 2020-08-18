﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//160

public class MainAI : MonoBehaviour
{
    [Header("Вручную")]    
    public int enemyHP = 100;    
    public float speedMove = 10f;
    public float searchDistanseCoeff = 10f;
    public float speedRotTurret = 1f;
    public float speedRotation = 1f;
    public HPBarEnemy hpBarEnemy;
    public GameObject explosionPrefab;


    [Header("Автоматически")]
    public float searchDistance;
    public int playerDamage;
    public BorderCheck borderCheck;
    public new Rigidbody2D rigidbody;
    public Transform turret;
    public Vector3 tankPosition;
    public Vector3 distanceTank;
    public bool moveOn = false;
    public bool destroy;
    public bool playerFound = false;

    GameObject lastTrigger = null;
    FireAI fireAI;
    float rotTime;
    public float rayTime;
    float waveFrequency = 25f;
    float waveWidth = 2f;
    float xLimit, yLimit;
    bool enemyEscape = false;
    bool enemyManeuver = false;
    Vector3 rotTurret;
    RaycastHit2D hit;


    private void Awake()
    {
        borderCheck = GetComponent<BorderCheck>();
        fireAI = GetComponent<FireAI>();
        turret = gameObject.transform.GetChild(0);
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        playerDamage = Tank.S.playerDamage;
        rayTime = rotTime = Time.time;       
    }

    void FixedUpdate()
    {
        if(rayTime < 1f) {
            playerDamage = Tank.S.playerDamage;            
        }

        xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        transform.position = new Vector3(xLimit, yLimit, transform.position.z);

        DistanceTank();

        if(Time.time > rayTime + 1f) {
            SearchPlayer();
            rayTime = Time.time;
        } 

        EnemyManeuver();
        RotateTurret();        
        EnemyDestroy();
        EnemyMoving();
        EnemyAttack();
        EnemyEscape();

        //Debug.Log(distanceTank.sqrMagnitude);

    }

    private void OnTriggerEnter2D(Collider2D other)
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
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);

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
        searchDistance = borderCheck.screenHeight * 2 * searchDistanseCoeff / 10f;
        return distanceTank;
    }


    private void SearchPlayer()
    {        
        if(distanceTank.magnitude <= searchDistance) {
            playerFound = true;
        }

        Vector3 rayPos = transform.localPosition + Vector3.up;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        
        hit = Physics2D.Raycast(rayPos, Vector2.up, searchDistance, layerMask);
                
        Transform allies = hit.transform;

        if(allies != null && playerFound && allies.CompareTag("Enemy")) {            
            allies.GetComponent<MainAI>().playerFound = true;
            //Debug.Log(allies.tag);
        } else return;

        
    }


    private void EnemyMoving()
    {
        if(distanceTank.magnitude <= searchDistance || playerFound) {
            if(enemyEscape || enemyManeuver) {
                return;
            }
            rigidbody.AddRelativeForce(Vector3.down * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode2D.Impulse);             
        }      
    }


    private void EnemyEscape()
    {
        if(enemyHP <= 50) {
            enemyEscape = true;
            rigidbody.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode2D.Impulse);
        } else enemyEscape = false;
    }


    private void EnemyManeuver()
    {
        if(distanceTank.sqrMagnitude <= 40f) {
            enemyManeuver = true;
            rigidbody.AddRelativeForce(Vector3.zero * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode2D.Impulse);
        } else enemyManeuver = false;
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
        destroy = GameManager.S.restart;

        if(enemyHP <= 0) {            
            Destroy(gameObject);
        }

        if(destroy) {
            Destroy(gameObject);
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
            

    }


}
