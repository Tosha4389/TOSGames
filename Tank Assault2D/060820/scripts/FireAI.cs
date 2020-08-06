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
        Vector3 turretPos = mainAI.turret.transform.localPosition + Vector3.down * 2f;
        Vector3 shellPos = mainAI.turret.transform.TransformPoint(turretPos);

        if(Time.time > time + fireRate && on) {
            shellEnemy = Instantiate(shellEnemyPrefab, shellPos, mainAI.turret.transform.rotation);
            time = Time.time;
        }
    }

}
