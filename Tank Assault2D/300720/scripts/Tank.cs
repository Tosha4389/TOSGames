﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// 130

public class Tank : MonoBehaviour
{
    static public Tank S;

    [Header("Вручную:")]
    public float speedMove;
    public float speedRotate;
    public float speedRotateTurret;
    public GameObject shellTankPrefab;
    public GameObject explosionPrefab;
    public float fireRate = 5f;
    public bool godMode = false;

    [Header("Автоматически:")]    
    public Transform turret;
    public BorderCheck borderCheck;
    public Shell shell;
    public new Rigidbody rigidbody;
    public HPBar hpBar;


    private float xLimit;
    private float yLimit;    
    private IEnumerator coruntine;
    private bool reload = false;
    private GameObject lastTrigger = null;


    private void Awake()
    {            
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("Tank.Awake() - создан второй Tank");
            Destroy(gameObject);
        }

        borderCheck = GetComponent<BorderCheck>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        turret = gameObject.transform.GetChild(0);
        coruntine = Reload(fireRate);
        
    }
        
    void FixedUpdate()
    {
        Movement();
        RotateTurret();        
        Fire();
        PlayerDestroy();
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal") * speedRotate * 3f * Time.fixedDeltaTime;
        float moveY = Input.GetAxis("Vertical") * speedMove * Time.fixedDeltaTime;

        rigidbody.AddRelativeForce(Vector3.up * moveY, mode: ForceMode.VelocityChange);         
        rigidbody.AddRelativeTorque(Vector3.back * moveX, mode: ForceMode.VelocityChange);

        xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        transform.position = new Vector3(xLimit, yLimit, transform.position.z);


    }

    private void RotateTurret()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();
        float rotateTurret = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotateTurret - 90f);

    }

    public void Fire()
    {
        if(Input.GetMouseButton(0) && reload == false) {
            Instantiate(shellTankPrefab /*parent*/); 
            StartCoroutine(Reload(fireRate));
            
        }

        shellTankPrefab.transform.position = turret.transform.position;
        shellTankPrefab.transform.rotation = turret.transform.rotation; 
    }

    public IEnumerator Reload(float time)
    {
        while(true) {
            reload = true;                      
            yield return new WaitForSeconds(fireRate);
            reload = false;
            break;
        }       

    }

    private void PlayerDestroy()
    {
        if(GameManager.S.playerHP <= 0 && !godMode) {
            GameManager.S.GameOver();
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject gameObject = rootTransform.gameObject;

        if(lastTrigger == gameObject) {
            return;
        }
        lastTrigger = gameObject;

        if(other.gameObject.CompareTag("ShellEnemy")) {
            GameManager.S.HitDamage();
            hpBar.ScaleBar();
            Destroy(other.gameObject);
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
    }


}
