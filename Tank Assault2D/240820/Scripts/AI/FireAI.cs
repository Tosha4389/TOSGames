using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 40

public class FireAI : MonoBehaviour
{
    [Header("Вручную")]
    public float fireRate;
    public GameObject shellEnemyPrefab;
    public float accuracy = 80f;
    public float scatter = 20f;

    [Header("Автоматически")]
    public GameObject shellEnemy;
    public MainAI mainAI;
    public bool fireOn = false;
    public float time;
    
    public IEnumerator coroutine;

    private void Awake()
    {
        mainAI = GetComponent<MainAI>();        
    }

    private void Start()
    {
        time = Time.time;
    }


    void FixedUpdate()
    {        
        Fire(fireOn);
    }

    public virtual void Fire(bool on)
    {
        if(Time.time > time + fireRate && on) {
            shellEnemy = Instantiate(shellEnemyPrefab);
            Vector3 turretPos = mainAI.turret.transform.localPosition + Vector3.down * 2f;
            Vector3 shellPos = mainAI.turret.transform.TransformPoint(turretPos);
            shellEnemy.transform.position = shellPos;
            shellEnemy.transform.rotation = mainAI.turret.transform.rotation;

            float chanseHit = Random.value;
            if(chanseHit >= accuracy / 100f) {
                shellEnemy.transform.Rotate(0f, 0f, Random.Range(-scatter, scatter));
                //Debug.Log(chanseHit);
            } else shellEnemy.transform.rotation = mainAI.turret.rotation;

            time = Time.time;
        }
    }

}
