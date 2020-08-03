using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 10

public class FireAI : MonoBehaviour
{
    [Header("Вручную")]
    public float fireRate;
    public GameObject shellEnemyPrefab;

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
            shellEnemy.transform.position = mainAI.turret.transform.position;
            shellEnemy.transform.rotation = mainAI.turret.transform.rotation;
            time = Time.time;
        }
    }

}
